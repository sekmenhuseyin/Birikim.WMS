﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Hubs
{
    public class ZigChatHub : Hub
    {
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
        /// disconnect
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new WMSEntities())
            {
                var connection = db.Connections.Where(x => x.ConnectionId == Context.ConnectionId).SingleOrDefault();
                if (connection != null)
                {
                    connection.IsOnline = false;
                    db.SaveChanges();
                }
            }

            UsersOnline();
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// reconnect
        /// </summary>
        public override Task OnReconnected()
        {
            using (var db = new WMSEntities())
            {
                var connection = db.Connections.Where(x => x.ConnectionId == Context.ConnectionId).SingleOrDefault();
                if (connection != null)
                {
                    connection.IsOnline = true;
                    db.SaveChanges();
                }
            }

            UsersOnline();
            return base.OnReconnected();
        }

        /// <summary>
        /// send message
        /// </summary>
        public void SendMessage(string userName, string usersend, string message)
        {
            using (var db = new WMSEntities())
            {
                var kişi = db.Users.Where(m => m.Kod == userName).FirstOrDefault();
                var guid = HtmlHelperExtension.ImageAddressOrDefault(kişi.Guid.ToString());
                if (usersend != "")
                {
                    // sadece bir kişiye mesaj gönderiyor
                    db.Messages.Add(new Message()
                    {
                        MesajTipi = ComboItems.KişiselMesaj.ToInt32(),
                        Tarih = DateTime.Now,
                        Kimden = userName,
                        Kime = usersend,
                        Mesaj = message
                    });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "HUB/SendMessage/1", userName);
                    }

                    // online ise hemen gönder
                    var pmConnection = db.Connections.Where(x => x.UserName.ToLower() == usersend && x.IsOnline).SingleOrDefault();
                    if (pmConnection != null)
                    {
                        Clients.Clients(new List<string> { Context.ConnectionId, pmConnection.ConnectionId }).UpdateChat(userName, usersend, message, kişi.AdSoyad, guid);
                        return;
                    }
                }
                else
                {
                    // herkese mesaj gönderiyor
                    db.Messages.Add(new Message()
                    {
                        MesajTipi = ComboItems.GrupMesajı.ToInt32(),
                        Tarih = DateTime.Now,
                        Kimden = userName,
                        Kime = "",
                        Mesaj = message
                    });
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "HUB/SendMessage/All", userName);
                    }

                    // online ise hemen gönder
                    Clients.All.UpdateChat(userName, usersend, message, kişi.AdSoyad, guid);
                }
            }
        }

        /// <summary>
        /// send notification
        /// </summary>
        public void SendNotifications()
        {
            using (var db = new WMSEntities())
            {
                var list = db.Database.SqlQuery<frmNewNotification>(@"EXEC BIRIKIM.dbo.GetNotificationsForActiveUsers").ToList();
                // eğer liste varsa gönder
                if (list.Count > 0)
                    foreach (var item in list)
                        Clients.Clients(new List<string> { item.ConnectionId }).ReceiveNotification(item.Name, item.Mesaj);
            }
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
        /// logging
        /// </summary>
        public void Logger(Exception ex, string page, string username)
        {
            using (var db = new WMSEntities())
            {
                var inner = "";
                if (ex.InnerException != null)
                {
                    inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
                }

                db.Logger(username, "", "", ex.Message, inner, page);
            }
        }
    }
}