(function () {
    'use strict';

    var app = angular.module('bowlingApp');

    app.factory('authInterceptor', function ($rootScope, $q, $window) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.getItem("accessToken")) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.getItem("accessToken");
                }
                return config;
            },
            response: function (response) {
                if (response.status === 401) {
                    // handle case where user is not authenticated
                }
                return response || $q.when(response);
            }
        };
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });
})();