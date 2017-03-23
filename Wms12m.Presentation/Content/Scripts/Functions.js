function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
};
function formatDate(date) {
    var monthNames = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();
    return day + ' ' + monthNames[monthIndex] + ' ' + year;
};
function fromOADate(oadate) {
    var date = new Date(((oadate - 25569) * 86400000));
    var tz = date.getTimezoneOffset();
    return formatDate(new Date(((oadate - 25569 + (tz / (60 * 24))) * 86400000)));
};
function toOADate(date) {
    var timezoneOffset = date.getTimezoneOffset() / (60 * 24);
    var msDateObj = (date.getTime() / 86400000) + (25569 - timezoneOffset);
    return msDateObj;
};
String.prototype.replaceAll = function (target, replacement) {
    return this.split(target).join(replacement);
};
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
