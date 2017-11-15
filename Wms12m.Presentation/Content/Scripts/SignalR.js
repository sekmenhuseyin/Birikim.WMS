'use strict';
$(function () {
    var zigChatHubProxy = $.connection.zigChatHub;
    var currentUserAvatar = currentUserImage;
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
        if (userNameFrom === currentUserName) { return; }
        else if (userNameFrom === SendMessageTo) {
            var wrapper = $('.page-quick-sidebar-wrapper');
            var wrapperChat = wrapper.find('.page-quick-sidebar-chat');
            var chatContainer = wrapperChat.find(".page-quick-sidebar-chat-user-messages");
            var input = wrapperChat.find('.page-quick-sidebar-chat-user-form .form-control');
            // handle post
            var time = new Date();
            var messages = preparePost(currentUserName !== userNameFrom ? 'in' : 'out', (time.getHours() + ':' + time.getMinutes()), userRealName, imageAddress, message);
            messages = $(messages);
            chatContainer.append(messages);
            chatContainer.slimScroll({ scrollTo: '1000000px' });
            input.val("");
            input.focus();
        }
        else
            CT("info", message, userRealName);

    };
    //herkese uyarı gönderme
    zigChatHubProxy.client.receiveNotification = function (title, message) {
        if (title !== "" && title !== null)CT("info", message, title);
        PartialView('/Home/Notifications', 'header_notification_bar', "");
    };
    ///online kullanıcıları listeler
    zigChatHubProxy.client.updateUsersOnline = function (data) {
        if (!data.Success) {
            alert(data.ErrorMessage);
            return;
        }
        $('.badge').removeClass("badge-success");
        $('.badge').removeClass("badge-danger");
        $('.badge').addClass("badge-danger");
        //geri dönen değerin "UsersOnline" değişkenine bakar
        for (var user of data.UsersOnline) {
            $('.badge-' + user).removeClass("badge-danger");
            $('.badge-' + user).addClass("badge-success");
        }
    };
    //connection başladığında
    $.connection.hub.start().done(function () {
        var status = zigChatHubProxy.server.connectUser(currentUserName).done(function (data, textStatus, jqXHR) {
            if (!data.Success) {
                alert(data.ErrorMessage);
                return;
            }
            //disconnect on logout
            $('#btnLogout').click(function () {
                zigChatHubProxy.connection.stop();
                window.location.href = "/Security/LogOut";
            });
            //send notifications on page load and on new items
            zigChatHubProxy.server.sendNotifications()
            $('#send-realtime-notifications').click(function () {
                zigChatHubProxy.server.sendNotifications()
            });
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
                if (text.length === 0) { alert("boş"); return; }
                // handle post
                var time = new Date();
                var message = preparePost('out', (time.getHours() + ':' + time.getMinutes()), currentRealName, currentUserAvatar, text);
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