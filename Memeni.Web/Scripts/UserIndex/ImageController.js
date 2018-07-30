(function () {
    "use strict"
    angular
        .module('mainApp')
        .controller('imageController', imageController)

    imageController.$inject = ['$scope', 'genericService', '$window', 'toastr', 'Upload', '$timeout']

    function imageController($scope, genericService, $window, toastr, Upload, $timeout) {
        var vm = this;
        vm.$scope = $scope;
        vm.data = {};
        vm.item = {};
        vm.items = [];
        vm.file = [];
        vm.genericService = genericService;

        vm.Upload = Upload;
        vm.$timeout = $timeout;
        vm.saveLogo = _saveLogo;
        vm.toastr = toastr;
        //vm.itemId = parseInt($("#itemId").val());
        vm.$onInit = _init;

        function _init() {
            vm.genericService.get("/api/auth/current")
                .then(_loggedInCheckSuccess, _loggedInCheckError);
        }

        //--CHECK FOR LOGGED IN USER SUCCESS / GET PROFILE BY ID IF ID EXISTS--
        function _loggedInCheckSuccess(response) {
            //console.log(response);
            vm.data = response.data;
            vm.id = vm.data.id;
            if (vm.id > 0) {
                vm.genericService.getById("/api/profile/", vm.id)
                    .then(_profileGetByIdSuccess, _profileGetByIdError);
            }
        }

        //--CHECK FOR LOGGED IN USER ERROR--
        function _loggedInCheckError(error) {
            console.log(error);
        }

        //--Get BY PROFILE ID SUCCESS--
        function _profileGetByIdSuccess(response) {
            //console.log(response);
            vm.item = response.data.item;
            vm.fileId = response.data.item.fileId;
        }
        //--Get BY PROFILE ID ERROR--
        function _profileGetByIdError(error) {
            console.log(error);
        }

        //--POST/PUT Logo
        function _saveLogo(file) {
            vm.item.coLogoId = vm.data.id;
            file.upload = Upload.upload({
                url: '/api/file/upload',
                data: { file: file },
                method: 'POST'
            });

            file.upload.then(function (response) {
                vm.cfsIds = { fileId: response.data.item, userId: vm.data.id };
                vm.userId = vm.data.id;
                vm.genericService.postById("/api/file/store/", vm.userId, vm.cfsIds)
                    .then(_postToCFSSuccess, _postToCFSError);
                $timeout(function () {
                    file.result = response.data;
                });
                function _postToCFSSuccess(response) {
                    //console.log("success", response);
                    $window.location.href = "/user/home/index";
                }
                function _postToCFSError(error) {
                    console.log(error)
                }
            },
                function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                });          
        }
    }
})();