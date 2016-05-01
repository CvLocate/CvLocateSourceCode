(function () {
    'use strict';

    var serviceId = 'usersService';
    angular.module('app').factory(serviceId, ['common', '$http', '$location', 'datacontext', 'cacheManager', 'Base64', userService]);

    function userService(common, $http, $location, datacontext, cacheManager, Base64) {
        function clearCredentials() {
            cacheManager.setItem("user", {});//todo replace"user" to enum ('cacheItems.user')
            $http.defaults.headers.common.Authorization = 'Basic ';
        };

        function setCredentials(userEmail, sessionId) {
            var authdata = Base64.encode(userEmail + ':' + sessionId);
            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;
            $location.path('/'); //???? what is it?
        };


        function getLoggedInUser() {
            return cacheManager.getItem("user");//todo replace"user" to enum ('cacheItems.user')
        };

        function onSignIn(userEmail, userType, sessionId) {
            setCredentials(userEmail, sessionId);
            cacheManager.setItem("user", { userEmail: userEmail, userType: userType, sessionId: sessionId });//todo replace"user" key to enum ('cacheItems.user') and the user object to js class
        };

        function signUp(params, callback) {
            clearCredentials();
            datacontext.users.signUp(params).then(function onSignUpResponse(response) {
                if (!response.data.Error) {
                    onSignIn(params.Email, response.data.UserType, response.data.SessionId);
                }
                callback(response.data);
            });
        };

        function signIn(params, callback) {
            clearCredentials();

        };

        function signOut(callback) {
            datacontext.users.signOut().then(
                function onSignOutResponse(response) {
                    if (!response.data.Error) {
                        clearCredentials();
                    }
                    if (callback) {
                        callback(response.data);
                    }
                });
        };




        var service = {
            signUp: signUp,
            signIn: signIn,
            signOut: signOut,
            getLoggedInUser: getLoggedInUser
        };

        return service;

    };
})();