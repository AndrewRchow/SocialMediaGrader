(function () {

    angular.module('mainApp')
        .controller('IndexFaqCategoryController', IndexFaqCategoryController);

    IndexFaqCategoryController.$inject = ['$scope', 'FaqCategoryService', '$window'];

    function IndexFaqCategoryController($scope, FaqCategoryService, $window) {
        var vm = this;
        vm.$scope = $scope;
        vm.FaqCategoryService = FaqCategoryService;
        vm.$onInit = _init;
        vm.postBtn = _postButton;
        vm.editBtn = _editButton;
        vm.deleteSettingsBtn = _deleteButton;
        vm.currentIndex;
        vm.allCategories = [];
        vm.faqCategories = [];
        vm.default = 10;

        function _init() {
            vm.FaqCategoryService.getAllFaqCategories()
                .then(_getAllCategoriesSuccess, _getAllCategoriesError);
        }
        function _getAllCategoriesSuccess(r) {
            console.log(r);
            vm.allCategories = r.data.items;
        }
        function _getAllCategoriesError(r) {
            console.log(r, ":(");
        }
        function _postButton() {
            $window.location.href = '/admin/faqcategory/create';
        }
        function _editButton(Id) {
            $window.location.href = '/admin/faqcategory/' + Id + '/edit/';
        }
        function _deleteButton(Id, index) {
            vm.currentIndex = index;
            vm.faqCategoryService.deleteFaq(Id)
                .then(_deleteSuccess, _deleteError);
        }
        function _deleteSuccess(r) {
            console.log(r);
            alert('Delete Successful');
            vm.allCategories.splice(vm.currentIndex, 1);
        }
        function _deleteError(r) {
            console.log(r, ":(");
        }
    }

})();