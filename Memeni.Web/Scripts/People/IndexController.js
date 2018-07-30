(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("indexController", indexController);

    indexController.$inject = ["$scope", "$window", "peopleService"];

    function indexController($scope, $window, peopleService) {
        //Admin
        var vm = this;

        //View Model
        vm.peopleData = [];
        vm.thisPersonData = {};
        vm.$scope = $scope;
        vm.peopleService = peopleService;
        vm.$onInit = _init;
        vm.edit = _edit;
        vm.deletePerson = _delete;

        //The Fold

        function _init() {
            console.log("firing");
            vm.peopleService.getAllPeople()
                .then(_getAllComplete, _getAllFailed);
        }

        function _getAllComplete(response) {
            console.log(response);
            vm.peopleData = response.data.items;
        }

        function _getAllFailed(error) {
            console.log(error);
        }

        // Edit existing entry
        function _edit(id) {
            $window.location.href = "/people/" + id + "/edit";
        }

        //Delete
        function _delete(id, index) {
            if ($window.confirm('Are you sure you wish to delete? This can not be reversed')) {
                vm.index = index;
                vm.peopleService.deletePerson(id)
                    .then(_deleteSuccess, _deleteFailed);
            }
        }

        //Delete Success - Splice from array
        function _deleteSuccess() {
            vm.peopleData.splice(vm.index, 1);
            vm.index = -1;
        }

        function _deleteFailed(error) {
            console.log(error);
        }
    }
})();