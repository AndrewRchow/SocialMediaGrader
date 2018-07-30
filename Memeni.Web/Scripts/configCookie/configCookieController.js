(function () {

    angular.module('mainApp')
        .controller('cookieController1', cookieController1);

    cookieController1.$inject = ['$scope', '$cookies'];

    function cookieController1($scope, $cookies) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _init;
        $scope.myCookieVal = $cookies.get('cookie');
        vm.setCookie = _setCookie;

        function _init() {
            console.log('hihiiii');
        }
    }
})();
