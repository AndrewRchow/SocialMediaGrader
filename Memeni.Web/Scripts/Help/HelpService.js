(function () {
    'use strict';

    angular
        .module('mainApp')
        .factory('helpService', helpService);

    helpService.$inject = ["$http", "$q"];

    function helpService($http, $q) {

        //method indentifiers object
        return {
            post: _Post,                   
            getAll: _GetAll,
            getById: _GetById,
            getByUrl: _GetByUrl,
            getCurrentAdmin: _GetCurrentAdmin,
            getCategories: _GetCategories,
            gridPost: _GridPost,
            put: _Put,
            delete: _Delete,
            logOutUser: _logOutUser
        };

        // El Folderino

// ====[ POST ]===============================================================
        function _Post(data) {

            return $http.post("/api/help", data)
                .then(_PostSuccess)
                .catch(_PostFail);

            function _PostSuccess(response) {
                console.log("post to server succesful");
                return response;
            }

            function _PostFail(error) {
                console.log("post to server unsuccesful");
                return error;
            }
        }

// ====[ GET ALL ]============================================================
        function _GetAll() {

            return $http.get("/api/help/")
                .then(_GetAllSuccess)
                .catch(_GetAllFail);

            function _GetAllSuccess(response) {
                console.log("data retrieved successful");
                return response;
            }

            function _GetAllFail(error) {
                console.log("error getting data from server");
                return error;
            }
        }

// ====[ GET BY HELP ITEM ID ]================================================
        function _GetById(id) {

            return $http.get("/api/help/" + id)
                .then(_GetByIdSuccess)
                .catch(_GetByIdFail);

            function _GetByIdSuccess(response) {
                console.log("get by id successful");
                return response;
            }

            function _GetByIdFail(error) {
                console.log("get by id failed");
                return error;
            }
        }

// ====[ GET HELP CATEGORIES ]================================================
        function _GetCategories() {

            return $http.get("/api/help/categories")
                .then(_GetCategoriesSuccess)
                .catch(_GetCategoriesFail);

            function _GetCategoriesSuccess(response) {
                return response;
            }

            function _GetCategoriesFail(error) {
                return error;
            }
        }

// ====[ GET BY URL ]=========================================================
        function _GetByUrl(url) {


            return $http.post("/api/help/items?pageUrl=" + url)
                .then(_getByUrlGood)
                .catch(_getByUrlBad);

            function _getByUrlGood(response) {
                console.log("get by url good");
                return response;
            }

            function _getByUrlBad(error) {
                console.log("get by url bad");
                return error;
            }
        }

// ====[ GET CURRENT ADMIN IDENTITY ]=========================================
        function _GetCurrentAdmin() {

            return $http.get("/api/auth/current/roles/admin")
                .then(_getUserGood)
                .catch(_getUserBad);

            function _getUserGood(response) { 
                return response;
            }
            function _getUserBad(error) {
                return error;
            }
        }

// ====[ HELP GRID (POST) ]===================================================
        function _GridPost(data) {

            return $http.post("/api/help/grid", data)
                .then(_GridPostSuccess)
                .catch(_GridPostFail);

            function _GridPostSuccess(response) {
                console.log("successful post to grid");
                return response.data.item;
            }
            function _GridPostFail(error) {
                console.log("error posting to grid");
                return error;
            }
        }
        
// ====[ PUT ]================================================================
        function _Put(id, data) {

            return $http.put("/api/help/" + id, data)
                .then(_PutSuccess)
                .catch(_PutFail);

            function _PutSuccess(response) {
                return response;
            }

            function _PutFail(error) {
                return error;
            }
        }

// ====[ DELETE ]=============================================================
        function _Delete(id) {

            return $http.delete("/api/help/" + id)
                .then(_DeleteSuccess)
                .catch(_DeleteFail);

            function _DeleteSuccess(response) {
                return response;
            }

            function _DeleteFail(error) {
                return error;
            }
        }

// ====[ LOGOUT ]=============================================================
        function _logOutUser() {
            return $http.get("/api/auth/logout", { withCredentials: true })
                .then(_logOutUserSuccess, _logOutUserError);
        }
        function _logOutUserSuccess(resp) {
            return resp;
        }
        function _logOutUserError(err) {
            return $q.reject(err);
        }
    }
})();