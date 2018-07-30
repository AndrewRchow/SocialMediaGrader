(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('configDataIndexController', configDataIndexController);

    configDataIndexController.$inject = ['$scope', 'configDataService', '$window'];

    function configDataIndexController($scope, configDataService, $window) {
        var vm = this;
        vm.$onInit = _init;
        vm.configDataService = configDataService;
        vm.item = [];
        vm.addBtn = _addButton;
        vm.editBtn = _editButton;
        vm.deleteBtn = _deleteBtn;
        vm.currentIndex;

        function _init() {
            vm.configDataService.getAllCategories()
                .then(_getAllCategoriesSuccess, _getAllCategoriesError);
        }
        function _getAllCategoriesSuccess(r) {
            vm.item = r.data.items;
            console.log(vm.item);
        }
        function _getAllCategoriesError(r) {
            console.log(r, ":(");
        }
        function _addButton() {
            $window.location.href = '/admin/configcategory/manage';
        }
        function _editButton(Id) {
            $window.location.href = '/admin/configcategory/' + Id + '/edit/';
        }
        function _deleteBtn(Id, index) {
            vm.currentIndex = index;
            vm.configDataService.deleteConfigDataSettings(Id)
                .then(_deleteConfigSuccess, _deleteConfigError);
        }
        function _deleteConfigSuccess(r) {
            console.log(r);
            alert('Delete Successful');
            vm.item.splice(vm.currentIndex, 1);
        }
        function _deleteConfigError(r) {
            console.log(r, ":(");
        }
    }
})();