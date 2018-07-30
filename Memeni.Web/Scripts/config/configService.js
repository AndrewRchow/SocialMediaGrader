(function () {
    angular.module('mainApp')
        .factory('configService', configService);

    configService.$inject = ['$http', '$q'];

    function configService($http, $q) {
        return {
            getAllConfigSettings: _getAllConfigSettings,
            getConfigSettingsById: _getConfigSettingsById,
            postConfigSettings: _postConfigSettings,
            putConfigSettings: _putConfigSettings,
            deleteConfigSettings: _deleteConfigSettings,
            getAllDataCategories: _getAllDataCategories,
            getAllConfigCategories: _getAllConfigCategories
        };
        function _getAllConfigSettings() {
            var settings = {
                url: '/api/config'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllConfigSettingsComplete, _getAllConfigSettingsFailed);
        }
        function _getAllConfigCategories() {
            var settings = {
                url: '/api/configCategory'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllConfigSettingsComplete, _getAllConfigSettingsFailed);
        }
        function _getAllConfigSettingsComplete(data) { 
            //console.log(data);
            return data;
        }
        function _getAllConfigSettingsFailed(error) {
            return $q.reject(error);
        }
        //configDataGetAll
        function _getAllDataCategories() {
            var settings = {
                url: '/api/configdata'
                , method: "GET"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllCategoriesComplete, _getAllCategoriesFailed);
        }
        function _getAllCategoriesComplete(r) {
            //console.log(r);
            return r;
        }
        function _getAllCategoriesFailed(r) {
            return $q.reject(r);
        }
        function _getConfigSettingsById(id) {
            var settings = {
                url: "/api/config/" + id
                , method: "GET"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(id)
                , withCredentials: true
            };
            return $http(settings)
                .then(_successSelectById, _errorSelectById);
        }
        function _successSelectById(r) {
            return r;
        }
        function _errorSelectById(r) {
            return $q.reject(r);
        }
        function _postConfigSettings(data) {
            var settings = {
                url: "/api/config"
                , method: "POST"
                , cache: false
                , contentType: "application/json; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postConfigSettingsComplete, _postConfigSettingsFailed);
        }
        function _postConfigSettingsComplete(r) {
            return r;
        }
        function _postConfigSettingsFailed(r) {
            return $q.reject(r);
        }
        function _putConfigSettings(data, id) {
            var settings = {
                url: "/api/config/" + id
                , method: 'PUT'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_putConfigSettingsComplete, _putConfigSettingsFailed);
        }
        function _putConfigSettingsComplete(r) {
            return r;
        }
        function _putConfigSettingsFailed(r) {
            return $q.reject(r);
        }
        function _deleteConfigSettings(id) {
            var settings = {
                url: "/api/config/" + id
                , method: "DELETE"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "JSON"
                , withCredentials: true
            };
            return $http(settings)
                .then(_deleteConfigSettingsComplete, _deleteConfigSettingsFailed);
        }
        function _deleteConfigSettingsComplete(r) {
            return r;
        }
        function _deleteConfigSettingsFailed(r) {
            return $q.reject(r);
        }
    }
})();