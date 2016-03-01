// create the module and name it scotchApp
var scotchApp = angular.module('scotchApp', ['ngRoute']);

// configure our routes
scotchApp.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'workCardDetails/ovedDailyWork/pirteyOvedDetails.html'//,
          //  controller: 'mainWorkCardController'
        });

      
});

//// create the controller and inject Angular's $scope
//scotchApp.controller('mainController', function ($scope) {
//    // create a message to display in our view
//    $scope.message = 'Everyone come and see how good I look!';
//});