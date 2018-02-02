function SignUpViewModel() {
    var self = this;
    self.username = ko.observable('');
    self.email = ko.observable('');
    self.password = ko.observable('');
    self.repeatPassword = ko.observable('');

    self.signUp = function () {
        if (self.username() !== '' && self.password() !== '' && self.email() !== '') {
            if (self.password() === self.repeatPassword()) {
                $.ajax({
                    type: "POST",
                    url: '/Account/SignUp',
                    data: {
                        token: $('.form').data('signup-token'),
                        username: self.username(),
                        email: self.email(),
                        password: self.password(),
                    },
                    success: function (result) {
                        document.location = '/';                     
                    },
                    error: krypto.validation.createAjaxErrorHandler(),
                    dataType: 'json'
                });
            }
            else {
                window.krypto.validation.setMessage('form', 'PasswordRepeat', 'The passwords do not match');
            }           
        }
    }
}

var viewModel = new SignUpViewModel();

ko.applyBindings(viewModel);

// Custom submit binding because of an iphone bug
$(".form form").submit(function (e) {
    viewModel.signUp();
    return false;
});