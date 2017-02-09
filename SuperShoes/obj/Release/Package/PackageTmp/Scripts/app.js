var ViewModel = function () {
    var self = this;
    self.stores = ko.observableArray();
    self.error = ko.observable();
    self.articles = ko.observable();
    self.visibility = ko.observable();
    self.visible = ko.observable();
    self.show = ko.observable(false);
    self.loader = ko.observable(true);

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
            self.loader(false);
        });
    }

    // Fetch the initial data.
    getAllStores();

    self.getArticles = function (item) {
        self.currentStore.id = item.id;
        self.currentStore.name(item.name);
        self.currentStore.address(item.address);
        self.show(true);
        self.visible(false);
        self.visibility(false);

        self.articles(new Array());
        ajaxHelper(articlestoresUri + item.id, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }

    self.getAllArticles = function () {
        self.visible(false);
        self.show(false);
        self.hideVisibility()

        ajaxHelper(articlesUri, 'GET').done(function (data) {
            self.articles(data.articles);
        });
    }

    self.removeStore = function (item) {
        ajaxHelper(storesUri + item.id, 'DELETE').done(function () {
            getAllStores();
            self.articles(new Array());
            self.show(false);
            self.visible(false);
        });
    }

    self.removeArticle = function (item) {
        var store = item.store_id;

        ajaxHelper(articlesUri + item.id, 'DELETE').done(function () {
            if (self.currentStore.id == 0) {
                self.getAllArticles();
            }
            else {
                self.getArticles({ "id": article.StoreId });
            }
        });
    }

    self.setVisibility = function () {
        self.visibility(true);
        self.currentStore.id = 0;
        self.currentStore.name('');
        self.currentStore.address('');
        self.articles(new Array());
        self.show(false);
        self.visible(false);
    }

    self.hideVisibility = function () {
        self.visibility(false);
        self.currentStore.id = 0;
        self.currentStore.name('');
        self.currentStore.address('');
    }

    self.currentStore = {
        id: 0,
        name: ko.observable(),
        address: ko.observable()
    }

    self.editStore = function (item) {
        self.visibility(true);
        self.currentStore.id = item.id;
        self.currentStore.name(item.name);
        self.currentStore.address(item.address);
        self.articles(new Array());
        self.show(false);
        self.visible(false);
    }

    self.addStore = function (formElement) {
        var store = {
            id: self.currentStore.id,
            name: self.currentStore.name(),
            address: self.currentStore.address()
        };

        if (store.id !== 0) {
            ajaxHelper(storesUri + store.id, 'PUT', store).done(function () {
                getAllStores();
                self.hideVisibility();
            });
        }
        else{
            ajaxHelper(storesUri, 'POST', store).done(function (item) {
                getAllStores();
                self.hideVisibility();
            });
        }
    }

    self.setVisible = function () {
        self.visible(true);
        self.currentArticle.id = 0;
        self.currentArticle.name('');
        self.currentArticle.description('');
        self.currentArticle.price('');
        self.currentArticle.total_in_shelf('');
        self.currentArticle.total_in_vault('');
        self.currentArticle.StoreId = self.currentStore.id;
        self.visibility(false);
    }

    self.hideVisible = function () {
        self.visible(false);
        self.currentArticle.id = 0;
        self.currentArticle.name('');
        self.currentArticle.description('');
        self.currentArticle.price('');
        self.currentArticle.total_in_shelf('');
        self.currentArticle.total_in_vault('');
        self.currentArticle.StoreId = self.currentStore.id;
    }

    self.currentArticle = {
        id: 0,
        name: ko.observable(),
        description: ko.observable(),
        price: ko.observable(),
        total_in_shelf: ko.observable(),
        total_in_vault: ko.observable(),
        StoreId: self.currentStore.id
    }

    self.editArticle = function (item) {
        self.visible(true);
        self.currentArticle.id = item.id;
        self.currentArticle.name(item.name);
        self.currentArticle.description(item.description);
        self.currentArticle.price(item.price);
        self.currentArticle.total_in_shelf(item.total_in_shelf);
        self.currentArticle.total_in_vault(item.total_in_vault);
        self.currentArticle.StoreId = item.store_id;
    }

    self.addArticle= function (formElement) {
        var article = {
            id: self.currentArticle.id,
            name: self.currentArticle.name(),
            description: self.currentArticle.description(),
            price: parseFloat(self.currentArticle.price()),
            total_in_shelf: parseFloat(self.currentArticle.total_in_shelf()),
            total_in_vault: parseFloat(self.currentArticle.total_in_vault()),
            StoreId: self.currentArticle.StoreId
        };

        if (article.id !== 0) {
            ajaxHelper(articlesUri + article.id, 'PUT', article).done(function () {
                if (self.currentStore.id == 0) {
                    self.getAllArticles();
                }
                else {
                    self.getArticles({ "id": article.StoreId });
                }
                self.hideVisible();
            });
        }
        else {
            ajaxHelper(articlesUri, 'POST', article).done(function (item) {
                if (self.currentStore.id == 0) {
                    self.getAllArticles();
                }
                else{
                    self.getArticles({ "id": article.StoreId });
                }
                self.hideVisible();
            });
        }
    }
};

ko.applyBindings(new ViewModel());