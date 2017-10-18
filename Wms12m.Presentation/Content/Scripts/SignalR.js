'use strict';
$(function () {
    var zigChatHubProxy = $.connection.zigChatHub;

    zigChatHubProxy.client.updateChat = function (userName, message, isPm) {
        var $newMessage = $('<div class="panel panel-primary" style="margin-' + currentUserName === userName ? 'left' : 'right' + ': 7em; background-color: #337ab7;">' +
                                '<div style="padding: .5em; color: white; border-bottom: .1em solid white; font-size: 11px;">' + userName + ' ' + userName + '</div>' +
                                '<div style="padding: .5em; color: white; text-align: right;">' + message + '</div>' +
                            '</div>');

        $('#chat').append($newMessage);

        $newMessage[0].scrollIntoView(true);
    };

    zigChatHubProxy.client.updateUsersOnline = function (data) {
        if (!data.Success) {
            alert(data.ErrorMessage);
            return;
        }

        var $users = $('#users');
        $users.html(null);

        for (var user of data.UsersOnline) {
            if (user === currentUserName)
                $users.append($('<p class="user-current">' + user + '</p>'));
            else {
                var $user = $('<p class="user">' + user + '</p>');
                $user.click(function () {
                    var $message = $('#message');
                    $message.val('@' + $(this).text() + ' ');
                    $message.focus();
                });

                $users.append($user);
            }
        }
    };
    $.connection.hub.start({ transport: 'longPolling' })
        .done(function () {
            var status = zigChatHubProxy.server.connectUser(currentUserName).done(function (data, textStatus, jqXHR) {
                if (!data.Success) {
                    alert(data.ErrorMessage);
                    return;
                }

                var $message = $('#message');

                var sendMessage = function () {
                    $message.focus();

                    if ($message.val() === '')
                        return;

                    zigChatHubProxy.server.sendMessage(currentUserName, $message.val());
                    $message.val('');
                };

                $message.keyup(function (data) {
                    if (data.which === 13)
                        sendMessage();
                });

                $('#send').click(sendMessage);

                $message.focus();
            });
        });
});