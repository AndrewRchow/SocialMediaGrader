(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("IndexController", IndexController);
   
    IndexController.$inject = ['$scope', 'PrivacyService', "$window", "toastr", "$location", "$anchorScroll", "$sanitize"];

    function IndexController($scope, PrivacyService, $window, toastr, $location, $anchorScroll, $sanitize) {
        var vm = this;
        vm.$scope = $scope;
        vm.PrivacyService = PrivacyService;
        vm.allPolicies = [];
        vm.$onInit = _init;
        vm.postButton = _postButton;
        vm.editButton = _editButton;
        vm.deleteButton = _deleteButton;
        vm.changeOrderButton = _changeOrderButton;
        vm.currentIndex = null;
        vm.viewsButton = _viewsButton;
        vm.adminButton = _adminButton;
        vm.targetList = [];
        vm.toastr = toastr;

        function _init() {
            vm.PrivacyService.getAllPrivacy()
                .then(_getAllPrivacySuccess, _getAllPrivacyError);
        }
        function _getAllPrivacySuccess(response) {
            for (var i = 0; i < response.data.items.length; i++) {
                console.log(response.data.items[i]);
            }
            vm.allPolicies = response.data.items;
            vm.allPolicies.sort(_compare);
        }
        function _getAllPrivacyError(data) {
            console.log(data);
        }

        function _postButton() {
            $window.location.href = "/Admin/PrivacyPolicy/Create";
        }

        function _editButton(id) {
            $window.location.href = "/Admin/PrivacyPolicy/" + id + "/edit/";
        }

        function _deleteButton(id, index) {
            window.confirm("Are you sure?");
            vm.currentIndex = index;
            vm.PrivacyService.deletePrivacy(id)
                .then(_deletePrivacySuccess, _deletePrivacyError);
        }
        function _deletePrivacySuccess(response) {
            console.log(response);
            vm.toastr.success("Delete Success.");
            vm.allPolicies.splice(vm.currentIndex, 1);
        }
        function _deletePrivacyError(response) {
            console.log(response);
            vm.toastr.Error("Delete Error.");
        }

        function _viewsButton() {
            $window.location.href = "/Privacy/Policies";
        }

        function _adminButton() {
            $window.location.href = "/Admin/Home/Index";
        }

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

        function _changeOrderButton() {
            if (vm.targetList.length > 0) {
                var lists = $(".unorderedList").find(".ibox");
                for (var i = 0; i < lists.length; i++) {
                    vm.targetList[i].displayOrder = (i);
                }
                console.log(vm.targetList);
                vm.PrivacyService.putPrivacyMultiple(vm.targetList)
                    .then(_putPrivacyMultipleSuccess, _putPrivacyMultipleError);
            }
        }
        function _putPrivacyMultipleSuccess(response) {
            console.log(response);
            vm.toastr.success('Order Change Success.');
        }
        function _putPrivacyMultipleError(response) {
            console.log(response);
            vm.toastr.error("Order Change Error");
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
    }
})();