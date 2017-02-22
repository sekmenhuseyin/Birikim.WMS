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
function FunctionDelete(deleteId) {
    var $Return = false;
    $.ajax({
        url: DeleteFunctionUrl,
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
// silme işleminde method silinecek nesne ile ilgili method belirtiyor,divname ise günceleme sonrası hangi div veya elementi günceliyecegini belirliyor
function Delete(deleteId, Method, DivName, extraId) {
    ModalYesNoClick('Kaydı silmek istediğinizden eminmisiniz !!!', ' İşlemi', "Evet", 'btn-success', DeleteTriger, 'Hayır', 'btn-warning', null);
    function DeleteTriger() {
        var Status = FunctionDelete(deleteId);
        if (Status) {
            PartialView(Method, DivName, JSON.stringify({ Id: extraId }));
        }
        //else {
        //    Modaldialog("Hata oluştu", "Hata", "Tamam", "btn-success");
        //}
    }
}

