app.controller('ovedGeneralDetailsController',
    function firstController($scope, apiProvider, $location, workCardStateService) {

        $scope.name = "General details"
        $scope.misparIshi = $location.search().misparIshi;
        $scope.cardDate = new Date(Date.now());//$location.search().cardDate;

        $scope.result = "";

        $scope.getOvedDetails = function () {
            //$scope.misparIshi = 5;
            getGeneralDetails();
            getPeiluyot();
        };

        var getPeiluyot = function () {
            var promise = apiProvider.getEmployeePeiluyot($scope.misparIshi, $scope.cardDate);
            //Since the http method is async - need to use a promise that will return the value using a callback inline method http://chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls/
            promise.then(function (payload) {
                var res = payload.data.d;
                workCardStateService.cardGlobalData.ovedPeiluyot = res;
            }, function (errorPayload) { });
        };

        var getGeneralDetails = function () {
            var promise = apiProvider.getOvedDetails($scope.misparIshi, $scope.cardDate);
            //Since the http method is async - need to use a promise that will return the value using a callback inline method http://chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls/
            promise.then(function (payload) {
                var res = payload.data.d;
                $scope.result = res.sHalbasha;
                workCardStateService.cardGlobalData.ovedDetails = res;
            }, function (errorPayload) { });
        }
    });