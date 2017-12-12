using System.Security.Principal;

namespace Wms12m.Security
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string email)
        {
            Identity = new GenericIdentity(email);
        }
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) => false;

        public Identity AppIdentity { get; set; }
    }
}