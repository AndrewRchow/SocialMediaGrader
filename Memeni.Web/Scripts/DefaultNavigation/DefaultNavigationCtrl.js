(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('DefaultNavigationController', DefaultNavigationController);

    DefaultNavigationController.$inject = ['$scope', 'DefaultNavigationService', '$window']

    function userIndexController($scope, userIndexService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.DefaultNavigationService = DefaultNavigationService;
        vm.logOut = _logOut;

        function _logOut() {
            vm.DefaultNavigationService.logOutUser()
                .then(_logOutGood, _logOutBad);
        }

        function _logOutGood(resp) {
            $window.location.href = "/Home/login";
        };
        function _logOutBad(err) {
            console.log(err);
        }

    }
})();