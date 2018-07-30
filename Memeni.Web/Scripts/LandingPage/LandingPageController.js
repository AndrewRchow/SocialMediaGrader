(function () {
    'use strict';

    angular
        .module('mainApp')
        .controller('landingPageController', landingPageController);

    landingPageController.$inject = ['$scope', '$cookies', 'landingPageService', 'genericService', 'toastr'];

    function landingPageController($scope, $cookies, landingPageService, genericService, toastr) {
        var vm = this;
        // View model
        vm.$scope = $scope;
        vm.item = {};
        vm.items = [];
        vm.landingPageService = landingPageService;
        vm.genericService = genericService;
        vm.submit = _submit;
        vm.$onInit = _init;
        vm.toastr = toastr;
        
        function _submit() {
            //If no current url cookie, then new seesion indicated, used for tracking
            vm.item.Session = $cookies.get('socialURL');
            vm.item.Website = vm.item.Website.toLowerCase();

            if (vm.item.Website.includes('facebook.com/')) {
                $cookies.put('socialURL', vm.item.Website, { 'path': '/' });
                $cookies.put('lpEmail', vm.item.Email, { 'path': '/' });
                vm.genericService.post('/api/tracking', vm.item)
                .then(_trackingFacebookSuccess, _trackingFacebookError)
            } else if (vm.item.Website.includes('twitter.com/')) {
                $cookies.put('socialURL', vm.item.Website, { 'path': '/' });
                $cookies.put('lpEmail', vm.item.Email, { 'path': '/' });
                vm.genericService.post('/api/tracking', vm.item)
                    .then(_trackingTwitterSuccess, _trackingTwitterError)
            } else {
                vm.toastr.error("Please Enter Your Facebook Or Twitter Link.");
                vm.item.Website = "";
            }
        }
        function _trackingFacebookSuccess(response) {
            console.log(response);
            vm.genericService.post('/api/salesforce/facebook', vm.item)
            .then(_salesforceFacebookSuccess, _salesforceFacebookError)
        }
        function _trackingFacebookError(error) {
            console.log(error);
            vm.genericService.post('/api/salesforce/facebook', vm.item)
                .then(_salesforceFacebookSuccess, _salesforceFacebookError)
        }
        function _trackingTwitterSuccess(response) {
            console.log(response);
            vm.genericService.post('/api/salesforce/twitter', vm.item)
                .then(_salesforceTwitterSuccess, _salesforceTwitterError)
        }
        function _trackingTwitterError(error) {
            console.log(error);
            vm.genericService.post('/api/salesforce/twitter', vm.item)
                .then(_salesforceTwitterSuccess, _salesforceTwitterError)
        }

        function _salesforceFacebookSuccess(response) {
            console.log(response);
            window.location.href = "/fb/loading"; 
        }
        function _salesforceFacebookError(error) {
            console.log(error);
            window.location.href = "/fb/loading";
        }

        function _salesforceTwitterSuccess(response) {
            console.log(response);
            window.location.href = "/twitter/loading";
        }
        function _salesforceTwitterError(error) {
            console.log(error);
            window.location.href = "/twitter/loading";
        }

        function _init() {
            //Gets url of previous page, used for campaign ads
            var web = document.referrer;   
            
            if (web.match(/utm_source=(.*?)&/i)) {
                var source = web.match(/utm_source=(.*?)&/i)[1];
            }
            if (web.match(/utm_medium=(.*?)&/i)) {
                var medium = web.match(/utm_medium=(.*?)&/i)[1];
            }
            if (web.match(/utm_campaign=(.*?)&/i)) {
                var campaign = web.match(/utm_campaign=(.*?)&/i)[1];
            }
            if (web.match(/utm_term=(.*?)&/i)) {
                var term = web.match(/utm_term=(.*?)&/i)[1];
            }
            if (web.match(/utm_content=(.*?)&/i)) {
                var content = web.match(/utm_content=(.*?)&/i)[1];
            }
            if (web.match(new RegExp("gclid=" + "(.*)"))) {
                var id = web.match(new RegExp("gclid=" + "(.*)"))[1];
            }

            vm.item.AdSource = source;
            vm.item.AdMedium = medium;
            vm.item.AdName = campaign;
            vm.item.AdTerm = term;
            vm.item.AdContent = content;
            vm.item.AdId = id;            
        }
    }
})();