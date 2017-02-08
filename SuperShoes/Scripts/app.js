var ViewModel = function () {
    var self = this;
    self.stores = ko.observableArray();
    self.error = ko.observable();
    self.articles = ko.observable();

    var storesUri = '/api/stores/';
    var articlesUri = '/api/articles/stores/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllStores() {
        ajaxHelper(storesUri, 'GET').done(function (data) {
            self.stores(data.stores);
        });
    }

    // Fetch the initial data.
    getAllStores();

    self.getArticles = function (item) {
        ajaxHelper(articlesUri + item.id, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }

    self.getAllArticles = function () {
        ajaxHelper(articlesUri, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }
};

ko.applyBindings(new ViewModel());