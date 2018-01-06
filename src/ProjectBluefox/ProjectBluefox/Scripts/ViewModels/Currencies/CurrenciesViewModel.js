function CurrencyViewModel(data) {
    var self = this;
    self.id = ko.observable(data.Id);
    self.displayName = ko.observable(data.DisplayName);
    self.shortCode = ko.observable(data.ShortCode);
    self.dateCreated = ko.observable(data.FormattedDateCreated);
    self.createdBy = ko.observable('@' + data.CreatedBy);
    self.url = ko.observable('/Currencies/Currency/' + self.shortCode());
    self.score = ko.observable(data.Score);
    self.totalComments = ko.observable(data.TotalComments);
    self.recentComments = ko.observable(data.RecentComments);

    self.scoreLabel = ko.computed(function () {
        if (this.score() > 0) {
            return '+' + this.score();
        }
        else {
            return this.score();
        }        
    }, self);

    self.scoreClass = ko.computed(function () {
        if (this.score() > 0) {
            return 'positive';
        }
        else if (this.score() < 0) {
            return 'negative';
        }
        else {
            return 'neutral';
        }
    }, self);
}

function CurrenciesViewModel() {
    var self = this;
    self.currencies = ko.observableArray();

    self.load = function () {
        $.ajax({
            type: "GET",
            url: '/Currencies/GetCurrencies',
            data: { },
            success: function (result) {
                console.log('result', result);                
                var currencies = [];
                for (var i = 0; i < result.length; i++) {      
                    currencies.push(new CurrencyViewModel(result[i]));
                }   
                self.currencies(currencies);
            },
            dataType: 'json'
        });
    };

}

var viewModel = new CurrenciesViewModel();
ko.applyBindings(viewModel);
viewModel.load();