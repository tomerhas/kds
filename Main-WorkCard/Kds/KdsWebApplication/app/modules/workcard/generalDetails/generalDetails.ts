module modules.workcard {
    class GeneralDetailsController {
        lastModifier:string = "System";
        lastModifyDate: Date;
        misparIshi:number;
        cardDateStr:string;
        cardDate: Date;
        lastUpdateWCList: UserWCUpdateInfo;  
        modalLastUpdateShown: boolean;
        error: string;
        modalErrorsShown: boolean;


        EmployeeIds: EmployeeIdContainer[];
        EmployeeNames : EmployeeNameContainer[];

        SelectedEmployeeId: EmployeeIdContainer ;
        SelectedEmployeeName: EmployeeNameContainer;

        EmployeeName:string = "";
        EmployeeMaamad: string = "";

        status: string;
        lblDay: string;

        constructor(
            private $log: ng.ILogService,
            private IApiProviderService: modules.workcard.IApiProviderService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private IWorkCardStateService: IWorkCardStateService,
            private $rootScope: ng.IRootScopeService,
            private IUiHelperService: modules.common.IUiHelperService) {

            this.lastModifyDate = new Date(Date.now());  
            this.misparIshi =  this.$location.search().misparIshi;
            this.cardDateStr = this.$location.search().cardDate;
            this.cardDate = new Date(Date.now());
            this.modalLastUpdateShown = false;
            this.RegisterToSelectedEmployee();
            this.RegisterToSelectedEmployeeName();
            this.InitDatePicker();
            this.RegisterToDatePickerChange();
            this.RegisterToRequestChangeDate();
            this.RegisterToRequestGetErrorNextDay();

            this.InitCardDate();

            this.GetDailyDetails();
            this.getPeiluyot();

        }

        InitCardDate = () => {
            if (this.cardDateStr) {
                var tar = this.cardDateStr.split('/');
                this.cardDate = new Date(Number(tar[2]), Number(tar[1]) - 1, Number(tar[0]));
            }
            else {
                this.cardDate = new Date(2015, 11, 1);
                this.cardDateStr = "01/12/2015";
                this.misparIshi = 722;
                $("#mispar_ishi_value").val(this.misparIshi);
            }
            this.checkCardExists();
        }

        InitDatePicker = () => {
            (<any>$("#datepicker")).mask("99/99/9999");
            (<any>$("#datepicker")).datepicker({
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
        }



        getPeiluyot = () => {
                this.IApiProviderService.getUserWorkCard(this.misparIshi.toString(), this.cardDate)
                    .then(res=> {
                        this.status = res.CardStatus.TeurStatus;
                        this.$log.debug("ovedGeenralDetailsController.getPeiluyot - recieved work card result");
                        
                        this.IWorkCardStateService.workCardResult = res;
                        this.$log.debug("ovedGeenralDetailsController.getPeiluyot broadcasting ovedPeiluyot-changed event");
                        this.$rootScope.$broadcast(modules.workcard.GeneralEvents.OVED_PEILUT_CHANGED);
                });
        };

         RegisterToDatePickerChange = () => {
             var self = this;
             $('#datepicker').change(function(){
                 var d = $(this).val().split('/');
                 self.cardDate = new Date(d[2], d[1] - 1, d[0]);
                 self.checkCardExists();
             });
         };

         RegisterToRequestChangeDate = () => {
             this.$scope.$on(modules.workcard.GeneralEvents.ADVANCE_DATE_BY_DAYS, (event,payload:number) => {
                 modules.common.DateHelper.addDaysToDate(this.cardDate, payload);
                 this.cardDateStr = modules.common.DateHelper.formatDate(this.cardDate);
                 this.GetDailyDetails();
                 this.getPeiluyot();
             })
         }

         RegisterToRequestGetErrorNextDay = () => {
             this.$scope.$on(modules.workcard.GeneralEvents.SHOW_NEXT_ERROR_DAY, (event, payload: number) => {

                 this.IApiProviderService.getNextErrorCardDate(this.misparIshi, this.cardDate)
                     .then(res=> {

                         if (res != this.cardDateStr) {
                          
                             this.cardDate = modules.common.DateHelper.convertToDate(res);
                             this.cardDateStr = res;// modules.common.DateHelper.formatDate(this.cardDate);
                             this.GetDailyDetails();
                             this.getPeiluyot();
                         }
                         else {
                             alert('לא קיים כרטיס שגוי הבא');
                         }

                     }, error=> {
                         this.error = error.data.Message;
                         this.modalErrorsShown = true;
                     }
                 );
             });
         }

         checkCardExists = () => {
             this.IApiProviderService.isCardExists(this.misparIshi, this.cardDate)
                 .then(res=> {
                     var arrReturnValue = res.split("|");
                     if (arrReturnValue[0] == "0")
                         alert("לא נמצא כרטיס לתאריך זה");
                     else {
                         this.lblDay = arrReturnValue[1];
                     }

                 }, error=> {
                     this.error = error.data.Message;
                     this.modalErrorsShown = true;
                 });
         };

        showLasModifierPopup = ()=> {
            this.IApiProviderService.getWCLastUpdatsDetails(this.misparIshi, this.cardDate)
                .then(res=> {
                    this.lastUpdateWCList = res;
                    this.modalLastUpdateShown = true;
                }, error=> {
                    this.error = error.data.Message;
                    this.modalErrorsShown = true;
                });
        }
        ////////////////////////////////////////////////////////////////////////
        userIdinputChanged = (misparIshi: string) => {
            this.SelectedEmployeeId = new EmployeeIdContainer();
            this.SelectedEmployeeId.misparIshi = misparIshi;
            
            this.$log.debug("SelectedEmployeeId- " + misparIshi);
            if (misparIshi.length == 1) {
                this.$log.debug("go to server- " + misparIshi);
                this.IApiProviderService.getOvdimToUser(misparIshi, 75757)
                    .then(res=> {
                        this.EmployeeIds = res;
                    }, error=> {
                    this.error = error.data.Message;
                    this.modalErrorsShown = true;
                });
            }
        }

        RegisterToSelectedEmployee=()=> {
        //Listen on selected employee changed. The intention to to get the data once an employee id is selected
            this.$scope.$watch( () =>{ return this.SelectedEmployeeId }, (newVal:any, oldVal:any) =>{
                //When users selectes an item from the list - the item will be contained in the newVal.description
                //When using the keyboard to insert new characters before selecting the item from the list, the newVal will contain the item directly (e.g. newVal.MisparIshi)
                if (newVal && newVal.description) {
                    this.misparIshi = newVal.description.misparIshi;
                    //vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                    this.EmployeeIds = [];
                    this.EmployeeNames = [];
                   this.IUiHelperService.EnablePage(true);
                }
                else {
                    return;
                }
            this.$log.debug("$watch: SelectedEmployeeId- :" + this.misparIshi);
            this.GetDailyDetails();
            }, true);
        }

        isUserIdValid=()=> {
            var idText = $("#mispar_ishi_value").val().trim();
            this.EmployeeIds = [];
            this.SelectedEmployeeId = new EmployeeIdContainer();
            this.SelectedEmployeeName = new EmployeeNameContainer();

            if (idText.trim() != "") {

                this.IApiProviderService.getOvdimToUser(idText, 75757)
                    .then(res=> {
                        this.EmployeeIds = res;
                        if (this.EmployeeIds.length == 0) {
                            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                             this.IUiHelperService.EnablePage(false);
                             this.enabledControls();
                        }
                        else if (this.EmployeeIds.length > 0) {

                            var isexist = false;

                            this.EmployeeIds.forEach((s: EmployeeIdContainer) => {
                                if (s.misparIshi == idText) {
                                    this.misparIshi = idText;
                                    //   vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                                    isexist = true;
                                }
                            });

                            if (!isexist) {
                                alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                                this.IUiHelperService.EnablePage(false);
                                this.enabledControls();
                                //  disabledFrame = false;

                            }
                            else {
                                this.GetDailyDetails();
                                this.IUiHelperService.EnablePage(true);
                            }
                        }
                    }, error=> {
                        this.error = error.data.Message;
                        this.modalErrorsShown = true;
                    });

                   
                }
        }
    
         enabledControls() {
            $("#mispar_ishi_value").prop('disabled', '');
            $("#employee_name_value").prop('disabled', '');
        }
        /////////////////////////////////////////////////////////////

          /********************************* txt name functions ******************************************/

        userNameinputChanged=(str:string)=> {
        //$scope.$broadcast('angucomplete-alt:clearInput');
            this.SelectedEmployeeName = new EmployeeNameContainer();
            this.SelectedEmployeeName.employeeName = str;
            if (str.length == 1) {
                this.$log.debug("go to server names- " + str);
                //   $log.debug("input data changed- " + str);

                this.IApiProviderService.getOvdimToUserByName(str, 75757)
                    .then(res=> {
                        this.EmployeeNames = res;
                    }, error=> {
                        this.error = error.data.Message;
                        this.modalErrorsShown = true;
                    });
                }
            }

    RegisterToSelectedEmployeeName = () => {

        this.$scope.$watch(() => { return this.SelectedEmployeeName }, (newVal: any, oldVal: any) => {
            //When users selectes an item from the list - the item will be contained in the newVal.description
            //When using the keyboard to insert new characters before selecting the item from the list, the newVal will contain the item directly (e.g. newVal.MisparIshi)
            if (newVal && newVal.description) {
                this.misparIshi = newVal.description.misparIshi;
                //vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                this.EmployeeIds = [];
                this.EmployeeNames = [];
                this.IUiHelperService.EnablePage(true);

                var name = newVal.description.employeeName.split('(');
                this.EmployeeName = name[0];
                this.misparIshi = name[1].split(')')[0].trim();
                $("#mispar_ishi_value").val(this.misparIshi);
                this.IUiHelperService.EnablePage(true);
            }
            //We do not want to fetch data whenever there is a change on the data only when user selects an item
            //else if (newVal.MisparIshi) {
            //    employeeId = newVal.MisparIshi;
            //}
            else {
                return;
            }
        
            this.$log.debug("$watch: SelectedEmployeeId- :" + this.EmployeeName);
            this.GetDailyDetails();
        }, true);
    }
   
    isUserNameValid=() =>{
        var nameText = $("#employee_name_value").val();
        var name;
        this.EmployeeNames = [];
        this.SelectedEmployeeId = new EmployeeIdContainer();
        this.SelectedEmployeeName = new EmployeeNameContainer();

        if (nameText.trim() != "") {
            if (nameText.indexOf('(') > -1)
                nameText = nameText.split('(')[0].trim();

            this.IApiProviderService.getOvdimToUserByName(nameText, 75757)
                .then(res=> {
                    this.EmployeeNames = res;

                    if (this.EmployeeNames.length == 0) {
                        alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                        this.IUiHelperService.EnablePage(false);
                        this.enabledControls();
                    }
                    else if (this.EmployeeNames.length > 0) {

                        var isexist = false;
                        this.EmployeeNames.forEach((s: EmployeeNameContainer) => {
                            name = s.employeeName;
                            if (s.employeeName == nameText || nameText == s.employeeName.split('(')[0].trim()) {
                                this.EmployeeName = s.employeeName.split('(')[0];
                                this.misparIshi = +(s.employeeName.split('(')[1].split(')')[0].trim());

                                //   vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                                //   vm.SelectedEmployeeName = { EmployeeName: vm.EmployeeName };
                                isexist = true;
                            }
                        });
                        if (!isexist) {
                            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
                            this.IUiHelperService.EnablePage(false);
                            this.enabledControls();
                        }
                        else {
                            $("#mispar_ishi_value").val(this.misparIshi);
                            $("#employee_name_value").val(this.EmployeeName);
                            this.GetDailyDetails();
                            this.IUiHelperService.EnablePage(true);
                        }
                    }  
                }, error=> {
                    this.error = error.data.Message;
                    this.modalErrorsShown = true;
                });
        }
    }
        /************************************ end - txt name functions ***************************************/


        GetDailyDetails=() =>{

            this.IApiProviderService.getOvedAllDetails(this.misparIshi, this.cardDate)
                .then(res=> {
                    this.IWorkCardStateService.ovedDetails = res;
                    this.EmployeeMaamad = res.TeurMaamad;
                    $("#employee_name_value").val(res.FullName);
                    $("#mispar_ishi_value").val(res.MisparIshi);
                    this.SelectedEmployeeId = new EmployeeIdContainer();
                    this.SelectedEmployeeId.misparIshi = res.MisparIshi.toString();
                    this.SelectedEmployeeName = new EmployeeNameContainer();
                    this.SelectedEmployeeName.employeeName = res.FullName ;
                    this.$rootScope.$broadcast(modules.workcard.GeneralEvents.OVED_DETAILS_CHANGED);
                }, error=> {
                    this.error = error.data.Message;
                    this.modalErrorsShown = true;
            });
        }
    }

    angular.module("modules.workcard").directive("generalDetailsDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/generalDetails/generalDetails.tpl.html",
            controller: GeneralDetailsController,
            controllerAs: "vm",
            
        }
    });

}