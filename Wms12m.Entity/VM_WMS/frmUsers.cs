using System;

namespace Wms12m.Entity
{
    public class frmChatUser
    {
        public string AdSoyad { get; set; }
        public bool Aktif { get; set; }
        public string Email { get; set; }
        public string Guid { get; set; }
        public int ID { get; set; }
        public string Kod { get; set; }
        public string RoleName { get; set; }
    }

    /// <summary>
    /// mesaj tablosu
    /// </summary>
    public class frmMessages
    {
        public string AdSoyad { get; set; }
        public Guid ID { get; set; }
        public string Kod { get; set; }
        public string Mesaj { get; set; }
        public int MesajTipi { get; set; }
        public DateTime Tarih { get; set; }
    }

    /// <summary>
    /// notifications tablosu
    /// </summary>
    public class frmNewNotification
    {
        public string ConnectionId { get; set; }
        public int ID { get; set; }
        public string Kime { get; set; }
        public string Mesaj { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// RolePerms
    /// </summary>
    public partial class frmRolePerms
    {
        public string Deleting { get; set; }
        public int ID { get; set; }
        public string PermName { get; set; }
        public string Reading { get; set; }
        public string RoleName { get; set; }
        public string Updating { get; set; }
        public string UserName { get; set; }
        public string Writing { get; set; }
    }

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
        public string AdSoyad { get; set; }
        public int DepoID { get; set; }
        public string DepoKodu { get; set; }
        public string Guid { get; set; }
        public int ID { get; set; }
        public string Kod { get; set; }
    }

    /// <summary>
    /// b2b kullanıcıları
    /// </summary>
    public class mdlB2BUsers
    {
        public string HesapKodu { get; set; }
        public int ID { get; set; }
        public string Parola { get; set; }
        public string Unvan { get; set; }
        public string YetkiliEMail { get; set; }
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
    /// sipariş onay
    /// </summary>
    public class SipOnayYetkiler
    {
        public string AdSoyad { get; set; }
        public string GostCHKKodAlani { get; set; }
        public string GosterilecekSirket { get; set; }
        public string GostKod3OrtBakiye { get; set; }
        public string GostRiskDeger { get; set; }
        public string GostSTKDeger { get; set; }
        public string Kod { get; set; }
        public string RoleName { get; set; }
        public int UserID { get; set; }
    }
}