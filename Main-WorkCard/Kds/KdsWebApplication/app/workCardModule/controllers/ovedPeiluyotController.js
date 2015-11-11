workCardApp.controller('ovedPeiluyotController',
    function($scope, apiProvider, workCardStateService) {

        var vm = this;
        vm.Sidurim = {};
        vm.NumSidurim = 0;
        vm.ChangeCollapeImg = ChangeCollapeImg;
        vm.SibotDivuachDLL = {};
        vm.HarigaDLL = {};
        vm.PizulDLL = {};
        vm.HshlamaDLL = {};

        activate();

        function activate() {
            InilizeSibot();
            InilizeHariga();
            InilizePizul();
            InilizeHashlama();
        }

        function InilizeSibot() {
            var promise = apiProvider.getSibotLedivuach();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.SibotDivuachDLL = res;

            }, function (errorPayload) {

            });
        }

        function InilizeHariga() {
            var promise = apiProvider.getHariga();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.HarigaDLL = res;
            }, function (errorPayload) {

            });
        }

        function InilizePizul() {
            var promise = apiProvider.getPizul();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.PizulDLL = res;
            }, function (errorPayload) {

            });
        }

        function InilizeHashlama() {
            var promise = apiProvider.getHashlama();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.HashlamaDLL = res;
            }, function (errorPayload) {

            });
        }

        $scope.$on('ovedPeiluyot-changed', function (event, args) {
            vm.Sidurim = workCardStateService.cardGlobalData.ovedPeiluyot.SidurimList;
            vm.NumSidurim = vm.Sidurim.length;
        });





        function ChangeCollapeImg(index) {
            if (vm.Sidurim[index].CollapseSrc.Value.indexOf("openArrow") > -1) {
                vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/closeArrow.png";
            }
            else vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/openArrow.png";
        }

        
    });