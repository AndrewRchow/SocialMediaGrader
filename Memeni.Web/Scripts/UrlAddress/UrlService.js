(function () {
    angular
        .module('mainApp')
        .factory("UrlService", UrlService);

    UrlService.$inject = ['$http', '$q'];
    function UrlService($http, $q) {
        return {
            getAllUrlAddr: _getAllUrlAddr
            , getUrlAddrById: _getUrlAddrById
            , postUrlAddr: _postUrlAddr
            , putUrlAddr: _putUrlAddr
            , deleteUrlAddr: _deleteUrlAddr
        }

        function _getAllUrlAddr() {
            var settings = {
                url: "/api/UrlAddress"
                , method: "GET"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllUrlAddrComplete, _getAllUrlAddrFailed)
        }
        function _getAllUrlAddrComplete(data) {
            return data;
        }
        function _getAllUrlAddrFailed(error) {
            return $q.reject(error);
        }

        function _getUrlAddrById(id) {
            var settings = {
                url: "/api/UrlAddress/" + id
                , method: "GET"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getUrlAddrByIdComplete, _getUrlAddrByIdFailed)
        }
        function _getUrlAddrByIdComplete(data) {
            return data;
        }
        function _getUrlAddrByIdFailed(error) {
            return $q.reject(error);
        }

        function _postUrlAddr(urlData) {
            var settings = {
                url: "/api/UrlAddress"
                , method: "POST"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(urlData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postUrlAddrComplete, _postUrlAddrFailed)
        }
        function _postUrlAddrComplete(data) {
            return data;
        }
        function _postUrlAddrFailed(error) {
            return $q.reject(error);
        }

        function _putUrlAddr(urlData, id) {
            var settings = {
                url: "/api/UrlAddress/" + id
                , method: "PUT"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(urlData)
                , withCredentials: true
            };
            return $http(settings)
                .then(_putUrlAddrComplete, _putUrlAddrFailed)
        }
        function _putUrlAddrComplete(data) {
            return data;
        }
        function _putUrlAddrFailed(error) {
            return $q.reject(error);
        }

        function _deleteUrlAddr(id) {
            var settings = {
                url: "/api/UrlAddress/" + id
                , method: "DELETE"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_deleteUrlAddrComplete, _deleteUrlAddrFailed)
        }
        function _deleteUrlAddrComplete(data) {
            return data;
        }
        function _deleteUrlAddrFailed(error) {
            return $q.reject(error);
        }

    }

})();