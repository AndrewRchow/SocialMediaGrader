(function() {
    "use strict";
    angular
        .module("mainApp")
        .controller("manageController", manageController);

    manageController.$inject = ["$scope", "$window", "peopleService"];

    function manageController($scope, $window, peopleService) {
        //Admin
        var vm = this;
        //View Model
        vm.peopleData = [];
        vm.thisPersonData = {};
        vm.$scope = $scope;
        vm.peopleService = peopleService;
        vm.$onInit = _init;
        vm.submit = _submit;
        vm.modelId = $("#modelId").val();
        vm.personForm = personForm;

        //Fold
        function _init() {
            console.log("firing");
            if (vm.modelId > 0) {
                vm.peopleService.getPerson(vm.modelId)
                    .then(_getPersonSuccess, _generalError);
            }
        }

        function _submit(isValid) {
            if (isValid) {
                if (vm.modelId > 0) {
                    //Put
                    vm.peopleService.putPerson(vm.thisPersonData)
                        .then(_putPostSuccess, _generalError);
                } else {
                    //Post
                    vm.peopleService.postPerson(vm.thisPersonData)
                        .then(_putPostSuccess, _generalError);
                }
            };
        }

        //Success functions
        function _getPersonSuccess(response) {
            console.log(response);
            vm.thisPersonData = response.data.item;
        }

        function _putPostSuccess(response) {
            console.log(response);
            $window.location.href = "/people/index";
        }

        //Error Functions
        function _generalError(error) {
            console.log(error);
        }
    }
})()