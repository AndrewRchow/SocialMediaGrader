(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("ManageController", ManageController);

    ManageController.$inject = ["$scope", "$window", "PrivacyService", "adminNavService"];

    function ManageController($scope, $window, PrivacyService, adminNavService) {

        var vm = this;
        vm.$scope = $scope;
        vm.backButton = _backButton;
        vm.allPolicies = [];
        vm.storePolicy = {};
        vm.storePolicy.id = parseInt($("#modelId").val());
        vm.PrivacyService = PrivacyService;
        vm.submitButton = _submitButton;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.route = "Create";
        vm.viewsButton = _viewsButton;
        vm.adminButton = _adminButton;
        vm.adminNavService = adminNavService;

        function _init() {    
            vm.storePolicy.modifiedBy = 'placeholder';
            if (vm.storePolicy.id > 0) {      /*If id is selected, get item's info'*/
                vm.route = "Edit";
                vm.PrivacyService.getPrivacyById(vm.storePolicy.id)    
                    .then(_getPrivacyByIdSuccess, _getPrivacyByIdError);
            }
            if (vm.storePolicy.id == 0) {      /*if no id, automatically add next display order number*/
                vm.storePolicy.parentId = 2;
                vm.PrivacyService.getAllPrivacy()
                    .then(_getAllPrivacySuccess, _getAllPrivacyError);
            }
        }
        function _getPrivacyByIdSuccess(response) {
            console.log(response);
            vm.storePolicy = response.data.item;
            vm.adminNavService.adminAuth()
                .then(_adminAuthSuccess, _adminAuthError);
        }
        function _getPrivacyByIdError(response) {
            console.log(response);
        }
        function _getAllPrivacySuccess(response) {
            vm.storePolicy.displayOrder = response.data.items.length;    /*generates next displayOrder Number (hidden)*/
            vm.adminNavService.adminAuth()
                .then(_adminAuthSuccess, _adminAuthError);
        }
        function _getAllPrivacyError(response) {
            console.log(response);
        }
        function _adminAuthSuccess(response) {
            console.log(response);
            console.log(response.data.name);
            vm.storePolicy.modifiedBy = response.data.name;
        }
        function _adminAuthError(response) {
            console.log(response);
        }
        function _backButton() {
            $window.location.href = "/Admin/PrivacyPolicy/Index";
        }

        function _submitButton() {
                if (vm.storePolicy.id > 0) {
                    vm.PrivacyService.putPrivacy(vm.storePolicy, vm.storePolicy.id)
                        .then(_privacySuccess, _privacyError);
                }
                else {
                    vm.PrivacyService.postPrivacy(vm.storePolicy)
                        .then(_privacySuccess, _privacyError);
                }
        }
        function _privacySuccess(response) {
            console.log(response);
            $window.location.href = "/Admin/PrivacyPolicy/Index";
        }
        function _privacyError(response) {
            console.log(response);
        }

        function _viewsButton() {
            $window.location.href = "/Privacy/Policies";
        }

        function _adminButton() {
            $window.location.href = "/Admin/Home/Index";
        }
    }
})();