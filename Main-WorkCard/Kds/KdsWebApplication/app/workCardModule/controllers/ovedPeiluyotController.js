workCardApp.controller('ovedPeiluyotController',
    function firstController($scope, apiProvider, workCardStateService) {
        $scope.name = "peiluyot"
        $scope.updateRes = "";

        $scope.$on("ovedPeiluyot-changed", function (event, args) {
            
            updateProps();
        });
        //$scope.$watch(function () { return workCardStateService.cardGlobalData }, function (newVal, oldVal) {
        //    updateProps();
        //}, true);

        var updateProps = function () {
            $scope.updateRes = workCardStateService.cardGlobalData.ovedPeiluyot;
        }

    });