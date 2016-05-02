module modules.workcard {

    export interface IWorkCardStateService {
        ovedDetails: EmplyeeDetails;
        workCardResult: WorkCardResultContainer,
        lookupsContainer: WorkCardLookupsContainer;
        DisablePageX: boolean ;
    }
    
    class WorkCardStateService implements IWorkCardStateService {
        constructor() {
            this.DisablePageX = false;
        }
        public ovedDetails: EmplyeeDetails;
        public workCardResult: WorkCardResultContainer;
        public lookupsContainer: WorkCardLookupsContainer;
        public DisablePageX: boolean ;
    }

    angular.module("modules.workcard").service("IWorkCardStateService", WorkCardStateService);

}