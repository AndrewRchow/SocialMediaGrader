//Register Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('registerController', registerController);

    registerController.$inject = ['$scope', 'authService', '$window', 'toastr'];

    function registerController($scope, authService, $window, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.authService = authService;
        vm.$window = $window;
        vm.registerForm = {};
        vm.registerUser = _registerUser;
        vm.toastr = toastr;
        vm.recaptchaPayload = {
            'secret': '6LdYGSwUAAAAAKnNM1426K7_ieWToTkChQ53zeOm'
            , 'response': ""
        };
        vm.loginForm = {};

        function _registerUser() {
            //(#g-recaptcha-response) is a hidden generated response from completing the captcha
            vm.recaptchaPayload.response = (angular.element('#g-recaptcha-response').val());  
            vm.authService.recaptcha(vm.recaptchaPayload)
                .then(_recaptchaSuccess, recaptchaError);
        }
        function _recaptchaSuccess(response) {
            console.log(response);
            if (response.data) {
                vm.authService.register(vm.registerForm)
                    .then(_registerGood, _registerBad);
                return
            }
            if (!response.data) {
                vm.toastr.error('Please click on "Im not a robot"');
            }
        }
        function recaptchaError(response) {
            console.log(response);
        }
        function _registerGood(resp) {
            vm.loginForm.email = vm.registerForm.email;
            vm.loginForm.password = vm.registerForm.password;
            vm.authService.login(vm.loginForm)
                .then(_loginGood, _loginBad);
            return;
        }
        function _registerBad(err) {
            vm.toastr.error('Email is already registered', 'Error');
            grecaptcha.reset();
            return
        }
        function _loginGood(resp) {
            $window.location.href = "/Home/Confirmation";
            return;
        }
        function _loginBad(err) {
            return console.log(err);
        }   
    }
})();

//confirm PW matching verification
(function () {
    "use strict";
    angular
        .module('mainApp')
        .directive('passwordVerify', passwordVerify);
    function passwordVerify() {
        return {
            restrict: 'A', // only activate on element attribute
            require: '?ngModel', // get a hold of NgModelController
            link: function (scope, elem, attrs, ngModel) {
                if (!ngModel) return; // do nothing if no ng-model

                // watch own value and re-validate on change
                scope.$watch(attrs.ngModel, function () {
                    validate();
                });

                // observe the other value and re-validate on change
                attrs.$observe('passwordVerify', function (val) {
                    validate();
                });

                var validate = function () {
                    // values
                    var val1 = ngModel.$viewValue;
                    var val2 = attrs.passwordVerify;

                    // set validity
                    ngModel.$setValidity('passwordVerify', val1 === val2);
                };
            }
        }
    }
})();