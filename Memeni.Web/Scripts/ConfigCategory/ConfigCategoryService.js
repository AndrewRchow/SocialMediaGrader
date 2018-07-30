(function () {
    angular
        .module('mainApp')
        .factory('configDataService', configDataService);

    configDataService.$inject = ['$http', '$q'];

    function configDataService($http, $q) {
        return {
            getAllCategories: _getAllCategories,
            getConfigCategorySettingsById: _getConfigCategorySettingsById,
            postConfigCategorySettings: _postConfigCategorySettings,
            putConfigCategorySettings: _putConfigCategorySettings,
            deleteConfigDataSettings: _deleteConfigDataSettings
        };
        function _getAllCategories() {
            var settings = {
                url: '/api/configcategory'
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
            return $q.reject(error);
        }
        function _getConfigCategorySettingsById(id) {
            var settings = {
                url: "/api/configcategory/" + id
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
        function _postConfigCategorySettings(data) {
            var settings = {
                url: "/api/configcategory"
                , method: "POST"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postConfigCategorySettingsComplete, _postConfigCategorySettingsFailed);
        }
        function _postConfigCategorySettingsComplete(r) {
            return r;
        }
        function _postConfigCategorySettingsFailed(r) {
            return $q.reject(error);
        }
        function _putConfigCategorySettings(data, id) {
            var settings = {
                url: "/api/configcategory/" + id
                , method: 'PUT'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_putConfigCategorySettingsComplete, _putConfigCategorySettingsFailed);
        }
        function _putConfigCategorySettingsComplete(r) {
            return r;
        }
        function _putConfigCategorySettingsFailed(r) {
            return $q.reject(r);
        }
        function _deleteConfigDataSettings(id) {
            var settings = {
                url: "/api/configcategory/" + id
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