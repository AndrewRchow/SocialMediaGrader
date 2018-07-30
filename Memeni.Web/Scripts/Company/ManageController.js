(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("manageController", manageController);

    manageController.$inject = ["$scope", "$window", "companyService"];

    function manageController($scope, $window, companyService) {
        var vm = this;

        vm.item = {};
        vm.items = [];
        vm.companyService = companyService;
        vm.submit = _submit;
        vm.itemId = parseInt($("#itemId").val());
        vm.$onInit = _init;

        //--THE FOLD--

        //--Get BY ID if any on page start up
        function _init() {
            if (vm.itemId > 0) {
                vm.companyService.companyGetById(vm.itemId)
                    .then(_companyGetByIdSuccess, _companyGetByIdError);
            }
        }

        //--Get BY ID SUCCESS--
        function _companyGetByIdSuccess(response) {
            console.log(response);
            vm.item = response.data.item;
        }

        //--Get BY ID ERROR--
        function _companyGetByIdError(error) {
            console.log(error);
        }

        //--POST/PUT submit function 
        function _submit($valid) {
            if ($valid) {
                if (vm.itemId > 0) {
                    //put
                    vm.companyService.companyUpdate(vm.item, vm.item.userId)
                        .then(_serviceCallSuccess, _serviceCallError);
                } else {
                    // post
                    vm.companyService.companyPost(vm.item)
                        .then(_serviceCallSuccess, _serviceCallError);
                }
            }
        }

        //--Post/Put Success
        function _serviceCallSuccess(response) {
            console.log(response);
            vm.item = {};
            $window.location.href = "/companies/index";
        }

        //--Post/Put Error
        function _serviceCallError(error) {
            console.log(error);
        }
    }
})();