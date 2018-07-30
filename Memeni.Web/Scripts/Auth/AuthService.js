//Auth Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('authService', authService);
    authService.$inject = ['$http', '$q', '$window'];

    function authService($http, $q, $window) {
        return {
            login: _login,
            register: _register,
            recaptcha: _recaptcha,
            reset: _reset,
        };

        function _login(myData) {
            var settings = {
                url: "/api/auth/login"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_loginSuccess, _loginFail);
        }
        function _loginSuccess(data) {
            return data;
        }
        function _loginFail(err) {
            console.log('login failed');
            return $q.reject(err);
        }

        function _register(myData) {
            var settings = {
                url: "/api/auth/register"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_registerSuccess, _registerFail);
        }
        function _registerSuccess(data) {
            return data;
        }
        function _registerFail(err) {
            console.log('register failed');
            return $q.reject(err);
        }

        function _recaptcha(myData) {
            var settings = {
                url: "/api/auth/recaptcha"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_recaptchaComplete, _recaptchaFail);
        }
        function _recaptchaComplete(data) {
            return data;
        }
        function _recaptchaFail(err) {
            console.log('recaptcha failed');
            return $q.reject(err);
        }

        function _reset(myData) {
            var settings = {
                url: "/api/auth/reset"
                , method: "PUT"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_resetSuccess, _resetFail);
        }
        function _resetSuccess(resp) {
            return resp;
        }
        function _resetFail(err) {
            console.log('reset failed');
            return $q.reject(err);
        }
    }
})();