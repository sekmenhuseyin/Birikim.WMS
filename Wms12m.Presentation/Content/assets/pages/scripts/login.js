function ReturnPage(data) {

	if (data.data) {
		window.location.href = Link;
	} else {
		Modaldialog('L�tfen bilgilerinizi kontrol edin', 'Login ��lemi', 'Kapat', 'btn-warning');
	}

}
var onSuccess = function (result) {
	$.validator.unobtrusive.parse($(result));
};