(function () {
    'use strict';

    // Define the common module 
    // Contains services:
    //  - common
    //  - logger
    //  - spinner
    var app = angular.module('app');
    app.constant('resources.en-US', {
                signup: "Sign Up" ,
                resRecruiter: "Recruiter"
      });
})();