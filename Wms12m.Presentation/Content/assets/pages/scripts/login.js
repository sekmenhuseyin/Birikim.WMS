function ReturnPage(data) {

	if (data.data) {
		window.location.href = Link;
	} else {
		Modaldialog('Lütfen bilgilerinizi kontrol edin', 'Login Ýþlemi', 'Kapat', 'btn-warning');
	}

}
var onSuccess = function (result) {
	$.validator.unobtrusive.parse($(result));
};