(function () {
    'use strict';
    var controllerId = 'signUp';

    angular.module('app').controller(controllerId, ['common', 'enums','authentication','usersService','resourceManager','cacheManager', '$state', signUp]);

    function signUp(common, enums, authentication, usersService,resourceManager,cacheManager, $state) {

        var vm = this;
        vm.enums = enums;
        vm.resourceManager = resourceManager;

        vm.signUpStep1 = {
            email: null,
            password: null,
            userType: 1
        }

        vm.nextStep = function () {
            usersService.signUp(this.signUpStep1).then(function signUpThen(response)
            {
                if (response.data.Error) {
                    switch (response.Error) {
                        case vm.enums.serverErrors.emailAlreadyExists.id:
                            break;
                        default:

                    }
                    return;
                }

                cacheManager.setItem("user", { id: response.data.UserId, userType: response.data.UserType });//todo replace key to enum and the user object to js class
                $state.go('signUp.signUpStep2');
            });
           
        }
    }
})();