namespace Wms12m.Entity
{
    public class TahsilatSorumlusuSelect
    {
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Kod { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string Email { get; set; }


        public static string Sorgu = @"
        SELECT Kod, Email FROM BIRIKIM.usr.Users WHERE Tip = 2";


    }
}
