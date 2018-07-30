(function () {
    "use strict";

    angular
        .module("mainApp")
        .factory("companyService", companyService);

    companyService.$inject = ["$http", "$q"];

    function companyService($http, $q) {
        return {
            companyGetAll: _companyGetAll,
            companyGetById: _companyGetById,
            companyPost: _companyPost,
            companyUpdate: _companyUpdate,
            companyDelete: _companyDelete
        };

        //--GET ALL--
        function _companyGetAll() {
            return $http.get("/api/companies", { withCredentials: true })
                .then(_serviceCallComplete, _serviceCallFailed);
        }

        //--GET BY ID--
        function _companyGetById(id) {
            return $http.get("/api/companies/" + id, { withCredentials: true })
                .then(_serviceCallComplete, _serviceCallFailed);
        }

        //--Company POST--
        function _companyPost(data) {
            return $http.post("/api/companies", data, { withCredentials: true })
                .then(_serviceCallComplete, _serviceCallFailed);
        }

        //--Company UPDATE--
        function _companyUpdate(data, id) {
            return $http.put("/api/companies/" + id, data, { withCredentials: true })
                .then(_serviceCallComplete, _serviceCallFailed);
        }

        //--Company DELETE--
        function _companyDelete(id) {
            return $http.delete("/api/companies/" + id, { withCredentials: true })
                .then(_serviceCallComplete, _serviceCallFailed);
        }

        //--SERVICE CALL SUCCESS--
        function _serviceCallComplete(response) {
            return response;
        }

        //--SERVICE CALL FAILURE--
        function _serviceCallFailed(error) {
            var msg = 'error';
            if (error.data && error.data.description) {
                msg += '\n' + error.data.description;
            }
            error.data.description = msg;
            return $q.reject(error, "Oops. Something went wrong");
        }
    }   
})();