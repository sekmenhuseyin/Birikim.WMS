$(function () { $("#div_loading").hide(); }); $(document).ajaxStart(function () { $("#div_loading").show(); }); $(document).ajaxStop(function () { $("#div_loading").hide(); });
function replaceAll(str, find, replace) { return str.replace(new RegExp(find, 'g'), replace); }
function formatDate(date) {
    var monthNames = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();
    return day + ' ' + monthNames[monthIndex] + ' ' + year;
}
function fromOADate(oadate) {
    var date = new Date(((oadate - 25569) * 86400000));
    var tz = date.getTimezoneOffset();
    return formatDate(new Date(((oadate - 25569 + (tz / (60 * 24))) * 86400000)));
}
function toOADate (date) {
    var timezoneOffset = date.getTimezoneOffset() / (60 * 24);
    var msDateObj = (date.getTime() / 86400000) + (25569 - timezoneOffset);
    return msDateObj;
}
Number.prototype.toHHMMSS = function () {
    var sec_num = parseInt(this, 10); // don't forget the second param
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);

    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    return hours + ':' + minutes + ':' + seconds;
}
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "," : d,
        t = t == undefined ? "." : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};
//personel ayrıntıları editleme sayfaları
function editInModal(URL) {
    $("#modalEditPage").html("");
    $.ajax({
        type: "POST",
        url: URL,
        datatype: "html",
        success: function (data) { $("#modalEditPage").html(data); }
    });
}
//url:method adresi ,div:render edeceği div,Id:detay için id göndere bilir
function PartialView(Url, Div, Id) {
    $.ajax({
        url: Url,
        type: 'POST',
        data: Id,
        cache: false,
        dataType: "html",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data == "") {
                alert("Hata Oluştu !!!");
            } else {
                $('#' + Div).html("");
                $('#' + Div).html(data);
            }
        }
    });
}
// silme için deleteıd ıd gönderme işlemi
function FunctionDelete(URL, deleteId) {
    var $Return = false;
    if (URL == "") URL = DeleteFunctionUrl;
    $.ajax({
        url: URL,
        type: 'POST',
        async: false,
        data: JSON.stringify({ Id: deleteId }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.Id == 0) {
                if (data.Message == "Depo") {
                    Modaldialog("Bu depoya kayıtlı koridor bilgisi bulunmaktadır. Silme işlemi gerçekleştirilemedi.", "Hata", "Tamam", "btn-danger");
                }
                else if (data.Message == "Koridor") {
                    Modaldialog("Bu koridora kayıtlı raf bilgisi bulunmaktadır. Silme işlemi gerçekleştirilemedi.", "Hata", "Tamam", "btn-danger");
                }
                else if (data.Message == "Raf") {
                    Modaldialog("Bu rafa kayıtlı bölüm bilgisi bulunmaktadır. Silme işlemi gerçekleştirilemedi.", "Hata", "Tamam", "btn-danger");
                }
                else if (data.Message == "Bölüm") {
                    Modaldialog("Bu bölüme kayıtlı kat bilgisi bulunmaktadır. Silme işlemi gerçekleştirilemedi.", "Hata", "Tamam", "btn-danger");
                }
                else if (data.Message == "Kat") {
                    Modaldialog("Bu kata kayıtlı ölçü bilgisi bulunmaktadır. Silme işlemi gerçekleştirilemedi.", "Hata", "Tamam", "btn-danger");
                }
                else {
                    Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
                }
            }
        }
    }).done(function (data) {
        if (data.Id == -2) {
            Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
        } else if (data.Id > 0) {
            return $Return = true;
        }
    });
    return $Return;
}
function CreateEditHide(CreateEditFunction) {
    $('#' + CreateEditFunction).html("");
}
// silme işleminde method silinecek nesne ile ilgili method belirtiyor,divname ise günceleme sonrası hangi div veya elementi güncelleyecegini belirliyor
function Delete(deleteId, Method, DivName, extraId, URL) {
    ModalYesNoClick('Kaydı silmek istediğinizden eminmisiniz !!!', ' İşlemi', "Evet", 'btn-success', DeleteTriger, 'Hayır', 'btn-warning', null);
    URL = URL || DeleteFunctionUrl;
    function DeleteTriger() {
        var Status = FunctionDelete(URL, deleteId);
        if (Status) {
            if (Method == "")
                window.location.reload();
            else
                PartialView(Method, DivName, JSON.stringify({ Id: extraId }));
        }
        //else {
        //    Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-success");
        //}
    }
}

