using System.Security.Principal;

namespace Wms12m.Security
{
    interface ICustomPrincipal : IPrincipal
    {
        Identity AppIdentity { get; set; }
    }
}
