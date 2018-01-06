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
                dataType: 'json'
            });
        }
    }
}

ko.applyBindings(new CreateCurrencyViewModel());