$(document).ready(function () {
    $lib.setPageTitle("Complaints");
    // complaint view model.
    var viewModel = {
        PhoneNumber: $lib.rules.requiredPhoneNumber(),
        Name: $lib.rules.requiredWithNoSpecialChars("Please enter name.").extend({ maxLength: 30 }),
        EmailAddress: $lib.rules.requiredEmailAddress(),
        Cnic: $lib.rules.requiredCNIC(),
        ComplaintTitle: ko.observable().extend({
            required: {
                message: "Please enter title.",
            },
            maxLength: 100
        }),
        ComplaintDescription: ko.observable(),
        ComplaintTypes: ko.observable(),
        DistrictList: ko.observable(),
        displayVerification: ko.observable(false),
        VerificationCode: ko.observable(),
        complaintSubmitted: ko.observable(false),
        SelectedDistrict: $lib.rules.required("Please select district.", ko.observable("")),
        SelectedComplaintType: $lib.rules.required("Please select neture of complaints.", ko.observable("")),
        ComplaintCode: ko.observable(),
        ComplaintSearchValue: ko.observable(),
        errors: ko.observable(),
        selectedFiles: ko.observableArray(),
        TimeElapse: ko.observable("00:20"),
        SelectedSearchType: ko.observable("CN"),
        VerificationCodeError: ko.observable(""),
        ComplaintSearchError: ko.observable(""),
        SelectedOpinion: ko.observable(),
        EnableResendLink: ko.observable(false),
        DisableResentLink: ko.observable(""),



        SearchFieldPlaceHolder: ko.pureComputed(function () {
            if (viewModel.SelectedSearchType() == "PH") return "Enter Phone Number";
            if (viewModel.SelectedSearchType() == "CN") return "Enter Complaint Number";
            if (viewModel.SelectedSearchType() == "CNIC") return "Enter CNIC Number";
            return "Enter Complaint Number";
        }),
        hasFiles: function () {
            return viewModel.selectedFiles().length > 0;
        }
    }
    // send verification token
    viewModel.onComplaintVerification = function () {
        SendVerificationCode();
        EnableResendButton();
        //if (viewModel.errors().length > 0) {
        //    viewModel.errors.showAllMessages();
        //    return;
        //};
        //var payload = {
        //    PhoneNumber: viewModel.PhoneNumber,
        //    EmailAddress: viewModel.EmailAddress
        //}
        //viewModel.displayVerification(true);
        //$lib.http.post('/verification-token', payload, (err, res) => {
        //    if (err) return $lib.handleError(err);
        //    console.log(res);
        //})
    }

    // Verification Code Submit. 
    // 1. verify, verifiction code validity.  
    viewModel.onVerificationCodeSubmit = function () {

        var verificationCode = viewModel.VerificationCode();
        if (!verificationCode || verificationCode == "") {
            viewModel.VerificationCodeError("Please enter verification code.");
            $("#verificationField").addClass("error-message-holder");
            return;
        }
        var complaintSearchValue = viewModel.ComplaintSearchValue();
        // if user comes from search. 
        if (complaintSearchValue && verificationCode) {
            searchComplaintCode(verificationCode, complaintSearchValue);
            return;
        }
        // if user comes from submit. 
        complaintSubmit();
    }


    // Resend Verification Code Submit.     
    var resendAttempt = 0;
    viewModel.onResendVerificationCode = function () {

        if (viewModel.DisableResentLink() == "") {
            if (resendAttempt < 3) {


                viewModel.EnableResendLink(false);

                EnableResendButton();
                SendVerificationCode();
                resendAttempt++;

            }
            else {
                //viewModel.EnableResendLink(false);
                //  $toastMessages.showInfo("You have attempted 3 resends.");
                viewModel.DisableResentLink("disabled");
            }
        }


        /* if (viewModel.errors().length > 0) {
             viewModel.errors.showAllMessages();
             return;
         };
         var payload = {
             PhoneNumber: viewModel.PhoneNumber,
             EmailAddress: viewModel.EmailAddress
         }
         viewModel.displayVerification(true);
         $lib.http.post('/verification-token', payload, (err, res) => {
             if (err) return $lib.handleError(err);
             console.log(res);
         })*/
    }



    var complaintSubmit = function () {
        var payload = {
            CitizenPhoneNumber: viewModel.PhoneNumber(),
            CitizenName: viewModel.Name(),
            CitizenEmailAddress: viewModel.EmailAddress(),
            CitizenCnic: viewModel.Cnic(),
            ComplaintTitle: viewModel.ComplaintTitle(),
            ComplaintDescription: viewModel.ComplaintDescription(),
            ComplaintCategoryId: viewModel.SelectedComplaintType(),
            LocationId: viewModel.SelectedDistrict(),
            ComplaintStatusId: "1",
            VerificationCode: viewModel.VerificationCode()
        }
        var formData = new FormData();
        var selectedFiles = viewModel.selectedFiles();
        for (var i = 0; i < selectedFiles.length; i++) {
            formData.append('Files', selectedFiles[i]);
        }
        for (var p in payload) {
            formData.append(p, payload[p]);
        }
        $lib.showSpinner(true);
        $lib.http.postForm('/complaint', formData, (err, res) => {
            $lib.showSpinner(false);

            if (err) {
                if (err.responseText == "INVALID_VERIFICATION_CODE") {
                    viewModel.VerificationCodeError("Invalid verification code.");
                    return;
                }
            };
            viewModel.complaintSubmitted(true);

            viewModel.ComplaintCode(res.ComplaintCode);
            console.log(res);

        });
    }
    viewModel.onGoBackFromVerification = function () {
        viewModel.displayVerification(false);
    }

    // Complaint Search: Step 1
    // 1. Check registered complaint code. 
    // 2. Send verification token on phone number and email address againts registered complaint code.
    viewModel.onComplaintSearchVerificationToken = function () {

        const complaintCode = viewModel.ComplaintSearchValue();

        viewModel.displayVerification(true);
        var payload = {
            ComplaintCode: complaintCode
        }
        $lib.http.post(`/complaint/search/${complaintCode}`, payload, (err, res) => {
            if (err) return $lib.handleError(err);
            viewModel.CitizenEmailAddress(res.EmailAddress);
            viewModel.CitizenPhoneNumber(res.PhoneNumber);
            viewModel.VerificationCode(res.VerificationCode);
            console.log(res);
        })
    }
    viewModel.onComplaintSearch = function () {


        sessionStorage.setItem("complaintSerach", true);
        var searchBy = viewModel.SelectedSearchType();
        var serachVal = viewModel.ComplaintSearchValue();
        if (!serachVal || serachVal == "") {
            $("#complaintSearchField").addClass("error-message-holder");
            viewModel.ComplaintSearchError(viewModel.SearchFieldPlaceHolder());
            return;
        }

        if (!serachVal) return;
        if (searchBy == "CN") {
            $util.navigateUrl(`complaint/${serachVal}`);
        } else {
            $util.navigateUrl(`complaints/${searchBy}/${serachVal}`);
        }

        EnableResendOnComplaintSearch();
    }

    function EnableResendButton() {

        var sec = 20;
        var timeLeft = 0;
        var timer = setInterval(function () {
            --sec;
            if (sec < 10) timeLeft = "0" + sec; else timeLeft = sec;
            viewModel.TimeElapse("00:" + timeLeft);
            viewModel.DisableResentLink("disabled");
            if (sec == 0) {
                clearInterval(timer);
                viewModel.EnableResendLink(true);
                viewModel.DisableResentLink("");
                //  $("#divTimer").hide();
                //   $("#divResendLink").show();

            }
        }, 1000);


    }

    function EnableResendOnComplaintSearch() {

        var sec = 20;
        var timeLeft = 0;
        var timerSearch = setInterval(function () {
            --sec;
            if (sec < 10) timeLeft = "0" + sec; else timeLeft = sec;
            viewModel.TimeElapse("00:" + timeLeft);
            viewModel.DisableResentLink("disabled");
            if (sec == 0) {
                clearInterval(timerSearch);
                viewModel.EnableResendLink(true);
                viewModel.DisableResentLink("");
                //  $("#divTimer").hide();
                //   $("#divResendLink").show();

            }
        }, 1000);

        viewModel.DisableResentLink("");
    }



    function hasExtension(fileName, exts) {
        return (new RegExp('(' + exts.join('|').replace(/\./g, '\\.') + ')$')).test(fileName);
    }

    function SendVerificationCode() {
        if (viewModel.errors().length > 0) {
            viewModel.errors.showAllMessages();
            return;
        };
        var payload = {
            PhoneNumber: viewModel.PhoneNumber,
            EmailAddress: viewModel.EmailAddress
        }
        viewModel.displayVerification(true);
        $lib.http.post('/verification-token', payload, (err, res) => {
            if (err) return $lib.handleError(err);
            console.log(res);
        })
    }

    viewModel.onFileSelect = function (elem, event) {
        const target = event.target;
        for (var i = 0; i < target.files.length; i++) {
            if (!hasExtension(target.files[i].name, [".jpg", ".png", ".pdf"])) {
                $lib.handleError("Only JPG,PNG and PDF files are allowed.");
                return;
            }
        }
        if ((viewModel.selectedFiles().length + target.files.length) > 6) {
            $lib.handleError("Maximum 6 files are allowed.");
            return;
        }
        for (var i = 0; i < target.files.length; i++) {
            var file = target.files[i];
            viewModel.selectedFiles.push(file);
        }
    }
    viewModel.deleteSelectedFile = function (elemId, event) {
        var index = viewModel.selectedFiles().findIndex(i => i.name == event.name);
        var arr = viewModel.selectedFiles();
        viewModel.selectedFiles().splice(index, 1);

        $("#" + elemId).remove();

    }
    viewModel.onComplaintUpdate = function () {
        var userOpinion = viewModel.SelectedOpinion();
    }
    viewModel.viewSelectedFile = function (elemId, file) {
        var reader = new FileReader();
        reader.onload = function (f) {
            var win = window.open();
            win.document.write('<iframe src="' + f.target.result + '" frameborder="0" style="border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
        };
        reader.readAsDataURL(file);
    }
    $lib.http.get('/categories/short-list', (err, res) => {
        viewModel.ComplaintTypes(res);
    })
    $lib.http.get('/locations', (err, res) => {
        viewModel.DistrictList(res);
    })
    viewModel.OnSearchTypeSelected = function (event) {
        return true;
    }
    viewModel.errors = ko.validation.group(viewModel);
    $lib.bindUI(viewModel);
}())
