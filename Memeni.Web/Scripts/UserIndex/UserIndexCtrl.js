(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('userIndexController', userIndexController);

    userIndexController.$inject = ['$scope', 'genericService', '$window', 'toastr']

    function userIndexController($scope, genericService, $window, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.data = {};
        vm.item = {};
        vm.items = [];
        vm.file = {};
        vm.genericService = genericService;
        vm.edit = _edit;
        vm.saveProfilePerson = _saveProfilePerson;
        vm.saveProfileCompany = _saveProfileCompany;
        vm.uploadIcon = _uploadIcon;
        vm.saveProfilePhone = _saveProfilePhone;
        vm.toastr = toastr;
        vm.$onInit = _init;

        //--THE FOLD--

        function _init() {
            vm.genericService.get("/api/auth/current")
                .then(_loggedInCheckSuccess, _loggedInCheckError);    
            // Get call to seungs table
            // Get users urls and whether reports are on or off 
            // push urls into new vm
            // push checked db values into vm.switch
        }       

        //--CHECK FOR LOGGED IN USER SUCCESS / GET PROFILE BY ID IF ID EXISTS--
        function _loggedInCheckSuccess(response) {
            vm.data = response.data;
            vm.id = vm.data.id;
            $scope.$broadcast('userId', vm.id);
            $scope.$broadcast('userEmail', response.data.name)
            if (vm.id > 0) {
                vm.genericService.getById("/api/profile/", vm.id)
                    .then(_profileGetByIdSuccess, _profileGetByIdError);
            }
        }
        //--CHECK FOR LOGGED IN USER ERROR--
        function _loggedInCheckError(error) {
            console.log(error);
        }

        //--Get BY PROFILE ID SUCCESS--
        function _profileGetByIdSuccess(response) {
            vm.item = response.data.item;
            $scope.$broadcast('firstName', vm.item.firstName);
            vm.switchFacebook = { "Id": "f" + vm.id, "Email": vm.item.email, "Name": vm.facebookUrl, "Url": "facebook", "FirstName": vm.item.firstName }; // Change hard coded "Name" to users social media url 
            vm.switchTwitter = { "Id": "t" + vm.id, "Email": vm.item.email, "Name": vm.twitterUrl, "Url": "twitter", "FirstName": vm.item.firstName }; // ""
            //if (vm.item.companyId == 0) {
            //    $('#logoBtn').addClass('logoBtnInvis');
            //} else {
            //    $('#logoBtn').addClass('logoBtnVis');
            //}
            console.log("profileId:", vm.item.id);
        }
        //--Get BY PROFILE ID ERROR--
        function _profileGetByIdError(error) {
            console.log(error);
        }

        //--on edit icon click, opens modal to edit
        function _edit(id) {
            vm.item.id;
            vm.item.userId = vm.item.id;
            //$window.location.href = "/profile/" + vm.item.id + "/edit";
        }

        //--on img icon click, opens modal to add/edit
        function _uploadIcon(id) {
            vm.item.id;
            $window.location.href = "/user/home/upload/" + vm.item.id + "/edit";
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
            vm.toastr.success("Save successful!");          
        }

        //--Post/Put Error
        function _serviceCallError(error) {
            console.log(error);
            vm.toastr.error("There was an error saving changes.");
        }

        function _logOut() {
            vm.genericService.get("/api/auth/logout")
                .then(_logOutGood, _logOutBad);
        }

        function _logOutGood(resp) {
            $window.location.href = "/Home";
        };
        function _logOutBad(err) {
            console.log(err);
        }
    }
})();