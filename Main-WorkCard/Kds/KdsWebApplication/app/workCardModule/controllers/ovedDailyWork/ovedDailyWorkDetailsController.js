workCardApp.controller('ovedDailyWorkDetailsController',
    function ($scope, apiProvider) {
        var vm = this;
        vm.TachgrafList = {};
        vm.LinaList = {};
        vm.HalbashaList = {};
        vm.HashlamaList = {};
        vm.val = 1;

        activate();

        function activate() {
            InilizeTachgraf();
            InilizeLina();
            InilizeHalbasha();
            InilizeHashlama();
        }

        function InilizeTachgraf() {
            var promise = apiProvider.getTachograf();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.TachgrafList = res;

            }, function (errorPayload) {

            });
        }

        function InilizeLina() {
            var promise = apiProvider.getLina();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.LinaList = res;

            }, function (errorPayload) {

            });
        }

        function InilizeHalbasha() {
            var promise = apiProvider.getHalbasha();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.HalbashaList = res;

            }, function (errorPayload) {

            });
        }

        function InilizeHashlama() {
            var promise = apiProvider.getHashlameLeYom();
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.HashlamaList = res;

            }, function (errorPayload) {

            });
        }
        
    });
/*
workCardApp.controller('ovedDailyWorkDetailsController',
    function($scope, apiProvider,workCardStateService) {

        $scope.name = "daily work"
        $scope.updateRes = "";
        
        //In order to watch a property on the service,  the first param of the watch must be a method that returns the property and not to watch the property itself
        $scope.$watch(function () { return workCardStateService.cardGlobalData }, function (newVal, oldVal) {
            updateProps();
        },true);

        var updateProps = function () {
            //$scope.updateRes = workCardStateService.cardGlobalData.ovedPeiluyot;
        }
    });*/