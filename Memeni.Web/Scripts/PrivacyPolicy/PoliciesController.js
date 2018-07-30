(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller("PoliciesController", PoliciesController);

    PoliciesController.$inject = ['$scope', 'PrivacyService', "$window","$location","$anchorScroll", "$sanitize"];

    function PoliciesController($scope, PrivacyService, $window, $location, $anchorScroll, $sanitize) {
        var vm = this;
        vm.$scope = $scope;
        vm.PrivacyService = PrivacyService
        vm.$onInit = _init;
        vm.backButton = _backButton;
        vm.allPolicies = [];
        vm.goToAnchor = _goToAnchor;

        function _init() {
            vm.PrivacyService.getAllPrivacy()
                .then(_getAllPrivacySuccess, _getAllPrivacyError);
        }
        function _getAllPrivacySuccess(response) {
            var policies = response.data.items;
            console.log(policies);
            vm.allPolicies = policies;
        }
        function _getAllPrivacyError(data) {
            console.log(data);
        }

        function _backButton() {
            $window.location.href = "/Admin/PrivacyPolicy/Index";
        }

        function _goToAnchor(x) {
            var newHash = 'anchor' + x;
            if ($location.hash() !== newHash) {
                $location.hash('anchor' + x);              
            } else {
                $anchorScroll();            
            }
        }
    }
})();