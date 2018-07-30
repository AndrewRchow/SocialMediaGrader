(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("indexController", IndexController);

    // Inject services
    IndexController.$inject = ["$scope", "$window", "service", "$sanitize"];

    function IndexController($scope, $window, service, $sanitize) {
        var vm = this;

        // View model
        vm.item = {};
        vm.items = [];
        vm.targetList = [];
        vm.$scope = $scope;
        vm.service = service;
        vm.$onInit = _init;
        vm.add = _add;
        vm.edit = _edit;
        vm.delete = _delete;
        vm.compare = _compare;
        vm.changeOrderButton = _changeOrderButton;

        // The Fold
        // Get All on Initialize
        function _init() {
            vm.service.tncGetAll()
                .then(_tncGetAllSuccess, _tncGetAllError);
        }
        // Get All Success
        function _tncGetAllSuccess(res) {
            console.log(res);
            vm.items = res.data.items;
            vm.items.sort(_compare);
        }
        // Get All Error
        function _tncGetAllError(err) {
            console.log(err);
        }
        // Delete on Click
        function _delete(id, index) {
            vm.index = index;
            vm.service.tncDelete(id)
                .then(_tncDeleteSuccess, _tncDeleteError);
        }
        // Delete Success
        function _tncDeleteSuccess(res) {
            console.log(res);
            vm.items.splice(vm.index, 1);
            vm.index = -1;
        }
        // Delete Error
        function _tncDeleteError(err) {
            console.log(err);
        }
        // Add new entry
        function _add() {
            $window.location.href = "/admin/terms/create";
        }
        // Edit existing entry
        function _edit(id) {
            vm.item.id = id;
            $window.location.href = "/admin/terms/" + vm.item.id + "/edit";
        }
        // Sort Function
        function _compare(a, b) {
            if (a.displayOrder < b.displayOrder) {
                return -1;
            }
            if (a.displayOrder > b.displayOrder) {
                return 1;
            }
            return 0;
        }
        // Drag and Drop Function
        vm.$scope.onDrop = function (srcList, srcIndex, targetList, targetIndex) {
            // Copy the item from source to target.
            targetList.splice(targetIndex, 0, srcList[srcIndex]);
            // Remove the item from the source, possibly correcting the index first.
            // We must do this immediately, otherwise ng-repeat complains about duplicates.
            if (srcList == targetList && targetIndex <= srcIndex) srcIndex++;
            srcList.splice(srcIndex, 1);
            // By returning true from dnd-drop we signalize we already inserted the item.
            console.log(targetList);
            vm.targetList = targetList;

            return true;
        };
        // Change display order after drag and drop
        function _changeOrderButton() {
            if (vm.targetList.length > 0) {
                var lists = $(".unorderedList").find(".ibox");
                for (var i = 0; i < lists.length; i++) {
                    vm.targetList[i].displayOrder = (i);
                }
                console.log(vm.targetList);
                vm.service.tncPutAll(vm.targetList)
                    .then(_tncPutAllSuccess, _tncPutAllError);
            }
        }
        // Put All Success
        function _tncPutAllSuccess(res) {
            console.log(res);
        }
        // Put All Error
        function _tncPutAllError(err) {
            console.log(err);
        }
    }
})();