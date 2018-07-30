//Loading Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('loadingController', loadingController);

    loadingController.$inject = ['$scope', 'loadingService', '$window'];

    function loadingController($scope, loadingService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.loadingService = loadingService;
        vm.$window = $window;
        vm.$onInit = _init;

        function _init() {
            return vm.loadingService.checkUser();
        }
        function _checkUserGood() {
            return console.log('check user good');
        }
        function _checkUserBad(err) {
            $window.location.href = "/Home/Login";
            return console.log(err);
        }
    }
})();