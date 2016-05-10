(function () {
    'use strict';

    var controllerId = 'error';
    angular.module('app').controller(controllerId,
        ['common', 'config', error]);

    function error(common, config) {
        var vm = this;
        //var logSuccess = common.logger.getLogFn(controllerId, 'success');
        //var events = config.events;
        vm.errorMassage = common.cacheManager.getItem('error');
  
        activate();

        function activate() {
            var promises = [];
            common.activateController(promises, controllerId);
        }
    };
})();