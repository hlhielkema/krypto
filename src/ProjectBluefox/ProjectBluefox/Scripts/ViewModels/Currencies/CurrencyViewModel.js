function CommentViewModel(data) {
    var self = this;
    self.message = ko.observable(data.Message);
    self.vote = ko.observable(data.Vote);
    self.createdBy = ko.observable('@' + data.CreatedBy);
    self.dateCreated = ko.observable(data.FormattedDateCreated);

    self.voteLabel = ko.computed(function () {
        if (this.vote() > 0) {
            return '+' + this.vote();
        }
        else {
            return this.vote();
        }
    }, self);

    self.voteClass = ko.computed(function () {
        if (this.vote() > 0) {
            return 'positive';
        }
        else if (this.vote() < 0) {
            return 'negative';
        }
        else {
            return 'neutral';
        }
    }, self);
}

function formatNumber(value) {
    var parts = value.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

function ValueRatesViewModel(data) {
    var self = this;
    self.rank = ko.observable(data.Rank);
    self.priceUsd = ko.observable('$' + formatNumber(data.PriceUsd));
    self.priceEur = ko.observable('€' + formatNumber(data.PriceEur));
    self.percentChange1h = ko.observable(data.PercentChange1h);
    self.percentChange24h = ko.observable(data.PercentChange24h);    
    self.volumeUsd24h = ko.observable('$' + formatNumber(data.VolumeUsd24h));
    self.marketCapUsd = ko.observable('$' + formatNumber(data.MarketCapUsd));
    self.volumeEur24h = ko.observable('€' + formatNumber(data.VolumeEur24h));
    self.marketCapEur = ko.observable('€' + formatNumber(data.MarketCapEur));
    self.availableSupply = ko.observable(formatNumber(data.AvailableSupply) + ' ' + data.Symbol);
    self.totalSupply = ko.observable(formatNumber(data.TotalSupply) + ' ' + data.Symbol);


    // Format max supply
    if (data.MaxSupply !== null) {
        self.maxSupply = ko.observable(formatNumber(data.MaxSupply) + ' ' + data.Symbol);
    }
    else {
        self.maxSupply = ko.observable('-');
    }
    self.lastUpdated = ko.observable(data.FormattedLastUpdated);     

    // Format percent change 1H
    if (data.PercentChange1h !== null) {
        self.formattedPercentChange1h = ko.observable(data.PercentChange1h + '%');
        if (data.PercentChange1h > 0) {
            self.formattedPercentChange1h('+' + self.formattedPercentChange1h());
        }
    }
    else {
        self.formattedPercentChange1h('-');
    }

    // Format percent change 24H
    if (data.PercentChange24h !== null) {
        self.formattedPercentChange24h = ko.observable(data.PercentChange24h + '%');
        if (data.PercentChange24h > 0) {
            self.formattedPercentChange24h('+' + self.formattedPercentChange24h());
        }
    }
    else {
        self.formattedPercentChange24h('-');
    }

    // Percent change 1H CSS class
    self.percentChange1hClass = ko.computed(function () {
        if (this.percentChange1h() !== null) {
            if (this.percentChange1h() > 0) {
                return 'positive';
            }
            else if (this.percentChange1h() < 0) {
                return 'negative';
            }
        }
        return 'neutral';
    }, self);

    // Percent change 24H CSS class
    self.percentChange24hClass = ko.computed(function () {
        if (this.percentChange24h() !== null) {
            if (this.percentChange24h() > 0) {
                return 'positive';
            }
            else if (this.percentChange24h() < 0) {
                return 'negative';
            }
        }
        return 'neutral';
    }, self);

    // Enable users to view details on click
    self.moreVisible = ko.observable(false);
    self.showMore = function () {
        self.moreVisible(!self.moreVisible());
    }
}

function CurrencyViewModel(currencyId) {
    var self = this;
    self.id = ko.observable(currencyId);
    self.displayName = ko.observable();
    self.shortCode = ko.observable();
    self.dateCreated = ko.observable();
    self.createdBy = ko.observable();    

    // Value rates
    self.valueRates = ko.observable();

    // Comments
    self.comments = ko.observableArray();
    self.vote = ko.observable(0); 
    self.message = ko.observable(''); 

    self.voteOptions = ko.observableArray([
        {
            label: '[+1] Recommended',
            value: 1
        },
        {
            label: '[0] No vote',
            value: 0
        },
        {
            label: '[-1] NOT Recommended',
            value: -1
        }
    ]);

    self.load = function () {
        self.loadBasic();
        self.loadComments();
    }

    self.loadBasic = function () {
        // Load the basic information
        $.ajax({
            type: "GET",
            url: '/Currencies/GetCurrency/' + self.id(),
            success: function (result) {         
                self.displayName(result.DisplayName);
                self.shortCode(result.ShortCode);
                self.dateCreated(result.FormattedDateCreated);
                self.createdBy('@' + result.CreatedBy);
                if (result.ValueRates !== null) {
                    self.valueRates(new ValueRatesViewModel(result.ValueRates));
                }
            },
        });
    }

    self.loadComments = function () {
        // Load the comment
        $.ajax({
            type: "GET",
            url: '/Currencies/GetComments/' + self.id(),
            success: function (result) {
                var comments = [];
                for (var i = 0; i < result.length; i++) {
                    comments.push(new CommentViewModel(result[i]));
                }
                self.comments(comments);
            },
        });
    }

    self.sendComment = function () {
        $.ajax({
            type: "POST",
            url: '/Currencies/CreateComment',
            data: {
                currency: self.id(),
                vote: self.vote(),
                message: self.message()
            },
            success: function (result) {
                self.loadComments();
            },
            dataType: 'json'
        });
    }
}


var currencyId = $('.currency-view').data('id');
var viewModel = new CurrencyViewModel(currencyId);
ko.applyBindings(viewModel);
viewModel.load();