(function () {
    'use strict';

    angular
        .module("mainApp")
        .controller("indexController", indexController);

    indexController.$inject = ["$scope", "phoneService"];

    function indexController($scope, phoneService) {
        // View model
        var vm = this;
        vm.phoneService = phoneService;
        vm.item = {};
        vm.items = [];
        vm.$onInit = _init;
        vm.edit = _edit;
        vm.delete = _delete;

        //-------------------------- The Fold ------------------------//

        //-------------- Get All On Startup -------------
        function _init() {
            vm.phoneService.phoneGetAll()
                .then(_phoneGetAllSuccess, _phoneGetAllError);
        }
        function _phoneGetAllSuccess(response) {
            vm.items = response.data.items;
        }
        function _phoneGetAllError(error) {
            return ("Error", error);
        }

        //-------------- Edit Button -------------
        function _edit(id) {
            vm.item.userId = id;
            window.location.href = "/phones/" + vm.item.userId + "/edit";
        }

        //-------------- Delete Button -------------
        function _delete(id, index) {
            vm.index = index;
            vm.phoneService.phoneDelete(id)
                .then(_phoneDeleteSuccess, _phoneDeleteError);
        }
        function _phoneDeleteSuccess(response) {
            vm.items.splice(vm.index, 1);
        }
        function _phoneDeleteError(error) {
            return ("Error", error);
        }
    }
})();