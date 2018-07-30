//Admin Error Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('TrackingController', TrackingController);

    TrackingController.$inject = ['$scope', 'genericService', '$window'];

    function TrackingController($scope, genericService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.config = [{
            "displayLength": "10",
            "displayStart": "0",
            "sortCol": "0",
            "sortDir": "asc",
            "search": ""
        }]
        vm.item = {};
        vm.selected = [];
        vm.searchTerm = _searchTerm;
        vm.displayPage = _displayPage;
        vm.id;
        vm.index;
        vm.sortOrder = _sortOrder;
        vm.pagination = _pagination;
        vm.page = 1;
        vm.showingFirst;
        vm.showingLast;
        vm.resetFilter = _resetFilter;
        vm.exist = _exist;
        vm.toggleSelection = _toggleSelection;
        vm.checkAll = _checkAll;
        vm.selectAll;
        vm.deleteChecked = _deleteChecked;
        vm.showUrls = _showUrls;
        vm.UserUrls = [];
        vm.selectedUser = {};

        // the fold
        function _init() {
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure);
            return;
        }

        function _exist(items) {
            return vm.selected.indexOf(items) > -1;
        }

        function _toggleSelection(item) {
            var idx = vm.selected.indexOf(item);
            console.log(vm.selected);
            if (idx > -1) {
                vm.selected.splice(idx, 1);
            } else {
                vm.selected.push(item);
            }
        }

        function _checkAll() {
            if (vm.selectAll == true) {
                angular.forEach(vm.item.data, function (item) {
                    var idx = vm.selected.indexOf(item);
                    if (idx >= 0) {
                        return true;
                    } else {
                        vm.selected.push(item.id)
                    }
                })
            } else {
                vm.selected = [];
            }
        }

        function _trackingPostSuccess(item) {
            console.log(item.data.item);
            vm.item = item.data.item;
            //vm.itemId = item.id;
            _showing();
            return item;
        }

        function _trackingPostFailure(err) {
            return console.log(err, "user grid post bad");
        }

        function _searchTerm(text) {
            vm.config[0].search = text;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure)
            return;
        }

        function _resetFilter() {
            vm.config[0].search = "";
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure);
            return;
        }
        function _displayPage(number) {
            vm.config[0].displayLength = number;
            vm.config[0].displayStart = 0;
            vm.page = 1;
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure);
            return;
        }
        function _sortOrder(col) {
            if (vm.config[0].sortCol == col) {
                if (vm.config[0].sortDir == 'asc') {
                    vm.config[0].sortDir = 'desc';
                } else {
                    vm.config[0].sortDir = 'asc';
                }
                return vm.genericService.post("/api/tracking/grid/", vm.config[0])
                    .then(_trackingPostSuccess, _trackingPostFailure);
            }
            vm.config[0].sortCol = col;
            vm.config[0].sortDir = 'asc';
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure);
            return;
        }

        function _pagination(page) {
            switch (page) {
                case 'start':
                    vm.config[0].displayStart = 0;
                    break;
                case 'previous':
                    vm.config[0].displayStart = vm.config[0].displayStart - vm.config[0].displayLength;
                    if (vm.config[0].displayStart < 0) {
                        vm.config[0].displayStart = 0;
                    }
                    break;
                case 'next':
                    vm.config[0].displayStart = parseInt(vm.config[0].displayStart) + parseInt(vm.config[0].displayLength);
                    break;
                case 'end':
                    vm.config[0].displayStart = vm.item.recordsFiltered - (vm.item.recordsFiltered % vm.config[0].displayLength);
                    break;
            }
            vm.genericService.post("/api/tracking/grid/", vm.config[0])
                .then(_trackingPostSuccess, _trackingPostFailure);
            _pageNumber();
            return;
        }
        function _pageNumber() {
            if (vm.config[0].displayStart == 0) {
                vm.page = 1;
            } else {
                vm.page = 1 + Math.floor(vm.config[0].displayStart / vm.config[0].displayLength);
            }
            return;
        }
        function _showing() {
            vm.showingFirst = parseInt(vm.config[0].displayStart) + 1;
            vm.showingLast = vm.showingFirst + parseInt(vm.config[0].displayLength) - 1;
            if (vm.showingLast > vm.item.recordsFiltered) {
                vm.showingLast = vm.item.recordsFiltered;
            }
            return;
        }

        function _checkbox(id, index) {
            console.log(id);
            console.log(index);
            vm.checkedItems = id;
        }

        function _deleteChecked() {
            console.log("delete checked clicked");
            console.log(vm.selected);
            var ids = vm.selected;

            var deleteIds = "";

            for (var i = 0; i < ids.length; i++) {
                deleteIds += ("ids=" + ids[i] + "&");
            }

            (function () {
                swal({
                    title: "Are you sure?",
                    text: "This will delete selected items",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete items",
                    cancelButtonText: "No, cancel",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            vm.genericService.delete("/api/tracking/deleteselected?", deleteIds)
                                .then(_deleteCheckedSuccess, _deleteCheckedFailure);

                            function _deleteCheckedSuccess(response) {
                                console.log(response);
                                _init();
                                vm.selected = [];
                                vm.selectAll = false;
                                swal("Deleted!", "Selected items have been deleted", "success");
                            }
                            function _deleteCheckedFailure(err) {
                                console.log(err);
                                swal("Failed!", "Failed to delete items", "error");
                            }
                        } else {
                            swal("Cancelled", "", "error");
                        }
                    });
            })();
        }

        function _showUrls(email) {
            var data = {
                "Email": email
            };
            var emailName = email.substr(0, email.indexOf('@')); 
            var emailName = emailName.charAt(0).toUpperCase() + emailName.slice(1);
            vm.selectedUser = emailName
            vm.genericService.post("/api/tracking/UserUrls", data)
                .then(_showUrlsSuccess, _showUrlsFailure);
        }
        function _showUrlsSuccess(response) {
            vm.UserUrls = response.data.items;
        }
        function _showUrlsFailure(err) {
             console.log(err);
        }

        $scope.config = {
            autoHideScrollbar: false,
            theme: 'light',
            advanced: {
                updateOnContentResize: true
            },
            setHeight: 200,
            scrollInertia: 0
        }
    }
})();