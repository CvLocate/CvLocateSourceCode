(function () {
    'use strict';
    var controllerId = 'signUp';

    angular.module('app').controller(controllerId, ['common', 'enums', 'authentication', 'usersService', 'resourceManager', '$state', signUp]);

    function signUp(common, enums, authentication, usersService, resourceManager, $state) {

        var vm = this;
        vm.enums = enums;
        vm.resourceManager = resourceManager;
        vm.reenterPassword;
        vm.signUpStep1Form='';

        vm.signUpStep1 = {
            email: null,
            password: null,
            userType: 1
        }
        vm.resetEmailValidation = function resetEmailValidation(signUpForm) {
            signUpForm.email.$setValidity("emailExists", true);
        }

        vm.getSrc =  'C:\\1.jpg';
        vm.checkEmail = function checkEmail(signUpForm) {
            vm.signUpStep1Form = signUpForm;
            if (vm.signUpStep1.email) {
                usersService.checkEmail({ email: vm.signUpStep1.email },
                    function onCheckEmail(response) {
                        if (response.IsFreeEmail == false) {
                            signUpForm.email.$setValidity("emailExists", false);
                        }
                        else
                            signUpForm.email.$setValidity("emailExists", true);
                    });
            }
        };

        vm.checkPassword = function checkPassword(signUpForm) {
            if (vm.signUpStep1.password && vm.reenterPassword) {
                if(vm.signUpStep1.password == vm.reenterPassword)
                    signUpForm.reenterPassword.$setValidity("equalPassword", true);
                else
                    signUpForm.reenterPassword.$setValidity("equalPassword", false);
            }
        };

       
        vm.nextStep = function () {
            usersService.signUp(this.signUpStep1,
                function onSignUp(response) {
                    if (response.Error) {
                        switch (response.Error) {
                            case vm.enums.serverErrors.emailAlreadyExists.id:
                                vm.signUpStep1Form.email.$setValidity("emailExists", false);
                                break;
                            default: vm.signUpStep1Form.email.$setValidity("emailExists", false);
                        }
                    }
                    else {
                        $state.go('signUp.signUpStep2');
                    }
                });

        }
    }
})();