var bowlersController = angular.module('bowlingController', [])

bowlersController.controller('bowlersListController', ['$scope', function ($scope) {
    var bowlers = [{ id: 1, name: 'Ed' }];

    $scope.bowlers = bowlers;
}]);