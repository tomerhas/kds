module modules.workcard {

    export interface IWorkCardStateService {
        ovedDetails: EmplyeeDetails;
        workCardResult: WorkCardResultContainer,
        lookupsContainer: WorkCardLookupsContainer;
    }
    
    class WorkCardStateService implements IWorkCardStateService {
        public ovedDetails: EmplyeeDetails;
        public workCardResult: WorkCardResultContainer;
        public lookupsContainer: WorkCardLookupsContainer;
    }

    angular.module("modules.workcard").service("IWorkCardStateService", WorkCardStateService);

}