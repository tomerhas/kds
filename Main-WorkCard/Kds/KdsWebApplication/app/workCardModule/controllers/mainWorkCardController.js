workCardApp.controller('mainWorkCardController',

    function ($rootScope, apiProvider, workCardStateService) {

        var vm = this;

        vm.SetLookupsInCache = SetLookupsInCache;
        activate();

        function activate() {
            SetLookupsInCache();
        }

        function SetLookupsInCache() {
         
                apiProvider.getLookupsContainer().then(function (result) {
                    workCardStateService.lookupsContainer = result;
                    $rootScope.$broadcast("workcardLookups-changed");
                }); 
        }
       
    });