(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('layoutHelpController', layoutHelpController);

    layoutHelpController.$inject = ['$scope', 'genericService', 'toastr', '$window'];

    function layoutHelpController($scope, genericService, toastr, $window) {

        var vm = this;
        vm.genericService = genericService;
        vm.currentPageHelpItems = [];
        // this val is passed through razor & jQ
        vm.pageUrl = $("#pageUrlTxt").val();
        vm.saveEditClick = _saveEditClick;
        vm.deleteClick = _deleteClick;
        vm.getHelpData = _getHelpData;
        vm.admin;
        vm.index;
        vm.$onInit = _init;
        vm.logOut = _logOut;

//====[ THE FOLD ]=======================================

        function _init() {
            // gets admin identity on page load
            // if an admin is logged in
            if ($("#bLoggedIn").val() === "true") {
                vm.genericService.get("/api/auth/current/roles/admin")
                    .then(_getAdminGood)
                    .catch(_getAdminBad);
            }
        }
        function _getAdminGood(response) {
            // set current admin identity
            vm.admin = response.data.name;
        }
        function _getAdminBad(error) {
            console.log(error);
        }

        function _getHelpData() {

            // empty the array so as not
            // to load the same help items
            // on the page on repeated btn clicks
            vm.currentPageHelpItems = [];

            // get relevant data based on url path
            vm.genericService.postById("/api/help/items?pageUrl=", vm.pageUrl)
                .then(_getHelpDataGood)
                .catch(_getHelpDataBad);
        }
        function _getHelpDataGood(response) {
            // array is populated with relevant data
            vm.currentPageHelpItems = response.data.items;
        }
        function _getHelpDataBad(error) {
            console.log(error)
        }

        function _saveEditClick(item) {
            // if the admin identity is different
            // it is altered here
            item.modifiedBy = vm.admin;

            vm.genericService.put("/api/help/", item.id, item)
                .then(_saveEditGood)
                .catch(_saveEditBad);
        }
        function _saveEditGood(response) {
            toastr.success("Item Updated Successfully");
        }
        function _saveEditBad(error) {
            toastr.error("Error When Updating")
            console.log(error);
        }

        function _deleteClick(id, index) {
            //place holder for splice
            vm.index = index;
            // item is deleted from the page
            vm.genericService.delete("/api/help/" ,id)
                .then(_deleteGood)
                .catch(_deleteBad);
        }
        function _deleteGood(response) {
            vm.currentPageHelpItems.splice(vm.index, 1);
            toastr.success("Item Deleted Succesfully");
            console.log(response);
        }
        function _deleteBad(error) {
            toastr.error("Error When Deleting");
            console.log(error);
        }

        function _logOut() {
            vm.genericService.logOutUser()
                .then(_logOutGood, _logOutBad);
        }

        function _logOutGood(resp) {
            $window.location.href = "/Home/login";
        };
        function _logOutBad(err) {
            console.log(err);
        }

    }
})();