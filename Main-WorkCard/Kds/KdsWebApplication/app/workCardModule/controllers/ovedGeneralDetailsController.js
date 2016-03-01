workCardApp.controller('ovedGeneralDetailsController',
    function ($scope, apiProvider, $location, $log, workCardStateService, $rootScope, $timeout) {
        var vm = this;

        vm.lastModifier = "System";
        vm.lastModifyDate = new Date(Date.now());
        vm.LastUpdateWCList = [];
        vm.modalLastUpdateShown = false;
        vm.modalErrorsShown = false;
        vm.name = "General details"
        vm.misparIshi =  $location.search().misparIshi;
       // var dateStr = $location.search().cardDate;
        vm.cardDateStr = $location.search().cardDate;// "15/12/2015";
        vm.cardDate = new Date(Date.now());
        vm.result = "";
        vm.error = "";
        vm.lblDay = "";
        vm.EmployeeIds = [];
        vm.EmployeeNames = [];

        vm.SelectedEmployeeId = {};
        vm.SelectedEmployeeName = {};

        vm.EmployeeName = "";
        vm.EmployeeMaamad = "";
        vm.showLasModifierPopup = showLasModifierPopup;
        //  vm.getOvedDetails = getOvedDetails;

        vm.userIdinputChanged = userIdinputChanged;
        vm.userNameinputChanged = userNameinputChanged;

        //Get informatiion for the 3 employee detail tabs
        vm.GetEmpoyeeData = GetEmpoyeeData;
        vm.getPeiluyot = getPeiluyot;
        vm.GetDailyDetails = GetDailyDetails;
       // vm.focusTextById = focusTextById;
        vm.isUserIdValid = isUserIdValid;
        vm.isUserNameValid = isUserNameValid;

        vm.RegisterToSelectedEmployee = RegisterToSelectedEmployee;
        vm.RegisterToSelectedEmployeeName = RegisterToSelectedEmployeeName;

        vm.status = "";
        vm.StatusClass;
     //   vm.names = ["john", "bill", "charlie", "robert", "alban", "oscar", "marie", "celine", "brad", "drew"];

        activate();


       
        function activate() {
            RegisterToSelectedEmployee();
            RegisterToSelectedEmployeeName();

            if (vm.cardDateStr) {
                var tar = vm.cardDateStr.split('/');
                vm.cardDate = new Date(tar[2],tar[1]-1,tar[0]);
                vm.getPeiluyot();
            }
            else{
                vm.cardDate = new Date(2015, 11, 1);
                vm.cardDateStr = "01/12/2015";
                vm.misparIshi = "722";
                $("#mispar_ishi_value").val(vm.misparIshi);
            }

            GetGeneralDataToEmployee();
            vm.getPeiluyot();
            
            
            $("#datepicker").mask("99/99/9999");
            $("#datepicker").datepicker({
                showOn: "button",
                buttonImage: "../../../../Images/calendar.gif",
                buttonImageOnly: true,
                buttonText: "בחר תאריך",
                onSelect: function (d, i) {
                    if (d !== i.lastVal) {
                        $(this).change();
                    }
                }
            }).next(".ui-datepicker-trigger").addClass("cntlDis");


            checkCardExists();
           
        }
        //////////////////**datepicker//////////////////
        $('#datepicker').change(function () {
            var d = $(this).val().split('/');
            vm.cardDate = new Date(d[2], d[1] - 1, d[0]);

            checkCardExists();
           
        });
        function checkCardExists() {
            var promise = apiProvider.isCardExists(vm.misparIshi, vm.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                var arrReturnValue = res.split("|");
                if (arrReturnValue[0] == "0") 
                    alert("לא נמצא כרטיס לתאריך זה");
                else {
                    vm.lblDay = arrReturnValue[1];
                }

            }, function (errorPayload) {
                var er = errorPayload;
                vm.error = er.data.Message;
            });
        }
        $("#datepicker").blur(function () {
            val = $(this).val();
            if (val != '') {
                var d = val.split('/');
                val1 = Date.parse(d[1]+"/" +d[0] +"/" +d[2]);
                //   val1 = Date.parse(val);
                if (isNaN(val1) == true) {
                    alert("תאריך לא תקין")
                }
            }

        });
        //////////////*****************//////////////////

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

        function GetDailyDetails() {

            var promise = apiProvider.getOvedAllDetails(vm.misparIshi, vm.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                var jsonObj = angular.fromJson(res);
              
                workCardStateService.cardGlobalData.ovedDetails = jsonObj;
                vm.EmployeeMaamad = jsonObj.TeurSnif;
                $("#employee_name_value").val(jsonObj.FullName);
                $("#mispar_ishi_value").val(jsonObj.MisparIshi);
                vm.SelectedEmployeeId = { MisparIshi: jsonObj.MisparIshi };
                vm.SelectedEmployeeName = { EmployeeName: jsonObj.FullName };

                $rootScope.$broadcast("ovedDetails-changed");
            }, function (errorPayload) {
                var er = errorPayload;
            });
        };

        function getPeiluyot() {
            apiProvider.getUserWorkCard(vm.misparIshi, vm.cardDate)
                .then(function (res) {
                    vm.status = res.CardStatus;
                    $log.debug("ovedGeenralDetailsController.getPeiluyot - recieved work card result");
                    workCardStateService.cardGlobalData.workCardResult = res;
                    $log.debug("ovedGeenralDetailsController.getPeiluyot broadcasting ovedPeiluyot-changed event");
                    $rootScope.$broadcast("ovedPeiluyot-changed");
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

        
        /********************************* txt id functions ******************************************/

        function userIdinputChanged(str) {
            //$scope.$broadcast('angucomplete-alt:clearInput');
            vm.SelectedEmployeeId = { MisparIshi: str };
            $log.debug("SelectedEmployeeId- " + str);
            if (str.length == 1) {
                $log.debug("go to server- " + str);
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
        }

        function RegisterToSelectedEmployee()  {
            //Listen on selected employee changed. The intention to to get the data once an employee id is selected
            $scope.$watch(function () { return vm.SelectedEmployeeId }, function (newVal, oldVal) {

                //When users selectes an item from the list - the item will be contained in the newVal.description
                //When using the keyboard to insert new characters before selecting the item from the list, the newVal will contain the item directly (e.g. newVal.MisparIshi)
                if (newVal && newVal.description) {
                    vm.misparIshi = newVal.description.MisparIshi;
                    //vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                    vm.EmployeeIds = [];
                    vm.EmployeeName = [];
                    $rootScope.EnablePage('');
                }
                    //We do not want to fetch data whenever there is a change on the data only when user selects an item
                    //else if (newVal.MisparIshi) {
                    //    employeeId = newVal.MisparIshi;
                    //}
                else {
                    return;
                }

                $log.debug("$watch: SelectedEmployeeId- :" + vm.misparIshi);

                GetGeneralDataToEmployee();
              
            }, true);
        }
        //function isUserIdValid() {
        //    var idText = $("#mispar_ishi_value").val().trim();
        //    var isexist = false;
        //    if (idText.trim() != "") {
        //        vm.EmployeeIds.forEach(function (s) {
        //            if (s.MisparIshi == idText) {
        //                vm.misparIshi = idText;
        //                isexist = true;
        //            }
        //        });
        //        if (!isexist) {
        //            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
        //            enablebuttons();
        //        }
        //        else {
        //            GetGeneralDataToEmployee();
        //        }
        //    }
        //}

        function isUserIdValid() {
            var idText = $("#mispar_ishi_value").val().trim();
            vm.EmployeeIds = [];
            vm.SelectedEmployeeId = {};
            vm.SelectedEmployeeName = {};
            if (idText.trim() != "") {

                var promise = apiProvider.getOvdimToUser(idText, "75757");
                promise.then(function (payload) {
                    var res = payload.data.d;
                    
                    res.forEach(function (s) {
                        vm.EmployeeIds.push(s);
                    });
                    if (vm.EmployeeIds.length == 0) {
                        alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                        $rootScope.EnablePage('disabled');
                        enabledControls();
                    }
                    else if (vm.EmployeeIds.length > 0) {

                        var isexist = false;
                        vm.EmployeeIds.forEach(function (s) {
                            if (s.MisparIshi == idText) {
                                vm.misparIshi = idText;
                                //   vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                                isexist = true;
                            }
                        });

                        if (!isexist) {
                            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                            $rootScope.EnablePage('disabled');
                            enabledControls();
                            //  disabledFrame = false;

                        }
                        else {
                            GetGeneralDataToEmployee();
                            $rootScope.EnablePage('');
                        }
                    }
                });
            }
        }
        /************************************ end - txt id functions ***************************************/

        /********************************* txt name functions ******************************************/

        function userNameinputChanged(str) {
            //$scope.$broadcast('angucomplete-alt:clearInput');
            vm.SelectedEmployeeName = { EmployeeName: str };
            if (str.length == 1) {
                $log.debug("go to server names- " + str);
             //   $log.debug("input data changed- " + str);

                var promise = apiProvider.getOvdimToUserByName(str, "75757");
                promise.then(function (payload) {
                    var res = payload.data.d;
                    vm.EmployeeNames = [];
                    res.forEach(function (s) {
                        vm.EmployeeNames.push(s);
                    });



                }, function (errorPayload) {

                });
            }
            
        }

        function RegisterToSelectedEmployeeName() {
            //Listen on selected employee changed. The intention to to get the data once an employee id is selected
            $scope.$watch(function () { return vm.SelectedEmployeeName }, function (newVal, oldVal) {

                //When users selectes an item from the list - the item will be contained in the newVal.description
                //When using the keyboard to insert new characters before selecting the item from the list, the newVal will contain the item directly (e.g. newVal.MisparIshi)
                if (newVal && newVal.description) {
                    var name = newVal.description.EmployeeName.split('(');
                    vm.EmployeeName = name[0];
                    vm.misparIshi = name[1].split(')')[0].trim();
                    $("#mispar_ishi_value").val(vm.misparIshi);
                    $rootScope.EnablePage('');
                   // vm.EmployeeIds = [];
                  //  vm.EmployeeName = [];
                   // vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                   // vm.SelectedEmployeeName = { EmployeeName: vm.EmployeeName };
                   // $("#employee_name_value").val(vm.EmployeeName);
                }
                    //We do not want to fetch data whenever there is a change on the data only when user selects an item
                    //else if (newVal.MisparIshi) {
                    //    employeeId = newVal.MisparIshi;
                    //}
                else {
                    return;
                }

                $log.debug("$watch: SelectedEmployeeId- :" + vm.EmployeeName);

                GetGeneralDataToEmployee();
            }, true);
        }
        //function isUserNameValid() {
        //    var nameText = $("#employee_name_value").val();
        //    var name;
        //    var isexist = false;
        //    if (nameText.trim() != "") {
        //        vm.EmployeeNames.forEach(function (s) {
        //            name = s.EmployeeName;
        //            if (s.EmployeeName == nameText || nameText == s.EmployeeName.split('(')[0].trim()) {
        //                vm.misparIshi = s.EmployeeName.split('(')[1].split(')')[0].trim();
        //                isexist = true;
        //            }
        //        });
        //        if (!isexist) {
        //            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
        //            enablebuttons();
        //        }
        //        else {
        //            $("#mispar_ishi_value").val(vm.misparIshi);
        //            GetGeneralDataToEmployee();
        //        }
        //    }
        //}
        function isUserNameValid() {
            var nameText = $("#employee_name_value").val();
            var name;
            vm.EmployeeNames = [];
            vm.SelectedEmployeeName = {};
            vm.SelectedEmployeeId = {};
            if (nameText.trim() != "") {
                if (nameText.indexOf('(') > -1)
                    nameText = nameText.split('(')[0].trim();
                var promise = apiProvider.getOvdimToUserByName(nameText, "75757");
                promise.then(function (payload) {
                    var res = payload.data.d;
                    res.forEach(function (s) {
                        vm.EmployeeNames.push(s);
                    });

                    if (vm.EmployeeNames.length == 0) {
                        alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                        $rootScope.EnablePage('disabled');
                        enabledControls();
                    }
                    else if (vm.EmployeeNames.length > 0) {

                        var isexist = false;
                        vm.EmployeeNames.forEach(function (s) {
                            name = s.EmployeeName;
                            if (s.EmployeeName == nameText || nameText == s.EmployeeName.split('(')[0].trim()) {
                                vm.EmployeeName = s.EmployeeName.split('(')[0];
                                vm.misparIshi = s.EmployeeName.split('(')[1].split(')')[0].trim();

                                //   vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                                //   vm.SelectedEmployeeName = { EmployeeName: vm.EmployeeName };
                                isexist = true;
                            }
                        });
                        if (!isexist) {
                            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                            $rootScope.EnablePage('disabled');
                            enabledControls();
                        }
                        else {
                            $("#mispar_ishi_value").val(vm.misparIshi);
                            $("#employee_name_value").val(vm.EmployeeName);
                            GetGeneralDataToEmployee();
                            $rootScope.EnablePage('');
                        }
                    }
                });
            }
        }
        /************************************ end - txt name functions ***************************************/


        function GetGeneralDataToEmployee() {
          //  GetEmpoyeeData(vm.misparIshi);
            GetDailyDetails();
        }

       function GetEmpoyeeData(selectedEmployeeId) {
            $log.debug("empoyee idea selected- " + selectedEmployeeId);

            var promise = apiProvider.getEmployeeBasicDetails(selectedEmployeeId, vm.cardDate);
            promise.then(function (payload) {
                var res = payload.data.d;
                if (res) {
                    vm.EmployeeName = res.Name;
                  
                  //  $timeout(function () {
                        $("#employee_name_value").val(vm.EmployeeName);
                        $("#mispar_ishi_value").val(vm.misparIshi);
                  //  },100);
                        vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                        vm.SelectedEmployeeName = { EmployeeName: vm.EmployeeName };
                        vm.EmployeeIds = [];
                        vm.EmployeeName = [];
                    //if (vm.EmployeeNames.length == 0) {
                    //    vm.EmployeeNames.push({ "EmployeeName": res.Name + " (" + vm.misparIshi+")" });
                    //}

                //    vm.$apply();
                    if (res.UnitName != null)
                        vm.EmployeeMaamad = res.UnitName;
                    else vm.EmployeeMaamad = "";
                }
                $log.debug("GetEmpoyeeData res: " + res.UnitName);
            }, function (errorPayload) {

            });

        }
       //function focusTextById(id) {
       //    $("#" + id).select();
       //}
      
       function enabledControls() {
           $("#mispar_ishi_value").prop('disabled', '');
           $("#employee_name_value").prop('disabled', '');
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