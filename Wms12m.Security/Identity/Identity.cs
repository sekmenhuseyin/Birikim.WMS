using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Wms12m.Security
{
    [DataContract, Serializable]
    public class Identity
    {
        private static Identity _identity = null;

        [DataMember]
        public PresentationIdentity Application { get; set; }

        [DataMember]
        public UserIdentity User { get; set; }

        [DataMember]
        public ActionIdentity Action { get; set; }

        public static Identity Current
        {
            get
            {
                if (OperationContext.Current != null)
                {
                    Identity identity = OperationContext.Current.IncomingMessageHeaders.GetHeader<Identity>("Identity", "");

                    if (identity != null)
                    {
                        identity.Application.Layer = Layer.Service;
                    }
                    return identity;
                }
                else if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Identity identity = (HttpContext.Current.User as CustomPrincipal).AppIdentity;
                    if (identity != null)
                    {
                        identity.Application.Layer = Layer.Application;
                    }
                    return identity;
                }
                else if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    Identity identity = HttpContext.Current.Session["Identity"] as Identity;

                    if (identity != null)
                    {
                        identity.Application.Layer = Layer.Application;
                    }
                    return identity;
                }
                else
                {
                    if (_identity != null)
                    {
                        return _identity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set
            {
                _identity = value;

            }
        }
    }
}
