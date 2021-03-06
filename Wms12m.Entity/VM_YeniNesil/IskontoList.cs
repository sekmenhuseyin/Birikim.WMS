﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class IskontoList : IDisposable
    {
        public void Dispose() { GC.SuppressFinalize(this); }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public IskontoList() { }
        private string _listeNo;
        private string _listeAdi;
        private string _malKod;
        private Single _iskOran1;
        private Single _iskOran2;
        private Single _iskOran3;

        public string ListeNo { get { return _listeNo; } set { _listeNo = value; OnPropertyChanged("ListeNo"); } }
        public string ListeAdi { get { return _listeAdi; } set { _listeAdi = value; OnPropertyChanged("ListeAdi"); } }
        public string MalKod { get { return _malKod; } set { _malKod = value; OnPropertyChanged("MalKod"); } }
        public Single IskOran1 { get { return _iskOran1; } set { _iskOran1 = value; OnPropertyChanged("IskOran1"); } }
        public Single IskOran2 { get { return _iskOran2; } set { _iskOran2 = value; OnPropertyChanged("IskOran2"); } }
        public Single IskOran3 { get { return _iskOran3; } set { _iskOran3 = value; OnPropertyChanged("IskOran3"); } }
        public static string SelectSorgu = @"SELECT
                                       K.ListeNo
                                      ,K.ListeAdi
                                      ,K.MalKod
                                      ,K.IskOran1
                                      ,K.IskOran2
                                      ,K.IskOran3
                                      FROM YNS{0}.YNS{0}.TempIskontoListesi AS K WITH (NOLOCK)";
        public static string DeleteSorgu = @"DELETE FROM YNS{0}.YNS{0}.TempIskontoListesi";
        public static string InsertSorgu = @"USE YNS{0}
                                            BEGIN TRANSACTION  
                                            BEGIN TRY  
                                             INSERT INTO YNS{0}.YNS{0}.TempIskontoListesi
                                            (ListeNo,BasTarih,SiraNo,ListeAdi,BitTarih
                                            ,HesapKodu,MalKodGrup,MalKod,IskOran1,IskOran2
                                            ,IskOran3,Aciklama,AktifMi,Kaydeden,KayitTarih
                                            ,KayitSaat,Degistiren,DegisTarih,DegisSaat)
                                            VALUES (
                                            @LISTENO,@BASTARIH,@SIRANO,@LISTEADI,@BITTARIH
                                            ,@HESAPKODU,@MALKODGRUP,@MALKOD,@ISKORAN1,@ISKORAN2
                                            ,@ISKORAN3,@ACIKLAMA,@AKTIFMI,@KAYDEDEN,@KAYITTARIH
                                            ,@KAYITSAAT,@DEGISTIREN,@DEGISTARIH,@DEGISSAAT)
                                            END TRY  
                                            BEGIN CATCH  IF(@@TRANCOUNT > 0) BEGIN ROLLBACK TRANSACTION END  END CATCH
                                            IF(@@TRANCOUNT > 0)  BEGIN COMMIT TRANSACTION END  
                                            ";
    }
}
