(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('phoneService', phoneService);

    phoneService.$inject = ['$http', '$q'];

    function phoneService($http, $q) {
        return {
            phoneGetAll: _phoneGetAll,
            phoneGetById: _phoneGetById,
            phoneDelete: _phoneDelete,
            phoneInsert: _phoneInsert,
            phoneUpdate: _phoneUpdate
        };

        //------------------ GET ALL -----------------------
        function _phoneGetAll() {
            return $http.get("/api/phones", { withCredentials: true })
                .then(_phoneGetAllSuccess, _phoneGetAllError);
        }
        function _phoneGetAllSuccess(response) {
            return response;
        }
        function _phoneGetAllError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        //------------------ GET BY ID -----------------------
        function _phoneGetById(id) {
            return $http.get("/api/phones/" + id, { withCredentials: true })
                .then(_phoneGetByIdSuccess, _phoneGetByIdError);
        }
        function _phoneGetByIdSuccess(response) {
            return response;
        }
        function _phoneGetByIdError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        //------------------ DELETE NUMBER -----------------------
        function _phoneDelete(id) {
            return $http.delete("/api/phones/" + id, { withCredentials: true })
                .then(_phoneDeleteSuccess, _phoneDeleteError);
        }
        function _phoneDeleteSuccess(response) {
            //console.log("GET BY ID Success", response);
            return response;
        }
        function _phoneDeleteError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        //------------------ INSERT NUMBER -----------------------
        function _phoneInsert(data) {
            return $http.post("/api/phones", data, { withCredentials: true })
                .then(_phoneInsertSuccess, _phoneInsertError);
        }
        function _phoneInsertSuccess(response) {
            return response;
        }
        function _phoneInsertError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }

        //------------------ UPDATE NUMBER -----------------------
        function _phoneUpdate(data, id) {
            return $http.put("/api/phones/" + id, data, { withCredentials: true })
                .then(_phoneUpdateSuccess, _phoneUpdateError);
        }
        function _phoneUpdateSuccess(response) {
            return response;
        }
        function _phoneUpdateError(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error);
        }
}
})();