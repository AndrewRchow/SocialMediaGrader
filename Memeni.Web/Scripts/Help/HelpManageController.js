(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('manageController', manageController);

    manageController.$inject = ['$scope', 'genericService', 'toastr', '$window'];

    function manageController($scope, genericService, toastr, $window) {

        var vm = this;
        vm.genericService = genericService;
        vm.$onInit = _init;
        vm.config = [{
            "displayLength": "10",
            "displayStart": "0",
            "sortCol": "0",
            "sortDir": "asc",
            "search": ""
        }];
        vm.item = [];
        vm.categories = [];
        vm.singleItem;
        vm.itemId;
        vm.index;
        vm.editIndex;
        vm.admin;
        vm.addNewItem = _addNewItem;
        vm.confirmEdit = _confirmEdit;
        vm.loadSelectOptions = _loadSelectOptions;
        vm.editClick = _editClick;
        vm.deleteClick = _deleteClick;
        vm.clear = _clear;

        // pagination
        vm.page = 1;
        vm.showingFirst;
        vm.showingLast;
        vm.showing = _showing;
        vm.searchTerm = _searchTerm;
        vm.displayPage = _displayPage;
        vm.resetFilter = _resetFilter;
        vm.sortOrder = _sortOrder;
        vm.pagination = _pagination;

        // emit data for updating category drop down
        $scope.$on('CATEGORIES', function (event, data) {
            //adjusting the select options
            //everytime a change is made 
            //in the manager
            vm.categories = [];
            for (var i = 0; i < data.length; i++) {
                var newSelectOption = {
                    label: data[i].name,
                    value: data[i].id
                };
                //add items to a view model array
                vm.categories[i] = newSelectOption;
            }
        });
        
        //====[ THE FOLD ]===============================================

        function _init() {

            // load vm.categories
            // to load select options
            vm.loadSelectOptions(); 

            // load items from SQL DB onto grid
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);

            // get the current admin identity
            // to add to the payload for a new
            // post or a new edit
            vm.genericService.get("/api/auth/current/roles/admin")
                .then(_getAdminGood)
                .catch(_getAdminBad);
        }

        function _gridPostGood(response) {
            // gets all items from DB
            // very important!
            vm.item = response.data.item;
            vm.showing();
        }
        function _gridPostBad(error) {
            console.log(error, "error grid post");
        }
        function _getAdminGood(response) {
            // sets the current admin indetifier
            vm.admin = response.data.name;
        }
        function _getAdminBad(error) {
            console.log(error);
        }

        function _loadSelectOptions() {          
            //call to grab all help categories
            vm.genericService.get("/api/help/categories")
                .then(getCategoriesSuccess)
                .catch(getCategoriesFail)
        }
        function getCategoriesSuccess(response) {
            //placeholder for raw data from server
            var tempCategories = response.data.items;

            //create objects in the format of select options
            for (var i = 0; i < tempCategories.length; i++) {
                var newSelectOption = {
                    label: tempCategories[i].name,
                    value: tempCategories[i].id
                };
                //add items to a view model array
                vm.categories[i] = newSelectOption;
            }
        }
        function getCategoriesFail(error) {
            console.log(error);
        }

        // ON-CLICK FUNCTIONS & RESPONSES

        function _addNewItem() {
            // this sets the DispName value in the payload
            // by using the HelpCategoryId as a reference and
            // searching the vm.categories array for a match
            for (var i = 0; i < vm.categories.length; i++) {
                if (vm.singleItem.helpCategoryId == vm.categories[i].value) {
                    vm.singleItem.dispName = vm.categories[i].label;
                }
            }

            // vm.admin value is retrieved on init used here
            vm.singleItem.modifiedBy = vm.admin;

            // call to post new item and pass in payload
            vm.genericService.post("/api/help", vm.singleItem)
                .then(_postGood)
                .catch(_postBad);
        }
        function _postGood(response) {
            //get the id of the item after posting
            vm.singleItem.id = response.data.item;
            // set the modifiedBy value
            vm.singleItem.modifiedBy = vm.admin;
            //push the item to the array
            vm.item.data.push(vm.singleItem);
            // clear the object
            console.log(vm.singleItem);
            vm.singleItem = {};

            toastr.success("Item Added Successfully");
        }
        function _postBad(error) {
            console.log(error)
        }

        function _editClick(id, index) {
            // place holder for id for
            // call to update function
            vm.itemId = id;
            vm.editIndex = index;

            // gather item info to be loaded
            // into the edit form on btn click
            vm.genericService.getById("/api/help/", id)
                .then(_getByIdGood)
                .catch(_getByIdBad);
        }
        function _getByIdGood(response) {
            // data to populate edit form
            vm.singleItem = response.data.item;
        }
        function _getByIdBad(error) {
            console.log(error);
        }

        function _confirmEdit() {
            // this sets the DispName value in the payload
            // by using the HelpCategoryId as a reference and
            // searching the vm.categories array for a match
            for (var i = 0; i < vm.categories.length; i++) {
                if (vm.singleItem.helpCategoryId == vm.categories[i].value) {
                    vm.singleItem.dispName = vm.categories[i].label;
                }
            }

            // vm.admin value is retrieved on init used here
            vm.singleItem.modifiedBy = vm.admin;

            // call to update the current item
            vm.genericService.put("/api/help/", vm.itemId, vm.singleItem)
                .then(_putGood)
                .catch(_putBad);
        }
        function _putGood(response) {
            // change the data in the table to reflect the edit
            vm.item.data[vm.editIndex].title = vm.singleItem.title;
            vm.item.data[vm.editIndex].helpMsg = vm.singleItem.helpMsg;
            vm.item.data[vm.editIndex].dispName = vm.singleItem.dispName;

            toastr.success("Item Edited Successfully");
        }
        function _putBad(error) {
            console.log(error);
        }

        function _deleteClick(id, index) {
            // storing the placeholder
            // for array index of item
            // being deleted
            vm.index = index;
            // call to delete item
            vm.genericService.delete("/api/help/", id)
                .then(_deleteGood)
                .catch(_deleteBad);
        }
        function _deleteGood(response) {
            console.log(response);
            // item is spliced and removed from view
            vm.item.data.splice(vm.index, 1);
            toastr.success("Item Deleted Successfully");
        }
        function _deleteBad(error) {
            console.log(error);
        }
           
        function _clear() {
            //this clears the payload object so
            // data in edit form is removed from
            // the new item form
            vm.singleItem = {};
        }

        // sql server side sorting/pagination

        function _showing() {
            vm.showingFirst = parseInt(vm.config[0].displayStart) + 1;
            vm.showingLast = vm.showingFirst + parseInt(vm.config[0].displayLength) - 1;
            if (vm.showingLast > vm.item.recordsFiltered) {
                vm.showingLast = vm.item.recordsFiltered;
            }
            return;
        }

        function _searchTerm(text) {
            vm.config[0].search = text;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);
            return;
        }

        function _resetFilter() {
            vm.config[0].search = "";
            $("#searchTxt").val("");
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);
            return;
        }

        function _pageNumber() {
            if (vm.config[0].displayStart == 0) {
                vm.page = 1;
            } else {
                vm.page = 1 + Math.floor(vm.config[0].displayStart / vm.config[0].displayLength);
            }
            return;
        }

        function _displayPage(number) {
            vm.config[0].displayLength = number;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);
            return;
        }

        function _pagination(page) {
            switch (page) {
                case 'start':
                    vm.config[0].displayStart = 0;
                    break;
                case 'previous':
                    vm.config[0].displayStart = vm.config[0].displayStart - vm.config[0].displayLength;
                    if (vm.config[0].displayStart < 0) {
                        vm.config[0].displayStart = 0;
                    }
                    break;
                case 'next':
                    vm.config[0].displayStart = parseInt(vm.config[0].displayStart) + parseInt(vm.config[0].displayLength);
                    break;
                case 'end':
                    vm.config[0].displayStart = vm.item.recordsFiltered - (vm.item.recordsFiltered % vm.config[0].displayLength);
                    break;
            }
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);
            _pageNumber();
            return;
        } 

        function _sortOrder(col) {
            if (vm.config[0].sortCol == col) {
                if (vm.config[0].sortDir == 'asc') {
                    vm.config[0].sortDir = 'desc';
                } else {
                    vm.config[0].sortDir = 'asc';
                }
                return vm.genericService.post("/api/help/grid", vm.config[0])
                    .then(_gridPostGood)
                    .catch(_gridPostBad);
            }
            vm.config[0].sortCol = col;
            vm.config[0].sortDir = 'asc';
            vm.genericService.post("/api/help/grid", vm.config[0])
                .then(_gridPostGood)
                .catch(_gridPostBad);
            return;
        }
}})();