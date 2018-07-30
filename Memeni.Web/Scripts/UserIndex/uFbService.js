//Facebook Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('uFbService', uFbService);
    uFbService.$inject = ['$http', '$q'];

    function uFbService($http, $q) {
        return {
            getFb: _getFb,
            fbProfile: _fbProfile,
            userDashboard: _userDashboard,
            dashboardPosts: _dashboardPosts,
            fbReach: _fbReach,
            fbGrowth: _fbGrowth,
            fbProfileInfo: _fbProfileInfo,
        };

        function _getFb(id) {
            return $http.get('/api/user/fb/' + id, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _fbProfile(name) {
            return $http.get('/api/user/fb/profile/' + name, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _fbProfileInfo(id) {
            return $http.get('/api/fb/profile/' + id, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _userDashboard(data) {
            return $http.post('/api/user/fb/dashboard/', data, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _fbReach(name) {
            return $http.get('/api/user/fb/reach/' + name, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _fbGrowth(name) {
            return $http.get('/api/user/fb/growth/' + name, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _dashboardPosts(name) {
            return $http.post('/api/user/fb/posts/' + name, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        }
        function _getFbComplete(response) {
            return response;
        }
        function _getFbFail(error) {
            return $q.reject(error);
        }

    }
})();