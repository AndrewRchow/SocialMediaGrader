//Facebook Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('fbService', fbService);
    fbService.$inject = ['$http', '$q'];

    function fbService($http, $q) {
        return {
            getFb: _getFb,
            postReport: _postReport,
        };

        function _getFb(name, type) {
            return $http.get('/api/fb/'+ type + '/' + name, { withCredentials: true })
                .then(_getFbComplete, _getFbFail);
        };
        function _getFbComplete(response) {
            return response;
        };
        function _getFbFail(error) {
            return $q.reject(error);
        };
       
        function _postReport(data) {
            return $http.post('/api/fb/report/', JSON.stringify(data), { withCredentials: true })
                .then(_postReportComplete, _postReportFail);
        };
        function _postReportComplete(response) {
            return response;
        };
        function _postReportFail(error) {
            return $q.reject(error);
        };
    }
})();