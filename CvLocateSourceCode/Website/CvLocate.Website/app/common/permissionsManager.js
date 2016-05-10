(function () {
    'use strict';

    angular.module('app').directive('permission', ['authorizationService', permissiondirective]);


    var accessLevels = {
        notVisible: { id: 1, name: "notVisible" },
        readonly: { id: 2, name: "readonly" },
        editable: { id: 3, name: "editable" },
        permit: { id: 4, name: "permit" },
        notPermit: { id: 5, name: "notPermit" }
    };

    function getAccessLevel(common, permissionName) {
        var accessLevel;
        // Get pemission data from cache
        var permissions = common.cacheManager.getItem('permissions');
        if (permissions) {
            accessLevel = permissions[permissionName];
        }
        else {
            accessLevel = accessLevels.notVisible;
        }

        return accessLevel;
    }

    function permissiondirective(authorizationService) {
        var link = function (scope, element, attrs) {
            var accessLevel = authorizationService.getAccessLevel(attrs.permission);//getAccessLevel(common, attrs.permission);

            switch (accessLevel) {
                case accessLevels.notVisible.id:
                    $(element).remove()//addClass("not-visible");
                    break;
                case accessLevels.readonly.id:
                    $(element).attr("disabled", "disabled");
                    $(element).children().attr("disabled", "disabled");
                    break;
                case accessLevels.editable.id: break;
                case accessLevels.permit.id: break;
                case accessLevels.notPermit.id:
                    $(element).attr("disabled", "disabled");
                    $(element).find("*").attr("disabled", "disabled");
                    break;
                default: break;
            }
        }

        return {
            restrict: 'EA',
            link: link
        };
    };



}());

