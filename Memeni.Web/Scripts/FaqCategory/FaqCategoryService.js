(function () {
    angular.module('mainApp')
        .factory('FaqCategoryService', FaqCategoryService);

    FaqCategoryService.$inject = ['$http', '$q'];

    function FaqCategoryService($http, $q) {
        return {
            getFaqsCategories: _getAllFaqCategories,
            getCategoryById: _getCategoryById,
            postFaq: _postFaq,
            putFaq: _putFaq,
            deleteFaq: _deleteFaq,
            getAllFaqCategories: _getAllFaqCategories
        };
        //function _getCategoryById() {
        //    var settings = {
        //        url: '/api/faqcategory'
        //        , cache: false
        //        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //        , responseType: "json"
        //        , withCredentials: true
        //    };
        //    return $http(settings)
        //        .then(_getAllFaqsComplete, _getAllFaqsFailed);
        //}
        function _getAllFaqsComplete(data) {
            //console.log(data);
            return data;
        }
        function _getAllFaqsFailed(error) {
            return $q.reject(error);
        }
        //configDataGetAll
        function _getAllFaqCategories() {
            var settings = {
                url: '/api/faqcategory'
                , method: "GET"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "json"
                , withCredentials: true
            };
            return $http(settings)
                .then(_getAllFaqsComplete, _getAllFaqsFailed);
        }
        function _getCategoryById(id) {
            var settings = {
                url: "/api/faqcategory/" + id
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
        function _postFaq(data) {
            var settings = {
                url: "/api/faqcategory"
                , method: "POST"
                , cache: false
                , contentType: "application/json; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_postComplete, _postFailed);
        }
        function _postComplete(r) {
            return r;
        }
        function _postFailed(r) {
            return $q.reject(r);
        }
        function _putFaq(data, id) {
            var settings = {
                url: "/api/faqcategory/" + id
                , method: 'PUT'
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , data: JSON.stringify(data)
                , withCredentials: true
            };
            return $http(settings)
                .then(_putComplete, _putFailed);
        }
        function _putComplete(r) {
            return r;
        }
        function _putFailed(r) {
            return $q.reject(r);
        }
        function _deleteFaq(id) {
            var settings = {
                url: "/api/faqcategory/" + id
                , method: "DELETE"
                , cache: false
                , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                , responseType: "JSON"
                , withCredentials: true
            };
            return $http(settings)
                .then(_deleteComplete, _deleteFailed);
        }
        function _deleteComplete(r) {
            return r;
        }
        function _deleteFailed(r) {
            return $q.reject(r);
        }
    }
})();