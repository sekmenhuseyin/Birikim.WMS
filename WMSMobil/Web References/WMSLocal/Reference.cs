﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8745
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.8745.
// 
namespace WMSMobil.WMSLocal {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MobilServisSoap", Namespace="http://www.12mbilgisayar.com/")]
    public partial class MobilServis : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public MobilServis() {
            this.Url = "http://localhost:3841/MobilServis.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/LoginKontrol", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Login LoginKontrol(string userID, string sifre) {
            object[] results = this.Invoke("LoginKontrol", new object[] {
                        userID,
                        sifre});
            return ((Login)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginLoginKontrol(string userID, string sifre, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("LoginKontrol", new object[] {
                        userID,
                        sifre}, callback, asyncState);
        }
        
        /// <remarks/>
        public Login EndLoginKontrol(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Login)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetGorevOzet", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GorevOzet[] GetGorevOzet(int ID) {
            object[] results = this.Invoke("GetGorevOzet", new object[] {
                        ID});
            return ((GorevOzet[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetGorevOzet(int ID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetGorevOzet", new object[] {
                        ID}, callback, asyncState);
        }
        
        /// <remarks/>
        public GorevOzet[] EndGetGorevOzet(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((GorevOzet[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetIrsaliyeList", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Tip_IRS[] GetIrsaliyeList() {
            object[] results = this.Invoke("GetIrsaliyeList", new object[0]);
            return ((Tip_IRS[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetIrsaliyeList(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetIrsaliyeList", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public Tip_IRS[] EndGetIrsaliyeList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Tip_IRS[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetIrsaliye", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Tip_IRS GetIrsaliye(int GorevID) {
            object[] results = this.Invoke("GetIrsaliye", new object[] {
                        GorevID});
            return ((Tip_IRS)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetIrsaliye(int GorevID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetIrsaliye", new object[] {
                        GorevID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Tip_IRS EndGetIrsaliye(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Tip_IRS)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetGorevList", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Tip_GOREV[] GetGorevList(int gorevli, int durum, int gorevtipi, int DepoID) {
            object[] results = this.Invoke("GetGorevList", new object[] {
                        gorevli,
                        durum,
                        gorevtipi,
                        DepoID});
            return ((Tip_GOREV[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetGorevList(int gorevli, int durum, int gorevtipi, int DepoID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetGorevList", new object[] {
                        gorevli,
                        durum,
                        gorevtipi,
                        DepoID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Tip_GOREV[] EndGetGorevList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Tip_GOREV[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetUsers", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Kullanicilar[] GetUsers() {
            object[] results = this.Invoke("GetUsers", new object[0]);
            return ((Kullanicilar[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUsers(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUsers", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public Kullanicilar[] EndGetUsers(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Kullanicilar[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetDurums", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Durum[] GetDurums() {
            object[] results = this.Invoke("GetDurums", new object[0]);
            return ((Durum[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDurums(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDurums", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public Durum[] EndGetDurums(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Durum[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/GetMalzemes", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Tip_STI[] GetMalzemes(int GorevID, bool devamMi) {
            object[] results = this.Invoke("GetMalzemes", new object[] {
                        GorevID,
                        devamMi});
            return ((Tip_STI[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetMalzemes(int GorevID, bool devamMi, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetMalzemes", new object[] {
                        GorevID,
                        devamMi}, callback, asyncState);
        }
        
        /// <remarks/>
        public Tip_STI[] EndGetMalzemes(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Tip_STI[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/Mal_Kabul", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result Mal_Kabul(frmMalKabul[] StiList) {
            object[] results = this.Invoke("Mal_Kabul", new object[] {
                        StiList});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginMal_Kabul(frmMalKabul[] StiList, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Mal_Kabul", new object[] {
                        StiList}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndMal_Kabul(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/MalKabul_GoreviTamamla", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result MalKabul_GoreviTamamla(int GorevID, int kulID) {
            object[] results = this.Invoke("MalKabul_GoreviTamamla", new object[] {
                        GorevID,
                        kulID});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginMalKabul_GoreviTamamla(int GorevID, int kulID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("MalKabul_GoreviTamamla", new object[] {
                        GorevID,
                        kulID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndMalKabul_GoreviTamamla(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/Rafa_Kaldir", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result Rafa_Kaldir(frmYerlesme[] YerlestirmeList, int kulID) {
            object[] results = this.Invoke("Rafa_Kaldir", new object[] {
                        YerlestirmeList,
                        kulID});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRafa_Kaldir(frmYerlesme[] YerlestirmeList, int kulID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Rafa_Kaldir", new object[] {
                        YerlestirmeList,
                        kulID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndRafa_Kaldir(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/RafaKaldir_GoreviTamamla", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result RafaKaldir_GoreviTamamla(int GorevID, int kulID) {
            object[] results = this.Invoke("RafaKaldir_GoreviTamamla", new object[] {
                        GorevID,
                        kulID});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRafaKaldir_GoreviTamamla(int GorevID, int kulID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RafaKaldir_GoreviTamamla", new object[] {
                        GorevID,
                        kulID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndRafaKaldir_GoreviTamamla(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/Siparis_Topla", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result Siparis_Topla(frmYerlesme[] YerlestirmeList, int kulID) {
            object[] results = this.Invoke("Siparis_Topla", new object[] {
                        YerlestirmeList,
                        kulID});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSiparis_Topla(frmYerlesme[] YerlestirmeList, int kulID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Siparis_Topla", new object[] {
                        YerlestirmeList,
                        kulID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndSiparis_Topla(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.12mbilgisayar.com/SiparisTopla_GoreviTamamla", RequestNamespace="http://www.12mbilgisayar.com/", ResponseNamespace="http://www.12mbilgisayar.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result SiparisTopla_GoreviTamamla(int GorevID, int kulID) {
            object[] results = this.Invoke("SiparisTopla_GoreviTamamla", new object[] {
                        GorevID,
                        kulID});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSiparisTopla_GoreviTamamla(int GorevID, int kulID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SiparisTopla_GoreviTamamla", new object[] {
                        GorevID,
                        kulID}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndSiparisTopla_GoreviTamamla(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Login {
        
        private int idField;
        
        private string kodField;
        
        private string adSoyadField;
        
        private string depoKoduField;
        
        private int depoIDField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Kod {
            get {
                return this.kodField;
            }
            set {
                this.kodField = value;
            }
        }
        
        /// <remarks/>
        public string AdSoyad {
            get {
                return this.adSoyadField;
            }
            set {
                this.adSoyadField = value;
            }
        }
        
        /// <remarks/>
        public string DepoKodu {
            get {
                return this.depoKoduField;
            }
            set {
                this.depoKoduField = value;
            }
        }
        
        /// <remarks/>
        public int DepoID {
            get {
                return this.depoIDField;
            }
            set {
                this.depoIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class frmYerlesme {
        
        private int gorevIDField;
        
        private int irsIDField;
        
        private int irsDetayIDField;
        
        private int depoIDField;
        
        private string malKoduField;
        
        private string birimField;
        
        private decimal miktarField;
        
        private string rafNoField;
        
        /// <remarks/>
        public int GorevID {
            get {
                return this.gorevIDField;
            }
            set {
                this.gorevIDField = value;
            }
        }
        
        /// <remarks/>
        public int IrsID {
            get {
                return this.irsIDField;
            }
            set {
                this.irsIDField = value;
            }
        }
        
        /// <remarks/>
        public int IrsDetayID {
            get {
                return this.irsDetayIDField;
            }
            set {
                this.irsDetayIDField = value;
            }
        }
        
        /// <remarks/>
        public int DepoID {
            get {
                return this.depoIDField;
            }
            set {
                this.depoIDField = value;
            }
        }
        
        /// <remarks/>
        public string MalKodu {
            get {
                return this.malKoduField;
            }
            set {
                this.malKoduField = value;
            }
        }
        
        /// <remarks/>
        public string Birim {
            get {
                return this.birimField;
            }
            set {
                this.birimField = value;
            }
        }
        
        /// <remarks/>
        public decimal Miktar {
            get {
                return this.miktarField;
            }
            set {
                this.miktarField = value;
            }
        }
        
        /// <remarks/>
        public string RafNo {
            get {
                return this.rafNoField;
            }
            set {
                this.rafNoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Result {
        
        private int idField;
        
        private bool statusField;
        
        private string messageField;
        
        private object dataField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public bool Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public object Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class frmMalKabul {
        
        private int idField;
        
        private decimal okutulanMiktarField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public decimal OkutulanMiktar {
            get {
                return this.okutulanMiktarField;
            }
            set {
                this.okutulanMiktarField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Tip_STI {
        
        private int idField;
        
        private int irsIDField;
        
        private string malKoduField;
        
        private string malAdiField;
        
        private decimal miktarField;
        
        private string birimField;
        
        private string barkodField;
        
        private int siraNoField;
        
        private string kaydedenField;
        
        private bool aktarimDurumuField;
        
        private decimal okutulanMiktarField;
        
        private decimal yerlestirmeMiktariField;
        
        private string rafField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public int irsID {
            get {
                return this.irsIDField;
            }
            set {
                this.irsIDField = value;
            }
        }
        
        /// <remarks/>
        public string MalKodu {
            get {
                return this.malKoduField;
            }
            set {
                this.malKoduField = value;
            }
        }
        
        /// <remarks/>
        public string MalAdi {
            get {
                return this.malAdiField;
            }
            set {
                this.malAdiField = value;
            }
        }
        
        /// <remarks/>
        public decimal Miktar {
            get {
                return this.miktarField;
            }
            set {
                this.miktarField = value;
            }
        }
        
        /// <remarks/>
        public string Birim {
            get {
                return this.birimField;
            }
            set {
                this.birimField = value;
            }
        }
        
        /// <remarks/>
        public string Barkod {
            get {
                return this.barkodField;
            }
            set {
                this.barkodField = value;
            }
        }
        
        /// <remarks/>
        public int SiraNo {
            get {
                return this.siraNoField;
            }
            set {
                this.siraNoField = value;
            }
        }
        
        /// <remarks/>
        public string Kaydeden {
            get {
                return this.kaydedenField;
            }
            set {
                this.kaydedenField = value;
            }
        }
        
        /// <remarks/>
        public bool AktarimDurumu {
            get {
                return this.aktarimDurumuField;
            }
            set {
                this.aktarimDurumuField = value;
            }
        }
        
        /// <remarks/>
        public decimal OkutulanMiktar {
            get {
                return this.okutulanMiktarField;
            }
            set {
                this.okutulanMiktarField = value;
            }
        }
        
        /// <remarks/>
        public decimal YerlestirmeMiktari {
            get {
                return this.yerlestirmeMiktariField;
            }
            set {
                this.yerlestirmeMiktariField = value;
            }
        }
        
        /// <remarks/>
        public string Raf {
            get {
                return this.rafField;
            }
            set {
                this.rafField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Durum {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Kullanicilar {
        
        private int idField;
        
        private string kodField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Kod {
            get {
                return this.kodField;
            }
            set {
                this.kodField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Tip_GOREV {
        
        private int idField;
        
        private int irsaliyeIDField;
        
        private string evrakNoField;
        
        private string bilgiField;
        
        private string atayanField;
        
        private string depoIDField;
        
        private string olusturmaTarihiField;
        
        private string gorevliField;
        
        private string aciklamaField;
        
        private string gorevNoField;
        
        private string durumField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public int IrsaliyeID {
            get {
                return this.irsaliyeIDField;
            }
            set {
                this.irsaliyeIDField = value;
            }
        }
        
        /// <remarks/>
        public string EvrakNo {
            get {
                return this.evrakNoField;
            }
            set {
                this.evrakNoField = value;
            }
        }
        
        /// <remarks/>
        public string Bilgi {
            get {
                return this.bilgiField;
            }
            set {
                this.bilgiField = value;
            }
        }
        
        /// <remarks/>
        public string Atayan {
            get {
                return this.atayanField;
            }
            set {
                this.atayanField = value;
            }
        }
        
        /// <remarks/>
        public string DepoID {
            get {
                return this.depoIDField;
            }
            set {
                this.depoIDField = value;
            }
        }
        
        /// <remarks/>
        public string OlusturmaTarihi {
            get {
                return this.olusturmaTarihiField;
            }
            set {
                this.olusturmaTarihiField = value;
            }
        }
        
        /// <remarks/>
        public string Gorevli {
            get {
                return this.gorevliField;
            }
            set {
                this.gorevliField = value;
            }
        }
        
        /// <remarks/>
        public string Aciklama {
            get {
                return this.aciklamaField;
            }
            set {
                this.aciklamaField = value;
            }
        }
        
        /// <remarks/>
        public string GorevNo {
            get {
                return this.gorevNoField;
            }
            set {
                this.gorevNoField = value;
            }
        }
        
        /// <remarks/>
        public string Durum {
            get {
                return this.durumField;
            }
            set {
                this.durumField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class Tip_IRS {
        
        private int idField;
        
        private string evrakNoField;
        
        private string hesapKoduField;
        
        private string teslimCHKField;
        
        private string depoIDField;
        
        private string kaydedenField;
        
        private string tarihField;
        
        private string sirketKodField;
        
        private string unvanField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string EvrakNo {
            get {
                return this.evrakNoField;
            }
            set {
                this.evrakNoField = value;
            }
        }
        
        /// <remarks/>
        public string HesapKodu {
            get {
                return this.hesapKoduField;
            }
            set {
                this.hesapKoduField = value;
            }
        }
        
        /// <remarks/>
        public string TeslimCHK {
            get {
                return this.teslimCHKField;
            }
            set {
                this.teslimCHKField = value;
            }
        }
        
        /// <remarks/>
        public string DepoID {
            get {
                return this.depoIDField;
            }
            set {
                this.depoIDField = value;
            }
        }
        
        /// <remarks/>
        public string Kaydeden {
            get {
                return this.kaydedenField;
            }
            set {
                this.kaydedenField = value;
            }
        }
        
        /// <remarks/>
        public string Tarih {
            get {
                return this.tarihField;
            }
            set {
                this.tarihField = value;
            }
        }
        
        /// <remarks/>
        public string SirketKod {
            get {
                return this.sirketKodField;
            }
            set {
                this.sirketKodField = value;
            }
        }
        
        /// <remarks/>
        public string Unvan {
            get {
                return this.unvanField;
            }
            set {
                this.unvanField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.12mbilgisayar.com/")]
    public partial class GorevOzet {
        
        private int idField;
        
        private string adField;
        
        private int sayiField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Ad {
            get {
                return this.adField;
            }
            set {
                this.adField = value;
            }
        }
        
        /// <remarks/>
        public int Sayi {
            get {
                return this.sayiField;
            }
            set {
                this.sayiField = value;
            }
        }
    }
}
