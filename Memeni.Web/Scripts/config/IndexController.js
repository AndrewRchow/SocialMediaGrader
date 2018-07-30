(function () {

    angular.module('mainApp')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['$scope', 'configService', 'genericService', '$window'];

    function IndexController($scope, configService, genericService, $window) {
        var vm = this;
        vm.$scope = $scope;
        vm.item = {};
        vm.configService = configService;
        vm.genericService = genericService;
        vm.allSettings = [];
        vm.$onInit = _init;
        vm.postBtn = _postButton;
        vm.editBtn = _editButton;
        vm.deleteSettingsBtn = _deleteButton;
        vm.currentIndex;
        vm.getAllDataCategories;
        vm.dataCategories = [];
        vm.userId = '';


        function _init() {
            vm.configService.getAllConfigSettings()
                .then(_getAllConfigSettingsSuccess, _getAllConfigSettingsError);
        }
        function _getAllConfigSettingsSuccess(r) {
            //console.log(r);
            vm.allSettings = r.data.items;
            console.log("vm.allSettings: ", vm.allSettings);
            _currentUser();
        }
        function _getAllConfigSettingsError(r) {
            console.log(r, ":(");
        }
        function _postButton() {
            $window.location.href = '/admin/config/create';
        }
        function _editButton(Id) {
            $window.location.href = '/admin/config/' + Id + '/edit/';
        }
        function _deleteButton(Id, index) {
            vm.currentIndex = index;
            vm.configService.deleteConfigSettings(Id)
                .then(_deleteConfigSuccess, _deleteConfigError);
        }
        function _deleteConfigSuccess(r) {
            console.log(r);
            alert('Delete Successful');
            vm.allSettings.splice(vm.currentIndex, 1);
        }
        function _deleteConfigError(r) {
            console.log(r, ":(");
        }
        function _currentUser() {
            vm.genericService.get("/api/auth/current")
                .then(_getUserSuccess);
        }
        function _getUserSuccess(r) {
            vm.allSettings.userId = r.data.id.toString();
            console.log("User ID: ", vm.allSettings.userId);
            console.log("Updated allSettings", vm.allSettings);
        }
    }
})();