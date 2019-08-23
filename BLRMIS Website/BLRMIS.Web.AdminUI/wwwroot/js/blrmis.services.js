var $services = (function (httpClient) {
    var apiBaseUrl = "";
    if (!httpClient) {
        console.error("$httpClient not found!");
    }
    var _init = function (options) {
        apiBaseUrl = options.apiBaseUrl;
    }
    var newsService = {
        getAllNews: function (onSuccess, onFail) {
            httpClient.get(apiBaseUrl + 'api/news', onSuccess, onFail);
        }
    }
    return {
        init: _init,
        newsService: newsService
    }
})($httpClient($));