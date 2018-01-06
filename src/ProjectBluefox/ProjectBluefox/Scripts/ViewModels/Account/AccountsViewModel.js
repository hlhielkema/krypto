function AccountViewModel(data) {
    var self = this;    
    self.username = ko.observable(data.Username);
    self.emailAddress = ko.observable(data.EmailAddress);
    self.role = ko.observable(data.RoleName);
    self.enabled = ko.observable(data.Enabled);
}

function CurrenciesViewModel() {
    var self = this;
    self.accounts = ko.observableArray();

    self.load = function () {
        $.ajax({
            type: "GET",
            url: '/Account/GetAccounts',
            data: {},
            success: function (result) {                
                var accounts = [];
                for (var i = 0; i < result.length; i++) {
                    accounts.push(new AccountViewModel(result[i]));
                }
                self.accounts(accounts);
            },
            dataType: 'json'
        });
    };
}

var viewModel = new CurrenciesViewModel();
ko.applyBindings(viewModel);
viewModel.load();