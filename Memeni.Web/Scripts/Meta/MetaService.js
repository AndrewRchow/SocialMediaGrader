(function () {
    "use strict";
    angular.module("mainApp")
        .factory('service', service);

    // Inject services
    service.$inject = ['$http', '$q'];

    function service($http, $q) {
        return {
            metaGetAll: _metaGetAll
            , metaPost: _metaPost
            , metaDelete: _metaDelete
            , metaPut: _metaPut
            , tagsGetUrl: _tagsGetUrl
            , metaPostDefault: _metaPostDefault
            , metaDeleteTags: _metaDeleteTags
            , metaPutLock: _metaPutLock
        };
        // GET ALL
        function _metaGetAll() {
            return $http.get('/api/metaurl', { withCredentials: true })
                .then(_metaGetAllComplete, _metaGetAllFailed);
        }
        function _metaGetAllComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaGetAllFailed(error) {
            var msg = 'Failed to retrieve list';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // POST
        function _metaPost(data) {
            return $http.post('/api/metaurl', data, { withCredentials: true })
                .then(_metaPostComplete, _metaPostFailed);
        }
        function _metaPostComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaPostFailed(error) {
            var msg = 'Failed to add entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // POST DEFAULT
        function _metaPostDefault(id, data) {
            return $http.post('/api/meta/' + id, data, { withCredentials: true })
                .then(_metaPostDefaultComplete, _metaPostDefaultFailed);
        }
        function _metaPostDefaultComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaPostDefaultFailed(error) {
            var msg = 'Failed to add entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // DELETE
        function _metaDelete(id) {
            return $http.delete('/api/metaurl/' + id, { withCredentials: true })
                .then(_metaDeleteComplete, _metaDeleteFailed);
        }
        function _metaDeleteComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaDeleteFailed(error) {
            var msg = 'Failed to delete entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // DELETE TAGS
        function _metaDeleteTags(id) {
            return $http.delete('/api/meta/' + id, { withCredentials: true })
                .then(_metaDeleteTagsComplete, _metaDeleteTagsFailed);
        }
        function _metaDeleteTagsComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaDeleteTagsFailed(error) {
            var msg = 'Failed to delete entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // PUT
        function _metaPut(data, id) {
            return $http.put('/api/metaurl/' + id, data, { withCredentials: true })
                .then(_metaPutComplete, _metaPutFailed);
        }
        function _metaPutComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaPutFailed(error) {
            var msg = 'Failed to update entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // PUT LOCK
        function _metaPutLock(data, id) {
            return $http.put('/api/metaurl/lock/' + id, data, { withCredentials: true })
                .then(_metaPutLockComplete, _metaPutLockFailed);
        }
        function _metaPutLockComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _metaPutLockFailed(error) {
            var msg = 'Failed to update entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
        // GET BY ID
        function _tagsGetUrl(id) {
            return $http.get('/api/meta/url/' + id, { withCredentials: true })
                .then(_tagsGetUrlComplete, _tagsGetUrlFailed);
        }
        function _tagsGetUrlComplete(response) {
            // unwrap the data from the response
            return response;
        }
        function _tagsGetUrlFailed(error) {
            var msg = 'Failed to retrieve entry';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
    }
})();