(function () {
    'use strict';
    var app = angular.module('app');

    app.directive('ngUnique', ['$http', function ($http) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                elem.on('blur', function (evt) {
                    //scope.$apply(function () {
                    //    $http({
                    //        method: 'POST',
                    //        url: 'backendServices/checkUsername.php',
                    //        data: {
                    //            username: elem.val(),
                    //            dbField: attrs.ngUnique
                    //        }
                    //    }).success(function (data, status, headers, config) {
                    //        ctrl.$setValidity('unique', data.status);
                    //    });
                    //});

                    ctrl.$setValidity('unique', false);

                });
            }
        }
    }
    ]);

    var regexpTime = /([01][0-9]|[02][0-3]):[0-5][0-9]/;
    app.directive('validtime', FuncValidTime);
    function FuncValidTime() {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keydown', function (event) {
                    return jsDecimalsTime(event);
                });

                ctrl.$validators.validtime = function (modelValue, viewValue) {
                    if (ctrl.$isEmpty(modelValue)) {
                        // consider empty models to be valid
                        return true;
                    }
                    if (regexpTime.test(viewValue)) {
                        // it is valid
                        return true;
                    }

                    // it is invalid
                    return false;
                };
            }
        }
    }

    app.directive('validendtimelatethanstarttime', FuncValidEndtimeLateThanStartTime);
    function FuncValidEndtimeLateThanStartTime() {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                elem.on('blur', function (evt) {
                    ctrl.$validators.validendtimelatethanstarttime = function (modelValue, viewValue) {

                        debugger;

                        var EndTime = attrs.endtime;
                        var StartTime = attrs.starttime;

                        var startDate = new Date("1/1/1900 " + StartTime);
                        var endDate = new Date("1/1/1900 " + EndTime);

                        //if (ctrl.$isEmpty(StartTime) || ctrl.$isEmpty(EndTime)) {
                        //    // consider empty models to be valid
                        //    return true;
                        //}
                        //if (StartTime && EndTime && (startDate > endDate || startDate == endDate))
                        //    return false;

                        //return true;

                        if (StartTime && EndTime && (startDate > endDate || startDate == endDate)) {
                            ctrl.$setValidity('validendtimelatethanstarttime', false);
                        }
                        else
                            ctrl.$setValidity('validendtimelatethanstarttime', true);

                    };
                });
            }
        }
    }

    app.directive('validdate', FuncValidDate);
    function FuncValidDate() {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {

                elem.on('keydown', function (event) {
                    return jsDecimalsDate(event);
                });

                ctrl.$validators.validdate = function (modelValue, viewValue) {
                    if (ctrl.$isEmpty(modelValue)) {
                        // consider empty models to be valid
                        return true;
                    }
                    if (Date.parse(parseDMYtoMDY(viewValue))) {
                        // it is valid
                        return true;
                    }

                    // it is invalid
                    return false;
                };
            }
        }
    }



})();