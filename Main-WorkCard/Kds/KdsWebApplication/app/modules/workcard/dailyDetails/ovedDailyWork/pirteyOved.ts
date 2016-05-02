module modules.workcard {

    class PirteyIOvedController { 
        OvedDetails: EmplyeeDetails;

        constructor(
            private IWorkCardStateService: IWorkCardStateService,
            private $scope: ng.IScope)
        {
            this.SetOvedDetails();
            this.RegisterToOvedDeatilsChanged();
        }

        RegisterToOvedDeatilsChanged = () => {
            this.$scope.$on(modules.workcard.GeneralEvents.OVED_DETAILS_CHANGED, () => {
                this.SetOvedDetails();
            });

        }

        SetOvedDetails = () => {
            if (this.IWorkCardStateService.workCardResult) {
                this.OvedDetails = this.IWorkCardStateService.ovedDetails;

            }
        }

       
    }

    angular.module("modules.workcard").directive("pirteyOvedDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/dailyDetails/ovedDailyWork/pirteyOved.tpl.html",
            controller: PirteyIOvedController,
            controllerAs: "vm"
        }
    });
}