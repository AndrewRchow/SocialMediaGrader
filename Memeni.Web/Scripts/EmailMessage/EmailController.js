(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('emailController', emailController);

    emailController.$inject = ['$scope', 'emailMsgService', '$location'];

    function emailController($scope, emailMsgService, $location) {
        var vm = this;
        vm.$scope = $scope;
        vm.emailMsgService = emailMsgService;
        vm.$onInit = _init;
        vm.$location = $location;
        vm.id;
        vm.confirmed=false;
        vm.url = $location.absUrl();
        vm.urlCode = vm.url.substring(vm.url.lastIndexOf('?') + 1);
        vm.resendEmail = _resendEmail;
        vm.email;
        vm.emailData = {};
        vm.emailSent = false;
        vm.emailError = false;

        function _init() {
            vm.confirmed = false;          
            vm.emailMsgService.currentUser()
                .then(_currentUserGood, _currentUserBad);
                 
            return;
        }
        function _resendEmail() {
            vm.emailData.id = vm.id;
            vm.emailData.email = vm.email;
            vm.emailMsgService.resendEmail(vm.emailData)
                .then(_resendEmailGood, _resendEmailBad);
        }
        function _resendEmailGood(resp) {
            vm.emailSent = true;
        }
        function _resendEmailBad(err) {
            vm.emailError = true;
        }
        function _currentUserGood(data) {   
            if (data.data.roles.indexOf("Anon") < 0) {
                vm.confirmed = true;
            } else {                
                vm.id = data.data.id;
                vm.email = data.data.name;
                vm.emailMsgService.verifyCode(vm.id)
                    .then(_verifyGood, _verifyBad);
            }
            return; 
        }
        function _currentUserBad(err) {
            return 
        }

        function _verifyGood(data) {
            if (vm.urlCode == data.code) {
                vm.emailMsgService.confirmEmail(data.userId)
                    .then(_confirmGood, _confirmBad);
            } else {
            }
            return;
        }
        function _verifyBad(err) {
            return 
        }

        function _confirmGood(resp) {
            return vm.confirmed = true;
        }
        function _confirmBad(err) {
        }
    }
})();