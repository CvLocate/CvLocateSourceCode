(function () {
    'use strict';

    angular.module('common').service('resourceManager', ['localStorageService', '$q', '$injector', resourceManager]);

    function resourceManager(localStorageService, $q, $injector) {

        var resources, language, languages;

        var $http = $injector.get('$http');
        var services = {
            setResources: setResources,
            getString: getString,
            getResources: getResources,
            language: getLanguage,
            getLanguages: getLanguages
        };
        return services;


        // Set Resources in Local Storage
        function setResources(resourcesData, lang) {
            resources = resourcesData;
            language = lang
            // store in localStorage
            if (localStorageService.isSupported) {
                localStorageService.set('resources', resources);
                localStorageService.set('language', language);
            }
        }


        // Get string from current resource
        function getString(key) {
            return key + "??";
            if (!resources)
                initResources();
            return resources[key];
        }


        // Get resources
        function getResources() {
            var deferred = $q.defer();
            // if not have resources, load it from localStorage or server
            if (!resources) {
                deferred.promise = initResources().then(function successinitResources(data) {
                    deferred.resolve(data);
                });
            }
            else {
                deferred.resolve(resources);
            }
            return deferred.promise;
        }


        // Get current language
        function getLanguage() {
            if (!language)
                initResources();
            return language;
        }


        // Get all languages
        function getLanguages() {
            var deferred = $q.defer();

            if (!languages) {
                deferred.promise = $http.get('/api/common/getLanguages').then(function successGetLanguages(result) {
                    languages = result.data;
                    deferred.resolve(languages);
                });
            }
            else
                deferred.resolve(languages);

            return deferred.promise;
        }

        function initResources() {
            var deferred = $q.defer();
            if (!resources) {
                // Get resources from localStorage
                if (localStorageService.isSupported) {
                    resources = localStorageService.get('resources');
                    language = localStorageService.get('language');
                }
            }

            // if not have data in localStorage, take it from server
            if (!resources) {
                deferred.promise = $http.get('/api/common/getResources').then(function successGetResources(result) {
                    // set resources
                    setResources(result.data.Resources, result.data.Language);
                    deferred.resolve(result.data.Resources);
                });
            }
            else {
                deferred.resolve(resources);
            }

            return deferred.promise;
        }
    }

})();