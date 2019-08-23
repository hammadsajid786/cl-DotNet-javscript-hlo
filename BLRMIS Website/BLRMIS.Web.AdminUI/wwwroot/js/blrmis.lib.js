var $httpClient = (function (jquery, ko, options) {
    jquery = jquery || {};
    options = options || {};
    var _get = function (url, onResult) {


        jquery.ajax({
            method: "GET",
            url: options.apiBaseUrl + url
        }).done((res) => {
            onResult(null, res);
        }).fail((err) => {

            var _err = "Service Error";
            if (err && err.responseJSON && err.responseJSON["description"]) {
                _err = err.responseJSON["description"];
            }
            //$lib.handleError(_err, 'error');
            onResult(err, null)
        });
    }
    var _post = function (url, viewModel, onResult) {

        var viewModelToJSON = (typeof viewModel === 'function') ? ko.toJSON(new viewModel()) : ko.toJSON(viewModel);
        jquery.ajax({
            method: "POST",
            url: options.apiBaseUrl + url,
            contentType: "application/json; charset=UTF-8",
            data: viewModelToJSON
        }).done((res) => {
            onResult(null, res);
        }).fail((error) => {

            onResult(error, null);
        });
    }

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
    var _putFormAsync = function (url, formData, async, onResult) {
        jquery.ajax({
            method: "PUT",
            url: options.apiBaseUrl + url,
            processData: false,
            contentType: false,
            cache: false,
            async: async,
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
        delete: _delete,
        putFormAsync: _putFormAsync

    }
});

var $rules = (function (ko) {
    function _requiredPhoneNumber() {
        return ko.observable().extend({
            required: {
                message: "Please enter valid phone number."
            },
            maxLength: 12,
        });
    }
    function _requried(message) {
        var _msg = message || "This field is required.";
        return ko.observable().extend(
            {
                required: {
                    message: _msg
                }
            });
    }
    function _CNIC() {
        return ko.observable().extend(
            {
                required: {
                    message: "Please enter valid CNIC."
                },
                maxLength: 13,
                minLength: 13
            }
        );
    }
    function _requiredEmailAddress() {

        return ko.observable().extend({
            required: {
                message: "Please enter email address."
            },
        });
    }
    return {
        requiredPhoneNumber: _requiredPhoneNumber,
        required: _requried,
        requiredCNIC: _CNIC,
        requiredEmailAddress: _requiredEmailAddress
    }
});

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
                if (prop.toLowerCase() == propertyToMatch.toString().toLowerCase()) {
                    if (typeof obj[prop] == 'function')
                        returnValue = obj[prop]();
                    found = true;
                    break;
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

    // Integer Numbers Only Generic
    ko.bindingHandlers.integerInputGeneric = {
        //all parameters
        //init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

            var lastValue = 0;
            var validValue = true;
            ko.utils.registerEventHandler(element, "keydown", function (event) {

                lastValue = parseInt(element.value);
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
                        validValue = false;
                    }
                }
            });

            ko.utils.registerEventHandler(element, "keyup", function (event) {

                if (isNaN(element.value) || element.value.trim().toLowerCase() == "")
                    element.value = 0;
                else if (validValue && lastValue == 0)
                    element.value = event.key;
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

var $lib = (function (ko, httpClient, validationRules, customBindings) {

    var _apiBaseUrl = "";
    var _httpClient;
    var _validationRules;
    var _appSettings = "";

    function setup() {

        $.ajaxSetup({
            beforeSend: function (xhr) {

                // var token = localStorage.getItem('token');
                var tokenCookie = document.cookie;
                var token = "";
                if (tokenCookie) {
                    var tokens = document.cookie.split('token=')
                    token = tokens[1].split(";")[0];
                }
                xhr.setRequestHeader('Authorization', 'bearer ' + token);
            },
            error: function (jqxhr, textStatus, errorThrown) {

                if (jqxhr.status == 401) {
                    var pathNames = window.location.pathname.split('/');
                    if (pathNames.length > 0) {
                        if (pathNames[pathNames.length - 1] != "login") {
                            var cookieName = ".AspNetCore.Cookies";
                            document.cookie = cookieName + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
                            cookieName = "token";
                            document.cookie = cookieName + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
                            localStorage.clear();
                            window.location.href = "login";
                        }
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
        if (!viewModel.errors) {
            viewModel.errors = ko.observable();
        }
        if (viewModel.errors && ko.validation) {
            viewModel.errors = ko.validation.group(viewModel);
            viewModel.hasErrors = function () {
                if (viewModel.errors().length > 0) {
                    viewModel.errors.showAllMessages();
                    return true;
                };
                return false;
            }
        }

        var hasSettings = localStorage.getItem("userSettings");
        if (hasSettings) {
            var menuItems = JSON.parse(hasSettings).Functions;
            var htmlMenu = $services.menuList(menuItems);
            for (var i = 0; i < htmlMenu.length; i++) {
                var item = htmlMenu[i];
                if (!item.SubMenu) {
                    item["SubMenu"] = [];
                }
            }
            viewModel.menuItems = ko.observableArray(htmlMenu);


        }

        var hasSettings = localStorage.getItem("userSettings");
        if (hasSettings) {
            var menuItems = JSON.parse(hasSettings).Functions;
            var htmlMenu = $services.menuList(menuItems);
            for (var i = 0; i < htmlMenu.length; i++) {
                var item = htmlMenu[i];
                if (!item.SubMenu) {
                    item["SubMenu"] = [];
                }
            }
            viewModel.menuItems = ko.observableArray(htmlMenu);


        }

        ko.applyBindings(viewModel, document.body);
        if (viewModel && viewModel.errors) {
            viewModel.errors.showAllMessages(false);
        }
        observeErrors();
        attachUIEvents();
    }

    var attachUIEvents = function () {
        //$('.grow > a').click(function (e) {
        //    e.preventDefault();
        //    $(this).parent().toggleClass('active');
        //});
    }

    var _handleError = function (err) {
        $.notify(err, "error");
        return false;
    }

    var _init = function (options) {

        if (ko) {

            if (ko.validation) {
                ko.validation.init({
                    errorMessageClass: 'error'
                });
            }
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

    var _showSpinner = function (show) {

        if (show != undefined && show != null)
            show = show;
        else
            show = true;
        show ? $(".loading").addClass("busy") : $(".loading").removeClass("busy");
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

var $util = (function () {
    var _navigateUrl = function (url) {
        window.location.href = url;
    }
    var _timeSince = function (date) {
        var seconds = Math.floor((new Date() - date) / 1000);
        var interval = Math.floor(seconds / 31536000);
        if (interval > 1) {
            return interval + " Years";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " Months";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " Days";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " Hours";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " Minutes";
        }
        return Math.floor(seconds) + " Seconds";
    }
    var _downloadFile = function (fileId) {

        //window.open(`/attachment/${fileId}`, '_blank');
        downloadURI($lib.apiBaseUrl + "/attachment/" + fileId, fileId);
    }
    var _viewFile = function (file) {
        var reader = new FileReader();
        reader.onload = function (f) {
            var win = window.open();
            win.document.write('<iframe src="' + f.target.result + '" frameborder="0" style="border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
        };
        reader.readAsDataURL(file);
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

    var _selectedFileID;
    var _event;

    var _confirmDeleteFile = function (eventName, elemID) {

        if (eventName != null) {
            $("#delete-confirmation").modal("show");

            _selectedFileID = elemID;
            _event = eventName;
        }
        //viewModel.selectedFileID(elemId);
        //funevent = event;
    }



    var _confirmBox = function (title, message, callback) {

        $("#delete-confirmation-title").text(title);
        $("#delete-confirmation-subtitle").text(message);
        $("#delete-confirmation").modal('show');
        $("#confirm-modal-btn-delete").unbind();
        $("#confirm-modal-btn-close").unbind();
        $("#confirm-modal-btn-delete").on("click", function () {
            callback(true);
            $("#delete-confirmation").modal('hide');
        });

        $("#confirm-modal-btn-close").on("click", function () {

            $("#delete-confirmation").modal('hide');
        });
    }

    return {
        navigateUrl: _navigateUrl,
        timeSince: _timeSince,
        downloadFile: _downloadFile,
        viewFile: _viewFile,
        confirmDeleteFile: _confirmDeleteFile,
        get selectedFileID() { return _selectedFileID; },
        get event() { return _event; },
        confirmBox: _confirmBox,
        formatAmount: _formatAmount

    }
})();

var $constants = (function () {

    //Regex
    const _cnicRegex = /^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$/;
    //const _phoneNumberRegex = /^(\+)?([9]{1}[2]{1})?-? ?(\()?([0]{1})?[1-9]{2,4}(\))?-? ??(\()?[1-9]{4,7}(\))?$/;
    const _phoneNumberRegex = /^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$/;
    const _passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !@("@")#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])\S{8,20}$/;
    const _alphaNumericRegex = /^[a-zA-Z0-9 ]*$/
    const _noSpaces = /^\S*$/;

    //Messages
    const _passwordPolicyMessage = "Password must contain atleast one small letter, one capital letter, one special character and one number";
    const _passwordLengthMessage = "Password must be between 8 to 20 Characters";
    const _passwordMustMatchMessage = "Password must match";
    const _alphaNumericMessage = "Special characters are not allowed";

    //Validation Lengths
    const _passwordMinLength = 8;
    const _passwordMaxLength = 20;

    //Misc
    const _pageSize = "10";
    const _resolvedStatusId = 4;

    return {
        get cnicRegex() { return _cnicRegex; },
        get phoneNumberRegex() { return _phoneNumberRegex; },
        get passwordRegex() { return _passwordRegex; },
        get pageSize() { return _pageSize; },
        get passwordPolicyMessage() { return _passwordPolicyMessage; },
        get passwordLengthMessage() { return _passwordLengthMessage; },
        get passwordMustMatchMessage() { return _passwordMustMatchMessage; },
        get passwordMinLength() { return _passwordMinLength; },
        get passwordMaxLength() { return _passwordMaxLength; },
        get alphaNumericRegex() { return _alphaNumericRegex; },
        get alphaNumericMessage() { return _alphaNumericMessage; },
        get noSpaces() { return _noSpaces; },
        get resolvedStatusId() { return _resolvedStatusId; }
    }
})();

var $toastMessages = (function () {

    var _message = "";
    var showSuccessToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "success");
    }
    var showErrorToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "error");
    }
    var showInfoToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "info");
    }
    var showWarningToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "warn");
    }

    return {
        showSuccess: function (message) { showSuccessToast(message); },
        showError: function (message) { showErrorToast(message); },
        showInfo: function (message) { showInfoToast(message); },
        showWarning: function (message) { showWarningToast(message); }
    }
})();

// common servies: 
var $services = (function () {
    var _deleteAttachment = function (attachmentId, onResult) {
        var confirm = window.confirm("Are you sure you want to delete this attachment? ");
        if (!confirm) return;
        $lib.http.delete(`/attachment/${attachmentId}`, (err, res) => {
            if (err) {
                onResult(err, false);
                $lib.handleError("Error while deleting this attachment:  " + attachmentId);
                return false;
            }
            onResult(null, true);
        })

    }
    var _complaintStatusList = function (onResult) {
        $lib.http.get(`/complaint-status`, (err, res) => {
            if (err) return;
            onResult(res);
        })
    }
    var _complaintCategoryList = function (onResult) {
        $lib.http.get('/categories/short-list', (err, res) => {
            if (err) return;
            onResult(res);
        })
    }

    var _locationList = function (onResult) {
        $lib.http.get(`/Locations`, (err, res) => {
            if (err) return;
            onResult(res);
        })
    }


    return {
        deleteAttachment: _deleteAttachment,
        complaintStatusList: _complaintStatusList,
        complaintCategoryList: _complaintCategoryList,
        locationList: _locationList
    }
}())

class MyUploadAdapter {
    constructor(loader) {
        // The file loader instance to use during the upload.
        this.loader = loader;
    }

    // Starts the upload process.
    upload() {
        return this.loader.file
            .then(file => new Promise((resolve, reject) => {
                this._initRequest();
                this._initListeners(resolve, reject, file);
                this._sendRequest(file);
            }));
    }

    // Aborts the upload process.
    abort() {
        if (this.xhr) {
            this.xhr.abort();
        }
    }

    _initRequest() {
        const xhr = this.xhr = new XMLHttpRequest();
        xhr.open('POST', $lib.apiBaseUrl + '/UploadImage/Post', true);
        xhr.responseType = 'json';
    }

    // Initializes XMLHttpRequest listeners.
    _initListeners(resolve, reject, file) {
        const xhr = this.xhr;
        const loader = this.loader;
        const genericErrorText = `Couldn't upload file: ${file.name}.`;

        xhr.addEventListener('error', () => reject(genericErrorText));
        xhr.addEventListener('abort', () => reject());
        xhr.addEventListener('load', () => {
            const response = xhr.response;

            if (!response || response.error) {
                return reject(response && response.error ? response.ErrorMessage : genericErrorText);
            }

            resolve({
                default: response.url
            });
        });
        if (xhr.upload) {
            xhr.upload.addEventListener('progress', evt => {
                if (evt.lengthComputable) {
                    loader.uploadTotal = evt.total;
                    loader.uploaded = evt.loaded;
                }
            });
        }
    }

    _sendRequest(file) {
        // Prepare the form data.
        const data = new FormData();

        data.append('upload', file);

        // Send the request.
        this.xhr.send(data);
    }
}
