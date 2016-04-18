/// <reference path="signUpStep1.drv.js" />
(function () {
    'use strict';

    var app = angular.module('app');

    app.directive('signUpStepOne', function () {
        return {
            templateUrl: '/app/login/signUpStep1.drv/signUpStep1.tmp.html',
        };
    });
})();



