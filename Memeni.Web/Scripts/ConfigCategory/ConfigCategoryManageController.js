(function () {
    'use strict';

    angular.module('mainApp')
        .controller('ConfigCategoryManageController', ConfigCategoryManageController);

    ConfigCategoryManageController.$inject = ['$scope', '$window', 'configDataService'];

    function ConfigCategoryManageController($scope, $window, configDataService) {
        var vm = this;
        vm.$scope = $scope;
        vm.backBtn = _backBtn;
        vm.submitBtn = _submitBtn;
        vm.storeConfigCategory = {};
        vm.Id = parseInt($("#modelId").val());
        vm.configDataService = configDataService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.ConfigSettingsById = _ConfigSettingsById;

        function _init() {
            if (vm.Id > 0) {
                vm.configDataService.getConfigCategorySettingsById(vm.Id)
                    .then(_getConfigCategorySettingsByIdSuccess, _getConfigCategorySettingsByIdError);
            }
        }

        function _getConfigCategorySettingsByIdSuccess(r) {
            console.log(r.data.item);
            return vm.storeConfigCategory = r.data.item;

        }
        function _getConfigCategorySettingsByIdError(r) {
            console.log(r, ":(");
        }
        function _ConfigSettingsById(id) {
            vm.configDataService.getConfigCategorySettingsById(vm.storeConfigCategory.Id)
                .then(_getConfigSettingsByIdSuccess, _getConfigSettingsByIdError);
        }
        function _backBtn() {
            $window.location.href = '/admin/configcategory/index';
        }
        function _submitBtn(isValid) {
            if (isValid) {
                if (vm.Id > 0) {
                    vm.configDataService.putConfigCategorySettings(vm.storeConfigCategory, vm.Id)
                        .then(_configCategorySettingsSuccess, _configCategorySettingsError);
                } else {
                    vm.configDataService.postConfigCategorySettings(vm.storeConfigCategory)
                        .then(_configCategorySettingsSuccess, _configCategorySettingsError);
                }
            }
        }
        function _configCategorySettingsSuccess(r) {
            console.log(r);
            $window.location.href = '/admin/configcategory/index';
        }
        function _configCategorySettingsError(r) {
            console.log(r, ":(");
        }
    }
})(); 