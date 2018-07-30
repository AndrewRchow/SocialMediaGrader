//Admin User Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('usersNgIndexController', usersNgIndexController);

    usersNgIndexController.$inject = ['$scope', 'adminNgUserService', '$window'];

    function usersNgIndexController($scope, adminNgUserService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.adminNgUserService = adminNgUserService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.items = [];
        vm.confirmEmail = _confirmEmail;
        vm.makeAdmin = _makeAdmin;
        vm.lockUser = _lockUser;
        vm.unlockUser = _unlockUser;
        vm.removeAdmin = _removeAdmin;
        vm.addInfo = {};
        vm.id;
        vm.sortType = 'userId'; // set the default sort type
        vm.sortReverse = false;  // set the default sort order
        
        function _init() {
            vm.adminNgUserService.userListGet()
                .then(_userListGetGood, _userListGetBad);
            return;
        }
        function _userListGetGood(items) {
            console.log(items);
            vm.items = items;
            return;
        }
        function _userListGetBad(err) {
            return console.log(err, "user get all bad");
        }

        function _confirmEmail(id) {
            vm.id = id - 1;
            vm.adminNgUserService.userConfirmEmail(id)
                .then(_confirmEmailGood, _confirmEmailBad);
            return;
        }
        function _confirmEmailGood(resp) {
            console.log('confirm email good', resp);
            vm.items[vm.id].emailConfirmed = true;
            vm.items[vm.id].role = 'User';
            return;
        }
        function _confirmEmailBad(err) {
            console.log('confirm email bad', err);
            return;
        }

        function _makeAdmin(id) {
            vm.id = id - 1;
            vm.addInfo.id = id;
            vm.addInfo.role = 3;
            vm.adminNgUserService.userAdminAccess(id, vm.addInfo)
                .then(_makeAdminGood, _makeAdminBad);
            return;
        }
        function _makeAdminGood(resp) {
            vm.addInfo = {};
            vm.items[vm.id].role = 'Admin';
            console.log('make admin good', resp);
            return;
        }
        function _makeAdminBad(err) {
            console.log('make admin bad', err);
            return;
        }

        function _removeAdmin(id) {
            vm.id = id - 1;
            vm.addInfo.id = id;
            vm.addInfo.role = 2;
            vm.adminNgUserService.userAdminAccess(id, vm.addInfo)
                .then(_removeAdminGood, _removeAdminBad);
            return;
        }
        function _removeAdminGood(resp) {
            vm.addInfo = {};
            vm.items[vm.id].role = 'User';
            console.log('remove admin good', resp);
            return;
        }
        function _removeAdminBad(err) {
            console.log('remove admin bad', err);
            return;
        }

        function _lockUser(id) {
            vm.id = id - 1;
            vm.addInfo.id = id;
            vm.addInfo.lock = true;
            vm.adminNgUserService.userLock(id, vm.addInfo)
                .then(_userLockGood, _userLockBad);
            return;
        }
        function _userLockGood(resp) {
            vm.addInfo = {};
            vm.items[vm.id].lock = true;
            console.log('user lock good', resp);
            return;
        }
        function _userLockBad(err) {
            console.log('user lock bad', err);
            return;
        }

        function _unlockUser(id) {
            vm.id = id - 1;
            vm.addInfo.id = id;
            vm.addInfo.lock = false;
            vm.adminNgUserService.userLock(id, vm.addInfo)
                .then(_userUnlockGood, _userUnlockBad);
            return;
        }
        function _userUnlockGood(resp) {
            vm.addInfo = {};
            vm.items[vm.id].lock = false;
            console.log('user unlock good', resp);
            return;
        }
        function _userUnlockBad(err) {
            console.log('user unlock bad', err);
            return;
        }
    }
})();