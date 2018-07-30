(function () {
    "use strict";

    angular
        .module("mainApp")
        .controller("adminProfileIndexController", adminProfileIndexController);

    adminProfileIndexController.$inject = ["$scope", "genericService", "toastr"];

    function adminProfileIndexController($scope, genericService, toastr,) {
        var vm = this;
        vm.$scope = $scope
        vm.data = {};
        vm.item = {};
        vm.items = [];
        vm.genericService = genericService;
        vm.itemId = parseInt($("#modelId").val());
        vm.edit = _edit;
        vm.saveProfilePerson = _saveProfilePerson;
        vm.saveProfileCompany = _saveProfileCompany;
        vm.saveProfilePhone = _saveProfilePhone;
        vm.toastr = toastr;
        vm.$onInit = _init;

        //--THE FOLD--

        //--Get ID on click & Get ADMIN ID on page start up
        function _init() {
            vm.genericService.get("/api/auth/current/roles/admin")
                .then(_adminAuthSuccess, _adminAuthError);

            if (vm.itemId > 0) {
                vm.genericService.getById("/api/profile/", vm.itemId)
                    .then(_profileGetByIdSuccess, _profileGetByIdError);
            }
        }

        //--Get BY ADMIN SUCCESS--
        function _adminAuthSuccess(response) {
            //console.log(response);
            vm.data = response.data;
            //vm.data.modifiedBy = vm.data.name;
            console.log("logged in admin:", vm.data.name)
        }
        //--Get BY ADMIN ERROR--
        function _adminAuthError(error) {
            console.log(error);
        }

        //--Get BY PROFILE ID SUCCESS--
        function _profileGetByIdSuccess(response) {
            //console.log(response);
            vm.item = response.data.item;
            console.log("profileId:", vm.item.id);
        }
        //--Get BY PROFILE ID ERROR--
        function _profileGetByIdError(error) {
            console.log(error);
        }

        //--on add click, opens modal to edit
        function _edit(id) {
            vm.item.id;
            vm.item.userId = vm.item.id;
            console.log("edit profileId: ", vm.item.id);
        }

        //--POST/PUT PERSON PROFILE function 
        function _saveProfilePerson() {
            if (vm.item.personId === 0) {
                vm.item.personId = vm.item.userId;
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.postById("/api/profile/person/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            } else {
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.put("/api/profile/person/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            }
        }

        //--POST/PUT COMPANY PROFILE function 
        function _saveProfileCompany() {
            if (vm.item.companyId === 0) {
                vm.item.companyId = vm.item.userId;
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.postById("/api/profile/company/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            } else {
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.put("/api/profile/company/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            }
        }

        //--POST/PUT PHONE PROFILE function 
        function _saveProfilePhone() {
            if (vm.item.phoneId === 0) {
                vm.item.phoneId = vm.item.userId;
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.postById("/api/profile/phone/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            } else {
                vm.item.userId = vm.item.id;
                vm.item.modifiedBy = vm.data.name;
                vm.genericService.put("/api/profile/phone/", vm.item.userId, vm.item)
                    .then(_serviceCallSuccess, _serviceCallError);
            }
        }

        //--Post/Put Success
        function _serviceCallSuccess(response) {
            console.log("success", response);
            vm.toastr.success("Save successful!");
        }

        //--Post/Put Error
        function _serviceCallError(error) {
            console.log(error);
            vm.toastr.error("There was an error saving changes.");
        }
    }
})();