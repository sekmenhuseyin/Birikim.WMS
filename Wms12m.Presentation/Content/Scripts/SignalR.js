'use strict';
$(function () {
    var zigChatHubProxy = $.connection.zigChatHub;
    //chat yazma: bu kısım hubdan çalıştırılıyor
    zigChatHubProxy.client.updateChat = function (userName, message, isPm) {
        var lr = currentUserName === userName ? 'left' : 'right';
        var $newMessage = $('<div class="panel panel-primary" style="margin-' + lr + ': 7em; background-color: #337ab7;">' +
                                '<div style="padding: .5em; color: white; border-bottom: .1em solid white; font-size: 11px;">' + userName + ' ' + moment().fromNow() + '</div>' +
                                '<div style="padding: .5em; color: white; text-align: right;">' + message + '</div>' +
                            '</div>');

        $('#chat').append($newMessage);

        $newMessage[0].scrollIntoView(true);
    };
    ///online kullanıcıları listeler
    zigChatHubProxy.client.updateUsersOnline = function (data) {
        if (!data.Success) {
            alert(data.ErrorMessage);
            return;
        }
        var $users = $('#users');
        $users.html(null);
        //geri dönen değerin "UsersOnline" değişkenine bakar
        for (var user of data.UsersOnline) {
            if (user === currentUserName)
                $users.append($('<p class="user-current">' + user + '</p>'));
            else {
                $users.append('<p class="user">' + user + '</p>');
            }
        }
    };
    //connection başladığında
    $.connection.hub.start({ transport: 'longPolling' })
        .done(function () {
            var status = zigChatHubProxy.server.connectUser(currentUserName).done(function (data, textStatus, jqXHR) {
                if (!data.Success) {
                    alert(data.ErrorMessage);
                    return;
                }
                //mesaj gönderme fonksiyonu
                var $message = $('#message');
                var sendMessage = function () {
                    $message.focus();

                    if ($message.val() === '')
                        return;

                    zigChatHubProxy.server.sendMessage(currentUserName, $message.val());
                    $message.val('');
                };
                //entera basınca da gönder
                $message.keyup(function (data) {
                    if (data.which === 13)
                        sendMessage();
                });

                $('#send').click(sendMessage);

                $message.focus();

            });
        });
});