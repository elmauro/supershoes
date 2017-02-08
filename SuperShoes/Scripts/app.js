var ViewModel = function () {
    var self = this;
    self.stores = ko.observableArray();
    self.error = ko.observable();
    self.articles = ko.observable();
    self.visibility = ko.observable();

    var storesUri = '/api/stores/';
    var articlesUri = '/api/articles/';
    var articlestoresUri = '/api/articles/stores/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Authorization": "Basic bXlfdXNlcjpteV9wYXNzd29yZA=="
            },
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
        ajaxHelper(articlestoresUri + item.id, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }

    self.getAllArticles = function () {
        ajaxHelper(articlesUri, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }

    self.removeStore = function (item) {
        ajaxHelper(storesUri + item.id, 'DELETE').done(function () {
            getAllStores();
        });
    }

    self.removeArticle = function (item) {
        var store = item.store_id;

        ajaxHelper(articlesUri + item.id, 'DELETE').done(function () {
            self.getArticles({ "id": store });
        });
    }

    self.setVisible = function () {
        self.visibility(true);
    }
};

ko.applyBindings(new ViewModel());