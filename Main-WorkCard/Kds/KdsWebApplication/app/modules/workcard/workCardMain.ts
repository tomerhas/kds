module modules.workcard {
    class WorkCardMainController {
        myDate: Date;
        myDate2: Date;

        constructor(private $log: ng.ILogService,
            private IApiProviderService: IApiProviderService,
            private IWorkCardStateService: IWorkCardStateService)
        {
            this.$log.debug("WorkCardMainController controller");
            this.GetLookups();
            this.myDate = new Date();
            this.myDate2 = new Date();
        }

        GetLookups = () => {
            this.IApiProviderService.getLookupsContainer().then(res=> {
                this.IWorkCardStateService.lookupsContainer = res;
                this.$log.debug(res.HalbashaList.length);
            })
        }
    }

    angular.module("modules.workcard").directive("workCardMainDirective", function () {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "modules/workcard/workCardMain.tpl.html",
            controller: WorkCardMainController,
            controllerAs:"vm"
        }
    });
    
}