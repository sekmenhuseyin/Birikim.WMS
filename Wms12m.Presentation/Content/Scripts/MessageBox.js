function ModalYesNoClick(Message, Title, YesLabel, YesClass, Yescallback, NoLabel, NoClass, Nocallback) {
    bootbox.dialog({
        message: Message,
        title: Title,
        buttons: {
            save: {
                label: YesLabel,
                className: YesClass,
                callback: function () {
                    Yescallback();
                }
            },
            cancel: {
                label: NoLabel,
                className: NoClass,
                callback: function () {
                    if (Nocallback == null)
                    { } else {
                        Nocallback();
                    }
                }
            }
        }
    });
}
function ModalConfirmClick(Message, Title, YesLabel, YesClass, Yescallback, NoLabel, NoClass, Nocallback, MainLabel, MainClass, Maincallback) {
    bootbox.dialog({
        message: Message,
        title: Title,
        buttons: {
            save: {
                label: YesLabel,
                className: YesClass,
                callback: function () {
                    Yescallback();
                }
            },
            cancel: {
                label: NoLabel,
                className: NoClass,
                callback: function () {
                    Nocallback();
                }
            },
            main: {
                label: MainLabel,
                className: MainClass,
                callback: function () {
                    Maincallback();
                }
            }
        }
    });
}


function Modaldialog(message, title, label, className) {
    bootbox.dialog({
        message: message,
        title: title,
        buttons: {
            cancel: {
                label: label,
                className: className
            }
        }
    });
}