(function () {
    'use strict';
    angular
        .module('mainApp')
        .factory('genericService', genericService);

    genericService.$inject = ['$http', '$q', 'toastr'];

    function genericService($http, $q, toastr) {

        return {
            post: _post,
            postById: _postById,
            get: _get,
            getById: _getById,
            put: _put,
            delete: _delete,
            truncate: _truncate,
            errorParse: _errorParse,
            postError: _postError,
            logOutUser: _logOutUser
        };
       
        // POST
        function _post(apiendpoint, data) {
            return $http.post(apiendpoint, data)
                .then(_postComplete)
                .catch(_postFailed);

            function _postComplete(res) {
                console.log("Post Complete", res);
                return res;
            }

            function _postFailed(err) {
                console.log("Post Failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // POST BY ID
        function _postById(apiendpoint, id, data) {
            return $http.post(apiendpoint + id, data)
                .then(_postByIdComplete)
                .catch(_postByIdFailed);

            function _postByIdComplete(res) {
                console.log("Post by id complete", res);
                return res;
            }

            function _postByIdFailed(err) {
                console.log("Post by id failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // GET
        function _get(apiendpoint) {
            return $http.get(apiendpoint)
                .then(_getComplete)
                .catch(_getFailed);

            function _getComplete(res) {
                console.log("Get Complete", res);
                return res;
            }

            function _getFailed(err) {
                console.log("Get Failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // GET BY ID
        function _getById(apiendpoint, id) {
            return $http.get(apiendpoint + id)
                .then(_getByIdComplete)
                .catch(_getByIdFailed);

            function _getByIdComplete(res) {
                console.log("Get by id success", res);
                return res;
            }

            function _getByIdFailed(err) {
                console.log("Get by id failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // PUT
        function _put(apiendpoint, id, data) {
            return $http.put(apiendpoint + id, data)
                .then(_putComplete)
                .catch(_putFailed);

            function _putComplete(res) {
                console.log("Put Complete", res);
                return res;
            }

            function _putFailed(err) {
                console.log("Put Failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // DELETE BY ID
        function _delete(apiendpoint, id) {
            return $http.delete(apiendpoint + id)
                .then(_deleteComplete)
                .catch(_deleteFailed);

            function _deleteComplete(res) {
                console.log("Delete Complete", res);
                return res;
            }

            function _deleteFailed(err) {
                console.log("Delete Failed", err);
                _errorParse(err);
                return $q.reject(err);
            }
        }

        // TRUNCATE
        function _truncate(apiendpoint) {
            return $http.delete(apiendpoint)
                .then(_truncateComplete)
                .catch(_truncateFailed);

            function _truncateComplete(res) {
                    console.log("Truncate Complete", res);
                    return res;
            }

            function _truncateFailed(err) {
                    console.log("Truncate Failed", err);
                    _errorParse(err);
                    return $q.reject(err);
            }
        }

        // Called when error is thrown
        function _errorParse(data) {
            var modifiedBy = "Admin";
            _get("/api/auth/current")
                .then(_currentUser);

            function _currentUser(res) {
                if (res.data == null) {
                    _noUser();
                } else {
                    var modifiedBy = res.data.name;

                    var newData = {
                        "errorMessage": data.data.message,
                        "errorNumber": data.status,
                        "modifiedBy": modifiedBy,
                        "errorSeverity": 0,
                        "errorState": 0,
                        "errorProcedure": data.config.method,
                        "errorLine": 0
                    };
                    _postError(newData);
                }
            }

            function _noUser() {
                var newData = {
                    "errorMessage": data.data.message,
                    "errorNumber": data.status,
                    "modifiedBy": "Admin",
                    "errorSeverity": 0,
                    "errorState": 0,
                    "errorProcedure": data.config.method,
                    "errorLine": 0
                };
                _postError(newData);
            }
            // Filter "errorSeverity" 
            // Try to find way to log error line

            // Move "Logout user"
            // If error is critical send email

        }

        // Function to catch errors
        function _postError(data) {
            return $http.post("/api/errors/", data)
                .then(_postErrorComplete)
                .catch(_postErrorFailed);

            function _postErrorComplete(res) {
                console.log("Error successfully logged", res);
            }

            function _postErrorFailed(err) {
                console.log("Error not logged", err);
                return $q.reject(err);
            }
        }

        //Logout
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