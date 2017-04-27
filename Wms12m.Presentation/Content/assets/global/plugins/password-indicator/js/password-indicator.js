$.fn.passwordStrength = function (options) {
    return this.each(function () {
        var that = this; that.opts = {};
        that.opts = $.extend({}, $.fn.passwordStrength.defaults, options);

        that.div = $(that.opts.targetDiv);
        that.defaultClass = that.div.attr('class');

        that.percents = (that.opts.classes.length) ? 100 / that.opts.classes.length : 100;

        v = $(this)
       .keyup(function () {
           if (typeof el == "undefined")
               this.el = $(this);
           var s = getPasswordStrength(this.value);
           var p = this.percents;
           var t = Math.floor(s / p);

           if (100 <= s)
               t = this.opts.classes.length - 1;

           this.div
               .removeAttr('class')
               .addClass(this.defaultClass)
               .addClass(this.opts.classes[t]);

       })
       .after('<a href="#">' + that.opts.generatePass + '</a>')
       .next()
       .click(function () {
           $(this).prev().val(randomPassword()).trigger('keyup');
           return false;
       });
    });

    function getPasswordStrength(H) {
        var bannedPasswords = [
            '11111',
            '111111',
            '1111111',
            '12345',
            '123456',
            '1234567',
            '12345678',
            '123456789',
            '654321',
            '1qaz2wsx',
            'password',
            'iloveyou',
            'princess',
            'football',
            'baseball',
            'rockyou',
            'abc123',
            'nicole',
            'daniel',
            'babygirl',
            'monkey',
            'master',
            'admin',
            'login',
            'jessica',
            'lovely',
            'michael',
            'ashley',
            'winter',
            'summer',
            'qwerty',
            'qwertyuiop',
            'sifre',
            'welcome',
            'welcome1',
            'welcome2',
            'welcome01',
            'passw0rd',
            'password1',
            'password2',
            'password3',
            'password4',
            'password6',
            'password7',
            'password8',
            'password9',
            'password01',
            'password123'
        ];
        var D = (H.length);
        if (D > 6) {
            D = 5
        }
        var F = H.replace(/[0-9]/g, "");
        var G = (H.length - F.length);
        if (G > 3) { G = 3 }
        var A = H.replace(/\W/g, "");
        var C = (H.length - A.length);
        if (C > 3) { C = 3 }
        var B = H.replace(/[A-Z]/g, "");
        var I = (H.length - B.length);
        if (I > 3) { I = 3 }
        var E = ((D * 10) - 20) + (G * 10) + (C * 15) + (I * 10);
        if (bannedPasswords.indexOf(H.toLowerCase()) !== -1) { E = 0; }
        //last check and return
        if (E < 0) { E = 0 }
        if (E > 100) { E = 100 }
        return E
    }

    function randomPassword() {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$_+";
        var size = 10;
        var i = 1;
        var ret = ""
        while (i <= size) {
            $max = chars.length - 1;
            $num = Math.floor(Math.random() * $max);
            $temp = chars.substr($num, 1);
            ret += $temp;
            i++;
        }
        return ret;
    }

};

$.fn.passwordStrength.defaults = {
    classes: Array('is10', 'is20', 'is30', 'is40', 'is50', 'is60', 'is70', 'is80', 'is90', 'is100'),
    targetDiv: '#passwordStrengthDiv',
    generatePass: "Generate Password",
    cache: {},
}

