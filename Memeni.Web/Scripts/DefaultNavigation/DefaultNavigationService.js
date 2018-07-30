(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('DefaultNavigationService', DefaultNavigationService);
    DefaultNavigationService.$inject = ['$http', '$q'];

    function DefaultNavigationService($http, $q) {
        return {
            logOutUser: _logOutUser
        };
        function _logOutUser() {
            return $http.get("/api/auth/logout", { withCredentials: true })
                .then(_logOutUserSuccess, _logOutUserError);
        }
        function _logOutUserSuccess(resp) {
            return resp;
        }
        function _logOutUserError(err) {
            return $q.reject(err);
        }
    }
})();