module modules.workcard {
    class SidurAvodaController {

        Sidurim: SidurWC[];
        NumSidurim :number;

        SibotDivuachList: SelectItem[];
        HarigaList: SelectItem[];
        PizulList: SelectItem[];
        HshlamaList: SelectItem[];
        error: string;
        modalErrorsShown:boolean

        peilutBdikotBehaviorObj: peilutBdikotBehavior;
              
        constructor(
            private IApiProviderService: modules.workcard.IApiProviderService,
            private IWorkCardStateService: IWorkCardStateService,
            private $scope: ng.IScope,
            private $log: ng.ILogService,
            private IUiHelperService: modules.common.IUiHelperService ) {

            this.NumSidurim = 0;
            this.peilutBdikotBehaviorObj = new peilutBdikotBehavior(IApiProviderService, IWorkCardStateService);
            this.RegisterToOvedPeiluyotChanged();
        }

        RegisterToOvedPeiluyotChanged = () => {
            this.$scope.$on(modules.workcard.GeneralEvents.OVED_PEILUT_CHANGED, (event, args) =>{
                this.$log.debug("ovedPeiluyotController recieved broadcast ovedPeiluyot-changed event");
                this.Sidurim = this.IWorkCardStateService.workCardResult.Sidurim.SidurimList;
                //this.Sidurim.forEach(function (sidur) {
                //    sidur.MyFullDate = new Date(sidur.FullShatHatchala.Value);
                //});
                this.InitializeCols();
                this.NumSidurim = this.Sidurim.length;
                this.UpdateLists();
            });
        }

        UpdateLists=()=> {
            if (this.IWorkCardStateService.lookupsContainer) {
                this.SibotDivuachList = this.IWorkCardStateService.lookupsContainer.SibotLedivuachList;
                this.HarigaList = this.IWorkCardStateService.lookupsContainer.HarigaList;
                this.PizulList = this.IWorkCardStateService.lookupsContainer.PizulList;
                this.HshlamaList = this.IWorkCardStateService.lookupsContainer.HashlamaList;
            }
    }

        InitializeCols = () => {
            var bPeilutActive;
            var bSidurActive;
            this.Sidurim.forEach(sidur=> {
                bSidurActive = sidur.SidurActive.Value;
                sidur.PeilutList.forEach(peilut => {
                    bPeilutActive = peilut.PeilutActive.Value;
                    if ((peilut.MisparKnisa.Value > 0) && (peilut.MakatType.Value == 1))  //כניסה לפי צורך
                    {
                        if (peilut.DakotBafoal.Value== "0")
                            peilut.CancelPeilut.Value = "ImgKnisaS";
                        else
                            peilut.CancelPeilut.Value = "ImgCheckedPeilut";

                        if (bSidurActive)
                            peilut.PeilutActive.Value = true;
                    }
                    else {
                        if (bPeilutActive)
                            peilut.CancelPeilut.Value = "ImgCheckedPeilut";
                        else
                            peilut.CancelPeilut.Value = "ImgCancel";
                    }
                });
            });
        }

       ChangeCollapeImg=(index:number)=> {
            if (this.Sidurim[index].CollapseSrc.Value.indexOf("openArrow") > -1) {
                this.Sidurim[index].CollapseSrc.Value = "../../../../Images/closeArrow.png";
            }
            else this.Sidurim[index].CollapseSrc.Value = "../../../../Images/openArrow.png";
        }
        
       CheckMakat = (peilut: PeilutWC) => {
           var res = this.peilutBdikotBehaviorObj.CheckMakat(peilut);
           this.displayErrorFromResult(res);
       }

       ChkOto = (iSidur: number, iPeilut: number) => {
           var promise = this.peilutBdikotBehaviorObj.ChkOto(iSidur, iPeilut, this.Sidurim);
           promise.then(res=> {
               this.displayErrorFromResult(res);
           });
       }

       displayErrorFromResult = (res: ValidationResult) => {
           if (res.isValid == false) {
               this.error = res.errorMessage;
               this.modalErrorsShown = true;
           }
       }
       
      
       /************************************* dakot bafoal*********************************************/ 

       CheckValidMinutes = (peilut: PeilutWC) => {
           var res = this.peilutBdikotBehaviorObj.CheckValidMinutes(peilut);
           this.displayErrorFromResult(res);
       }
    

       SetKnisaActualMin = (peilut: PeilutWC) => {
           var res = this.peilutBdikotBehaviorObj.SetKnisaActualMin(peilut);
           this.displayErrorFromResult(res);
       }
    }

    angular.module("modules.workcard").directive("sidorAvodaDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/sidurAvoda/sidurAvoda.tpl.html",
            controller: SidurAvodaController,
            controllerAs: "vm"
        }
    });

}