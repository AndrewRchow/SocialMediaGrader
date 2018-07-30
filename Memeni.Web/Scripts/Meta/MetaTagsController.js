(function () {
    "use strict";
    angular
        .module("mainApp")
        .controller("metaTagsController", MetaTagsController);

    // Inject services
    MetaTagsController.$inject = ["$scope", "$window", "genericService"];

    function MetaTagsController($scope, $window, genericService) {
        var vm = this;

        // View model
        vm.item = {};
        vm.items = [];
        vm.genericService = genericService;
        vm.$onInit = _init;
        vm.modelId = parseInt($("#modelId").val());
        vm.tagUrl = '/api/meta/url/';

        // The Fold
        // GET Url by ID on Initialize
        function _init() {
            vm.genericService.getById(vm.tagUrl, vm.modelId)
                .then(_tagsGetUrlSuccess, _tagsGetUrlError);
        }
        // Get by Url Success
        function _tagsGetUrlSuccess(res) {
            console.log(res);
            vm.items = res.data.items;
        }
        // Get by Url Error
        function _tagsGetUrlError(err) {
            console.log(err);
        }
    }
})();