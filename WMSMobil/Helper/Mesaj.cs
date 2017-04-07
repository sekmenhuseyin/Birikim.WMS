using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WMSMobil
{
    public static class Mesaj
    {
        public const string basariMesaj = "İşlem başarılı bir şekilde gerçekleştirildi.";
        public const string hataMesaj = "Hata!! İşlem gerçekleştirilemedi!!!";

        public static void Hata(Exception ex)
        {
            MessageBox.Show(hataMesaj + "\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
        public static void Hata(Exception ex, string ozelMesaj)
        {
            if (ex != null)
                MessageBox.Show(ozelMesaj + "\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else
                MessageBox.Show(ozelMesaj, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        public static void Basari()
        {
            MessageBox.Show(basariMesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        public static void Basari(string mesaj)
        {
            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        public static void Uyari(string mesaj)
        {
            MessageBox.Show(mesaj, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// MessageBoxButtons.YesNo
        /// </summary>
        /// <param name="mesaj"></param>
        /// <returns></returns>
        public static DialogResult Soru(string mesaj)
        {
            return MessageBox.Show(mesaj, "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }
}
