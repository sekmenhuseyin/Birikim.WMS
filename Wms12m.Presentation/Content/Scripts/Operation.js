﻿//loading panel
$(function () { $("#div_loading").hide(); });
$(document).ajaxStart(function () { $("#div_loading").show(); });
$(document).ajaxStop(function () { $("#div_loading").hide(); });
//browser-update.org
var $buoop = { notify: { i: +100, f: -4, o: -4, s: -2, c: -6 }, reminder: 1, reminderClosed: 1, unsecure: true, api: 5 };
function $buo_f()
{
    var e = document.createElement("script");
    e.src = "//browser-update.org/update.min.js";
    document.body.appendChild(e);
}
try { document.addEventListener("DOMContentLoaded", $buo_f, false); }
catch (e) { window.attachEvent("onload", $buo_f); }
//filter fn
function filter(tbl, col, val)
{
    tbl.column(col).search(val).draw();
}
//personel ayrıntıları editleme sayfaları
function editInModal(URL)
{
    $("#modalEditPage").html("");
    $.ajax({
        type: "POST",
        url: URL,
        datatype: "html",
        success: function (data)
        {
            $("#modalEditPage").html(data);
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
            $("#div_loading").hide();
        }
    });
}
function editInModal2(URL, data)
{
    $("#modalEditPage").html("");
    $.ajax({
        type: "POST",
        data: data,
        url: URL,
        datatype: "html",
        success: function (data)
        {
            $("#modalEditPage").html(data);
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
            $("#div_loading").hide();
        }
    });
}

//url:method adresi, div:render edeceği div, Id:detay için id gönderebilir
function PartialView(Url, Div, Id)
{
    $.ajax({
        url: Url,
        type: 'POST',
        data: Id,
        cache: false,
        dataType: "html",
        contentType: 'application/json; charset=utf-8',
        success: function (data)
        {
            if (data === "")
            {
                Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
                $("#div_loading").hide();
            } else
            {
                $('#' + Div).html(data);
            }
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
            $("#div_loading").hide();
        }
    });
}
function PartialViewClass(Url, Div, Id)
{
    $.ajax({
        url: Url,
        type: 'POST',
        data: Id,
        cache: false,
        dataType: "html",
        contentType: 'application/json; charset=utf-8',
        success: function (data)
        {
            if (data === "")
            {
                Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
                $("#div_loading").hide();
            } else
            {
                $('.' + Div).html(data);
            }
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
            $("#div_loading").hide();
        }
    });
}
// silme için deleteıd ıd gönderme işlemi
function FunctionDelete(URL, deleteId)
{
    var $Return = false;
    if (URL === "") URL = DeleteFunctionUrl;
    $.ajax({
        url: URL,
        type: 'POST',
        async: false,
        data: JSON.stringify({ Id: deleteId }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (data)
        {
            if (data.Id === 0)
            {
                if (data.Message === "") data.Message = "Hata oluştu";
                Modaldialog(data.Message, "Hata", "Tamam", "btn-danger");
            }
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
            $("#div_loading").hide();
        }
    }).done(function (data)
    {
        if (data.Id === -2)
        {
            Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-danger");
        } else if (data.Id > 0)
        {
            return $Return = true;
        }
    });
    return $Return;
}
// silme işleminde method silinecek nesne ile ilgili method belirtiyor,divname ise günceleme sonrası hangi div veya elementi güncelleyecegini belirliyor
function Delete(deleteId, Method, DivName, extraId, URL)
{
    ModalYesNoClick('Kaydı silmek istediğinizden emin misiniz?', 'Silme İşlemi', "Evet", 'btn-success', DeleteTriger, 'Hayır', 'btn-warning', null);
    URL = URL || DeleteFunctionUrl;
    function DeleteTriger()
    {
        var Status = FunctionDelete(URL, deleteId);
        if (Status)
        {
            if (Method === "")
                window.location.reload();
            else
            {
                PartialView(Method, DivName, JSON.stringify({ Id: extraId }));
            }
        }
    }
}
//url:method adresi ,div:render edeceği div,Id:detay için id göndere bilir
function AjaxCall(Url, Data, successTriger)
{
    $.ajax({
        url: Url,
        type: 'POST',
        data: Data,
        success: function (data)
        {
            if (data.Status === true)
            {
                Modaldialog("İşlem Başarılı", "Başarılı", "Tamam", "btn-success");
                successTriger();
            }
            else if (data.Message !== "" && data.Message !== null)
                Modaldialog(data.Message, 'Hata', 'Tamam', 'btn-danger');
            else
                Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
        },
        error: function (data)
        {
            Modaldialog("Hata oldu", 'Hata', 'Tamam', 'btn-danger');
        }
    });
}
//toastr mesajı oluştur
function CT(style, message, title, url)
{
    toastr[style](message, title, {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "showDuration": "30000",
        "hideDuration": "1000",
        "timeOut": "10000",
        "extendedTimeOut": "2000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        onclick: function () { if (url !== '' && url !== null && url !== undefined) window.location.href = url; }
    });
}
//refreshNotifications
function RefreshNotifications()
{
    $.ajax({
        url: '/Home/Notifications',
        type: 'POST',
        data: JSON.stringify({ Onay: true }),
        cache: false,
        dataType: "html",
        contentType: 'application/json; charset=utf-8',
        success: function (data)
        {
            $('.notificationCount').html("0");
        },
        error: function (data)
        {
            $("#div_loading").hide();
        }
    });
}