(function () {
    "use strict";
    angular.module("mainApp")
        .factory('service', service);

    // Inject services
    service.$inject = ['$http', '$q'];

    function service($http, $q) {
        return {
            tncGetAll: _tncGetAll
            , tncGetById: _tncGetById
            , tncPut: _tncPut
            , tncPost: _tncPost
            , tncDelete: _tncDelete
            , tncPutAll: _tncPutAll
            , adminAuth: _adminAuth
        };
        // GET ALL
        function _tncGetAll() {
            return $http.get('/api/terms/list', { withCredentials: true })
                .then(_tncGetAllComplete, _tncGetAllFailed);
        }
        function _tncGetAllComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncGetAllFailed(error) {
            var msg = 'Failed to retrieve list';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // DELETE
        function _tncDelete(id) {
            return $http.delete('/api/terms/' + id, { withCredentials: true })
                .then(_tncDeleteComplete, _tncDeleteFailed);
        }
        function _tncDeleteComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncDeleteFailed(error) {
            var msg = 'Failed to delete entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // POST
        function _tncPost(data) {
            return $http.post('/api/terms', data, { withCredentials: true })
                .then(_tncPostComplete, _tncPostFailed);
        }
        function _tncPostComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncPostFailed(error) {
            var msg = 'Failed to add entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // PUT
        function _tncPut(data, id) {
            return $http.put('/api/terms/' + id, data, { withCredentials: true })
                .then(_tncPutComplete, _tncPutFailed);
        }
        function _tncPutComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncPutFailed(error) {
            var msg = 'Failed to update entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // GET BY ID
        function _tncGetById(id) {
            return $http.get('/api/terms/' + id, { withCredentials: true })
                .then(_tncGetByIdComplete, _tncGetByIdFailed);
        }
        function _tncGetByIdComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncGetByIdFailed(error) {
            var msg = 'Failed to retrieve entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // PUT ALL
        function _tncPutAll(data) {
            return $http.put('/api/terms', data, { withCredentials: true })
                .then(_tncPutAllComplete, _tncPutAllFailed);
        }
        function _tncPutAllComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tncPutAllFailed(error) {
            var msg = 'Failed to update order';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // GET Admin Auth
        function _adminAuth() {
            return $http.get('/api/auth/current/roles/admin', { withCredentials: true })
                .then(_adminAuthSuccess, _adminAuthFail);
        }
        function _adminAuthSuccess(resp) {
            return resp;
        }
        function _adminAuthFail(err) {
            console.log('Admin Auth Failed');
            return err;
        }
    }
})();