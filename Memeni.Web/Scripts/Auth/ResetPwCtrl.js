//Reset Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('resetController', resetController);

    resetController.$inject = ['$scope', 'authService', '$window', '$location'];

    function resetController($scope, authService, $window, $location) {

        var vm = this;
        vm.$scope = $scope;
        vm.authService = authService;
        vm.$window = $window;
        vm.resetForm = {};
        vm.resetPassword = _resetPassword;
        vm.confirmPassword;
        vm.success = false;
        vm.error = false;
        vm.url = $location.absUrl();
        vm.urlCode = vm.url.substring(vm.url.lastIndexOf('?') + 1);

        function _resetPassword() {
            if (vm.url.length < 37 || vm.urlCode.length !== 36) {
                return _resetBad();
            } else {
                    vm.resetForm.code = vm.urlCode;
                    return vm.authService.reset(vm.resetForm)
                               .then(_resetGood, _resetBad);
            }
        }
        function _resetGood(resp) {
            vm.resetForm = {};
            vm.confirmPassword = null;
            return vm.success = true;
        }
        function _resetBad(err) {
            vm.error = true;
            vm.resetForm = {};
            return console.log('resetBad', err)
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