(function () {
    'use strict';

    var serviceId = 'usersService';
    angular.module('app').factory(serviceId, ['common', '$http', 'datacontext', userService]);

    function userService(common, $http, datacontext) {
        var serviceBase = '/api/Users';

        var service = {
            signUp: signUp
        };

        return service;

        function signUp(params) {
            return datacontext.users.signUp(params);
        };

       

    };
})();