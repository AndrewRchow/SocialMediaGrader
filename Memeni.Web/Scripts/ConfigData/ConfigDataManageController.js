(function () {
    'use strict';

    angular.module('mainApp')
        .controller('configDataManageController', configDataManageController);

    configDataManageController.$inject = ['$scope', '$window', 'configDataService1'];

    function configDataManageController($scope, $window, configDataService1) {
        var vm = this;
        vm.$scope = $scope;
        vm.backBtn = _backBtn;
        vm.submitBtn = _submitBtn;
        vm.storeConfigData = {};
        vm.Id = parseInt($("#modelId").val());
        vm.configDataService1 = configDataService1;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.ConfigDataSettingsById = _ConfigDataSettingsById;

        function _init() {
            if (vm.Id > 0) {
                vm.configDataService1.getConfigDataSettingsById(vm.Id)
                    .then(_getConfigDataSettingsByIdSuccess, _getConfigDataSettingsByIdError);
            }
        }

        function _getConfigDataSettingsByIdSuccess(r) {
            console.log(r.data.item);
            return vm.storeConfigData = r.data.item;

        }
        function _getConfigDataSettingsByIdError(r) {
            console.log(r, ":(");
        }
        function _ConfigDataSettingsById(id) {
            vm.configDataService1.getConfigDataSettingsById(vm.storeConfigData.Id)
                .then(_getConfigSettingsByIdSuccess, _getConfigSettingsByIdError);
        }
        function _backBtn() {
            $window.location.href = '/admin/configdata/index';
        }
        function _submitBtn(isValid) {
            if (isValid) {
                if (vm.Id > 0) {
                    vm.configDataService1.putConfigDataSettings(vm.storeConfigData, vm.Id)
                        .then(_configDataSettingsSuccess, _configDataSettingsError);
                } else {
                    vm.configDataService1.postConfigDataSettings(vm.storeConfigData)
                        .then(_configDataSettingsSuccess, _configDataSettingsError);
                }
            }
        }
        function _configDataSettingsSuccess(r) {
            console.log(r);
            $window.location.href = '/admin/configdata/index';
        }
        function _configDataSettingsError(r) {
            console.log(r, ":(");
        }
    }
})(); 