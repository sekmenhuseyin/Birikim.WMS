using System;

namespace Wms12m.Entity
{
    public class MyDep
    {
        public bool Secim { get; set; }
        public string Depo { get; set; }
        public string DepoAdi { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<decimal> YeniFiyat { get; set; }

        /// <summary>
        /// <para>Kod1 de Fiyat Tutuluyor.</para>
        /// <para>Veritabanında şu formatta olması lazım. 150.50</para>
        /// </summary>
        public string Kod1 { get; set; }
        /// <summary>
        /// <para>Kod2 de Yeni Fiyat tutuluyor.</para>
        /// <para>Veritabanında şu formatta olması lazım. 150.50</para>
        /// </summary>
        public string Kod2 { get; set; }

        public void Kod1denFiyat()
        {
            var yKod1 = Kod1.Replace('.', ',');
            decimal fiyat;
            if (decimal.TryParse(yKod1, out fiyat))
            {
                Fiyat = fiyat;
            }
        }

        public void Kod2denYeniFiyat()
        {
            var yKod2 = Kod2.Replace('.', ',');
            decimal fiyat;
            if (decimal.TryParse(yKod2, out fiyat))
            {
                YeniFiyat = fiyat;
            }
        }
    }
}