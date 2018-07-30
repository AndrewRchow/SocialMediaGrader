(function () {
    angular
        .module('mainApp')
        .factory('configDataService1', configDataService1);

    configDataService1.$inject = ['$http', '$q'];

    function configDataService1($http, $q) {
        return {
            getAllDataCategories: _getAllDataCategories,
            getConfigDataSettingsById: _getConfigDataSettingsById,
            postConfigDataSettings: _postConfigDataSettings,
            putConfigDataSettings: _putConfigDataSettings,
            deleteConfigDataSettings: _deleteConfigDataSettings
        };
        function _getAllDataCategories() {
            var settings = {
                url: '/api/configdata'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllCategoriesComplete, _getAllCategoriesFailed);
        }
        function _getAllCategoriesComplete(r) {
            console.log(r);
            return r;
        }
        function _getAllCategoriesFailed(r) {
            return $q.reject(r);
        }
        function _getConfigDataSettingsById(id) {
            var settings = {
                url: "/api/configdata/" + id
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
            return $q.reject(error);
        }
        function _postConfigDataSettings(data) {
            var settings = {
                url: "/api/configdata"
                , method: "POST"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postConfigDataSettingsComplete, _postConfigDataSettingsFailed);
        }
        function _postConfigDataSettingsComplete(r) {
            return r;
        }
        function _postConfigDataSettingsFailed(r) {
            return $q.reject(error);
        }
        function _putConfigDataSettings(data, id) {
            var settings = {
                url: "/api/configdata/" + id
                , method: 'PUT'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_putConfigDataSettingsComplete, _putConfigDataSettingsFailed);
        }
        function _putConfigDataSettingsComplete(r) {
            return r;
        }
        function _putConfigDataSettingsFailed(r) {
            return $q.reject(r);
        }
        function _deleteConfigDataSettings(id) {
            var settings = {
                url: "/api/configdata/" + id
                , method: "DELETE"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "JSON"
                , withCredentials: true
            };
            return $http(settings)
                .then(_deleteConfigDataSettingsComplete, _deleteConfigDataSettingsFailed);
        }
        function _deleteConfigDataSettingsComplete(r) {
            return r;
        }
        function _deleteConfigDataSettingsFailed(r) {
            return $q.reject(error);
        }
    }
})();