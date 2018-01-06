function CreateCurrencyViewModel() {
    var self = this;
    self.username = ko.observable();
    self.emailAddress = ko.observable();
    self.password = ko.observable();

    self.create = function () {
        if (self.username() !== '' && self.emailAddress() !== '' && self.password() !== '') {
            $.ajax({
                type: "POST",
                url: '/Account/CreateAccount',
                data: {
                    username: self.username(),
                    emailAddress: self.emailAddress(),
                    password: self.password(),
                },
                success: function (result) {
                    document.location = '/Account/Accounts';
                },
                dataType: 'json'
            });
        }
    }
}

ko.applyBindings(new CreateCurrencyViewModel());