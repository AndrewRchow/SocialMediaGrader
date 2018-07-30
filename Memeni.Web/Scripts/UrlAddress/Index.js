(function () {

    angular
        .module('mainApp')
        .controller("IndexController", IndexController);

    IndexController.$inject = ['$scope', 'UrlService', "$window"];

    function IndexController($scope, UrlService, $window) {
        var vm = this;
        vm.$scope = $scope;
        vm.UrlService = UrlService
        vm.allUrls = [];
        vm.storeUrl = {};
        vm.$onInit = _init;
        vm.postButton = _postButton;
        vm.editButton = _editButton;
        vm.deleteUrlButton = _deleteUrlButton;
        vm.currentIndex = null;
       

        function _init() {
            console.log("hi");
            vm.UrlService.getAllUrlAddr()
                .then(_getAllUrlAddrSuccess, _getAllUrlAddrError);
        }
        function _getAllUrlAddrSuccess(response) {
            console.log(response.data.items);
            vm.allUrls = response.data.items;
        }
        function _getAllUrlAddrError(data) {
            console.log(data);
        }
       
        function _postButton() {
            $window.location.href = "/UrlAddress/Create";
        }

        function _editButton(id) {
            $window.location.href = "/UrlAddress/" + id + "/edit/";
        }
      
        function _deleteUrlButton(id, index) {          
            vm.currentIndex = index;
            vm.UrlService.deleteUrlAddr(id)
                .then(_deleteUrlAddrSuccess, _deleteUrlAddrError);
        }
        function _deleteUrlAddrSuccess(response) {
            console.log(response);
            alert("Delete Success");
            vm.allUrls.splice(vm.currentIndex, 1);
        }
        function _deleteUrlAddrError(response) {
            console.log(response);
        }

        

    }
})();