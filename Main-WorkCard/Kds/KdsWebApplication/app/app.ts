module main {
    var mainApp = angular.module("mainApp", ["modules.workcard"]);

    mainApp.config(function ($routeProvider, $locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    });
}