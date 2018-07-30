//Index Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('smeController', smeController);

    smeController.$inject = ['$scope', 'smeService', '$window'];

    function smeController($scope, smeService, $window) {

        var vm = this;
        vm.$scope = $scope;
        vm.smeService = smeService;
        vm.$onInit = _init;
        vm.selectAll = _selectAll;
        vm.deleteById = _deleteById;
        vm.items = [];
        vm.$window = $window;
        vm.index;
        vm.sortType = 'id'; // set the default sort type
        vm.sortReverse = false;  // set the default sort order
        vm.avg = [];

        function _init() {
            _selectAll();
            return
        }                    
        function _selectAll() {
            return vm.smeService.getAll()
                    .then(_selectAllSuccess, _selectAllError)
        }
        function _selectAllSuccess(data) {
            vm.items = data.data.items;
            var minSum = 0;
            var maxSum = 0;
            var sum = 0;
            for (var i = 0; i < vm.items.length; i++) {
                minSum += parseInt(vm.items[i].minInteractionsPer1k, 10);
                maxSum += parseInt(vm.items[i].maxInteractionsPer1k, 10);
                sum += parseInt(vm.items[i].sumInteractionsPer1k, 10);
            }
            vm.avg.push((minSum / vm.items.length).toFixed(3));
            vm.avg.push((maxSum / vm.items.length).toFixed(3));
            vm.avg.push((sum / vm.items.length).toFixed(3));
            return
        }
        function _selectAllError(err) {
            return err;
        }              
        function _deleteById(index, id) {
            vm.index = index;
            return vm.smeService.delete(id)
                        .then(_deleteSuccess, _deleteError)
        }
        function _deleteSuccess() {
            vm.items.splice(vm.index, 1);
            return
        }
        function _deleteError(err) {
            return console.log(err)
        }
    }
})();

