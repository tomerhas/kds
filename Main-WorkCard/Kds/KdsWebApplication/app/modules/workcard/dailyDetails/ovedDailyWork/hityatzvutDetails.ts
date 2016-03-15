module modules.workcard {

    class HityatzvutDetailsController {
        SibotDivuachList : SelectItem[];
        FirstHityazvut : Hityatzvut;
        SecondHityazvut: Hityatzvut;
        OvedDetails: WorkCardResultContainer;

        constructor(
            private IWorkCardStateService: IWorkCardStateService,
            private $scope:ng.IScope) {

            this.RegisterToOvedDeatilsChanged();
            this.SetOvedDetails();
            this.UpdateLists();
        }

        RegisterToOvedDeatilsChanged = () => { 
            this.$scope.$on(modules.workcard.GeneralEvents.OVED_PEILUT_CHANGED, () => {
                this.SetOvedDetails();
                this.UpdateLists();
            });

        }

        SetOvedDetails = () => {
            if (this.IWorkCardStateService.workCardResult) {
                this.OvedDetails = this.IWorkCardStateService.workCardResult;
                this.FirstHityazvut = this.IWorkCardStateService.workCardResult.FirstHityazvut;
                this.SecondHityazvut = this.IWorkCardStateService.workCardResult.SecondHityazvut;
            }
        }

        UpdateLists = () => {
            if (this.IWorkCardStateService.lookupsContainer) {
                this.SibotDivuachList = this.IWorkCardStateService.lookupsContainer.SibotLedivuachList;
            }
        }
    }

    angular.module("modules.workcard").directive("hityatzvutDetailsDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/dailyDetails/ovedDailyWork/hityatzvutDetails.tpl.html",
            controller: HityatzvutDetailsController,
            controllerAs: "vm"
        }
    });
}