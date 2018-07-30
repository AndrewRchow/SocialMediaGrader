(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('helpCategoriesController', helpCategoriesController);

    helpCategoriesController.$inject = ['$scope', 'genericService', 'toastr'];

    function helpCategoriesController($scope, genericService, toastr) {

        var vm = this;
        vm.$onInit = _init;
        vm.items = [];
        vm.singleItem;
        vm.index;
        vm.admin;

        vm.genericService = genericService;
        vm.saveEditClick = _saveEditClick;
        vm.submitCategory = _submitCategory;
        vm.deleteClick = _deleteClick;
        
        //====[ THE FOLD ]=======================================

        function _init() {
            // gets the categories to be loaded onto the page
            vm.genericService.get("/api/helpcategories")
                .then(_getCategoriesGood)
                .catch(_getCategoriesBad);

            // this get the admin identity
            vm.genericService.get("/api/auth/current/roles/admin")
                .then(_getAdminGood)
                .catch(_getAdminBad);
        }
        function _getCategoriesGood(response) {
            vm.items = response.data.items;
        }
        function _getCategoriesBad(error) {
            console.log(error)
        }
        function _getAdminGood(response) {
            // placeholder for admin value
            vm.admin = response.data.name;
        }
        function _getAdminBad(error) {
            console.log(error, "get admin bad");
        }

        function _saveEditClick(item) {
            // if admin is different, changes the modifiedBy value in item
            item.modifiedBy = vm.admin;

            vm.genericService.put("/api/helpcategories/", item.id, item)
                .then(_editCategoryGood)
                .catch(_editCategoryBad);
        }
        function _editCategoryGood(response) {
            console.log(response);
            $scope.$emit('CATEGORIES', vm.items);
            toastr.success("Category Edited Successfully");
        }
        function _editCategoryBad(error) {
            console.log(error);
        }

        function _submitCategory() {
            // set the modifiedBy value in the payload
            // after getting admin identity
            vm.singleItem.modifiedBy = vm.admin;

            // post the new category to the DB
            vm.genericService.post("/api/helpcategories", vm.singleItem)
                .then(_postCategoryGood)
                .catch(_postCategoryBad);
        }
        function _postCategoryGood(response) {

            // set the id recieved from server response
            vm.singleItem.id = response.data.item;
            // set the modifiedBy value
            vm.singleItem.modifiedBy = vm.admin
            // push new category to array
            vm.items.push(vm.singleItem);
            //clear the object
            vm.singleItem = {};
            //update the list
            $scope.$emit('CATEGORIES', vm.items);

            toastr.success("New Category Added");
        }
        function _postCategoryBad(error) {
            console.log(error);
        }

        function _deleteClick(id, index) {
            // storing the placeholder
            // for array index of item
            // being deleted
            console.log(id);
            vm.index = index;
            // call to delete item
            vm.genericService.delete("/api/helpcategories/", id)
                .then(_deleteGood)
                .catch(_deleteBad);
        }
        function _deleteGood(response) {
            console.log(response);
            // item is spliced and removed from view
            vm.items.splice(vm.index, 1);
            $scope.$emit('CATEGORIES', vm.items);
            toastr.success("Category Deleted");
        }
        function _deleteBad(error) {
            console.log(error);
        }
}})();