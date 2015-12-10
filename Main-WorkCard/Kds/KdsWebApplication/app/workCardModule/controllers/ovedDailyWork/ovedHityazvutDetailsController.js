
workCardApp.controller('ovedHityazvutDetailsController',
    function ($scope, workCardStateService) {
        var vm = this;
        vm.SibotDivuachList = {};
        vm.val = 1;
      
        activate();

        function activate() {
            UpdateLists();
        }

        function UpdateLists()
        {
            if (workCardStateService.lookupsContainer.container) {
                vm.SibotDivuachList = workCardStateService.lookupsContainer.container.SibotLedivuachList;
            }
        }

        
    });