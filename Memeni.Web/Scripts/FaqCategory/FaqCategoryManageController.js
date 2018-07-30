(function () {
    'use strict';

    angular.module('mainApp')
        .controller('FaqCategoryManageController', FaqCategoryManageController);

    FaqCategoryManageController.$inject = ['$scope', '$window', 'FaqCategoryService'];

    function FaqCategoryManageController($scope, $window, FaqCategoryService) {
        var vm = this;
        vm.$scope = $scope;
        vm.backBtn = _backBtn;
        vm.submitBtn = _submitBtn;
        vm.storeCategoryFromId = {};
        vm.Id = parseInt($("#modelId").val());
        vm.FaqCategoryService = FaqCategoryService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.selectedItem = 0;
        vm.categoryById = _categoryById;

        function _init() {
            if (vm.Id > 0) {
                vm.FaqCategoryService.getCategoryById(vm.Id)
                    .then(_getFaqByIdSuccess, _getFaqByIdError);
            }
        }
        function _getFaqByIdSuccess(r) {
            console.log(r.data.item);
            return vm.storeCategoryFromId = r.data.item;
        }
        function _getFaqByIdError(r) {
            console.log(r, ":(");
        }
        function _categoryById(id) {
            vm.FaqCategoryService.getCategoryById(vm.storeCategoryFromId.Id)
                .then(_getFaqByIdSuccesss, _getFaqByIdErrorr);
        }
        function _backBtn() {
            $window.location.href = '/admin/faqcategory/index';
        }
        function _submitBtn(isValid) {
            if (isValid) {
                if (vm.Id > 0) {
                    vm.FaqCategoryService.putFaq(vm.storeCategoryFromId, vm.Id)
                        .then(_postSuccess, _postError);
                } else {

                    vm.FaqCategoryService.postFaq(vm.storeCategoryFromId)
                        .then(_postSuccess, _postError);
                }
            }
        }
        function _postSuccess(r) {
            $window.location.href = '/admin/faqcategory/index';
        }
        function _postError(r) {
            console.log(r, ":(");
        }
    }
})(); 