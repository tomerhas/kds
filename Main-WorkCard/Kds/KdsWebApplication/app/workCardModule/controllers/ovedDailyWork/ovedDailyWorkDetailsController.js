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
    });