function ChangePasswordViewModel() {
    var self = this;
    self.oldPassword = ko.observable('');
    self.newPassword = ko.observable('');
    
    self.update = function () {
        if (self.oldPassword() !== '' && self.newPassword() !== '') {
            $.ajax({
                type: "POST",
                url: '/Account/ChangePassword',
                data: {
                    oldPassword: self.oldPassword(),
                    newPassword: self.newPassword()
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

var viewModel = new ChangePasswordViewModel();

ko.applyBindings(viewModel);

// Custom submit binding because of an iphone bug
$(".form form").submit(function (e) {
    viewModel.update();
    return false;
});