var $consts = {
    COMPLAINT_SOURCE_TYPE: 5,
    PUBLIC_USER_ID: 1018
}
var $httpClient = (function (jquery, ko, options) {
    jquery = jquery || {};
    options = options || {};
    var lock = [];
    var _get = function (url, onResult) {
        //if (lock.indexOf(url).indexOf() != -1) return;
        jquery.ajax({
            method: "GET",
            url: options.apiBaseUrl + url
        }).done((res) => {

            onResult(null, res);
        }).fail((err) => {
            /*var _err = "Service Error"; 
            if (err && err.responseJSON && err.responseJSON["description"]) {
                _err = err.responseJSON["description"];
            }
            $lib.handleError(_err, 'error');*/
            onResult(err, null);
        }).always((res) => {

        });
    }
    var _post = function (url, viewModel, onResult) {
        var viewModelToJSON = (typeof viewModel === 'function') ? ko.toJSON(new viewModel()) : ko.toJSON(viewModel);
        jquery.ajax({
            method: "POST",
            url: options.apiBaseUrl + url,
            data: viewModelToJSON,
            contentType: "application/json; charset=UTF-8",
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {
            onResult(error, null);
        });
    }
    // use for posting form data with files 
    var _postForm = function (url, formData, onResult) {
        jquery.ajax({
            method: "POST",
            url: options.apiBaseUrl + url,
            processData: false,
            contentType: false,
            cache: false,
            data: formData,
            enctype: 'multipart/form-data'
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {
            onResult(error, null);
        });
    }
    var _put = function (url, viewModel, onResult) {
        var viewModelToJSON = (typeof viewModel === 'function') ? ko.toJSON(new viewModel()) : ko.toJSON(viewModel);
        jquery.ajax({
            method: "PUT",
            url: options.apiBaseUrl + url,
            contentType: "application/json; charset=UTF-8",
            data: viewModelToJSON
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {

            onResult(error, null);
        });
    }
    var _putForm = function (url, formData, onResult) {
        jquery.ajax({
            method: "PUT",
            url: options.apiBaseUrl + url,
            processData: false,
            contentType: false,
            cache: false,
            data: formData,
            enctype: 'multipart/form-data'
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {

            onResult(error, null);
        });
    }
    var _delete = function (url, onResult) {
        jquery.ajax({
            method: "DELETE",
            url: options.apiBaseUrl + url
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {
            onResult(error, null);
        });
    }
    return {
        get: _get,
        post: _post,
        postForm: _postForm,
        put: _put,
        putForm: _putForm,
        delete: _delete
    }
})
// add custom validations here 
var $rules = (function (ko) {
    function _requiredPhoneNumber(observable) {

        var _obs = observable || ko.observable();
        return _obs.extend({
            required: {
                message: "Please enter valid phone number.",
            },
            pattern: {
                message: "Please enter valid phone number.",
                params: /^\d+$/
            },
            maxLength: 11,
            minLength: 6
        });
    }
    function _requiredWithNoSpecialChars(message, observable) {
        var _obs = observable || ko.observable();
        return _obs.extend(
            {
                required: (message ? { message: message } : true),
                minLength: 3,
                pattern: {
                    message: "Please enter valid text.",
                    params: /^[a-zA-Z0-9 ]*$/
                }
            }
        );

    }
    function _requried(message, observable) {
        var _obs = observable || ko.observable();
        return _obs.extend(
            {
                required: (message ? { message: message } : true)
            });
    }
    function _CNIC(observable) {
        var _obs = observable || ko.observable();
        return _obs.extend({
            required: {
                message: "Please enter CNIC number."
            },
            maxLength: 15, pattern: {
                message: "CNIC format should be: (00000-0000000-0)",
                params: /^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$/
            }
        })
    }
    function _requiredEmailAddress(observable) {
        var _obs = observable || ko.observable();
        return _obs.extend({
            required: {
                message: "Please enter email address."
            },
            email: {
                message: "Please enter valid email address."
            }
        });
    }
    return {
        requiredPhoneNumber: _requiredPhoneNumber,
        required: _requried,
        requiredCNIC: _CNIC,
        requiredEmailAddress: _requiredEmailAddress,
        requiredWithNoSpecialChars: _requiredWithNoSpecialChars
    }
})

var $customBindings = (function (ko) {

    var found = false;
    var returnValue = "";

    function getValue(obj, propertyToMatch) {

        for (const prop in obj) {
            if (typeof obj[prop] == 'object') {
                getValue(obj[prop], propertyToMatch);
                if (found)
                    break;
            }
            else {
                if (propertyToMatch != undefined) {
                    if (prop.toLowerCase() == propertyToMatch.toString().toLowerCase()) {
                        if (typeof obj[prop] == 'function')
                            returnValue = obj[prop]();
                        found = true;
                        break;
                    }
                }
            }
        }
        return returnValue;
    }

    // Integer Numbers Only
    ko.bindingHandlers.integerInput = {
        //all parameters
        //init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

            var lastValue = 1;
            ko.utils.registerEventHandler(element, "keydown", function (event) {

                lastValue = parseInt(element.value) <= parseInt(getValue(viewModel, allBindings().propertyName)) ? element.value : lastValue;
                // Allow: backspace, delete, tab, escape, and enter
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    // Allow: Ctrl+A
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    // Allow: . ,
                    //(event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                    // Allow: home, end, left, right
                    (event.keyCode >= 35 && event.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });

            ko.utils.registerEventHandler(element, "keyup", function (event) {

                if (isNaN(element.value) || element.value.trim().toLowerCase() == "")
                    element.value = lastValue;
                else if (parseInt(element.value) == 0)
                    element.value = lastValue;
                else if (parseInt(element.value) > parseInt(getValue(viewModel, allBindings().propertyName)))
                    element.value = lastValue;
            });
        }
        //update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        //}
    };

    // Integer Numbers Only
    ko.bindingHandlers.NumberOnly = {
        //all parameters
        //init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

            // var lastValue = 1;
            ko.utils.registerEventHandler(element, "keydown", function (event) {


                // Allow: backspace, delete, tab, escape, and enter
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    // Allow: Ctrl+A
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    // Allow: . ,
                    //(event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                    // Allow: home, end, left, right
                    (event.keyCode >= 35 && event.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });
        }
        //update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        //}
    };

    ko.bindingHandlers.masked = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var mask = allBindingsAccessor().mask || {};
            $(element).mask(mask);
            //ko.utils.registerEventHandler(element, 'blur', function () {
            //    var observable = valueAccessor();
            //    observable($(element).val());
            //});
        },
        //update: function (element, valueAccessor) {
        //    var value = ko.utils.unwrapObservable(valueAccessor());
        //    $(element).val(value);
        //}
    };
});

var $toastMessages = (function () {

    var showSuccessToast = function (message) {
        $('.notifyjs-corner').empty();
        $.notify(message, "success");
    }
    var showErrorToast = function (message) {
        $('.notifyjs-corner').empty();
        $.notify(message, "error");
    }
    var showInfoToast = function (message) {
        $('.notifyjs-corner').empty();
        $.notify(message, "info");
    }
    var showWarningToast = function (message) {
        $('.notifyjs-corner').empty();
        $.notify(message, "warn");
    }

    return {
        showSuccess: function (message) { showSuccessToast(message); },
        showError: function (message) { showErrorToast(message); },
        showInfo: function (message) { showInfoToast(message); },
        showWarning: function (message) { showWarningToast(message); }
    }
})();

var $lib = (function (ko, httpClient, validationRules, customBindings) {
    var _apiBaseUrl = "";
    var _httpClient;
    var _validationRules;
    var _appSettings = "";
    function setup() {

        $.ajaxSetup({
            beforeSend: function (xhr) {

                var token = localStorage.getItem('token');
                xhr.setRequestHeader('Authorization', 'bearer ' + token);
            },
            error: function (jqxhr, textStatus, errorThrown) {

                if (jqxhr.status == 401) {
                    var pathNames = window.location.pathname.split('/');
                    if (pathNames.length > 0) {
                        //if (pathNames[pathNames.length - 1] != "login") {
                        //    document.cookie = "";
                        //    localStorage.clear();
                        //    window.location.href = "login";
                        //}
                    }
                }
            }
        });
    }
    function observeErrors() {

        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutationRecord) {

                var element = mutationRecord.target;
                if ($(element).css("display") != "none") {

                    $(element).parent().addClass("noerror-message error-message-holder");

                    if ($(element).prev()[0].nodeName.toLowerCase() != "select") {
                        $(element).append('<span class="error-message"></span>');
                        $(element).after('<span class="error-icon">!</span>');
                    }
                    else {
                        $(element).removeClass("error");
                        $(element).empty();
                    }
                }
                else {

                    $(element).parent().removeClass("noerror-message error-message-holder");
                    $(element).children().remove('span');
                    $(element).siblings().remove('span');
                }
            });
        });

        $(".error").each(function (index, element) {
            observer.observe(element, { attributes: true, attributeFilter: ['style'] });
        });
    }



    var _bindUI = function (viewModel) {

        var _appSettings = {};
        if (!viewModel) {
            console.error("view model is null or empty.");
            return;
        }
        if (viewModel.errors && ko.validation) {
            viewModel.errors = ko.validation.group(viewModel);
        }
        ko.applyBindings(viewModel, $("#wrapper").get(0));
        if (viewModel && viewModel.errors) {
            viewModel.errors.showAllMessages(false);
        }
        observeErrors();
    }
    var _handleError = function (err) {
        $.notify((err || "Internal server error."), "error");
        return false;
    }
    var _showSpinner = function (show) {
        if (show) {
            $(".loading").addClass("busy");
        } else {
            $(".loading").removeClass("busy")
        }

    }
    var _init = function (options) {
        if (ko && ko.validation) {
            ko.validation.init({
                //decorateElement: true,
                //errorElementClass: 'err',
                errorMessageClass: 'error'
            });
            if (customBindings) {
                customBindings(ko);
            }
        }
        setup();
        _apiBaseUrl = options.apiBaseUrl;
        _appSettings = JSON.parse(options.appSettings)
        _httpClient = httpClient($, ko, { apiBaseUrl: options.apiBaseUrl });
        _validationRules = validationRules(ko);
    }
    var _setPageTitle = function (pageTitle) {
        document.title = pageTitle;
    }
    return {
        init: _init,
        setPageTitle: _setPageTitle,
        bindUI: _bindUI,
        get rules() { return _validationRules; },
        get http() { return _httpClient; },
        get apiBaseUrl() { return _apiBaseUrl; },
        handleError: _handleError,
        showSpinner: _showSpinner,
        get appSettings() { return _appSettings }


    }
})(ko, $httpClient, $rules, $customBindings);

