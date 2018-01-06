function SignInViewModel() {
    var self = this;
    self.username = ko.observable('');
    self.password = ko.observable('');
    self.result = ko.observable('');

    self.submit = function () {        
        if (self.username() !== '' && self.password() !== '') {          
            $.ajax({
                type: "POST",
                url: '/Account/SignIn',
                data: {
                    username: self.username(),
                    password: self.password()
                },
                success: function (result) {
                    if (result.Ok) {
                        document.location = '/';
                    }
                    else {
                        self.result('Sign-in failed: ' + result.Reason);
                    }
                },
                dataType: 'json'
            });
        }


    }

}

ko.applyBindings(new SignInViewModel());