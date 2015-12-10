workCardApp.controller('ovedDailyDetailsController', ['$scope', 'workCardStateService','$log',
    function ($scope, workCardStateService,$log) {
        var vm = this;
        vm.tabDisplayed = 0;
        vm.replaceTab = replaceTab;
        vm.val = 1;
        vm.ZmanNesiaList = {};

        activate();

        function activate() {
            RegisterToLokupChanges();
            UpdateLists();
        }

        function RegisterToLokupChanges() {
            $scope.$on('workcardLookups-changed', function (event, args) {
                $log.debug("ovedDailyDetailsController - updating lookups after event changed recieved");
                UpdateLists();
            });
        }

        function UpdateLists() {
            if (workCardStateService.lookupsContainer.container) {
                $log.debug("ovedDailyDetailsController - updating lookups from cache");
                vm.ZmanNesiaList = workCardStateService.lookupsContainer.container.ZmanNesiaList;
            }
            else {
                $log.debug("ovedDailyDetailsController - when trying to fill in ZmanNesiaList - the cache data was empty");
            }
        }

        function replaceTab(num)
        {
            vm.tabDisplayed = num;
        }
    }]);