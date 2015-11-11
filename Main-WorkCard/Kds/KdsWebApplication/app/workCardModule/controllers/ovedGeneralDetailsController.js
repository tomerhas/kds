workCardApp.controller('ovedGeneralDetailsController',
    function($scope, apiProvider, $location,$log, workCardStateService,$rootScope) {
        var vm = this;

        vm.lastModifier = "System";
        vm.lastModifyDate = new Date(Date.now());
        vm.LastUpdateWCList = [];
        vm.modalLastUpdateShown = false;
        vm.modalErrorsShown = false;
        vm.name = "General details"
        vm.misparIshi =  $location.search().misparIshi;
        var dateStr =  $location.search().cardDate;
        vm.cardDate = new Date(Date.now());
        vm.result = "";
        vm.error = "";
        vm.EmployeeIds = [];
        vm.SelectedEmployeeId = {};
        vm.EmployeeName = "";
        vm.EmployeeMaamad = "";
        vm.showLasModifierPopup = showLasModifierPopup;
      //  vm.getOvedDetails = getOvedDetails;
        vm.userIdinputChanged = userIdinputChanged;
        vm.GetEmpoyeeData = GetEmpoyeeData;
        vm.getPeiluyot = getPeiluyot;

        activate();

        function activate(){
            if (dateStr)
            {
                vm.cardDate = new Date(dateStr);    
                vm.getPeiluyot();
            }
            vm.cardDate = new Date(2014, 5, 5);
            vm.misparIshi = "65929"; 
        }
        //Method for returning the list of modiifcatinos by the user
        function showLasModifierPopup() {
            var promise = apiProvider.getWCLastUpdatsDetails(vm.misparIshi, vm.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.LastUpdateWCList = res;
                vm.modalLastUpdateShown = true;
            }, function (errorPayload) {
                var er = errorPayload;
                vm.error = er.data.Message;
                vm.modalErrorsShown = true;
            });
        };

        function getPeiluyot() {

            var promise = apiProvider.getEmployeePeiluyot(vm.misparIshi, vm.cardDate);
                promise.then(function (payload) {
                    var res = payload.data.d;
                    var jsonObj = angular.fromJson(res);
                    workCardStateService.cardGlobalData.ovedPeiluyot = jsonObj;
                    $rootScope.$broadcast("ovedPeiluyot-changed");
                }, function (errorPayload) {
                    var er = errorPayload;
                });
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

        
        

        function userIdinputChanged(str) {
            //$scope.$broadcast('angucomplete-alt:clearInput');
            vm.SelectedEmployeeId = { MisparIshi: str };
            $log.debug("input data changed- " + str);
            var promise = apiProvider.getOvdimToUser(str, "75757");
            promise.then(function (payload) {
                var res = payload.data.d;
                vm.EmployeeIds = [];
                res.forEach(function (s) {
                    vm.EmployeeIds.push(s);
                });
                 
                
               
            }, function (errorPayload) {

            });
        }

        //Listen on selected employee changed. The intention to to get the data once an employee id is selected
        $scope.$watch(function () { return vm.SelectedEmployeeId }, function (newVal, oldVal) {
            
            //When users selectes an item from the list - the item will be contained in the newVal.description
            //When using the keyboard to insert new characters before selecting the item from the list, the newVal will contain the item directly (e.g. newVal.MisparIshi)
            if (newVal && newVal.description)
            {
                vm.misparIshi = newVal.description.MisparIshi;
            }
            //We do not want to fetch data whenever there is a change on the data only when user selects an item
            //else if (newVal.MisparIshi) {
            //    employeeId = newVal.MisparIshi;
            //}
            else {
                return;
            }
            
            $log.debug("$watch: SelectedEmployeeId- :" + vm.misparIshi);
            
            
            GetEmpoyeeData(vm.misparIshi);
        }, true);

       function GetEmpoyeeData(selectedEmployeeId) {
            $log.debug("empoyee idea selected- " + selectedEmployeeId);

            var promise = apiProvider.getEmployeeBasicDetails(selectedEmployeeId, vm.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                if (res) {
                    vm.EmployeeName = res.Name;
                    vm.EmployeeMaamad = res.UnitName;
                }
                $log.debug("GetEmpoyeeData res: " + res.UnitName);
            }, function (errorPayload) {

            });

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