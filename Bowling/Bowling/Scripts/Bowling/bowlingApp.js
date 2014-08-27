(function () {
    'use strict';
    
    
    
    var app = angular.module('bowlingApp', [ 
        // Angular modules
        'ngRoute',      // routing

        // Custom modules
        'common',       // common functions 
    ]);

    // Handle routing errors and success events
    app.run(['$route', function ($route) {
        // Include $route to kick start the router
    }])
})();

