//Login Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('loginController', loginController);

    loginController.$inject = ['$scope', 'authService', '$window'];

    function loginController($scope, authService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.authService = authService;
        vm.$window = $window;
        vm.loginForm = {};
        vm.loginUser = _loginUser;
        vm.showBadPwAlert = false;

        function _loginUser() {
            vm.authService.login(vm.loginForm)
                .then(_loginGood, _loginBad);
            return;
        }      
        function _loginGood(resp) {
            $window.location.href = "/Home/Loading";
            console.log(resp, 'login good');
            return;
        }
        function _loginBad(err) {
            console.log(err);
            vm.showBadPwAlert = true;
            return;
        }   
    }
})();