﻿$(function () { $("#div_loading").hide(); });
$(document).ajaxStart(function () { $("#div_loading").show(); });
$(document).ajaxStop(function () { $("#div_loading").hide(); });
var $buoop = { vs: { i: 10, f: -8, o: -8, s: 8, c: -8 }, api: 4 };
function $buo_f() {
    var e = document.createElement("script");
    e.src = "//browser-update.org/update.min.js";
    document.body.appendChild(e);
};
try { document.addEventListener("DOMContentLoaded", $buo_f, false) }
catch (e) { window.attachEvent("onload", $buo_f) }
//filter fn
function filter(tbl, col, val) {
    tbl.column(col).search(val).draw();
}
//personel ayrıntıları editleme sayfaları
function editInModal(URL) {
    $("#modalEditPage").html("");
    $.ajax({
        type: "POST",
        url: URL,
        datatype: "html",
        success: function (data) {
            $("#modalEditPage").html(data);
        }
    });
}
function editInModal2(URL,data) {
    $("#modalEditPage").html("");
    $.ajax({
        type: "POST",
        data: data,
        url: URL,
        datatype: "html",
        success: function (data) {
            console.log(data);
            $("#modalEditPage").html(data);
        }
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
                Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
            } else {
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
                if (data.Message == "") data.Message = "Hata oluştu";
                Modaldialog(data.Message, "Hata", "Tamam", "btn-danger");
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
            {
                PartialView(Method, DivName, JSON.stringify({ Id: extraId }));
            }
        }
    }
}
function DeleteCallBack(deleteId) {
    ModalYesNoClick('Kaydı silmek istediğinizden eminmisiniz !!!', ' İşlemi', "Evet", 'btn-success', DeleteTriger, 'Hayır', 'btn-warning', null);
    function DeleteTriger() {
        var Status = FunctionDelete(DeleteFunctionUrl, deleteId);
        if (Status) {
            RefreshPage();
        }
    }
}

function DeleteKontrol(deleteId, Method, DivName, KontrolMetod, extraId, URL) {
    URL = URL || DeleteFunctionUrl;
    $.ajax({
        url: window.location.origin + KontrolMetod,
        data: { Id: deleteId.toString() },
        type: "post",
        success: function (data) {
            if (data.Status == false) {
                Modaldialog(data.Message, "Silme İşlemi", "Tamam", "Sil");
            }
            else {
                Delete(deleteId, Method, DivName, extraId, URL)
                CreateEdit('0');
            }
        }
    });
}


