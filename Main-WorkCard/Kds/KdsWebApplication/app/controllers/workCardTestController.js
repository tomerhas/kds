app.controller('firstController',
    function firstController($scope,apiProvider) {

        $scope.prop1 = "Oded"
        $scope.snifInp = "0"
        $scope.snifRes = "";
        $scope.changeName = function ()
        {
            $scope.prop1 = "Merav";
        }

        $scope.getSnif = function ()
        {
            var promise = apiProvider.getSnifOved($scope.snifInp);
            //Since the http method is async - need to use a promise that will return the value using a callback inline method http://chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls/
            promise.then(function (payload) {
                $scope.snifRes = payload.data.d;
            }, function (errorPayload) { });
            
        }

    });