// Include in index.html so that app level exceptions are handled.
// Exclude from testRunner.html which should run exactly what it wants to run
(function () {
    'use strict';

    var app = angular.module('app');

    // Configure by setting an optional string value for appErrorPrefix.
    // Accessible via config.appErrorPrefix (via config value).
    app.config(['$provide', function ($provide) {
        $provide.decorator('$exceptionHandler',
            ['$delegate', 'config', '$injector', extendExceptionHandler]);
    }]);

    // Extend the $exceptionHandler service to also display a toast.
    function extendExceptionHandler($delegate, config, $injector) {
        var appErrorPrefix = config.appErrorPrefix;

        return function (exception, cause) {

            var logger = $injector.get('logger');
            var logError = logger.getLogFn('app', 'error');
            var appErrorPrefix = config.appErrorPrefix;
            var datacontext = $injector.get('datacontext');

            $delegate(exception, cause);
            if (appErrorPrefix && exception.message.indexOf(appErrorPrefix) === 0) { return; }

            var errorData = { exception: exception, cause: cause };
            var msg = appErrorPrefix + exception.message;
            logError(msg, errorData, true);

            // Write error to server log
            datacontext.common.writeLog(msg, exception.stack, cause);

        };
    }


    app.config(['$httpProvider', function ($httpProvider) {
        //Http Intercpetor to check auth failures for xhr requests
        $httpProvider.interceptors.push('authHttpResponseInterceptor');
    }]);
    app.factory('authHttpResponseInterceptor', ['$q','cacheManager', '$location', 'config', authHttpResponseInterceptor]);

    function authHttpResponseInterceptor($q,cacheManager, $location, config) {
        return {
            response: response,
            responseError: responseError
        }

        function response(response) {
            if (response.status === 401) {
                console.error("Response 401");
            }
            return response || $q.when(response);
        }
        function responseError(rejection) {
            if (rejection.status === 401) {
                console.error("Response Error 401 : " + rejection.statusText);
                $location.path('/login').search('returnTo', $location.path());
            }
            else if (rejection.status === 405) {
                console.error("Response Error 405 : " + rejection.statusText);
                cacheManager.setItem('error','Not Allowed!!!');
                $location.path('/error');
            }
            else {
                //throw new Error("Response Error " + rejection.status + " : " + rejection.statusText);
                console.error("Response Error " + rejection.status, rejection.statusText);
            }
            return $q.reject(rejection);
        }

    }



})();