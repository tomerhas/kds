
app.config(function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    $routeProvider.
        when('/', {
            templateUrl: 'partials/snif-by-userid.html',
            controller: 'firstController'
        }).
        otherwise({
            redirectTo: '/'
        });
});