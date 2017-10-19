
/**
Core script to handle the entire theme and core functions
**/
var QuickSidebar = function () {

    // Handles quick sidebar toggler
    var handleQuickSidebarToggler = function () {
        // quick sidebar toggler
        $('.dropdown-quick-sidebar-toggler a, .page-quick-sidebar-toggler, .quick-sidebar-toggler').click(function (e) {
            $('body').toggleClass('page-quick-sidebar-open'); 
        });
        $('#header_chat_bar').click(function (e) {
            $('body').toggleClass('page-quick-sidebar-open');
        });
    };

    // Handles quick sidebar chats
    var handleQuickSidebarChat = function () {
        var wrapper = $('.page-quick-sidebar-wrapper');
        var wrapperChat = wrapper.find('.page-quick-sidebar-chat');

        var initChatSlimScroll = function () {
            var chatUsers = wrapper.find('.page-quick-sidebar-chat-users');
            var chatUsersHeight;

            chatUsersHeight = wrapper.height() - wrapper.find('.nav-tabs').outerHeight(true);

            // chat user list 
            App.destroySlimScroll(chatUsers);
            chatUsers.attr("data-height", chatUsersHeight);
            App.initSlimScroll(chatUsers);

            var chatMessages = wrapperChat.find('.page-quick-sidebar-chat-user-messages');
            var chatMessagesHeight = chatUsersHeight - wrapperChat.find('.page-quick-sidebar-chat-user-form').outerHeight(true);
            chatMessagesHeight = chatMessagesHeight - wrapperChat.find('.page-quick-sidebar-nav').outerHeight(true);

            // user chat messages 
            App.destroySlimScroll(chatMessages);
            chatMessages.attr("data-height", chatMessagesHeight);
            App.initSlimScroll(chatMessages);
            //scroll and focus
            var chatContainer = wrapperChat.find(".page-quick-sidebar-chat-user-messages");
            chatContainer.slimScroll({ scrollTo: '1000000px' });
            var input = wrapperChat.find('.page-quick-sidebar-chat-user-form .form-control');
            input.focus();
        };

        App.addResizeHandler(initChatSlimScroll); // reinitialize on window resize

        //kullanýcýya týklayýnca sohbet penceresi açýlýyor
        wrapper.find('.page-quick-sidebar-chat-users .media-list > .media').click(function () {
            SendMessageTo = $(this).find('input').val();
            PartialViewClass('/Home/UsersChat', 'page-quick-sidebar-chat-user-messages', JSON.stringify({ ID: SendMessageTo }));
            wrapperChat.addClass("page-quick-sidebar-content-item-shown");
            setTimeout(initChatSlimScroll, 500);
        });

        //sohbette geriye basýnca kullanýcýlar geliyor
        wrapper.find('.page-quick-sidebar-chat-user .page-quick-sidebar-back-to-list').click(function () {
            SendMessageTo = "x";
            wrapperChat.removeClass("page-quick-sidebar-content-item-shown");
        });
    };

    return {

        init: function () {
            //layout handlers
            handleQuickSidebarToggler(); // handles quick sidebar's toggler
            handleQuickSidebarChat(); // handles quick sidebar's chats
        }
    };

}();

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {    
       QuickSidebar.init(); // init metronic core componets
    });
}