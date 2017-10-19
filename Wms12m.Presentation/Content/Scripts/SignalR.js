'use strict';
$(function () {
    var zigChatHubProxy = $.connection.zigChatHub;
    //chat yazma: bu kısım hubdan çalıştırılıyor
    zigChatHubProxy.client.updateChat = function (userNameFrom, userNameTo, message, userRealName, imageAddress) {
        //functions
        var preparePost = function (dir, time, name, avatar, message) {
            var tpl = '';
            tpl += '<div class="post ' + dir + '">';
            tpl += '<img class="avatar" alt="' + name + '" src="/Content/Uploads/' + avatar + '.jpg"/>';
            tpl += '<div class="message">';
            tpl += '<span class="arrow"></span>';
            tpl += '<a href="javascript:;" class="name">' + name + '</a>&nbsp;';
            tpl += '<span class="datetime">' + time + '</span>';
            tpl += '<span class="body">';
            tpl += message;
            tpl += '</span>';
            tpl += '</div>';
            tpl += '</div>';

            return tpl;
        };
        if (userNameTo === SendMessageTo) {
            var wrapper = $('.page-quick-sidebar-wrapper');
            var wrapperChat = wrapper.find('.page-quick-sidebar-chat');
            var chatContainer = wrapperChat.find(".page-quick-sidebar-chat-user-messages");
            var input = wrapperChat.find('.page-quick-sidebar-chat-user-form .form-control');
            // handle post
            var time = new Date();
            var messages = preparePost(currentUserName != userNameFrom ? 'in' : 'out', (time.getHours() + ':' + time.getMinutes()), userRealName, imageAddress, message);
            messages = $(messages);
            chatContainer.append(messages);
            chatContainer.slimScroll({ scrollTo: '1000000px' });
            input.val("");
            input.focus();
        }
        else
            CT("info", userRealName, message);

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
            else
                $users.append('<p class="user">' + user + '</p>');
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
                //functions
                var preparePost = function (dir, time, name, avatar, message) {
                    var tpl = '';
                    tpl += '<div class="post ' + dir + '">';
                    tpl += '<img class="avatar" alt="' + name + '" src="/Content/Uploads/' + avatar + '.jpg"/>';
                    tpl += '<div class="message">';
                    tpl += '<span class="arrow"></span>';
                    tpl += '<a href="javascript:;" class="name">' + name + '</a>&nbsp;';
                    tpl += '<span class="datetime">' + time + '</span>';
                    tpl += '<span class="body">';
                    tpl += message;
                    tpl += '</span>';
                    tpl += '</div>';
                    tpl += '</div>';

                    return tpl;
                };
                //vars
                var wrapper = $('.page-quick-sidebar-wrapper');
                var wrapperChat = wrapper.find('.page-quick-sidebar-chat');
                var chatContainer = wrapperChat.find(".page-quick-sidebar-chat-user-messages");
                var input = wrapperChat.find('.page-quick-sidebar-chat-user-form .form-control');
                var handleChatMessagePost = function (e) {
                    e.preventDefault();
                    var text = input.val();
                    if (text.length === 0) { return; }
                    // handle post
                    var time = new Date();
                    var message = preparePost('out', (time.getHours() + ':' + time.getMinutes()), currentUserName, currentUserImage, text);
                    message = $(message);
                    chatContainer.append(message);
                    chatContainer.slimScroll({ scrollTo: '1000000px' });
                    input.val("");
                    input.focus();
                    //save 2 db
                    zigChatHubProxy.server.sendMessage(currentUserName, SendMessageTo, text);
                };
                //entera basınca da gönder
                wrapperChat.find('.page-quick-sidebar-chat-user-form .btn').click(handleChatMessagePost);
                wrapperChat.find('.page-quick-sidebar-chat-user-form .form-control').keypress(function (e) {
                    if (e.which === 13) {
                        handleChatMessagePost(e);
                        return false;
                    }
                });
            });
        });
});