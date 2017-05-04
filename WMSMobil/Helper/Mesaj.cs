using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WMSMobil
{
    public static class Mesaj
    {
        private const string basariMesaj = "İşlem başarılı bir şekilde gerçekleştirildi.";
        private const string hataMesaj = "Hata! İşlem gerçekleştirilemedi.";
        /// <summary>
        /// hata mesajı normal
        /// </summary>
        public static void Hata(Exception ex)
        {
            MessageBox.Show(hataMesaj + "\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// hata mesajı özel
        /// </summary>
        public static void Hata(Exception ex, string ozelMesaj)
        {
            if (ex != null)
                MessageBox.Show(ozelMesaj + "\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else
                MessageBox.Show(ozelMesaj, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// başarı mesajı normal
        /// </summary>
        public static void Basari()
        {
            MessageBox.Show(basariMesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// başarı mesajı özel
        /// </summary>
        public static void Basari(string mesaj)
        {
            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// uyarı mesajı
        /// </summary>
        public static void Uyari(string mesaj)
        {
            MessageBox.Show(mesaj, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// MessageBoxButtons.YesNo
        /// </summary>
        public static DialogResult Soru(string mesaj)
        {
            return MessageBox.Show(mesaj, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }
}