$util = (function () {
    var _navigateUrl = function (url) {
        window.location.href = url;
    }
    var _timeSince = function (date) {

        var seconds = Math.floor((new Date() - date) / 1000);

        var interval = Math.floor(seconds / 31536000);

        if (interval > 1) {
            return interval + " years";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " months";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " days";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " hours";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " minutes";
        }
        return Math.floor(seconds) + " seconds";
    }
    var _getPlainText = function (html) {

        var div = document.createElement("div");
        div.innerHTML = html;
        var text = div.textContent || div.innerText || "";
        return text;
    }

    // client side
    _viewFile = function (file) {
        var reader = new FileReader();
        reader.onload = function (f) {
            var win = window.open();
            win.document.write('<iframe src="' + f.target.result + '" frameborder="0" style="border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
        };
        reader.readAsDataURL(file);
    }



    // server side
    var _downloadFile = function (fileId) {
        //window.open(`/attachment/${fileId}`, '_blank');
        downloadURI($lib.apiBaseUrl + "/attachment/" + fileId, fileId);
    }

    function downloadURI(uri, name) {

        var link = document.createElement("a");
        link.download = name;
        link.href = uri;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    }

    _formatAmount = function formatAmount(amount, c, d, t) {
        var c = isNaN(c = Math.abs(c)) ? 2 : c,
            d = d == undefined ? "." : d,
            t = t == undefined ? "," : t,
            s = amount < 0 ? "-" : "",
            i = String(parseInt(amount = Math.abs(Number(amount) || 0).toFixed(c))),
            j = (j = i.length) > 3 ? j % 3 : 0;

        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(amount - i).toFixed(c).slice(2) : "");
    };

    return {
        navigateUrl: _navigateUrl,
        timeSince: _timeSince,
        viewFile: _viewFile,
        downloadFile: _downloadFile,
        formatAmount: _formatAmount,
        getPlainText: _getPlainText
    }
})();

// add custom components here. 
var $components = function () {
    var _fileAttachments = function () {
    }
    return {
        fileAttachments: _fileAttachments
    }
}();

