module modules.workcard {
    class DailyDetailsController {
        tabDisplayed: number;

        constructor(
            private $rootScope:ng.IRootScopeService) {

            this.tabDisplayed = 0;
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