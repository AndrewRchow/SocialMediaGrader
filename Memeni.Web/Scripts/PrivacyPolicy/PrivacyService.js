(function () {
    "use strict";

    angular.module("mainApp")
        .factory('PrivacyService', PrivacyService);

    // Inject services
    PrivacyService.$inject = ['$http', '$q'];

    function PrivacyService($http, $q) {
        return {
            getAllPrivacy: _getAllPrivacy
            , getPrivacyById: _getPrivacyById
            , putPrivacy: _putPrivacy
            , putPrivacyMultiple: _putPrivacyMultiple
            , postPrivacy: _postPrivacy
            , deletePrivacy: _deletePrivacy

        };

        // GET ALL
        function _getAllPrivacy() {
            return $http.get('/api/privacy', { withCredentials: true })
                .then(_getAllPrivacyComplete, _getAllPrivacyFailed);
        }
        function _getAllPrivacyComplete(response) {
            return response;
        }
        function _getAllPrivacyFailed(error) {
            return $q.reject(error);
        }


        // GET BY ID
        function _getPrivacyById(id) {
            return $http.get('/api/privacy/' + id, { withCredentials: true })
                .then(_getPrivacyByIdComplete, _getPrivacyByIdFailed);
        }
        function _getPrivacyByIdComplete(response) {
            return response;
        }
        function _getPrivacyByIdFailed(error) {
            return $q.reject(error);
        }

        // POST
        function _postPrivacy(data) {
            return $http.post('/api/privacy', data, { withCredentials: true })
                .then(_postPrivacyComplete, _postPrivacyFailed);
        }
        function _postPrivacyComplete(response) {
            return response;
        }
        function _postPrivacyFailed(error) {
            return $q.reject(error);
        }

        // PUT
        function _putPrivacy(data, id) {
            return $http.put('/api/privacy/' + id, data, { withCredentials: true })
                .then(_putPrivacyComplete, _putPrivacyFailed);
        }
        function _putPrivacyComplete(response) {
            return response;
        }
        function _putPrivacyFailed(error) {
            return $q.reject(error);
        }

        //PUT MULTIPLE for displayOrder on Index page
        function _putPrivacyMultiple(data) {
            return $http.put('/api/privacy/', data, { withCredentials: true })
                .then(_putPrivacyMultipleComplete, _putPrivacyMultipleFailed);
        }
        function _putPrivacyMultipleComplete(response) {
            return response;
        }
        function _putPrivacyMultipleFailed(error) {
            return $q.reject(error);
        }

        // DELETE
        function _deletePrivacy(id) {
            return $http.delete('/api/privacy/' + id, { withCredentials: true })
                .then(_deletePrivacyComplete, _deletePrivacyFailed);
        }
        function _deletePrivacyComplete(response) {
            return response;
        }
        function _deletePrivacyFailed(error) {
            return $q.reject(error);
        }
    }
})();