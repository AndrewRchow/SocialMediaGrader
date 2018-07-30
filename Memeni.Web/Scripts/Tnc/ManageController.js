(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("manageController", ManageController);

    // Inject services
    ManageController.$inject = ["$scope", "$window", "service"];

    function ManageController($scope, $window, service) {
        var vm = this;

        // View model
        vm.item = {};
        vm.items = [];
        vm.service = service;
        vm.$onInit = _init;
        vm.submit = _submit;
        vm.index;
        vm.route = "Create";
        vm.modelId = parseInt($("#modelId").val());

        // The Fold
        // Populate on Initialize
        // Get by ID on Edit Button
        function _init() {
            if (vm.modelId > 0) {
                vm.route = "Edit";
            }
            vm.service.adminAuth()
                .then(_adminAuthGood, _adminAuthBad);
        }
        // Get Logged in User Success
        function _adminAuthGood(res) {
            console.log(res);
            if (res.data.name) {
                vm.user = res.data.name;
                vm.service.tncGetById(vm.modelId)
                    .then(_tncGetByIdSuccess, _tncGetByIdError);
            }
        }
        // Get Logged in User Error
        function _adminAuthBad(err) {
            console.log(err);
        }
        // Get by ID Success
        function _tncGetByIdSuccess(res) {
            console.log(res);
            if (res.data.item) {
                vm.item = res.data.item;
                vm.item.modifiedBy = vm.user;
            } else {
                vm.item.parentId = 0;
                vm.item.displayOrder = 0;
                vm.item.modifiedBy = vm.user;
            }
        }
        // Get by ID Error
        function _tncGetByIdError(err) {
            console.log(err);
        }
        // Post/Put on Submit Form
        function _submit() {
            if (vm.modelId > 0) {
                vm.service.tncPut(vm.item, vm.item.id)
                    .then(_tncSubmitSuccess, _tncSubmitError);
            } else {
                vm.service.tncPost(vm.item)
                    .then(_tncSubmitSuccess, _tncSubmitError);
            }
        }
        // Post/Put Success
        function _tncSubmitSuccess(res) {
            console.log(res);
            vm.item = {};
            $window.location.href = "/admin/terms/index";
        }
        // Post/Put Error
        function _tncSubmitError(err) {
            console.log(err.data.message);
        }
    }
})();