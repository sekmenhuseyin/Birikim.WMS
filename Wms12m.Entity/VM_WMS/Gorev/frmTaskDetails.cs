using System.Collections.Generic;
using Wms12m.Entity.Models;

namespace Wms12m.Entity
{
    /// <summary>
    /// görev ayrıntıları
    /// </summary>
    public class frmTaskDetails
    {
        public Gorev grv { get; set; }
        public List<frmIrsDetayfromGorev> irsdetay { get; set; }
    }
}