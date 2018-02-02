function SignInViewModel() {
    var self = this;
    self.username = ko.observable('');
    self.password = ko.observable('');
    
    self.signIn = function () {                
        if (self.username() !== '' && self.password() !== '') {          
            $.ajax({
                type: "POST",
                url: '/Account/SignIn',
                data: {
                    username: self.username(),
                    password: self.password()
                },
                success: function (result) {                    
                    document.location = '/';                                  
                },
                error: krypto.validation.createAjaxErrorHandler(),
                dataType: 'json'
            });
        }
    }
}

var viewModel = new SignInViewModel();

ko.applyBindings(viewModel);

// Custom submit binding because of an iphone bug
$(".form form").submit(function (e) {
    viewModel.signIn();
    return false;
});