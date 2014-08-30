(function () {
    'use strict';

    var serviceId = 'bowlerService';
    angular.module('bowlingApp').factory(serviceId, ['$http','$window','common', bowlerService]);

    function bowlerService($http, $window, common) {
        var $q = common.$q;

        var service = {
            getPeople: getPeople,
            getMe: getMe,
            getMessageCount: getMessageCount
        };

        return service;

        function getMessageCount() { return $q.when(72); }

        function getPeople() {
            var deferred = new $q.defer();
            $http.get('http://localhost:57855/api/bowlers')
            .success(function (resp) {
                deferred.resolve(resp);

            }).error(function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        }

        function getMe() {
            var deferred = new $q.defer();
            var token = $window.sessionStorage.getItem("accessToken");
            $http.get('http://localhost:57855/api/bowlers/Me')
            .success(function (resp) {
                deferred.resolve(resp);

            }).error(function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        }
    }
})();