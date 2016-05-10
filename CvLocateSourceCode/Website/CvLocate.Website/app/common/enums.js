(function () {
    'use strict';

    // Define the common module 
    // Contains services:
    //  - common
    //  - logger
    //  - spinner
    var app = angular.module('app');
    app.constant('enums', {
        userTypeEnum:
           {
               recruiter: { id: 1, resourceKey: "resRecruiter" },
               candidate: { id: 2, resourceKey: "resCandidate" }
           },
        serverErrors:
            {
                unknownError: { id: 1, resourceKey: "resUnknownError" },
                emailAlreadyExists: { id: 2, resourceKey: "resEmailAlreadyExists" },
                emailPasswordNotFound: { id: 3, resourceKey: "resEmailPasswordNotFound" },
            }

    });

})();