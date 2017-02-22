using System.Web.Mvc;
using Wms12m.Security;

namespace Wms12m
{
    public abstract class WebViewPageBase<T> : WebViewPage<T>
    {
        public virtual new UserIdentity User
        {
            get
            {
                var u = base.User as CustomPrincipal;
                if (u != null)
                {
                    return u.AppIdentity.User;
                }

                return null;
            }
        }
    }
}