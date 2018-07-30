//Facebook Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('FbController', FbController);

    FbController.$inject = ['$scope', 'fbService', '$window', '$cookies', 'toastr'];

    function FbController($scope, fbService, $window, $cookies, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.fbService = fbService;
        vm.$onInit = _init;
        vm.$window = $window;
        vm.$cookies = $cookies;
        vm.fbName = '';
        vm.fbEng = [];
        vm.fbAct = [];
        vm.fbGro = [];
        vm.fbRch = [];
        vm.fbProfile = [];
        vm.report = {};
        vm.report2 = {};
        vm.actgrade = 0;
        vm.ready = false;
        vm.error = false;
        vm.toastr = toastr;

        function _init() {
            var socialURLval = $cookies.get('socialURL');

            var Y = ".com/"
            var X = socialURLval;
            vm.fbName = X.split(Y).pop();

            vm.fbService.getFb(vm.fbName, 'profile')
                .then(_getProfileGood, _error);          
            return;
        }
        function _getProfileGood(response) {
            vm.fbProfile = response.data;
            if (response.data.name == undefined) {
                vm.error = true;
                return setTimeout(function () {
                    vm.$scope.$apply(function () {
                        $window.location.href = "/";
                    });
                }, 5000);
            }
            vm.profile = {};
            vm.profile.name = response.data.name;
            vm.profile.picture = response.data.picture.data.url;
            vm.profile.cover = response.data.cover.source;
            vm.profile.isVerified = response.data.is_verified;

            vm.$cookies.putObject('profile', vm.profile);

            vm.fbService.getFb(vm.fbName, 'reach')
                .then(_getReachGood, _error);
            vm.fbService.getFb(vm.fbName, 'growth')
                .then(_getGrowthGood, _error);
            vm.fbService.getFb(vm.fbName, 'activity')
                .then(_getActivityGood, _error);

            //if (vm.profile.isVerified == true) {
            //} else {
            //    vm.error = true;
            //    vm.ready = true;
            //    setTimeout(function () {
            //        vm.$scope.$apply(function () {
            //            $window.location.href = "/";
            //        });
            //    }, 3000);
            //}

            vm.report.total_likes = vm.fbProfile.engagement.count;
            return; 
        }
        function _getActivityGood(response) {
            vm.fbAct = response.data;
            vm.report.activity = vm.fbAct;
            return vm.actgrade = vm.fbAct.points / 25 * 100;
        }
        function _getGrowthGood(response) {
            vm.fbGro = response.data;
            vm.fbGro.percentChange = (vm.fbGro.total_Fans_Current - vm.fbGro.total_Fans_Month_Ago) / vm.fbGro.total_Fans_Month_Ago * 100;
            if (vm.fbGro.percentChange > 1){
                vm.fbGro.points = 25;
            } else if (vm.fbGro.percentChange > 0.75){
                vm.fbGro.points = 20;
            } else if (vm.fbGro.percentChange > 0.5) {
                vm.fbGro.points = 15;
            } else if (vm.fbGro.percentChange > 0.25) {
                vm.fbGro.points = 10;
            } else if (vm.fbGro.percentChange > 0) {
                vm.fbGro.points = 5;
            } else {
                vm.fbGro.points = 0;
            }
            vm.report2.growth = vm.fbGro;
            return;
        }
        function _getReachGood(response) {
            vm.fbRch = response.data;

            var score = vm.fbProfile.talking_about_count / vm.fbProfile.engagement.count * 100;
            if (score > 2) {
                vm.fbRch.score = 10;
            } else if (score >= 1.5) {
                vm.fbRch.score = 8;
            } else if (score >= 1) {
                vm.fbRch.score = 6;
            } else if (score >= 0.5) {
                vm.fbRch.score = 4;
            } else if (score >= 0.20) {
                vm.fbRch.score = 2;
            } else {
                vm.fbRch.score = 1;
            }
            vm.report2.reach = vm.fbRch;
            return vm.fbService.getFb(vm.fbName, 'feed')
                .then(_getEngagementGood, _error);
        }
        function _getEngagementGood(response) {
            vm.fbEng = response.data;
            vm.score = (vm.fbEng.total_Likes + vm.fbEng.total_Shares + vm.fbEng.total_Comments + vm.fbEng.total_Reactions) / vm.fbProfile.engagement.count * 100;
            if (vm.score > 3) {
                vm.fbEng.score = 25;
            } else if (vm.score >= 2.5) {
                vm.fbEng.score = 22;
            } else if (vm.score >= 1.5) {
                vm.fbEng.score = 15;
            } else if (vm.score >= 0.5) {
                vm.fbEng.score = 8;
            } else if (vm.score > 0) {
                vm.fbEng.score = 3;
            } else {
                vm.fbEng.score = 0;
            }
            vm.report.engagement = vm.fbEng;
            if (vm.profile.isVerified == false) {
                vm.report.activity.points *= 0.6;
                vm.report.engagement.score *= 0.6;
                vm.report2.growth.points *= 0.6;
                vm.report2.reach.score *= 0.6;
            } else {
                vm.report.activity.points += 3;
                if (vm.report.activity.points > 25) { vm.report.activity.points = 25 };
                vm.report.engagement.score += 3;
                if (vm.report.engagement.score > 25) { vm.report.engagement.score = 25 };
                vm.report2.growth.points += 4;
                if (vm.report2.growth.points > 25) { vm.report2.growth.points = 25 };
                vm.report2.reach.score += 2;
                if (vm.report2.reach.score > 10) { vm.report2.reach.score = 10 };
            }
            vm.$cookies.putObject('report', vm.report);
            vm.$cookies.putObject('report2', vm.report2);
            window.location.href = "/fb/report";
            return;
        }
        function _postReportGood(response) {
            $window.location.href = "/Fb/Report";
        }
        function _error(err) {
            vm.toastr.error('Please try again at a later time.', 'An error has occurred:');
            _errorParse(err);
            setTimeout(function () {
                vm.$scope.$apply(function () {
                    $window.location.href = "/";
                });
            }, 5000);
        }
    }
    // Called when error is thrown
    function _errorParse(data) {
        function _noUser() {
            var newData = {
                "errorMessage": data.data.message,
                "errorNumber": data.status,
                "modifiedBy": "Admin",
                "errorSeverity": 0,
                "errorState": 0,
                "errorProcedure": data.config.method,
                "errorLine": 0
            };
            _postError(newData);
        }
    }

    // Function to catch errors
    function _postError(data) {
        return $http.post("/api/errors/", data)
            .then(_postErrorComplete)
            .catch(_postErrorFailed);

        function _postErrorComplete(res) {
        }

        function _postErrorFailed(err) {
            return $q.reject(err);
        }
    }
})();