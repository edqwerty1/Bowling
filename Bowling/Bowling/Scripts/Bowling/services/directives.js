(function () {
    'use strict';

    var app = angular.module('bowlingApp');

    app.directive('ccCard', function () {
        return {
            restrict: 'EA',
            scope: {
                name: '@'
            },
            template: '' +
                '<div class="card">' +
                 '<h2>Name: {{name}} </h2>' +
                 '<div ng-transclude></div>' +
                 '</div>',
            replace: true,
            transclude: true
        };
    });
})();