//Manage Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('smeManageController', smeManageController);
    smeManageController.$inject = ['$scope', 'smeService', '$window', '$location'];
    function smeManageController($scope, smeService, $window, $location) {

        var vm = this;
        vm.$scope = $scope;
        vm.smeService = smeService;
        vm.$onInit = _init;
        vm.submit = _submit;
        vm.selectById = _selectById;
        vm.addForm = {};
        vm.$window = $window;
        vm.$location = $location;
        vm.id;

        function _init() {
            vm.id = $('#modelId').val();
            console.log(vm.id);
            if (vm.id !== 0) {
                _selectById(vm.id);
            }
            return;
        }
        function _submit() {
            if (vm.id !== 0) {
                vm.addForm.id = vm.id;
                return vm.smeService.put(vm.addForm, vm.id)
                    .then(_putComplete, _putError);
            } else {
                vm.smeService.post(vm.addForm)
                    .then(_postComplete, _postError);
            }
            return;
        }
        function _selectById(id) {
            return vm.smeService.getById(id)
                .then(_selectByIdComplete, _selectByIdError);
        }
        function _selectByIdComplete(data) {
            vm.addForm = data.data.item;
            return;
        }
        function _selectByIdError(err) {
            console.log(err);
            return;
        }
        function _postComplete(resp) {
            console.log(resp);
            vm.addForm = {};
            return;
        }
        function _postError(err) {
            console.log(err);
            return;
        }
        function _putComplete(resp) {
            console.log(resp);
            $window.location.href = '/SME/Index';
            return;
        }
        function _putError(err) {
            console.log(err);
            return;
        }
    }
})();