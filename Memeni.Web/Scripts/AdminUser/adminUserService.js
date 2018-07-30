//Admin User Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('adminUserService', adminUserService);
    adminUserService.$inject = ['$http', '$q', '$window'];

    function adminUserService($http, $q, $window) {
        return {
            userGridPost: _userGridPost,
            userConfirmEmail: _userConfirmEmail,
            userAdminAccess: _userAdminAccess,
            userLock: _userLock
        };

        function _userGridPost(data) {
            var settings = {
                url: "/api/admin/users/grid"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_userGridPostSuccess, _userGridPostFail);
        }
        function _userGridPostSuccess(data) {
            return data.data.item;
        }
        function _userGridPostFail(err) {
            console.log('user List Post Failed');
            return $q.reject(err);
        }

        function _userConfirmEmail(id) {
            var settings = {
                url: "/api/admin/users/confirm/" + id
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , withCredentials: true
            };
            return $http(settings)
                .then(_userConfirmEmailSuccess, _userConfirmEmailFail);
        }
        function _userConfirmEmailSuccess(resp) {
            return resp;
        }
        function _userConfirmEmailFail(err) {
            console.log('confirm email failed');
            return $q.reject(err);
        }

        function _userAdminAccess(id, data) {
            var settings = {
                url: "/api/admin/users/access/" + id
                , method: "PUT"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_userAdminAccessSuccess, _userAdminAccessFail);
        }
        function _userAdminAccessSuccess(resp) {
            return resp;
        }
        function _userAdminAccessFail(err) {
            console.log('admin access failed');
            return $q.reject(err);
        }

        function _userLock(id, data) {
            var settings = {
                url: "/api/admin/users/lock/" + id
                , method: "PUT"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_userLockSuccess, _userLockFail);
        }
        function _userLockSuccess(resp) {
            return resp;
        }
        function _userLockFail(err) {
            console.log('user lock failed');
            return $q.reject(err);
        }
    }
})();
