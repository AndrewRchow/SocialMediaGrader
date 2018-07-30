(function () {

    angular
        .module('mainApp')
        .controller("ManageController", ManageController);

    ManageController.$inject = ["$scope", "$window", "UrlService"];

    function ManageController($scope, $window, UrlService) {

        var vm = this;
        vm.$scope = $scope;
        vm.backButton = _backButton;
        vm.storeUrl = {};
        vm.storeUrl.id = parseInt($("#modelId").val());
        vm.UrlService = UrlService;
        vm.submitButton = _submitButton;
        vm.$window = $window;
        vm.$onInit = _init;

        function _init() {       
            if (vm.storeUrl.id > 0) {
                vm.UrlService.getUrlAddrById(vm.storeUrl.id)
                    .then(_getUrlAddrByIdSuccess, _getUrlAddrByIdError);
            }
        }
        function _getUrlAddrByIdSuccess(response) {
            console.log(response);
            vm.storeUrl = response.data.item;
        }
        function _getUrlAddrByIdError(response) {
            console.log(response);
        }


        function _backButton() {
            $window.location.href = "/UrlAddress/Index";
        }

        function _submitButton(isValid) {
            if (isValid) {
                if (vm.storeUrl.id > 0) {
                    vm.UrlService.putUrlAddr(vm.storeUrl, vm.storeUrl.id)
                        .then(_urlAddrSuccess, _urlAddrError)
                }
                else {
                    vm.UrlService.postUrlAddr(vm.storeUrl)
                        .then(_urlAddrSuccess, _urlAddrError)
                }
            }
        }
        function _urlAddrSuccess(response) {
            console.log(response);
            $window.location.href = "/UrlAddress/Index";
        }
        function _urlAddrError(response) {
            console.log(response);
        }
    }
})();