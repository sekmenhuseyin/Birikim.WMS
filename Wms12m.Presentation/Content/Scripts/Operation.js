$(function () { $("#div_loading").hide(); });
$(document).ajaxStart(function () { $("#div_loading").show(); });
$(document).ajaxStop(function () { $("#div_loading").hide(); });
function CreateEditHide(CreateEditFunction) { $('#' + CreateEditFunction).html(""); }
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
    $('#' + Div).html("");
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
                PartialView(Method, DivName, JSON.stringify({ Id: extraId }));
        }
    }
}
// Stringe karakter eklemek için
String.prototype.addAt = function (index, character) {
    return this.substr(0, index) + character + this.substr(index + character.length - 1);
}
// Sayılara ondalık binlik ayraçları eklemek için
function ondalikBinlik(Val) {
    if (Val.toString().indexOf(",") < 1) {
        var b = new Array();
        var decVal = Number(Val).toFixed(2).replace(".", ",");
        var a = decVal.split(",")[0].length;
        for (var i = a; i > 0; i = i - 3) {
            var c = i % 3;
            if (c == 0 && i != 3) {
                b.push(i - 3);
            }
            else if (c != 0 && i >3) {
                b.push(i - 3);
            }
        }
        $.each(b, function (index, value) {
            decVal = decVal.addAt(value, '.');
        });
        return decVal;
    }
    else if (Val.toString().indexOf(".") < 1) {
        return Val;
    }
    else {
        var b = new Array();
       
        var detVal = Val.split(",")[0].replace(/\./g, "");
        var a = detVal.length;
        for (var i = a; i > 0; i = i - 3) {
            var c = i % 3;
            if (c == 0 && i != 3) {
                b.push(i - 3);
            }
            else if (c != 0 && i > 3) {
                b.push(i - 3);
            }
        }
        $.each(b, function (index, value) {
            detVal = detVal.addAt(value, '.');
        });
        detVal += "," + Val.split(",")[1]
        return detVal;
    }
}
