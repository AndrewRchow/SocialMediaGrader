(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('adminHomeContoller', adminHomeController);

    adminHomeController.$inject = ['$scope'];

    function adminHomeController($scope) {

        var vm = this;
        vm.$onInit = _init;


        //====[ THE FOLD ]=======================================

        function _init() {
            window.alert("admin ctrl works");
        }
    }
})();