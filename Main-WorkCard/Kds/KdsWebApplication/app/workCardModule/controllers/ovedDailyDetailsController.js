﻿workCardApp.controller('ovedDailyDetailsController',['$scope', 'apiProvider',
    function($scope, apiProvider) {

        $scope.name = "daily details"
        $scope.tabDisplayed = 1;

        $scope.replaceTab = function ()
        {
            $scope.tabDisplayed++;
            if ($scope.tabDisplayed > 2)
                $scope.tabDisplayed = 1;
        }
    }]);