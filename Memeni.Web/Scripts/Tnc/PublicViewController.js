(function () {
    "use strict";

    angular
        .module("mainApp")
        .controller("publicViewController", PublicViewController);

    // Inject services
    PublicViewController.$inject = ["$scope", "$window", "service", "$sanitize"];

    function PublicViewController($scope, $window, service, $sanitize) {
        var vm = this;

        // View model
        vm.item = {};
        vm.items = [];
        vm.service = service;
        vm.$onInit = _init;

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
        }
        // Get All Error
        function _tncGetAllError(err) {
            console.log(err);
        }
    }
})();