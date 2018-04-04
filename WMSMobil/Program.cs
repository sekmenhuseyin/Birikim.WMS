using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using WMSMobil.TerminalService;
using System.IO;

namespace WMSMobil
{
    static class Program
    {
        public static Terminal Servis = new Terminal();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //create if file doesnt exists
            try
            {
                if (!File.Exists(@"\WMSMobil-ip.txt"))
                    using (var Dosya = new FileStream(@"\WMSMobil-ip.txt", FileMode.OpenOrCreate, FileAccess.Write))
                        using (var Oku = new StreamWriter(Dosya))
                            Oku.WriteLine("http://192.168.2.228/service/terminal.asmx");
                //get servis url
                using (var Dosya = new FileStream(@"\WMSMobil-ip.txt", FileMode.Open))
                    using (var Oku = new StreamReader(Dosya))
                    Ayarlar.ServisURL = Oku.ReadLine();
            }
            catch (Exception)
            {
                Ayarlar.ServisURL = Servis.Url;
            }
            //set servis
            Servis.Url = Ayarlar.ServisURL;
            //show login form
            Application.Run(new frmLogin());
        }
    }
}