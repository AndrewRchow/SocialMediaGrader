(function () {
    "use strict";
    angular
        .module("mainApp")
        .factory("peopleService", peopleService);

    peopleService.$inject = ["$http", "$q"];

    function peopleService($http, $q) {
        return{
            postPerson: _postPerson,
            getAllPeople: _getAllPeople,
            getPerson: _getPerson,
            putPerson: _putPerson,
            deletePerson: _deletePerson
        };

        //POST Method
        function _postPerson(personData) {
            var settings = {
                url: "api/people",
                method: "POST",
                cache: false,
                responseType: "json",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify(personData),
                withCredentials: true
            };
            return $http(settings)
                .then(_postComplete, _postFailed);

            function _postComplete(response) {
                // unwrap the data from the response
                console.log(response);
                return response;

            }

            function _postFailed(error) {
                console.log(error);
                return error;
            }
        }

        //GET ALL Method
        function _getAllPeople() {
            var settings = {
                url: "/api/people",
                method: "GET",
                cache: false,
                responseType: "json",
                contentType: "application/json; charset=UTF-8",
                withCredentials: true
            };
            return $http(settings)
                .then(_getAllComplete, _getAllFailed);

            function _getAllComplete(response) {
                // unwrap the data from the response
                console.log(response);
                return response;

            }

            function _getAllFailed(error) {
                console.log(error);

                return error;
            }

        }

        //Get By ID Method
        function _getPerson(id) {
            var settings = {
                url: "/api/people/" + id,
                method: "GET",
                cache: false,
                responseType: "json",
                contentType: "application/json; charset=UTF-8",
                withCredentials: true
            };
            return $http(settings)
                .then(_getPersonComplete, _getPersonFailed);

            function _getPersonComplete(response) {
                // unwrap the data from the response
                console.log(response);
                return response;

            }

            function _getPersonFailed(error) {
                console.log(error);

                return error;
            }

        }

        //UPDATE Method
        function _putPerson(personData) {
            var settings = {
                url: "/api/people/" + personData.id,
                method: "PUT",
                cache: false,
                responseType: "json",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify(personData),
                withCredentials: true
            };
            return $http(settings)
                .then(_putComplete, _putFailed);

            function _putComplete(response) {
                // unwrap the data from the response
                console.log(response);
                return response;

            }

            function _putFailed(error) {
                console.log(error);
                return error;
            }

        }

        //DELETE Method
        function _deletePerson(id) {
            var settings = {
                url: "/api/people/" + id,
                method: "DELETE",
                cache: false,
                responseType: "json",
                contentType: "application/json; charset=UTF-8",
                data: null,
                withCredentials: true
            };
            return $http(settings)
                .then(_deleteComplete, _deleteFailed);

            function _deleteComplete(response) {
                // unwrap the data from the response
                console.log(response);
                return response;

            }

            function _deleteFailed(error) {
                console.log(error);
                return error;
            }

        }
    }
})();