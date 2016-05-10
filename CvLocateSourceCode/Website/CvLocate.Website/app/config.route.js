(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('states', getStates());

    // Configure the routes and route resolvers
    app.config(['$stateProvider', '$urlRouterProvider', 'states', stateConfigurator]).run(['$window', 'commonConfig', '$state', '$injector', '$location', 'common', '$http', 'authentication', '$anchorScroll', stateRun]);

    function stateConfigurator($stateProvider, $urlRouterProvider, states) {

        states.forEach(function (s) {
            $stateProvider.state(s.stateName, s.config);
        });

        $urlRouterProvider.otherwise('/');
    }

    function stateRun($window, commonConfig, $state, $injector, $location, common, $http, authentication, $anchorScroll) {

        var $rootScope = $injector.get('$rootScope');

        //init resources
        //common.resourceManager.getResources()
        //       .then(function getResourcesSuccess(data) {
        //           return $rootScope.resources = data;
        //       });


        //// keep user logged in after page refresh
        //$rootScope.globals = $cookieStore.get('globals') || {};
        //if ($rootScope.globals.currentUser) {
        //    authentication.currentUser = $rootScope.globals.currentUser;
        //    common.cacheManager.setItem('globals', { currentUser: $rootScope.globals.currentUser });
        //    $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
        //}

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in

            var x = 0;
            //var globalData = common.cacheManager.getItem('globals');
            //if ($location.path() !== '/login' && (!globalData || !globalData.currentUser)) {
            //    $location.path('/login');
            //}
        });

        //$rootScope.$on("$locationChangeSuccess", function () {
        //    //$anchorScroll();
        //    $window.scrollTo(0, 0);
        //});

        $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
            if (error.Message == 'permissionNotPassed') {
                $state.go('login');
            }
            else {
                common.cacheManager.setItem('error', error.Message);
                $state.go('error');
            }
        });


    }



    // Define the routes 
    function getStates() {
        return [
            //{
            //    stateName: 'homepage',
            //    config: {
            //        url: '/',
            //        templateUrl: 'app/homepage/homepage.html',
            //        controller: 'homepage',
            //        controllerAs: 'vm',
            //        resolve: {
                        
            //        }
            //    }
            //},
            {
                stateName: 'error',
                config: {
                    url: '/error',
                    templateUrl: 'app/layout/error.html',
                }
            },
            {
                stateName: 'login',
                config: {
                    url: '/',
                    title: 'login',
                    templateUrl: 'app/login/login.html',
                }
            },
            {
                stateName: 'signIn',
                config: {
                    url: '/signIn',
                    title: 'signIn',
                    templateUrl: 'app/login/signIn.drv/signIn.tmp.html',
                }
            },
            {
                stateName: 'signUp',
                config: {
                    url: '/signUp',
                    title: 'signUp',
                    templateUrl: 'app/login/signUp.html',
                    controller: 'signUp',
                    controllerAs: 'vm',
                }
            },
            {
                stateName: 'signUp.signUpStep1',
                config: {
                    url: '/step1',
                    title: 'signUp',
                    templateUrl: 'app/login/signUpStep1.drv/signUpStep1.tmp.html',
                }
            },
            {
                stateName: 'signUp.signUpStep2',
                config: {
                    url: '/step2',
                    title: 'signUp',
                    templateUrl: 'app/login/signUpStep2.drv/signUpStep2.tmp.html',
                }
            },
        ];
    }
})();

