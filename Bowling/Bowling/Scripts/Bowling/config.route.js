(function () {
    'use strict';

    var app = angular.module('bowlingApp');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', '$locationProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, $locationProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/Bowling/Bowlers/' });
        $locationProvider.html5Mode(true);
    }

    // Define the routes 
    function getRoutes() {
        var viewBase = '/Areas/Bowling/app/views/'
        return [
            {
                url: '/Bowling/',
                config: {
                    templateUrl: viewBase + 'bowlers.html',
                    title: 'Bowlers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Bowlers'
                    }
                }
            },
            {
                url: '/Bowling/Bowlers',
                config: {
                    templateUrl: viewBase + 'bowlers.html',
                    title: 'Bowlers',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Bowlers'
                    }
                }
            }, {
                url: '/Bowling/Bowlers/Me',
                config: {
                    title: 'Me',
                    templateUrl: viewBase + 'me.html',
                    caseInsensitiveMatch: true,
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Me'
                    }
                }
            }
        ];
    }
})();