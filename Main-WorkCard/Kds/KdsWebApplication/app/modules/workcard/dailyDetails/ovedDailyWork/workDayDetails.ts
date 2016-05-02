module modules.workcard {

    class WorkDayDetailsController {
        DayDetailsObj: WorkCardResultContainer;
        TachgrafList: SelectItem[];
        LinaList: SelectItem[];
        HalbashaList: SelectItem[];
        HashlameLeYomList: SelectItem[];
        btnHashlamaForDayImage: string;
        HashlamaCBVal: number;

        constructor(
            private IWorkCardStateService: IWorkCardStateService,
            private $scope: ng.IScope,
            private IUiHelperService:modules.common.IUiHelperService) {

            this.UpdateLists();
            this.SetOvedDetails();
            this.RegisterToOvedDeatilsChanged();

            if (this.IWorkCardStateService.DisablePageX)
                this.IUiHelperService.EnablePage(false)
            //if ($rootScope.DisablePage)
            //    $rootScope.EnablePage('disabled');
        }

        RegisterToOvedDeatilsChanged = () => {
            this.$scope.$on(modules.workcard.GeneralEvents.OVED_PEILUT_CHANGED, () => {
                this.SetOvedDetails();
                this.UpdateLists();
            });

        }

        SetOvedDetails = () => {
            if (this.IWorkCardStateService.workCardResult) {
                this.DayDetailsObj = this.IWorkCardStateService.workCardResult;

                if (this.DayDetailsObj.DayDetails.HashlamaForDay.Value > 0)
                    this.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox.jpg";
                else this.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox-empty.jpg";
                this.HashlamaCBVal = this.DayDetailsObj.DayDetails.HashlamaForDay.Value;
            }
        }

        UpdateLists = () => {
            if (this.IWorkCardStateService.lookupsContainer) {
                this.TachgrafList = this.IWorkCardStateService.lookupsContainer.TachografList;
                this.LinaList = this.IWorkCardStateService.lookupsContainer.LinaList;
                this.HalbashaList = this.IWorkCardStateService.lookupsContainer.HalbashaList;
                this.HashlameLeYomList = this.IWorkCardStateService.lookupsContainer.HashlameLeYomList;
            }
        }

        HashlamaForDay_Click = () => {
            if (this.HashlamaCBVal == 0) {
                this.HashlamaCBVal = 1;
                this.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox.jpg";
            }
            else {
                this.HashlamaCBVal = 0;

                this.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox-empty.jpg";
            }
            this.IUiHelperService.DisabledControls();
        }

    }


    angular.module("modules.workcard").directive("workDayDetailsDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/dailyDetails/ovedDailyWork/workDayDetails.tpl.html",
            controller: WorkDayDetailsController,
            controllerAs: "vm"
        }
    });
}