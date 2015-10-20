workCardApp.controller('ovedGeneralDetailsController',
    function($scope, apiProvider, $location, workCardStateService,$rootScope) {

        $scope.lastModifier = "System";
        $scope.lastModifyDate = new Date(Date.now());
        $scope.LastUpdateWCList = [];
        $scope.modalLastUpdateShown = false;
        $scope.modalErrorsShown = false;
        $scope.name = "General details"
        $scope.misparIshi = $location.search().misparIshi;
        var dateStr = $location.search().cardDate;
        $scope.cardDate = new Date(Date.now());
        $scope.result = "";
        $scope.error = "";


        
        //Method for returning the list of modiifcatinos by the user
        $scope.showLasModifierPopup = function () {
            var promise = apiProvider.getWCLastUpdatsDetails($scope.misparIshi, $scope.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                $scope.LastUpdateWCList = res;
                $scope.modalLastUpdateShown = true;
            }, function (errorPayload) {
                var er = errorPayload;
                $scope.error = er.data.Message;
                $scope.modalErrorsShown = true;
            });
        };

      
        $scope.getOvedDetails = function () {
            //$scope.misparIshi = 5;
            getGeneralDetails();
            $scope.getPeiluyot();
        };

        //$scope.getPeiluyot = function () {
        //    var promise = apiProvider.getEmployeePeiluyot($scope.misparIshi, $scope.cardDate);
        //    promise.then(function (payload) {
        //        var res = payload.data.d;
        //        workCardStateService.cardGlobalData.ovedPeiluyot = res;
        //        $rootScope.$broadcast("ovedPeiluyot-changed", { data: 2 });
        //    }, function (errorPayload) {
        //        var er = errorPayload;
        //    });
        //};

        
        if (dateStr)
        {
            $scope.cardDate = new Date(dateStr);
            $scope.getPeiluyot();
        }

        //var getGeneralDetails = function () {
        //    var promise = apiProvider.getOvedDetails($scope.misparIshi, $scope.cardDate);
        //    promise.then(function (payload) {
        //        var res = payload.data.d;
        //        $scope.result = res.sHalbasha;
        //        workCardStateService.cardGlobalData.ovedDetails = res;
        //    }, function (errorPayload) { });
        //}
    });