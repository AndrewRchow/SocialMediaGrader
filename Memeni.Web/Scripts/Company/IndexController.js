(function () {
    "use strict";

    angular
        .module("mainApp")
        .controller("indexController", indexController);

    indexController.$inject = ["$scope", "$window", "companyService"];

    function indexController($scope, $window, companyService) {

        var vm = this;
        vm.items = [];
        vm.item = {};
        vm.companyService = companyService;
        vm.$onInit = _init;
        vm.add = _add;
        vm.edit = _edit;
        vm.delete = _delete;

        //--THE FOLD--


        //--GET ALL COMPANIES--
        function _init() {
            vm.companyService.companyGetAll()
                .then(_companyGetAllSuccess, _companyGetAllError);
        }

        //--Get All SUCCESS--
        function _companyGetAllSuccess(response) {
            console.log(response.data.items);
            vm.items = response.data.items;
        }

        //--Get All ERROR--
        function _companyGetAllError(error) {
            console.log(error);
        }


        //--on add click, opens form window to create
        function _add() {
            $window.location.href = "/companies/create";
        }


        //--on add click, opens form window to edit
        function _edit(id) {
            vm.item.userId = id;
            console.log("Item ID:", vm.item.userId);
            $window.location.href = "/companies/" + vm.item.userId + "/edit";
        }


        //--DELETE A COMPANY FROM TABLE--
        function _delete(id, index) {
            vm.index = index;
            vm.companyService.companyDelete(id)
                .then(_companyDeleteSuccess, _companyDeleteError);
        }

        //--DELETE SUCCESS--
        function _companyDeleteSuccess(response) {
            console.log(response);
            vm.items.splice(vm.index, 1);
        }

        //--DELETE ERROR--
        function _companyDeleteError() {
            console.log(error);
        }
    }

})();