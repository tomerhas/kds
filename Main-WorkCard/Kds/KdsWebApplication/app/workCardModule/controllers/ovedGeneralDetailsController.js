workCardApp.controller('ovedGeneralDetailsController',
    function($scope, apiProvider, $location, workCardStateService,$rootScope) {

        $scope.lastModifier = "System";
        $scope.lastModifyDate = new Date(Date.now());

        $scope.showLasModifierPopup = function () {
            $scope.modalShown = true;
        };

        $scope.modalShown = false;
        $scope.toggleModal = function () {
            $scope.modalShown = !$scope.modalShown;
        };


        $scope.getOvedDetails = function () {
            //$scope.misparIshi = 5;
            getGeneralDetails();
            $scope.getPeiluyot();
        };

        $scope.getPeiluyot = function () {
            var promise = apiProvider.getEmployeePeiluyot($scope.misparIshi, $scope.cardDate);
            //Since the http method is async - need to use a promise that will return the value using a callback inline method http://chariotsolutions.com/blog/post/angularjs-corner-using-promises-q-handle-asynchronous-calls/
            promise.then(function (payload) {
                var res = payload.data.d;
                workCardStateService.cardGlobalData.ovedPeiluyot = res;
                $rootScope.$broadcast("ovedPeiluyot-changed", { data: 2 });
            }, function (errorPayload) {
                var er = errorPayload;
            });
        };

        $scope.name = "General details"
        $scope.misparIshi = $location.search().misparIshi;
        var dateStr = $location.search().cardDate;
        $scope.cardDate = new Date(Date.now());
        if (dateStr)
        {
            $scope.cardDate = new Date(dateStr);
            $scope.getPeiluyot();
        }

        $scope.result = "";

        

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