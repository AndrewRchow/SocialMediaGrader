(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("metaIndexController", MetaIndexController);

    // Inject services
    MetaIndexController.$inject = ["$scope", "$window", "genericService"];

    function MetaIndexController($scope, $window, genericService) {
        var vm = this;

        // View model
        vm.item = {};
        vm.items = [];
        vm.genericService = genericService;
        vm.$scope = $scope;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.addUrl = _addUrl;
        vm.delete = _delete;
        vm.edit = _edit;
        vm.newAdd = _newAdd;
        vm.view = _view;
        vm.lock = _lock;
        vm.unlock = _unlock;
        vm.metaUrl = '/api/metaurl/';
        vm.meta = '/api/meta/';
        vm.metaLock = '/api/metaurl/lock/';

        // The Fold
        // Get All on Initialize
        function _init() {
            vm.genericService.get(vm.metaUrl)
                .then(_metaGetAllSuccess, _metaGetAllError);
        }
        // Get All Success
        function _metaGetAllSuccess(res) {
            console.log(res.data.items);
            vm.items = res.data.items;
        }
        // Get All Error
        function _metaGetAllError(err) {
            console.log(err);
        }
        // Post/Put on Modal Submit
        function _addUrl($isValid) {
            if (vm.item.id) {
                vm.genericService.put(vm.metaUrl, vm.item.id, vm.item)
                    .then(_metaPutSuccess, _metaPostError);
            } else {
                vm.genericService.post(vm.metaUrl, vm.item)
                    .then(_metaPostSuccess, _metaPostError);
            }
        }
        // Post Success
        function _metaPostSuccess(res) {
            console.log(res);
            vm.item = {};
            vm.item.ownerTypeId = res.data.item;
            vm.genericService.postById(vm.meta, vm.item.ownerTypeId, vm.item)
                .then(_metaPostDefaultSuccess, _metaPostDefaultError);
        }
        // Put Success
        function _metaPutSuccess(res) {
            console.log(res);
        }
        // Post Error
        function _metaPostError(err) {
            console.log(err);
        }
        // Post Default Success
        function _metaPostDefaultSuccess(res) {
            console.log(res);
            $window.location.href = "/admin/meta/" + vm.item.ownerTypeId + "/tags";
        }
        // Post Default Error
        function _metaPostDefaultError(err) {
            console.log(err);
        }
        // Delete on Trash Button
        function _delete(id, index) {
            vm.index = index;
            vm.ownerTypeId = id;
            vm.genericService.delete(vm.meta, vm.ownerTypeId)
                .then(_metaDeleteTagsSuccess, _metaDeleteTagsError);
        }
        // Delete Tags Success
        function _metaDeleteTagsSuccess(res) {
            console.log(res);
            vm.genericService.delete(vm.metaUrl, vm.ownerTypeId)
                .then(_metaDeleteSuccess, _metadeleteError);
        }
        // Delete Tags Error
        function _metaDeleteTagsError(err) {
            console.log(err);
        }
        // Delete Success
        function _metaDeleteSuccess(res) {
            console.log(res);
            vm.genericService.get(vm.metaUrl)
                .then(_metaGetAllSuccess, _metaGetAllError);
        }
        // Delete Error
        function _metadeleteError(err) {
            console.log(err);
        }
        // Edit Button
        function _edit(item, index) {
            vm.item = item;
        }
        // Add New Url Button
        function _newAdd() {
            vm.item = {};
        }
        // View Button
        function _view(id) {
            $window.location.href = "/admin/meta/" + id + "/tags";
        }
        // Lock
        function _lock(id, lock, index) {
            vm.item = {};
            vm.item.id = id;
            vm.item.isLocked = lock;
            vm.index = index;
            vm.items[vm.index].isLocked = true;
            vm.genericService.put(vm.metaLock, vm.item.id, vm.item)
                .then(_metaPutLockSuccess, _metaPutLockError);
        }
        // Unlock
        function _unlock(id, lock, index) {
            vm.item = {};
            vm.item.id = id;
            vm.item.isLocked = lock;
            vm.index = index;
            vm.items[vm.index].isLocked = false;
            vm.genericService.put(vm.metaLock, vm.item.id, vm.item)
                .then(_metaPutLockSuccess, _metaPutLockError);
        }
        // Put Lock Success
        function _metaPutLockSuccess(res) {
            console.log(res);
        }
        // Put Lock Error
        function _metaPutLockError(err) {
            console.log(err);
        }
    }
})();