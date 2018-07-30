(function () {
    'use strict';

    angular.module('mainApp')
        .controller('manageController', manageController);

    manageController.$inject = ['$scope', '$window', 'configService', 'genericService'];

    function manageController($scope, $window, configService, genericService) {
        var vm = this;
        vm.$scope = $scope;
        vm.backBtn = _backBtn;
        vm.submitBtn = _submitBtn;
        vm.storeConfig = {};
        vm.Id = parseInt($("#modelId").val());
        vm.configService = configService;
        vm.genericService = genericService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.ConfigSettingsById = _ConfigSettingsById;
        vm.dataCategoriesDisplayName = [];
        vm.configCategoriesDisplayName = [];
        vm.selectedItem = 0;

        function _init() {

            //DataCategories
            vm.configService.getAllDataCategories()
                .then(_getDataSuccess, _getDataError);


            function _getDataSuccess(r) {
                vm.dataCategoriesDisplayName = r.data.items;
                vm.newArry = vm.dataCategoriesDisplayName.concat(vm.storeConfig);
                console.log("Data Categories: ", vm.newArry);
            }
            function _getDataError(r) {
                console.log(r, ":(");
            }
            if (vm.Id > 0) {
                vm.configService.getConfigSettingsById(vm.Id)
                    .then(_getConfigSettingsByIdSuccess, _getConfigSettingsByIdError);
            }
            //ConfigCategory
            vm.configService.getAllConfigCategories()
                .then(_getConfigSuccess, _getConfigError);
        }
        function _getConfigSuccess(r) {
            vm.configCategoriesDisplayName = r.data.items;
            console.log("Config Categories: ", vm.configCategoriesDisplayName);
        }
        function _getConfigError(r) {
            console.log(r, ":(");
        }

        function _getConfigSettingsByIdSuccess(r) {
            //console.log(r.data.item);
            vm.storeConfig = r.data.item;
            console.log("Get By Id Data: ", vm.storeConfig);
            _currentUser();
        }
        function _getConfigSettingsByIdError(r) {
            console.log(r, ":(");
        }
        function _ConfigSettingsById(id) {
            vm.configService.getConfigSettingsById(vm.storeConfig.Id)
                .then(_getConfigSettingsByIdSuccess, _getConfigSettingsByIdError);
        }
        function _backBtn() {
            $window.location.href = '/admin/config/index';
        }
        function _submitBtn(isValid) {
            if (isValid) {
                if (vm.Id > 0) {
                    vm.configService.putConfigSettings(vm.storeConfig, vm.Id)
                        .then(_postConfigSuccess, _postConfigError);
                } else {

                    vm.configService.postConfigSettings(vm.storeConfig)
                        .then(_postConfigSuccess, _postConfigError);
                }
            }
        }
        function _postConfigSuccess(r) {
            $window.location.href = '/admin/config/index';
        }
        function _postConfigError(r) {
            console.log(r, ":(");
        }

        function _currentUser() {
            vm.genericService.get("/api/auth/current")
                .then(_getUserSuccess, _getUserError);
        }

        function _getUserSuccess(r) {
            vm.storeConfig.userId = r.data.id.toString();
            console.log("User ID: ", vm.storeConfig.userId);
            console.log('New storeConfig Data: vm.storeConfig', vm.storeConfig);
        }

        function _getUserError(r) {
            console.log(r, ":(");
        }
    }
})(); 