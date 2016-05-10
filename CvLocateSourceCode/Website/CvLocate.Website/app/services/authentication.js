(function () {
    'use strict';
    angular.module('app').service('authorizationService', ['$injector', 'common', '$location', authorizationService]);

    var authenticationModule = angular.module('authentication', ['app', 'common']);
    authenticationModule.factory('authentication', ['$http', 'Base64', 'common', '$timeout', '$location', 'datacontext', authentication]);

    //authenticationModule
    authenticationModule.factory('Base64', [Base64]);
    function Base64() {
        /* jshint ignore:start */

        var keyStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

        return {
            encode: encode,
            decode: decode
        }
        function encode(input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            do {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output +
                    keyStr.charAt(enc1) +
                    keyStr.charAt(enc2) +
                    keyStr.charAt(enc3) +
                    keyStr.charAt(enc4);
                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";
            } while (i < input.length);

            return output;
        }

        function decode(input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
            var base64test = /[^A-Za-z0-9\+\/\=]/g;
            if (base64test.exec(input)) {
                window.alert("There were invalid base64 characters in the input text.\n" +
                    "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                    "Expect errors in decoding.");
            }
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            do {
                enc1 = keyStr.indexOf(input.charAt(i++));
                enc2 = keyStr.indexOf(input.charAt(i++));
                enc3 = keyStr.indexOf(input.charAt(i++));
                enc4 = keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }

                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";

            } while (i < input.length);

            return output;
        }
    };


    function authentication($http, Base64, common, $timeout, $location, datacontext) {
        var service = {
            setCredentials: setCredentials,
            clearCredentials: clearCredentials,
            login: login
        };



        function login(username, password, calback) {

            ///* Dummy authentication for testing, uses $timeout to simulate api call
            // ----------------------------------------------*/
            //$timeout(function () {
            //    var response = { success: username === 'test' && password === 'test' };
            //    if (!response.success) {
            //        response.message = 'Username or password is incorrect';
            //    }
            //    callback(response);
            //}, 1000);


            clearCredentials();

            datacontext.login(Base64.encode(username), Base64.encode(password), common.resourceManager.language, calback);

        }

        // Set credentials in cache and set http header
        function setCredentials(username, password, data) {
            var authdata = Base64.encode(username + ':' + data.SessionGuid);
            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;

            var currentUser = { username: username, authdata: authdata, sessionid: data.SessionGuid };
            common.cacheManager.setItem('globals', { currentUser: currentUser });

            common.cacheManager.setItem('permissions', data.Permissions);

            // init resoures
            if (data.Resources) {
                common.resourceManager.setResources(data.Resources, data.Language);
            }

            //$cookieStore.put('globals', { currentUser: currentUser });
            $location.path('/');
        };

        function clearCredentials() {
            common.cacheManager.setItem('globals', {});
            //$cookieStore.remove('globals');
            $http.defaults.headers.common.Authorization = 'Basic ';
        };

        return service;
    }


    /* jshint ignore:end */

    var accessLevels = {
        notVisible: { id: 1, name: "notVisible" },
        readonly: { id: 2, name: "readonly" },
        editable: { id: 3, name: "editable" },
        permit: { id: 4, name: "permit" },
        notPermit: { id: 5, name: "notPermit" }
    };



    function authorizationService($injector, common, $location) {
        var $rootScope = $injector.get('$rootScope');
        var permissionManager = $injector.get('$rootScope');
        return {

            permissionCheck: permissionCheck,
            getPermission: getPermission,
            getAccessLevel: getAccessLevel
        }


        function permissionCheck(permissionName) {

            // we will return a promise .
            var deferred = common.$q.defer();

            //this is just to keep a pointer to parent scope from within promise scope.
            var parentPointer = this;

            //Check if the current user has required role to access the route
            this.getPermission(permissionName, deferred);

            return deferred.promise;
        }

        //Method to check if the current user has required role to access the route
        //'permissionModel' has permission information obtained from server for current user
        //'roleCollection' is the list of roles which are authorized to access route
        //'deferred' is the object through which we shall resolve promise
        function getPermission(permissionName, deferred) {
            var ifPermissionPassed = false;

            // Get pemission data from cache

            var accessLevel = getAccessLevel(permissionName);
            if (accessLevel && accessLevel != accessLevels.notVisible.id)
                ifPermissionPassed = true;

            if (!ifPermissionPassed) {
                //If user does not have required access, 
                //we will route the user to unauthorized access page
                $location.path('/login');
                //As there could be some delay when location change event happens, 
                //we will keep a watch on $locationChangeSuccess event
                // and would resolve promise when this event occurs.
                $rootScope.$on('$locationChangeSuccess', function (next, current) {
                    deferred.resolve();
                });
            } else {
                deferred.resolve();
            }
        }

        function getAccessLevel(permissionName) {
            var accessLevel;
            // Get pemission data from cache
            var permissions = common.cacheManager.getItem('permissions');
            if (permissions) {
                accessLevel = permissions[permissionName];
            }
            else {
                accessLevel = accessLevels.notVisible.id;
            }

            return accessLevel;
        }

    };

})();