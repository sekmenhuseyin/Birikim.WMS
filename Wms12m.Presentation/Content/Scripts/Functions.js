// Stringe karakter eklemek için
String.prototype.addAt = function (index, character) {
    return this.substr(0, index) + character + this.substr(index + character.length - 1);
}
// Sayılara ondalık binlik ayraçları eklemek için
// Sonrasında ondalık miktarıda parametreden gelecek şekilde ayarlanabilir
function ondalikBinlik(Val) {
    if (Val == null || Val == undefined || Val == 0 ){
        return 0;
    }
    else if (Val.toString().indexOf(",") < 1) {
        var b = new Array();
        var decVal = Number(Val).toFixed(2).replace(".", ",");
        var a = decVal.split(",")[0].length;
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
//hepsini değiştir
function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
};
//www.developer.com/net/dealing-with-json-dates-in-asp.net-mvc.html
function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getDate() + "/" + dt.getMonth() + 1) + "/" + dt.getFullYear();
}
//is numeric
function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
//GUID üretir
function Guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}
//türçe tarih formatına çevirir
function formatDate(date) {
    if (date == "" || date == null) return "";
    var monthNames = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();
    return day + ' ' + monthNames[monthIndex] + ' ' + year;
};
//json tipindeki tarihi çevirir
function formatDateFromJson(date) {
    return formatDate(new Date(parseInt(date.substr(6))));
};
//oadate to tarih
function fromOADate(oadate) {
    var date = new Date(((oadate - 25569) * 86400000));
    var tz = date.getTimezoneOffset();
    return formatDate(new Date(((oadate - 25569 + (tz / (60 * 24))) * 86400000)));
};
//tarih kutusundaki tarihi oadate yapar
function toOADateFromString(date) {
    var g = date.substr(0, 2);
    var m = date.substr(3, 2) - 1;
    var y = date.substr(6, 4);
    return Math.ceil((new Date(Date.UTC(y, m, g)) - new Date(Date.UTC(1899, 11, 30))) / (24 * 60 * 60 * 1000));
};
//oadate yapar
function toOADate(date) {
    var timezoneOffset = date.getTimezoneOffset() / (60 * 24);
    var msDateObj = (date.getTime() / 86400000) + (25569 - timezoneOffset);
    return msDateObj;
};
//yine replace all
String.prototype.replaceAll = function (target, replacement) {
    return this.split(target).join(replacement);
};
//saat formatı
Number.prototype.toHHMMSS = function () {
    var sec_num = parseInt(this, 10); // don't forget the second param
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);
    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    return hours + ':' + minutes + ':' + seconds;
}
//para ve miktar formatı
Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "," : d,
        t = t == undefined ? "." : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};
//create a cookie with javascript
function setCookie(c_name, value) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + 7);
    var c_value = escape(value) + "; expires=" + exdate.toUTCString() + "; path=/";
    document.cookie = c_name + "=" + c_value;
}
//get value of a cookie with javascript
function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
    return "";
}
//get selected option text
function getSelectedText(elementId) {
    var elt = document.getElementById(elementId);
    if (elt.selectedIndex == -1)
        return null;
    return elt.options[elt.selectedIndex].text;
}
//change url
function changeUrl(url) {
    window.location.href = url;
}
//check if valid url
function ValidURL(url) {
    var reurl = /^(http[s]?:\/\/){0,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}/;
    return reurl.test(url);
}