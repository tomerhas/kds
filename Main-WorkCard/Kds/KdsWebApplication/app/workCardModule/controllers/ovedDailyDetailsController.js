workCardApp.controller('ovedDailyDetailsController',['$scope', 'apiProvider',
    function($scope, apiProvider) {
        var vm = this;
        vm.tabDisplayed = 0;
        vm.replaceTab = replaceTab;
        vm.val = 1;
        vm.ZmanNesiaList = {};

        activate();

        function activate() {
            InilizeZmanNesia(); 
        }

        function InilizeZmanNesia() {
            var promise = apiProvider.getZmaneyNesioth();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.ZmanNesiaList = res;

            }, function (errorPayload) {

            });
        }


        function replaceTab(num)
        {
            vm.tabDisplayed = num;
        }
    }]);