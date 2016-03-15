module modules.workcard {
    class SidurAvodaController {

        Sidurim: SidurWC[];
        NumSidurim :number;

        SibotDivuachList: SelectItem[];
        HarigaList: SelectItem[];
        PizulList: SelectItem[];
        HshlamaList: SelectItem[];

        constructor(
            private IApiProviderService: modules.workcard.IApiProviderService,
            private IWorkCardStateService: IWorkCardStateService,
            private $scope: ng.IScope,
            private $log:ng.ILogService) {

            this.NumSidurim = 0;

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