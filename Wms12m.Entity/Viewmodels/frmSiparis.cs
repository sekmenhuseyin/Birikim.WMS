using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmSiparisler
    {
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
    }
}
