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

function CurrencyViewModel(currencyId) {
    var self = this;
    self.id = ko.observable(currencyId);
    self.displayName = ko.observable();
    self.shortCode = ko.observable();
    self.dateCreated = ko.observable();
    self.createdBy = ko.observable();    

    //
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