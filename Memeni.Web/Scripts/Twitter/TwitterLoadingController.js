//Twitter Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('twitterController', TwitterController);

    // Inject services
    TwitterController.$inject = ['$scope', 'genericService', '$window', '$cookies'];

    function TwitterController($scope, genericService, $window, $cookies) {
        var vm = this;

        // View Model
        vm.$onInit = _init;
        vm.$window = $window;
        vm.$cookies = $cookies;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.profile = {};
        vm.report = {};
        vm.timeline = [];
        vm.error = false;

        // The Fold
        // Get user timeline on initialize
        function _init() {
            var socialURLval = $cookies.get('socialURL');
            var Y = ".com/"
            var X = socialURLval;
            vm.username = X.split(Y).pop();
            vm.genericService.getById('/api/twitter/report/', vm.username)
                .then(_getReportSuccess, _getError);
        }
        // Get user report success
        function _getReportSuccess(res) {
            vm.report = res.data;
            console.log(res.data);
            if (res.data == "error") {
                vm.error = true;
                return setTimeout(function () {
                    vm.$scope.$apply(function () {
                        $window.location.href = "/";
                    });
                }, 5000);
            }
            vm.genericService.getById('/api/twitter/', vm.username)
                .then(_getTimelineSuccess, _getError);
        }
        // Get user timeline success
        function _getTimelineSuccess(res) {
            vm.timeline = JSON.parse(res.data);
            vm.profile = vm.timeline[0].user;
            // send report via cookie
            vm.$cookies.putObject('twreport', vm.report);
            vm.$cookies.putObject('twprofile', vm.profile);
            window.location.href = "/twitter/report";
        }
        // Get Error
        function _getError(err) {
            console.log(err);
        }
    }
})();