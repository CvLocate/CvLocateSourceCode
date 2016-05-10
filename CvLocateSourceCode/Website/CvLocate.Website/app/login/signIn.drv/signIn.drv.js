(function () {
    'use strict';

    var app = angular.module('app');

    app.directive('signIn', function () {
        return {
            templateUrl: '/app/login/signIn.drv/signIn.tmp.html',
        };
    });
})();



