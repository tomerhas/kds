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
            this.InitDatePicker();
            this.RegisterToDatePickerChange();
            this.RegisterToRequestChangeDate();

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



         getPeiluyot= () =>{
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
                    this.misparIshi = newVal.description.MisparIshi;
                    //vm.SelectedEmployeeId = { MisparIshi: vm.misparIshi };
                    this.EmployeeIds = [];
                    this.EmployeeNames = [];
                    this.IUiHelperService.EnablePage('');
                }
                //We do not want to fetch data whenever there is a change on the data only when user selects an item
                //else if (newVal.MisparIshi) {
                //    employeeId = newVal.MisparIshi;
                //}
                else {
                    return;
                }
            this.$log.debug("$watch: SelectedEmployeeId- :" + this.misparIshi);
            this.GetDailyDetails();
            }, true);
        }

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
                    this.$rootScope.$broadcast("ovedDetails-changed");
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