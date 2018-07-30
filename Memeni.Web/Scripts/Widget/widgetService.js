(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('widgetService', widgetService);

    widgetService.$inject = ["$http", "$q"];

    function widgetService($http, $q) {

        //method indentifiers object
        return {
            getFacebookInfo: _GetFacebookInfo,
            getFacebookImage: _GetFacebookImage,
            postWidgetSalesforce: _postWidgetSalesforce
        };

        // El Folderino

// ====[ GET FACEBOOK STUFF ]============================================================

        function _GetFacebookInfo(site) {
            return $http.get("/api/widget/profile/" + site)
                .then(_GetInfoSuccess)
                .catch(_GetInfoFail);
        }
        function _GetInfoSuccess(response) {
            response = JSON.parse(response.data);
            return response;
        }
        function _GetInfoFail(error) {
            return error;
        }

        function _GetFacebookImage(id) {
            return $http.get("/api/widget/profile/picture/" + id)
                .then(_getImageGood)
                .catch(_getImageBad);
        }
        function _getImageGood(response) {
            response = JSON.parse(response.data);
            return response;
        }
        function _getImageBad(error) {
            return error;
        }

        function _postWidgetSalesforce(data) {
            return $http.post('/api/salesforce/widget', data)
                .then(_postWidgetSalesforceGood, _postWidgetSalesforceBad);
        }
        function _postWidgetSalesforceGood(response) {
            return response;
        }
        function _postWidgetSalesforceBad(error) {
            return $q.reject(error);
        }







        





    }
})();