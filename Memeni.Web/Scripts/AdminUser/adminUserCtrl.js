//Admin User Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('usersIndexController', usersIndexController);

    usersIndexController.$inject = ['$scope', 'adminUserService', '$window'];

    function usersIndexController($scope, adminUserService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.adminUserService = adminUserService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.config = [{
            "displayLength": "10",
            "displayStart": "0",
            "sortCol": "0",
            "sortDir": "asc",
            "search": ""
        }];
        vm.item = [];
        vm.searchTerm = _searchTerm;
        vm.displayPage = _displayPage;
        vm.confirmEmail = _confirmEmail;
        vm.makeAdmin = _makeAdmin;
        vm.lockUser = _lockUser;
        vm.unlockUser = _unlockUser;
        vm.removeAdmin = _removeAdmin;
        vm.addInfo = {};
        vm.id;
        vm.index;
        vm.sortOrder = _sortOrder;
        vm.pagination = _pagination;
        vm.page = 1;
        vm.showingFirst;
        vm.showingLast;
        vm.resetFilter = _resetFilter;
        
        function _init() {
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            return;
        }
        function _userGridPostGood(item) {
            console.log(item);
            vm.item = item;       
            _showing();
            return;
        }
        function _userGridPostBad(err) {
            return console.log(err, "user grid post bad");
        }

        function _searchTerm(text) {
            vm.config[0].search = text;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            return;
        }
        function _resetFilter() {
            vm.config[0].search = "";
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            return;
        }
        function _displayPage(number) {
            vm.config[0].displayLength = number;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            return;
        }
        function _sortOrder(col) {
            if (vm.config[0].sortCol == col) {
                if (vm.config[0].sortDir == 'asc') {
                    vm.config[0].sortDir = 'desc';
                } else {
                    vm.config[0].sortDir = 'asc';
                }
                return vm.adminUserService.userGridPost(vm.config[0])
                    .then(_userGridPostGood, _userGridPostBad);
            }
            vm.config[0].sortCol = col;
            vm.config[0].sortDir = 'asc';
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            return;
        }

        function _pagination(page) {
            switch (page) {
                case 'start':
                    vm.config[0].displayStart = 0;
                    break;
                case 'previous':
                    vm.config[0].displayStart = vm.config[0].displayStart - vm.config[0].displayLength;
                    if (vm.config[0].displayStart < 0) {
                        vm.config[0].displayStart = 0;
                    }
                    break;
                case 'next':
                    vm.config[0].displayStart = parseInt(vm.config[0].displayStart) + parseInt(vm.config[0].displayLength);                    
                    break;
                case 'end':
                    vm.config[0].displayStart = vm.item.recordsFiltered - (vm.item.recordsFiltered % vm.config[0].displayLength);
                    break;
            }
            vm.adminUserService.userGridPost(vm.config[0])
                .then(_userGridPostGood, _userGridPostBad);
            _pageNumber();
            return;
        }
        function _pageNumber() {
            if (vm.config[0].displayStart == 0){
                vm.page = 1;
            } else {
                vm.page = 1 + Math.floor(vm.config[0].displayStart / vm.config[0].displayLength);
            }
            return;
        }
        function _showing() {
            vm.showingFirst = parseInt(vm.config[0].displayStart) + 1;
            vm.showingLast = vm.showingFirst + parseInt(vm.config[0].displayLength) - 1;
            if (vm.showingLast > vm.item.recordsFiltered) {
                vm.showingLast = vm.item.recordsFiltered;
            }
            return;
        }

        function _confirmEmail(id, index) {
            vm.index = index;
            vm.adminUserService.userConfirmEmail(id)
                .then(_confirmEmailGood, _confirmEmailBad);
            return;
        }
        function _confirmEmailGood(resp) {
            console.log('confirm email good', resp);
            vm.item.data[vm.index].emailConfirmed = true;
            vm.item.data[vm.index].role = 'User';
            return;
        }
        function _confirmEmailBad(err) {
            console.log('confirm email bad', err);
            return;
        }

        function _makeAdmin(id, index) {
            vm.index = index;
            vm.addInfo.id = id;
            vm.addInfo.role = 3;
            vm.adminUserService.userAdminAccess(id, vm.addInfo)
                .then(_makeAdminGood, _makeAdminBad);
            return;
        }
        function _makeAdminGood(resp) {
            vm.addInfo = {};
            vm.item.data[vm.index].role = 'Admin';
            console.log('make admin good', resp);
            return;
        }
        function _makeAdminBad(err) {
            console.log('make admin bad', err);
            return;
        }

        function _removeAdmin(id, index) {
            vm.index = index;
            vm.addInfo.id = id;
            vm.addInfo.role = 2;
            vm.adminUserService.userAdminAccess(id, vm.addInfo)
                .then(_removeAdminGood, _removeAdminBad);
            return;
        }
        function _removeAdminGood(resp) {
            vm.addInfo = {};
            vm.item.data[vm.index].role = 'User';
            console.log('remove admin good', resp);
            return;
        }
        function _removeAdminBad(err) {
            console.log('remove admin bad', err);
            return;
        }

        function _lockUser(id, index) {
            vm.index = index;
            vm.addInfo.id = id;
            vm.addInfo.lock = true;
            vm.adminUserService.userLock(id, vm.addInfo)
                .then(_userLockGood, _userLockBad);
            return;
        }
        function _userLockGood(resp) {
            vm.addInfo = {};
            vm.item.data[vm.index].lock = true;
            console.log('user lock good', resp);
            return;
        }
        function _userLockBad(err) {
            console.log('user lock bad', err);
            return;
        }

        function _unlockUser(id, index) {
            vm.index = index;
            vm.addInfo.id = id;
            vm.addInfo.lock = false;
            vm.adminUserService.userLock(id, vm.addInfo)
                .then(_userUnlockGood, _userUnlockBad);
            return;
        }
        function _userUnlockGood(resp) {
            vm.addInfo = {};
            vm.item.data[vm.index].lock = false;
            console.log('user unlock good', resp);
            return;
        }
        function _userUnlockBad(err) {
            console.log('user unlock bad', err);
            return;
        }
    }
})();