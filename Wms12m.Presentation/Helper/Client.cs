﻿using System;
using System.Web;
using Wms12m.Security;

namespace Wms12m.Presentation
{
    public sealed class Client
    {
        private static Identity Identity
        {
            get { return HttpContext.Current.Session["Identity"] as Identity; }
            set { HttpContext.Current.Session["Identity"] = value; }
        }

        private Client()
        {
        }

        public static void Initialize()
        {
            Identity = new Identity()
            {
                Application = new PresentationIdentity()
                {
                    Application = "Wms12m.Presentation",
                    Channel = "WEB-UI"
                },
                User = new UserIdentity()
                {
                    Id = 0,
                    UserName = "Anonymous",
                    FullName = "Anonymous",
                    Guid = "0"
                },
                Action = new ActionIdentity()
                {
                    TrackingCode = GenerateTrackingCode(),
                    Machine = Environment.MachineName,
                    IpAddress = HttpContext.Current.Request.UserHostAddress,
                    Url = string.Empty,
                    Operation = string.Empty
                }
            };
        }

        public static string GenerateTrackingCode()
        {
            return string.Format("{0}/{1}", Environment.TickCount, new Random().Next(1000, 9999));
        }
    }
}