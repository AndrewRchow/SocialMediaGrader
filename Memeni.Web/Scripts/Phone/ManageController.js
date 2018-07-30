(function () {
    'use strict';

    angular
        .module("mainApp")
        .controller("manageController", manageController);

    manageController.$inject = ["$scope", "phoneService"];

    function manageController($scope, phoneService) {
        // View model
        var vm = this;
        vm.$scope = $scope;
        vm.phoneService = phoneService;
        vm.item = {};
        vm.items = [];
        vm.$onInit = _init;
        vm.submit = _submit;
        vm.itemId = parseInt($("#itemId").val());

        //------------------------ The Fold --------------------//

        //-------------- Get By Id On Startup -------------
        function _init() {
            if (vm.itemId > 0) {
                vm.phoneService.phoneGetById(vm.itemId)
                    .then(_phoneGetByIdSuccess, _phoneGetByIdError);
            }
        }
        function _phoneGetByIdSuccess(response) {
            vm.item = response.data.item;
        }
        function _phoneGetByIdError(error) {
            return ("Error", error);
        }


        //-------------- Post/Put Button -------------
        function _submit(isValid) {
            if (isValid) {
                if (vm.itemId > 0) {
                    vm.phoneService.phoneUpdate(vm.item, vm.item.userId)
                        .then(_phoneSuccess, _phoneError)
                } else {
                    vm.phoneService.phoneInsert(vm.item)
                        .then(_phoneSuccess, _phoneError)
                }
            }
        }

        function _phoneSuccess(response) {
            alert("Successful Submit/Update!", response);
            window.location.href = "/phones/index";
        }
        function _phoneError(error) {
            return ("Error", error);
        }


    }
})();