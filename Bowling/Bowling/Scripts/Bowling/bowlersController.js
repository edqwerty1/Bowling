(function () {
    'use strict';
    var controllerId = 'bowlersController';
    angular.module('bowlingApp').controller(controllerId, ['common', 'bowlerService', bowlersController]);

    function bowlersController(common, bowlerService) {

        var vm = this;
        vm.news = {
            title: 'Hot Towel Angular',
            description: 'Hot Towel Angular is a SPA template for Angular developers.'
        };
        vm.messageCount = 0;
        vm.people = [];
        vm.title = 'bowlersController';

        activate();

        function activate() {
            var promises = [getMessageCount(), getPeople()];
            common.activateController(promises, controllerId)
                .then(function () { });
        }

        function getMessageCount() {
            return bowlerService.getMessageCount().then(function (data) {
                return vm.messageCount = data;
            });
        }

        function getPeople() {
             bowlerService.getPeople().then(function (data) {
                return vm.people = data;
            },
            function (error) {
                vm.error = error;
            });
        }
    }
})();