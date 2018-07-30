(function () {
    'use strict';

    angular.module('mainApp')
        .controller('FaqManageController', FaqManageController);

    FaqManageController.$inject = ['$scope', '$window', 'genericService'];

    function FaqManageController($scope, $window, genericService) {
        var vm = this;
        vm.$scope = $scope;
        vm.backBtn = _backBtn;
        vm.submitBtn = _submitBtn;
        vm.storeFaq = {};
        vm.Id = parseInt($("#modelId").val());
        vm.genericService = genericService;
        vm.$window = $window;
        vm.$onInit = _init;
        vm.selectedItem = 0;
        vm.displayOrders = {};
        vm.categories = {};

        function _init() {
            if (vm.Id > 0) {
                vm.genericService.getById('/api/faq/', vm.Id)
                    .then(_getFaqByIdSuccess, _getFaqByIdError);
            }
            //Display Order
            vm.genericService.get('/api/faq')
                .then(_getDisplayOrderSuccess, _getDisplayOrderError);
            //Category
            vm.genericService.get('/api/faqcategory')
                .then(_getCategoriesSuccess, _getCategoriesError);
        }
        function _getCategoriesSuccess(r) {
            vm.categories = r.data.items;
            console.log('Categories: ', vm.categories);
        }
        function _getCategoriesError(r) {
            console.log(r, ":(");
        }

           function _getDisplayOrderSuccess(r) {
               vm.displayOrders = r.data;
               console.log('displayOrders: ', vm.displayOrders);
           }
           function _getDisplayOrderError(r) {
               console.log($q.reject(r));
           }
        function _getFaqByIdSuccess(r) {
            vm.storeFaq = r.data.item;
            console.log('Faq By Id: ', vm.storeFaq);
        }
        function _getFaqByIdError(r) {
            console.log(r, ":(");
        }

        function _backBtn() {
            $window.location.href = '/admin/faq/index';
        }
        function _submitBtn(isValid) {
            if (isValid) {
                if (vm.Id > 0) {
                    vm.genericService.put('/api/faq/', vm.Id, vm.storeFaq)
                        .then(_postSuccess, _postError);
                } else {

                    vm.genericService.post('/api/faq', vm.storeConfig)
                        .then(_postSuccess, _postError);
                }
            }
        }
        function _postSuccess(r) {
            $window.location.href = '/admin/faq/index';
        }
        function _postError(r) {
            console.log(r, ":(");
        }
    }
})(); 