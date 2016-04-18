(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, ['$http', 'common', datacontext]);

    function datacontext($http, common) {

        var $q = common.$q;




        var users =
            {
                signUp: function signUp(params) {
                    return $http.post('/api/Users/signUp', params);
                }
            };

        var common =
            {
                writeLog: function writeLog(errorMsg, stack, cause) {
                    $http.post('/api/logger/WriteLog', { Msg: errorMsg, Stack: stack, Cause: cause })
                .error(function (err) { alert("Can't send error to server (" + err + ")") });
                }

            }
        var service = {
            users: users,
            common:common
        };

        return service;


        function getMessageCount() { return $q.when(72); }

        function getPeople(options) {
            var req = $http.get('/api/employee');
            if (options) {
                req.success(
                      function (results) {
                          options.success(results.employees);
                      })
            }
            else {
                return req;
            }
        }

        function login(userName, password, language, callback) {
            $http.post('/api/login/login', { username: userName, userPassword: password, language: language })
                .success(function (response) {
                    callback(response);
                });
        }

        function getClients(callback) {
            get('/api/clients', undefined, callback);
        }

        function writeLog(errorMsg, stack, cause) {
            $http.post('/api/logger/WriteLog', { Msg: errorMsg, Stack: stack, Cause: cause })
              .error(function (err) { alert("Can't send error to server (" + err + ")") });
        }

        function post(api, data, callback) {
            return $http.post(api, data)
        }

        function get(api, data) {
            return $http.get(api, data)

        }
        function put(api, data) {
            return $http.put(api, data)

        }
        function deletepost(api, data) {
            return $http.delete(api, data)
        }

        function success(response, callback) {
            if (response.status = 200) {
                callback(response.data);
            }
        }

        function uploadFile(file, uploadUrl, calback) {
            var fd = new FormData();
            fd.append('file', file);
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(calback).error(function () { });
        }

        function getResources() {
            return $http.get('/api/common/getResources');
        }

        function getLanguages() {
            return $http.get('/api/common/getLanguages');
        }

        function getRoles() {
            return $http.get('/api/common/getRoles');
        }
    }
})();