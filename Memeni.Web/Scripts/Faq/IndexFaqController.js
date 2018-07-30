(function () {

    angular.module('mainApp')
        .controller('IndexFaqController', IndexFaqController);

    IndexFaqController.$inject = ['$scope', 'genericService', '$window'];

    function IndexFaqController($scope, genericService, $window) {
        var vm = this;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.allFaqs = [];
        vm.$onInit = _init;
        vm.postBtn = _postButton;
        vm.editBtn = _editButton;
        vm.deleteSettingsBtn = _deleteButton;
        vm.currentIndex;
        vm.getAllFaqCategories;
        vm.default = 10;

        function _init() {
            vm.genericService.get('/api/faq')
                .then(_getAllFaqsSuccess, _getAllFaqsError);
        }
        function _getAllFaqsSuccess(r) {
            console.log("Faqs: ", r.data);
            vm.allFaqs = r.data;
            console.log('All Faqs: ', vm.allFaqs);
        }
        function _getAllFaqsError(r) {
            console.log(r, ":(");
        }
        function _postButton() {
            $window.location.href = '/admin/faq/create';
        }
        function _editButton(Id) {
            $window.location.href = '/admin/faq/' + Id + '/edit/';
        }
        function _deleteButton(Id, index) {
            vm.currentIndex = index;
            vm.genericService.delete('/api/faq', Id)
                .then(_deleteSuccess, _deleteError);
        }
        function _deleteSuccess(r) {
            console.log(r);
            alert('Delete Successful');
            vm.allSettings.splice(vm.currentIndex, 1);
        }
        function _deleteError(r) {
            console.log(r, ":(");
        }
    }

})();