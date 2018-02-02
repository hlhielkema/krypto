function CreateCurrencyViewModel() {
    var self = this;
    self.displayName = ko.observable();
    self.shortCode = ko.observable();    

    self.create = function () {
        if (self.displayName() !== '' && self.shortCode() !== '') {
            $.ajax({
                type: "POST",
                url: '/Currencies/CreateCurrency',
                data: {
                    displayName: self.displayName(),
                    shortCode: self.shortCode()
                },
                success: function (result) {
                    document.location = '/Currencies/Index';
                },
                error: krypto.validation.createAjaxErrorHandler(),
                dataType: 'json'
            });
        }
    }
}

var viewModel = new CreateCurrencyViewModel();

ko.applyBindings(viewModel);

// Custom submit binding because of an iphone bug
$(".form form").submit(function (e) {
    viewModel.create();
    return false;
});