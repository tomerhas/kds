
workCardApp.controller('ovedHityazvutDetailsController',
    function($scope, apiProvider) {
        var vm = this;
        vm.SibotDivuachList = {};
        vm.val = 1;
      
        activate();

        function activate() {
            InilizeSibot();
        }

        function InilizeSibot() {
            var promise = apiProvider.getSibotLedivuach();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.SibotDivuachList = res;

            }, function (errorPayload) {

            });
        }
    });