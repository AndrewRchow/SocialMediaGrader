(function () {

    angular.module('mainApp')
        .controller('cookieController', cookieController);


    cookieController.$inject = ['$scope', '$cookies', '$timeout', 'genericService'];

    function cookieController($scope, $cookies, $timeout, genericService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$cookies = $cookies;
        vm.myCookieVal = $cookies.get('cookie');
        vm.genericService = genericService;
        vm.$onInit = _init;
        vm.setCookie = _setCookie;
        vm.setApiCookie = _setApiCookie;
        vm.samCookie = '';

        function _init() {
            //vm.setApiCookie();
            var cookieName = 'santi';
            var cookieVal = 'Sam';
            vm.setCookie(cookieName, cookieVal);
            $timeout(function () {
                vm.genericService.get('/api/config/' + cookieName + '/cookie').then(newSuccess, newError);
            }, 6000);
        }
        function _getAllSuccess(r) {
            console.log("cookies: ", r.data.items);
            vm.cookie1 = r.data.items[0];
        }
        function _getAllFailed(r) {
            console.log(r, ":(");
        }
        function _setCookie(cookieName, cookieVal) {
            console.log('clicked');
            $cookies.put(cookieName, cookieVal);
            console.log(cookieName + ': ' + cookieVal);
        }
        function _setApiCookie() {

            vm.genericService.get('/api/config/sam/santi/cookie')
                .then(newSuccess, newError);
        }
        function newSuccess(r) {
            vm.samCookie = $cookies.get('santi');
            console.log(r.data);
        }
        function newError(r) {
            console.log(r, ":(")
        }
        function _setCookie(val) {
            $cookies.put('cookie', val);
            console.log('clicked');
        }
    }
})();

