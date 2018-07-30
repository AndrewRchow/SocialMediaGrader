//Twitter Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('twitterRepController', TwitterRepController);

    // Inject services
    TwitterRepController.$inject = ['$scope', 'genericService', '$window', '$cookies'];

    function TwitterRepController($scope, genericService, $window, $cookies) {
        var vm = this;

        // View Model
        vm.$onInit = _init;
        vm.$window = $window;
        vm.$cookies = $cookies;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.report = {};
        vm.profile = {};
        vm.classColor = _classColor;
        vm.gradeQuote = "";
        vm.regBtn = _regBtn;
        vm.home = _home;
        vm.suggestion = {};
        vm.moreBtn = _moreBtn;

        // The Fold
        // Get user timeline on initialize
        function _init() {
            // get cookie from loading page
            vm.report = vm.$cookies.getObject('twreport');
            if (vm.report.RepliesCount < 3) {
                vm.report.RepliesCount = 3;
            }
            vm.profile = vm.$cookies.getObject('twprofile');
            vm.grade = vm.report.OverallGrade;
            console.log(vm.grade);
            if (vm.grade > 79) {
                vm.gradeQuote = "Good! Let's see where we can improve."
            } else if (vm.grade > 59) {
                vm.gradeQuote = "Fair! Let's see where we can improve.";
            } else {
                vm.gradeQuote = "Not Good! Let's see where we can improve.";
            }
            getSuggestion();
        }
        function getSuggestion() {
            if (vm.report.EngGrade < 16) {
                vm.suggestion.engagement = 'Try to provide more value for your audience in your posts.';
            } else if (vm.report.EngGrade < 23) {
                vm.suggestion.engagement = 'Use gamification to incentivize your audience to invite their friends.'
            } else {
                vm.suggestion.engagement = 'Great!'
            }
            if (vm.report.ActGrade < 16) {
                vm.suggestion.activity = 'Try to post more often.';
            } else {
                vm.suggestion.activity = 'Good!'
            }
            if (vm.report.GrowGrade < 25) {
                vm.suggestion.growth = 'Offer prizes and incentives that can be shared on social media by your audience.';
            } else {
                vm.suggestion.growth = 'Good!'
            }
            if (vm.report.ReachGrade < 25) {
                vm.suggestion.reach = 'Limited reach is a known challenge on social media.';
            } else {
                vm.suggestion.reach = 'Good!'
            }
            vm.suggestion.data = 'Use a 3rd party to collect users actionable data.';
        }
        // set color
        function _classColor(num) {
            if (num > 69) {
                return 'g-color-green';
            } else if (num > 49) {
                return 'g-color-yellow';
            } else if (num > 29) {
                return 'g-color-orange';
            } else {
                return 'g-color-google-plus';
            }
        }
        function _regBtn() {
            return $window.location.href = "/home/register";
        }
        function _home() {
            return $window.location.href = "/";
        }
        function _moreBtn() {
            $window.location.href = "http://www.memeni.com/";
        }
    }
})();
//Activity Chart
(function () {
    angular
        .module("mainApp")
        .controller("BarCtrl", function ($scope, $cookies) {

            this.$onInit = _init;
            $scope.labels = [];
            $scope.series = ['Tweets'];
            $scope.data = [[]];

            function _init() {
                var report = $cookies.getObject('twreport');
                var weekReport = report.Stats;
                console.log(report);
                for (var i = weekReport.length - 1; i > -1; i--) {
                    $scope.labels.push(weekReport[i].DateString);
                    $scope.data[0].push(weekReport[i].Tweets);
                }
                $scope.options = {
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                                // OR //
                            }
                        }]
                    }
                };
                return;
            }
        });
})();
//Engagement Chart
(function () {
    angular
        .module("mainApp")
        .controller("LineCtrl", function ($scope, $cookies) {

            this.$onInit = _init;
            $scope.labels = [];
            $scope.series = ['Likes', 'Retweets'];
            $scope.data = [[], [], [], []];
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

            function _init() {
                var report = $cookies.getObject('twreport');
                var weekReport = report.Stats;
                for (var i = weekReport.length - 1; i > -1; i--) {
                    $scope.labels.push(weekReport[i].DateString);
                    $scope.data[0].push(weekReport[i].Likes);
                    $scope.data[1].push(weekReport[i].Retweets);
                }
                return;
            }
        });
})();
//Radar Chart
(function () {
    angular
        .module("mainApp")
        .controller("RadarCtrl", function ($scope, $cookies) {

            $scope.labels = ["Activity", "Engagement", "Growth", "Reach", "Data Collection"];
            $scope.data = [];
            $scope.options = {
                scale: {
                    ticks: {
                        beginAtZero: true,
                        min: 0,
                        max: 100,
                        stepSize: 20
                    }
                }
            };
            var report = $cookies.getObject('twreport');
            $scope.data.push(report.ActGrade / 25 * 100);
            $scope.data.push(report.EngGrade / 25 * 100);
            $scope.data.push(report.GrowGrade / 25 * 100);
            $scope.data.push(report.ReachGrade / 10 * 100);
            $scope.data.push(report.DataGrade / 15 * 100);

        })
})();
//Reach Chart
(function () {
    angular
        .module("mainApp")
        .controller("ReachCtrl", function ($scope, $cookies) {

            this.$onInit = _init;
            $scope.labels = [];
            $scope.series = ['Mentions'];
            $scope.data = [[]];

            function _init() {
                var report = $cookies.getObject('twreport');
                var weekReport = report.Stats;
                console.log(report);
                for (var i = weekReport.length - 1; i > -1; i--) {
                    if (weekReport[i].Mentions > 0) {
                        $scope.labels.push(weekReport[i].DateString);
                        $scope.data[0].push(weekReport[i].Mentions);
                    }
                }
                $scope.options = {
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                                // OR //
                            }
                        }]
                    }
                };
                return;
            }
        });
})();