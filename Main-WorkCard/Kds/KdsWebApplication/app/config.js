app.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: 'partials/snif-by-userid.html',
            controller: 'firstController'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);