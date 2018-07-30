(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('contactController', contactController);

    contactController.$inject = ["$scope", "genericService", "toastr"];

    function contactController($scope, genericService, toastr) {
        var vm = this;
        vm.submit = _submit;
        vm.items = {
            "ToEmail": "admin@memeni.dev",
            "ToName": "Admin"
        };
        vm.genericService = genericService;
        vm.recaptchaPayload = {
            'secret': '6LdYGSwUAAAAAKnNM1426K7_ieWToTkChQ53zeOm'
            , 'response': ""
        };

        function _submit(data) {
            console.log("send button clicked");
            vm.recaptchaPayload.response = (angular.element('#g-recaptcha-response').val());
            vm.genericService.post("/api/auth/recaptcha", vm.recaptchaPayload)
                .then(_recaptchaSuccess, recaptchaError);
        }

        function _recaptchaSuccess(response) {
            console.log(response);
            if (response.data) {
                vm.genericService.post("/api/email/contact/", vm.items)
                    .then(_onSgSuccess)
                    .catch(_onSgFailed);
                return
            }
            if (!response.data) {
                alert('Please click on "Im not a robot"');
            }
        }

        function recaptchaError(response) {
            console.log(response);
        }

        function _onSgSuccess(res) {
            console.log("Sent to Send Grid", res);
            vm.genericService.post("/api/email/", vm.items).then(_onDbSuccess);
        }

        function _onSgFailed(err) {
            console.log("Not sent to Send Grid", err);
            toastr.error("Unsuccessful");
        }

        function _onDbSuccess(data) {
            console.log("Sent to Database", data);
            toastr.success("Sent Successfully!");
            window.location.href = "/home/contact/";
        }
    }
})();