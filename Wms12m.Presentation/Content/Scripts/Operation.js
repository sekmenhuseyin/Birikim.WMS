////////////////////////////////////////////////////////////////////////////////////
//https://github.com/Chalarangelo/30-seconds-of-code
////////////////////////////////////////////////////////////////////////////////////
// isemail(sekmenhuseyin@gmail.com) -> true
const validateEmail = str => /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(str);
// capitalizeEveryWord('hello world!') -> 'Hello World!'
const capitalizeEveryWord = str => str.replace(/\b[a-z]/g, char => char.toUpperCase());
// capitalize('myName', true) -> 'Myname'
const capitalize = (str, lowerRest = false) => str.slice(0, 1).toUpperCase() + (lowerRest ? str.slice(1).toLowerCase() : str.slice(1));
// currentUrl() -> 'https://google.com'
const currentUrl = _ => window.location.href;
// isEven(3) -> false
const isEven = num => Math.abs(num) % 2 === 0;
// arrayMax([10, 1, 5]) -> 10
const arrayMax = arr => Math.max(...arr);
// arrayMin([10, 1, 5]) -> 1
const arrayMin = arr => Math.min(...arr);
// randomizeOrder([1,2,3]) -> [1,3,2]
const randomizeOrder = arr => arr.sort((a, b) => Math.random() >= 0.5 ? -1 : 1);
// redirect('https://google.com')
const redirect = (url, asLink = true) => asLink ? window.location.href = url : window.location.replace(url);
// rgbToHex(255, 165, 1) -> 'ffa501'
const rgbToHex = (r, g, b) => ((r << 16) + (g << 8) + b).toString(16).padStart(6, '0');
// scrollToTop()
const scrollToTop = _ =>
{
    const c = document.documentElement.scrollTop || document.body.scrollTop;
    if (c > 0)
    {
        window.requestAnimationFrame(scrollToTop);
        window.scrollTo(0, c - c / 8);
    }
};
// shuffle([1,2,3]) -> [2, 1, 3]
const shuffle = arr =>
{
    let r = arr.map(Math.random);
    return arr.sort((a, b) => r[a] - r[b]);
};
// truncate('boomerang', 7) -> 'boom...'
const truncate = (str, num) => str.length > num ? str.slice(0, num > 3 ? num - 3 : num) + '...' : str;
// getUrlParameters('http://url.com/page?name=Adam&surname=Smith') -> {name: 'Adam', surname: 'Smith'}
const getUrlParameters = url =>
    url.match(/([^?=&]+)(=([^&]*))?/g).reduce(
        (a, v) => (a[v.slice(0, v.indexOf('='))] = v.slice(v.indexOf('=') + 1), a), {}
    );
// uuid() -> '7982fcfe-5721-4632-bede-6000885be57d'
const uuid = _ =>
    ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
// validateNumber('10') -> true
const validateNumber = n => !isNaN(parseFloat(n)) && isFinite(n);

////////////////////////////////////////////////////////////////////////////////////
//loading panel
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
        }
    });
}
//url:method adresi ,div:render edeceği div,Id:detay için id göndere bilir
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
            } else
            {
                $('#' + Div).html(data);
            }
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
            } else
            {
                $('.' + Div).html(data);
            }
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
    ModalYesNoClick('Kaydı silmek istediğinizden eminmisiniz !!!', 'Silme İşlemi', "Evet", 'btn-success', DeleteTriger, 'Hayır', 'btn-warning', null);
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
        }
    });
}