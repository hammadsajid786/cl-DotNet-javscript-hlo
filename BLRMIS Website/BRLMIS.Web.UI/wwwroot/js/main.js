var httpClient = (function (jquery) {    
    var _get =  function(url, onSuccess,onFail) {
        jquery.ajax({
            method: "GET",
            url: url
        }).done(onSuccess).fail(onFail);
    }
    var _post = function(url, dataJson, onSuccess, onFail) {
        jquery.ajax({
            method: "POST",
            url: url,
            data: dataJson
        }).done(onSuccess).fail(onFail);
    }
    return {
        get: _get,
        post:_post
    }
})($); 