//Facebook Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('userTwitterController', UserTwitterController);

    UserTwitterController.$inject = ['$scope', 'genericService', '$window', 'toastr'];

    function UserTwitterController($scope, genericService, $window, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.$onInit = _init;
        vm.$window = $window;
        vm.report = {};
        vm.fbName = "";
        vm.twName = "";
        vm.twAcct = false;
        vm.btnLoad = false;
        vm.loading = false;
        vm.twDash = false;
        vm.submitTw = _submitTw;
        vm.userId;
        vm.smeDays = 0;
        vm.twProfileInfo;
        vm.resetLink = _resetLink;
        vm.reset = false;
        vm.fSwitch = false;
        vm.tSwitch = false;
        vm.toastr = toastr;

        function _init() {
            vm.loading = true;
            return;
        }
        $scope.$on('userId', function (event, data) {
            vm.userId = data;
            vm.genericService.getById('/api/user/fb/', data)
                .then(_getTwGood, _getTwError);
        });
        $scope.$on('userEmail', function (event, data) {
            vm.userEmail = data;
        });
        $scope.$on('firstName', function (event, data) {
            vm.firstName = data;
        });
        function _getTwGood(resp) {
            //receive settings info on logged in user and 
            console.log(resp.data);
            vm.fSwitch = resp.data.wklyFb;
            vm.tSwitch = resp.data.wklyTwt;
            vm.fbName = resp.data.facebook;
            vm.twName = resp.data.twitter;
            if (!resp.data.twitter) {
                vm.loading = false;
                vm.fbName = "";
                return vm.twAcct = true;
            } else {
                vm.reset = true;
                vm.genericService.getById('/api/twitter/report/', vm.twName)
                    .then(_dashboardPostsGood, _error);
                vm.genericService.getById('/api/twitter/', vm.twName)
                    .then(_twProfileInfoGood, _error);
            }
        }
        function _submitTw(name) {
            //creates new profile for user to link to fb, twitter, email services settings
            vm.genericService.getById('/api/twitter/', name)
                .then(_twProfileGood, _twProfileError);
            vm.btnLoad = true;
        }
        function _twProfileGood(resp) {
            var twUsername = JSON.parse(resp.data);
            var userDashData = {};
            userDashData.Id = vm.userId;
            userDashData.Twitter = twUsername[0].user.screen_name;
            vm.twName = twUsername[0].user.screen_name;
            userDashData.Facebook = vm.fbName;
            userDashData.WeeklyFB = vm.fSwitch;
            userDashData.WeeklyTwitter = vm.tSwitch;
            console.log(userDashData);
            //sets values for user dashboard profile that was successfully set up
            vm.genericService.post('/api/user/fb/dashboard/', userDashData)
                .then(_userDashboardGood, _error);
            vm.genericService.getById('/api/twitter/', vm.twName)
                .then(_twProfileInfoGood, _error);
            // broadcast function call 
            $scope.$emit('twitterSwitch', userDashData);
        }
        function _twProfileInfoGood(resp) {
            var twProfile = JSON.parse(resp.data);
            vm.twProfileInfo = twProfile[0].user;
            console.log(vm.twProfileInfo);
            return;
        }
        function _userDashboardGood(resp) {

            console.log(resp.data);

            if (vm.reset == true) {
                return $window.location.reload();
            }
            vm.genericService.getById('/api/twitter/report/', vm.twName)
                .then(_dashboardPostsGood, _error);
        }
        function _dashboardPostsGood(resp) {
            $scope.$broadcast('twDashboard', resp.data);
            vm.report = resp.data;
            vm.report.dailyLikes = vm.report.LikesCount / 30;
            vm.report.dailyRetweets = vm.report.RetweetCount / 30;
            vm.report.dailyReplies = vm.report.RepliesCount / 30;
            if (vm.report.RepliesCount < 3) {
                vm.report.RepliesCount = 3;
            }
            vm.report.dailyTweets = vm.report.TweetCount / 30;
            vm.smeDays = resp.data.Stats.length;
            vm.loading = false;
            vm.twAcct = false;
            vm.twDash = true;
            return;
        }
        function _error(error) {
            console.log(error);
        }
        function _twProfileError(error) {
            vm.btnLoad = false;
            console.log(error);
            return vm.toastr.error("This Account is Not Valid. Please try another one.");
        }
        function _getTwError(error) { //done
            vm.loading = false;
            vm.twAcct = true;
        }
        function _resetLink() {
            vm.twDash = false;
            vm.twAcct = true;
        }
    }
})();
//Activity Chart
(function () {
    angular
        .module("mainApp")
        .controller("twBarCtrl", function ($scope, $cookies) {

            $scope.labels = [];
            $scope.series = ['Tweets'];

            $scope.data = [[]];

            $scope.options = {
                scales: {
                    yAxes: [{
                        display: true,
                        ticks: {
                            suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                        }
                    }]
                }
            };
            $scope.$on('twDashboard', function (event, data) {
                var report = data.Stats;
                for (var i = report.length - 1; i > -1; i--) {
                    $scope.labels.push(report[i].DateString);
                    $scope.data[0].push(report[i].Tweets);
                };
            });
            return;
        });
})();
//Engagement Chart
(function () {
    angular
        .module("mainApp")
        .controller("twLineCtrl", function ($scope, $cookies) {

            $scope.labels = [];
            $scope.series = ['Likes', 'Retweets'];
            $scope.data = [[], [], []];
            $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-1' }];
            $scope.options = {
                scales: {
                    yAxes: [
                        {
                            id: 'y-axis-1',
                            type: 'linear',
                            display: true,
                            position: 'left'
                        }
                    ]
                }
            };
            $scope.$on('twDashboard', function (event, data) {
                var report = data.Stats;
                for (var i = report.length - 1; i > -1; i--) {
                    $scope.labels.push(report[i].DateString);
                    $scope.data[0].push(report[i].Likes);
                    $scope.data[1].push(report[i].Retweets);
                };
            });
        });
})();
//Reach Chart
(function () {
    angular
        .module("mainApp")
        .controller("twReachCtrl", function ($scope, $cookies) {

            $scope.labels = [];
            $scope.series = ['Unique Mentions', 'Tweets Retweeted'];

            $scope.data = [[], []];

            $scope.options = {
                scales: {
                    yAxes: [{
                        display: true,
                        ticks: {
                            suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                        }
                    }]
                }
            };
            $scope.$on('twDashboard', function (event, data) {
                var report = data.Stats;
                for (var i = report.length - 1; i > -1; i--) {
                    if (report[i].Mentions > 0 || report[i].Retweeted > 0) {
                        $scope.labels.push(report[i].DateString);
                        $scope.data[0].push(report[i].Mentions);
                        $scope.data[1].push(report[i].Retweeted);
                    }
                };
            });
            return;
        });
})();