function LinkViewModel(data) {
    var self = this;
    self.title = ko.observable(data.Title);
    self.url = ko.observable(data.Url);
    self.createdBy = ko.observable(data.CreatedBy);
    self.dateCreated = ko.observable(data.FormattedDateCreated);
}

function CategoryViewModel(data) {
    var self = this;
    self.title = ko.observable(data.Title);
    self.createdBy = ko.observable(data.CreatedBy);
    self.dateCreated = ko.observable(data.FormattedDateCreated);

    var items = [];
    for (var i = 0; i < data.Items.length; i++) {
        items.push(new LinkViewModel(data.Items[i]));
    }

    self.items = ko.observableArray(items);
}

function LinksViewModel() {
    var self = this;
    self.categories = ko.observableArray();

    self.load = function () {
        $.ajax({
            type: "GET",
            url: '/Links/GetLinks',
            data: {},
            success: function (result) {                
                var categories = [];
                for (var i = 0; i < result.length; i++) {
                    categories.push(new CategoryViewModel(result[i]));
                }
                self.categories(categories);
            },
            dataType: 'json'
        });
    };
}

var viewModel = new LinksViewModel();
ko.applyBindings(viewModel);
viewModel.load();