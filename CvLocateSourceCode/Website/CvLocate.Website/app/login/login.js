(function () {
    'use strict';
    var controllerId = 'login';
   
    angular.module('app').controller(controllerId, ['common', 'authentication','$state', login]);

    function login(common, authentication, $state) {
        
        var vm = this;

        vm.signIn = function () {
            $state.go('signIn');
        };

        vm.signUp = function () {
            $state.go('signUp.signUpStep1');
        };

    }
})();