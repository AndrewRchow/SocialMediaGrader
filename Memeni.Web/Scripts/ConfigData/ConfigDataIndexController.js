(function () {

    angular.module('mainApp')
        .controller('DataIndexController', DataIndexController);

    DataIndexController.$inject = ['$scope', 'configDataService1', '$window'];

    function DataIndexController($scope, configDataService1, $window) {
        var vm = this;
        vm.$scope = $scope;
        vm.configDataService1 = configDataService1;
        vm.item = [];
        vm.$onInit = _init;
        vm.postBtn = _postButton;
        vm.editBtn = _editButton;
        vm.deleteBtn = _deleteBtn;
        vm.currentIndex;
        vm.addBtn = _addBtn;

        function _init() {
            vm.configDataService1.getAllDataCategories()
                .then(_getAllDataSettingsSuccess, _getAllDataSettingsError);
        }
        function _getAllDataSettingsSuccess(r) {
            console.log(r.data.items);
            vm.item = r.data.items;
        }
        function _getAllDataSettingsError(r) {
            console.log(r, ":(");
        }
        function _postButton() {
            $window.location.href = '/admin/configdata/manage';
        }
        function _editButton(Id) {
            $window.location.href = '/admin/configdata/' + Id + '/edit/';
        }
        function _deleteBtn(Id, index) {
            vm.currentIndex = index;
            vm.configDataService1.deleteConfigDataSettings(Id)
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
        function _addBtn() {
            $window.location.href = '/admin/configdata/manage';
        }
    }

})();