//Admin Navivation Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('adminNavService', adminNavService);
    adminNavService.$inject = ['$http', '$q', '$window'];

    function adminNavService($http, $q, $window) {
        return {
            adminAuth: _adminAuth,
            logout: _logout,          
        };

        function _adminAuth() {
            var settings = {
                url: "/api/auth/current/roles/admin"
                , method: "GET"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , withCredentials: true
            };
            return $http(settings)
                .then(_adminAuthSuccess, _adminAuthFail);
        }
        function _adminAuthSuccess(resp) {
            console.log('Admin Auth Success', resp);
            return resp;
        }
        function _adminAuthFail(err) {
            console.log('Admin Auth Failed');
            $window.location.href = "/Home/Index";
            return $q.reject(err);
        }

        function _logout() {
            var settings = {
                url: "/api/auth/logout"
                , method: "GET"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , withCredentials: true
            };
            return $http(settings)
                .then(_logoutSuccess, _logoutFail);
        }
        function _logoutSuccess() {
            return console.log('logout Success');
        }
        function _logoutFail(err) {
            return $q.reject(err);
        }
    }
})();