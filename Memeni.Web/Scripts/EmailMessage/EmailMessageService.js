(function () {
    'use strict';
    angular
        .module('mainApp')
        .factory('emailMsgService', emailMsgService);

    emailMsgService.$inject = ['$http', '$q'];

    function emailMsgService($http, $q) {
        return {
            currentUser: _currentUser,
            verifyCode: _verifyCode,
            confirmEmail: _confirmEmail,
            resendEmail: _resendEmail,
            forgotPassword: _forgotPassword
        };

        function _currentUser() {
            return $http.get("/api/auth/current", { withCredentials: true })
                .then(_currentUserSuccess, _currentUserError);
        }
        function _currentUserSuccess(response) {
            return response;
        }
        function _currentUserError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        function _verifyCode(id) {
            return $http.get("/api/email/" + id, { withCredentials: true })
                .then(_verifyCodeSuccess, _verifyCodeError);
        }
        function _verifyCodeSuccess(response) {
            return response.data.item;
        }
        function _verifyCodeError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        function _confirmEmail(id) {
            return $http.delete("/api/email/delete/" + id, { withCredentials: true })
                .then(_confirmEmailSuccess, _confirmEmailError);
        }
        function _confirmEmailSuccess(response) {
            return response;
        }
        function _confirmEmailError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        function _resendEmail(myData) {
            var settings = {
                url: "/api/auth/resend"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_resendEmailSuccess, _resendEmailFail);
        }
        function _resendEmailSuccess(data) {
            return data;
        }
        function _resendEmailFail(err) {
            return $q.reject(err);
        }

        function _forgotPassword(myData) {
            var settings = {
                url: "/api/auth/forgotPassword"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: myData
                , withCredentials: true
            };
            return $http(settings)
                .then(_forgotPasswordSuccess, _forgotPasswordFail);
        }
        function _forgotPasswordSuccess(resp) {
            return resp;
        }
        function _forgotPasswordFail(err) {
            return $q.reject(err);
        }
    }
})();