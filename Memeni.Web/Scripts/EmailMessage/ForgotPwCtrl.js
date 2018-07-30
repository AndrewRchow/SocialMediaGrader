//Forgot Password Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('forgotPwController', forgotPwController);

    forgotPwController.$inject = ['$scope', 'emailMsgService', '$window', 'toastr'];

    function forgotPwController($scope, emailMsgService, $window, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.emailMsgService = emailMsgService;
        vm.$window = $window;
        vm.index;
        vm.forgotPw = _forgotPw;
        vm.forgotForm = {};
        vm.success = false;
        vm.error = false;
        vm.toastr = toastr;

        function _forgotPw() {
            return vm.emailMsgService.forgotPassword(vm.forgotForm)
                .then(_forgotGood, _forgotBad);
        }
        function _forgotGood(resp) {
            vm.forgotForm = {};
            vm.toastr.info('Please check your inbox.', 'Request is being evaluated.');
            return;
        }
        function _forgotBad(err) {
            vm.toastr.error('Please try this functionality at a later time.', 'Error!');
            return;
        }

    }
})();