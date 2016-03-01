
workCardApp.controller('ovedDailyDetailsController', ['$scope','apiProvider', 'workCardStateService','$log','$location',
    function ($scope, apiProvider, workCardStateService, $log, $location) {
        var vm = this;
        vm.DayDetails = {};
        vm.tabDisplayed = 0;
        vm.replaceTab = replaceTab;
        vm.val = 1;
        vm.ZmanNesiaList = {};
        vm.PrevDay_Click = PrevDay_Click;
        vm.NextDay_Click = NextDay_Click;
        vm.NextWrongDay_Click = NextWrongDay_Click;
      
        activate();

        function activate() {
            RegisterToLokupChanges();
            UpdateLists();
            vm.DayDetails = workCardStateService.cardGlobalData.workCardResult.DayDetails;
        }

        function RegisterToLokupChanges() {
            $scope.$on('workcardLookups-changed', function (event, args) {
                $log.debug("ovedDailyDetailsController - updating lookups after event changed recieved");
                UpdateLists();
            });

            $scope.$on('ovedPeiluyot-changed', function (event, args) {
                $log.debug("ovedDailyDetailsController recieved broadcast ovedPeiluyot-changed event xxx");
                        vm.DayDetails = workCardStateService.cardGlobalData.workCardResult.DayDetails;
            });
        }

      
        function UpdateLists() {
            if (workCardStateService.lookupsContainer.container) {
                $log.debug("ovedDailyDetailsController - updating lookups from cache");
                vm.ZmanNesiaList = workCardStateService.lookupsContainer.container.ZmanNesiaList;
            }
            else {
                $log.debug("ovedDailyDetailsController - when trying to fill in ZmanNesiaList - the cache data was empty");
            }
        }

        function replaceTab(num)
        {
            vm.tabDisplayed = num;
        }

        function PrevDay_Click() {

           // $location.url('LoadWorkCard');

            //  $location.path("LoadWorkCard");
            var objDetails = workCardStateService.cardGlobalData.ovedDetails;
            var id = objDetails.MisparIshi;
            var sDate = objDetails.DetailsDate.split('/');
            var dDate = new Date(sDate[2], sDate[1] - 1, sDate[0]);
            dDate.setDate(dDate.getDate() - 1);
            var prevdate = dDate.getDate() + '/' + (dDate.getMonth() + 1) + '/' + dDate.getFullYear();
         //   window.location = '../../workCardModule/views/MainWorkCard.html?misparIshi=' + id + '&cardDate=' + prevdate;
     
        }

        function NextDay_Click() {

            var objDetails = workCardStateService.cardGlobalData.ovedDetails;
            var id = objDetails.MisparIshi;
            var sDate = objDetails.DetailsDate.split('/');
            var dDate = new Date(sDate[2], sDate[1] - 1, sDate[0]);
            dDate.setDate(dDate.getDate() + 1);
            var prevdate = dDate.getDate() + '/' + (dDate.getMonth() + 1) + '/' + dDate.getFullYear();
            window.location = '../../workCardModule/views/MainWorkCard.html?misparIshi=' + id + '&cardDate=' + prevdate;

        }

        
        function NextWrongDay_Click() {

            var objDetails = workCardStateService.cardGlobalData.ovedDetails;
            var id = objDetails.MisparIshi;
            var sDate = objDetails.DetailsDate.split('/');
            var dDate = new Date(sDate[2], sDate[1] - 1, sDate[0]);

            var promise = apiProvider.getNextErrorCardDate(id, dDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                if( objDetails.DetailsDate != res)
                    window.location = '../../workCardModule/views/MainWorkCard.html?misparIshi=' + id + '&cardDate=' + res;
                else alert('לא קיים כרטיס שגוי הבא');
                

            }, function (errorPayload) {
                var er = errorPayload;
                vm.error = er.data.Message;
            });
           
        }

    }]);