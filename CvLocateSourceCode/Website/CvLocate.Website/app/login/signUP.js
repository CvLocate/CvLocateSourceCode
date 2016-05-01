(function () {
    'use strict';
    var controllerId = 'signUp';

    angular.module('app').controller(controllerId, ['common', 'enums', 'authentication', 'usersService', 'resourceManager', '$state', signUp]);

    function signUp(common, enums, authentication, usersService, resourceManager, $state) {

        var vm = this;
        vm.enums = enums;
        vm.resourceManager = resourceManager;

        vm.signUpStep1 = {
            email: null,
            password: null,
            userType: 1
        }

        vm.nextStep = function () {
           
            usersService.signUp(this.signUpStep1,
                function onSignUp(response) {
                    if (response.Error) {
                        switch (response.Error) {
                            case vm.enums.serverErrors.emailAlreadyExists.id:
                                break;
                            default:

                        }
                        return;
                    }
                    $state.go('signUp.signUpStep2');
                });

        }
    }
})();