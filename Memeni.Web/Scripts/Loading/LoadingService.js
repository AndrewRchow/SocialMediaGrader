//Auth Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('loadingService', loadingService);
    loadingService.$inject = ['$http', '$q', '$window'];

    function loadingService($http, $q, $window) {

        return {
            checkUser: _checkUser,
        };

        function _checkUser() {
            var settings = {
                url: "/api/auth/current"
                , method: "GET"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , withCredentials: true
            };
            return $http(settings)
                .then(_checkUserSuccess, _checkUserFail);
        }
        function _checkUserSuccess(data) {
            console.log(data);
            if ($.inArray("Admin", data.data.roles) > -1) {
                $window.location.href = "/Admin/Home/Index";
            } else if($.inArray("User", data.data.roles) > -1) {
                $window.location.href = "/User/Home/Index";
            } else if ($.inArray("Anon", data.data.roles) > -1){
                $window.location.href = "/Home/Confirmation";
            } else {
                $window.location.href = "/Home/Login";
            }
            return;
        }
        function _checkUserFail(err) {
            $window.location.href = "/Home/Index";
            return $q.reject(err);
        }

    }
})();