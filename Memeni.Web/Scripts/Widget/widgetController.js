(function () {
    'use strict';
    angular
        .module('mainApp', ['ngMaterial'])
        .controller('widgetController', widgetController);

    widgetController.$inject = ['$scope', 'widgetService'];

    function widgetController($scope, widgetService) {

        var vm = this;
        vm.widgetService = widgetService;
        vm.photoUrls = [];
        vm.likesArray = [];
        vm.urlInput = "";
        vm.likesInput = "";
        vm.urlPath;
        vm.loopCounter = 0;
        vm.name = "";
        vm.profilePic;
        vm.dummyProfilePic = "https://t3.ftcdn.net/jpg/00/64/67/52/240_F_64675209_7ve2XQANuzuHjMZXP3aIYIpsDKEbF5dD.jpg";
        vm.maxImages = 50;
        vm.budgetVal;
        vm.reachVal;
        vm.maxReach;
        vm.initialReach;
        vm.desiredReach;
        vm.getFacebookInfo = _getFacebookInfo;
        vm.reachSliderClick = _reachSliderClick;
        vm.budgetSliderClick = _budgetSliderClick;
        vm.$onInit = _init;
        vm.item = {};
        vm.newResponse = "";

        //====[ THE FOLD ]=======================================

        function _getFacebookInfo() {

            //url has input likes empty
            if ((vm.urlInput != "") && (vm.likesInput == "")) {
                formatUrl();
                vm.widgetService.getFacebookInfo(vm.urlPath)
                    .then(_getInfoGood)
                    .catch(_getInfoBad);
            }
            // likes has input url empty
            else if ((vm.likesInput != "") && (vm.urlInput == "")) {
                // checks for valid input before ajax call
                if (!(isNaN(vm.likesInput))) {
                    vm.widgetService.getFacebookInfo("facebook")
                        .then(_getInfoGood)
                        .catch(_getInfoBad);
                }
            }
            // both have input
            else if ((vm.likesInput != "") && (vm.urlInput != "")) {
                formatUrl();
                vm.widgetService.getFacebookInfo(vm.urlPath)
                    .then(_getInfoGood)
                    .catch(_getInfoBad);
            }
        }
        function _getInfoGood(response) {

            // likes entered no url
            if (vm.urlInput == "") {              
                vm.maxReach = vm.likesInput;
                vm.initialReach = Math.round(vm.maxReach * 0.03);
                vm.name = "Your Page";
                vm.profilePic = vm.dummyProfilePic;
            }
            // url entered only
            else {
                vm.maxReach = response.fan_count;
                vm.initialReach = Math.round(vm.maxReach * 0.03);
                vm.name = response.name;
                vm.profilePic = response.picture.data.url;
                vm.item.Website = vm.urlInput;
                console.log(vm.item);
                vm.newResponse = response;
                vm.widgetService.postWidgetSalesforce(vm.item)
                    .then(_salesforceWidgetSuccess, _salesforceWidgetError)
            } 
        }

        function _salesforceWidgetSuccess(response) {
            console.log(response);
            //refresh
            resetWidget();
            $("#realDisplay").removeClass("hide");
            $("#dummyDisplay").addClass("hide");
            $("#profilePic").removeAttr("hidden");
            $("#pictureContainer").removeAttr("hidden");

            // push data on profiles that liked the page to array
            // to be used in the loop below
            vm.likesArray = vm.newResponse.likes.data;
            //get images of people who liked the page
            for (var i = 0; i < vm.likesArray.length; i++) {
                vm.widgetService.getFacebookImage(vm.likesArray[i].id)
                    .then(_getImageGood)
                    .catch(_getImageBad);
            }
        }
        function _salesforceWidgetError(error) {
            console.log(error);
            //refresh
            resetWidget();
            $("#realDisplay").removeClass("hide");
            $("#dummyDisplay").addClass("hide");
            $("#profilePic").removeAttr("hidden");
            $("#pictureContainer").removeAttr("hidden");

            // push data on profiles that liked the page to array
            // to be used in the loop below
            vm.likesArray = vm.newResponse.likes.data;
            //get images of people who liked the page
            for (var i = 0; i < vm.likesArray.length; i++) {
                vm.widgetService.getFacebookImage(vm.likesArray[i].id)
                    .then(_getImageGood)
                    .catch(_getImageBad);
            }
        }
        function _getInfoBad(error) {
            console.log(error);
            // if no data on page likes is returned, just push
            // dummy images to array manually
            for (var i = 0; i < 50; i++) {
                vm.photoUrls.push(vm.dummyProfilePic);
            }
        }

        function _getImageGood(response) {
            // change image sources by pushing new urls to array
            vm.photoUrls.push(response.picture.data.url);
            // if < 50 imgs return, fill the rest with dummy img
            vm.loopCounter++;
            if (vm.loopCounter == vm.likesArray.length) {
                for (var i = vm.likesArray.length; i < 50; i++) {
                    vm.photoUrls.push(vm.dummyProfilePic);
                }
                vm.loopCounter = 0;
            }

        }
        function _getImageBad(error) {
            console.log(error);
        }

        // these can perhaps be merged into one function
        function _reachSliderClick() {

                vm.desiredReach = Math.round((vm.maxReach * (vm.reachVal / 100)));
                vm.budgetVal = Math.round((0.0075 * (vm.desiredReach - vm.initialReach)));
                // prevents reach value greater than 100%
                if (vm.reachVal > 100) {
                    vm.reachVal = 100;
                }
                // prevents negative numbers being calculated
                if (vm.reachVal < 3) {
                    vm.budgetVal = 0;
                }
                // sets max budget
                if (vm.budgetVal > 10000) {
                    vm.budgetVal = 10000;
                }
                updateImgs();
        }
        function _budgetSliderClick() {

            vm.desiredReach = Math.round((vm.maxReach * (vm.reachVal / 100)));
            vm.reachVal = Math.round(((((vm.initialReach) + (vm.budgetVal / 0.0075)) / vm.maxReach) * 100));
            if (vm.reachVal > 100) {
                vm.reachVal = 100;
            }
            if (vm.reachVal < 3) {
                vm.budgetVal = 0;
            }
            updateImgs();
        }

        function resetWidget() {
            vm.urlInput = "";
            vm.likesInput = "";
            vm.reachVal = 3;
            vm.desiredReach = 0;
            vm.budgetVal = 0;
            vm.likesArray = [];
            vm.photoUrls = [];
            updateImgs();
        }
        function formatUrl() {
            var start = (vm.urlInput.lastIndexOf("/") + 1);
            var end = vm.urlInput.length;
            vm.urlPath = vm.urlInput.slice(start, end);
        }

        function updateImgs() {
            // calculating # colored vs noncolored photos
            var colored = Math.floor(vm.reachVal / 2);
            var noncolored = (vm.maxImages - colored);

            changeImgCSS(colored, noncolored);
        }
        function changeImgCSS(colored, noncolored) {
            for (var i = 0; i < colored; i++) {
                var id = "#img" + i;
                $(id).css("filter", "grayscale(0%)");
            }
            for (var i = colored; i < (colored + noncolored); i++) {
                var id = "#img" + i;
                $(id).css("filter", "grayscale(100%)");
            }
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