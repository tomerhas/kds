module modules.workcard {
    class DailyDetailsController {
        tabDisplayed: number;
        ZmanNesiaList: SelectItem[];
        OvedDetails: WorkCardResultContainer;

        constructor(
            private $rootScope: ng.IRootScopeService,
            private IWorkCardStateService: IWorkCardStateService,
            private $scope: ng.IScope) {

            this.tabDisplayed = 0;
           
            
            this.UpdateLists();
            this.SetOvedDetails();
            this.RegisterToOvedDeatilsChanged();
            
        }

         replaceTab=(num:number) =>{
            this.tabDisplayed = num;
        }

        PrevDay_Click= () =>{
            this.$rootScope.$broadcast(modules.workcard.GeneralEvents.ADVANCE_DATE_BY_DAYS,-1);
        }

        NextDay_Click = () => {
            this.$rootScope.$broadcast(modules.workcard.GeneralEvents.ADVANCE_DATE_BY_DAYS,+1);
        }

        NextErrorCard_Click = () => {
            this.$rootScope.$broadcast(modules.workcard.GeneralEvents.SHOW_NEXT_ERROR_DAY, +1);
        }

        UpdateLists = () => {
            if (this.IWorkCardStateService.lookupsContainer) {
                this.ZmanNesiaList = this.IWorkCardStateService.lookupsContainer.ZmanNesiaList;
            }
        }

        SetOvedDetails = () => {
            if (this.IWorkCardStateService.workCardResult) {
                this.OvedDetails = this.IWorkCardStateService.workCardResult;
            }
        }

        RegisterToOvedDeatilsChanged = () => {
            this.$scope.$on(modules.workcard.GeneralEvents.OVED_PEILUT_CHANGED, () => {
                this.SetOvedDetails();    
                this.UpdateLists();
            }); 
        }

    }

    angular.module("modules.workcard").directive("dailyDetailsDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/dailyDetails/dailyDetails.tpl.html",
            controller: DailyDetailsController,
            controllerAs: "vm"
        }
    });

}