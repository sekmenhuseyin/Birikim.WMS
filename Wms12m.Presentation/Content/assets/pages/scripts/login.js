var handleLogin = function () {

	$('.login-form').validate({
		errorElement: 'span', //default input error message container
		errorClass: 'help-block', // default input error message class
		focusInvalid: false, // do not focus the last invalid input
		rules: {
			Kod: {
				required: true
			},
			Sifre: {
				required: true
			},
			remember: {
				required: false
			}
		},

		messages: {
			Kod: {
				required: "Username is required."
			},
			Sifre: {
				required: "Password is required."
			}
		},

		invalidHandler: function (event, validator) { //display error alert on form submit   
			$('.alert-danger', $('.login-form')).show();
		},

		highlight: function (element) { // hightlight error inputs
			$(element)
				.closest('.form-group').addClass('has-error'); // set error class to the control group
		},

		success: function (label) {
			label.closest('.form-group').removeClass('has-error');
			label.remove();
		},

		errorPlacement: function (error, element) {
			error.insertAfter(element.closest('.input-icon'));
		},

		submitHandler: function (form) {
			form.submit(); // form validation success, call ajax form submit
		}
	});

	$('.login-form input').keypress(function (e) {
		if (e.which == 13) {
			if ($('.login-form').validate().form()) {
				$('.login-form').submit(); //form validation success, call ajax form submit
			}
			return false;
		}
	});
}
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