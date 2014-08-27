(function () {
    'use strict';

    var serviceId = 'bowlerService';
    angular.module('bowlingApp').factory(serviceId, ['$http','common', bowlerService]);

    function bowlerService($http, common) {
        var $q = common.$q;

        var service = {
            getPeople: getPeople,
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
    }
})();