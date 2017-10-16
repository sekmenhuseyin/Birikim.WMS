using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wms12m.Entity.Models;

namespace Wms12m.Hubs
{
    public class ZigChatHub : Hub
    {
        /// <summary>
        /// send message
        /// </summary>
        public void SendMessage(string userName, string message)
        {
            if (message.StartsWith("@"))
            {
                var pmUserName = message.Split(' ')[0].Substring(1);
                using (var db = new WMSEntities())
                {
                    var pmConnection = db.Connections.Where(x => x.UserName.ToLower() == pmUserName && x.IsOnline).SingleOrDefault();
                    if (pmConnection != null)
                    {
                        Clients.Clients(new List<string> { Context.ConnectionId, pmConnection.ConnectionId }).UpdateChat(userName, message, true);
                        return;
                    }
                }
            }

            Clients.All.UpdateChat(userName, message);
        }
        /// <summary>
        /// update users online
        /// </summary>
        public void UsersOnline()
        {
            try
            {
                using (var db = new WMSEntities())
                {
                    Clients.All.UpdateUsersOnline(new { Success = true, UsersOnline = db.Connections.Where(x => x.IsOnline).Select(x => x.UserName).ToArray() });
                }
            }
            catch (Exception ex)
            {
                Clients.All.UpdateUsersOnline(new { Success = false, ErrorMessage = ex.Message });
            }
        }
        /// <summary>
        /// connect user
        /// </summary>
        public object ConnectUser(string userName)
        {
            try
            {
                using (var db = new WMSEntities())
                {
                    // Check if there if a connection for the specified user name have ever been made
                    var existingConnection = db.Connections.Where(x => x.UserName.ToLower() == userName.ToLower()).SingleOrDefault();

                    if (existingConnection != null)
                    {
                        // If there's an old connection only the connection id and the online status are changed.
                        existingConnection.ConnectionId = Context.ConnectionId;
                        existingConnection.IsOnline = true;
                    }
                    else
                    {
                        // If not, then a new connection is created
                        db.Connections.Add(new Entity.Models.Connection { ConnectionId = Context.ConnectionId, UserName = userName, IsOnline = true });
                    }

                    db.SaveChanges();
                }
                UsersOnline();
                return new { Success = true };
            }
            catch (Exception ex)
            {
                return new { Success = false, ErrorMessage = ex.Message };
            }
        }
        /// <summary>
        /// reconnect
        /// </summary>
        public override Task OnReconnected()
        {
            using (var db = new WMSEntities())
            {
                var connection = db.Connections.Where(x => x.ConnectionId == Context.ConnectionId).SingleOrDefault();
                if (connection == null)
                    throw new Exception("An attempt to reconnect a non tracked connection id have been made.");
                connection.IsOnline = true;
                db.SaveChanges();
            }

            UsersOnline();

            return base.OnReconnected();
        }
        /// <summary>
        /// disconnect
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new WMSEntities())
            {
                var connection = db.Connections.Where(x => x.ConnectionId == Context.ConnectionId).SingleOrDefault();
                if (connection == null)
                    throw new Exception("An attempt to disconnect a non tracked connection id have been made.");
                connection.IsOnline = false;
                db.SaveChanges();
            }
            UsersOnline();
            return base.OnDisconnected(stopCalled);
        }
    }
}