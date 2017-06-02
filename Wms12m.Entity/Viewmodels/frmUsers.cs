﻿namespace Wms12m.Entity
{
    /// <summary>
    /// şifre değiştir
    /// </summary>
    public class frmUserChangePass
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
    }
    /// <summary>
    /// giriş sonrası gelen veri
    /// </summary>
    public class Login
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public string Kod { get; set; }
        public string AdSoyad { get; set; }
        public string DepoKodu { get; set; }
        public int DepoID { get; set; }
    }
    /// <summary>
    /// menülerin yetki oluşturma formu
    /// </summary>
    public class mdlCreateMenuPermission
    {
        public short MenuNo { get; set; }
        public string RolNo { get; set; }
    }
    /// <summary>
    /// RolePerms
    /// </summary>
    public partial class frmRolePerms
    {
        public int ID { get; set; }
        public string PermName { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Reading { get; set; }
        public string Writing { get; set; }
        public string Updating { get; set; }
        public string Deleting { get; set; }
    }
}
