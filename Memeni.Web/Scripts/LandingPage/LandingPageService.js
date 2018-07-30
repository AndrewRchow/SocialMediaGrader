﻿(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('landingPageService', landingPageService);

    landingPageService.$inject = ['$http', '$q'];

    function landingPageService($http, $q) {
        return {
            urlCookieService: _urlCookieService
        };

        function _urlCookieService(urlVal) {
            console.log("This is from the service page: ", urlVal);
        }

    }
})();
