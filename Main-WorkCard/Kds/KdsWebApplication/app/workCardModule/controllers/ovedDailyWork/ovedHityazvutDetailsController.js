
workCardApp.controller('ovedHityazvutDetailsController',
    function ($rootScope,$scope, workCardStateService) {
        var vm = this;
        vm.SibotDivuachList = {};
        vm.OvedDetails;
        vm.FirstHityazvut = {};
        vm.SecondHityazvut = {};
       
        activate();

        function activate() {
            UpdateLists();
            vm.OvedDetails = workCardStateService.cardGlobalData.workCardResult;
            vm.FirstHityazvut = vm.OvedDetails.FirstHityazvut;
            vm.SecondHityazvut = vm.OvedDetails.SecondHityazvut;
           
            if ($rootScope.DisablePage)
                $rootScope.EnablePage('disabled');
        }

        function UpdateLists()
        {
            if (workCardStateService.lookupsContainer.container) {
                vm.SibotDivuachList = workCardStateService.lookupsContainer.container.SibotLedivuachList;
            }
        }

        
    });