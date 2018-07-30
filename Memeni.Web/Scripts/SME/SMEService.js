//SME Service
(function () {
    "use strict";
    angular
        .module('mainApp')
        .service('smeService', smeService);
    smeService.$inject = ['$http', '$q'];

    function smeService($http, $q) {
        return {
            getAll: _getAll,
            delete: _delete,
            post: _post,
            put: _put,
            getById: _getById
        };
        function _getAll() {
            var settings = {
                url: "/api/sme",
                method: 'GET',
                cache: false,
                responseType: 'json',
                withCredentials: true
            };
            return $http(settings)
                .then(_getAllComplete, _getAllFail);
        }
        function _getAllComplete(data) {
            console.log(data);
            return data;
        }
        function _getAllFail(err) {
            console.log('get all fail');
            return $q.reject(err);
        }

        function _delete(id) {
            var settings = {
                url: "/api/sme/" + id,
                method: 'DELETE',
                cache: false,
                responseType: 'json',
                withCredentials: true
            };
            return $http(settings)
                .then(_deleteComplete, _deleteFail);
        }
        function _deleteComplete(data) {
            console.log('delete complete');
            return data;
        }
        function _deleteFail(err) {
            console.log('delete fail');
            return $q.reject(err);
        }

        function _post(myData) {
            var settings = {
                url: "/api/sme"
                , method: "POST"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(myData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postSuccess, _postFail);
        }
        function _postSuccess(data) {
            return data;
        }
        function _postFail(err) {
            console.log('new post failed');
            return $q.reject(err);
        }

        function _put(data, id) {
            var settings = {
                url: "/api/sme/" + id
                , method: "PUT"
                , cache: false
                , headers: { 'Content-Type': "application/json; charset=UTF-8" }
                , data: JSON.stringify(data)
                , withCredentials: true

            };
            return $http(settings)
                .then(_putSuccess, _putFail);
        }
        function _putSuccess(data) {
            console.log('Update Successful');
            return data;
        }
        function _putFail(err) {
            console.log('Update Fail');
            return $q.reject(err);
        }

        function _getById(id) {
            var settings = {
                url: "/api/sme/" + id,
                method: 'GET',
                cache: false,
                responseType: 'json',
                withCredentials: true
            };
            return $http(settings)
                .then(_getByIdComplete, _getByIdFail);
        }
        function _getByIdComplete(data) {
            console.log(data);
            return data;
        }
        function _getByIdFail(err) {
            console.log('get by id fail');
            return $q.reject(err);
        }
    }
})();