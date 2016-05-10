/// <reference path="signUpStep2.drv.js" />
(function () {
	'use strict';

	var app = angular.module('app');

	app.directive('signUpStepTwo', function () {
		return {
			templateUrl: '/app/login/signUpStep2.drv/signUpStep2.tmp.html',
		};
	});
})();



