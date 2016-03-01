workCardApp.controller('pirteyOvedDetailsController',
    function ($rootScope,$scope, apiProvider, workCardStateService) {
        var vm = this;
        vm.OvedDetails;
        activate();

        function activate() {
            vm.OvedDetails = workCardStateService.cardGlobalData.ovedDetails;
            if ($rootScope.DisablePage)
                $rootScope.EnablePage('disabled');
        }

        $scope.$on('ovedDetails-changed', function (event, args) {
            vm.OvedDetails = workCardStateService.cardGlobalData.ovedDetails;
            //vm.NumSidurim = vm.Sidurim.length;
        });
    });

