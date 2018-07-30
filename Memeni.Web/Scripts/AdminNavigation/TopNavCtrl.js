//Top Nav Bar Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('adminTopNavController', adminTopNavController);

    adminTopNavController.$inject = ['$scope', 'adminNavService', '$window'];

    function adminTopNavController($scope, adminNavService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.adminNavService = adminNavService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.logout = _logout;

        function _init() {       
            //return vm.adminNavService.adminAuth();  
            console.log('Executing: adminTopNavController.$onInit');
        }
        function _logout() {
            return vm.adminNavService.logout()
                    .then(_logOutGood, _logOutBad);
        }
        function _logOutGood() {        
            return $window.location.href = "/Home/login";
        }
        function _logOutBad(err) {
            return console.log('logout bad', err);
        }

    }
})();