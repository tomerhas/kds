module modules.workcard {

    export interface IApiProviderService {
        getLookupsContainer(): ng.IPromise<WorkCardLookupsContainer>;
        getUserWorkCard(misparIshi: string, cardDate: Date): ng.IPromise<WorkCardResultContainer>;
        getWCLastUpdatsDetails(misparIshi: number, cardDate: Date): ng.IPromise<UserWCUpdateInfo>;
        getOvdimToUser(inputstring: string, userId: number): ng.IPromise<EmployeeIdContainer[]>;
        getOvdimToUserByName(inputstring: string, userId: number): ng.IPromise<EmployeeNameContainer[]>;
        getEmployeeBasicDetails(misparIshi: number, cardDate: Date): ng.IPromise<EmployeeBasicDetails>; 
        getOvedAllDetails(misparIshi: number, cardDate: Date): ng.IPromise<EmplyeeDetails>;
        isCardExists(misparIshi: number, cardDate: Date): ng.IPromise<string>;
        getNextErrorCardDate(misparIshi: number, cardDate: Date): ng.IPromise<string>;
        checkOtoNo(OtoNo: number): ng.IPromise<string>;
        getKnisaActualMin(makat: number, taarich: string, knisa: number): ng.IPromise<string>;
    }

    class ApiProviderService implements IApiProviderService{
        private root: string;

        constructor(
            private $log: ng.ILogService,
            private $http: ng.IHttpService,
            private $location: ng.ILocationService,
            private $rootScope: ng.IRootScopeService
        )
        {
            this.root = this.$location.host() + ":" + this.$location.port();
        }

        public getLookupsContainer = (): ng.IPromise<WorkCardLookupsContainer>=> {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetWorkCardLookups", {})
                .then((response) => {
                    return response.data.d;
                }, (er)=> {
                    this.logError("getLookupsContainer", er);
                    throw er;
                });
        }

        private logError = (methodName:string, error:any)=> {
            this.$log.error(methodName + " returned an error: " + error.Message + ". " + error.StackTrace);
        }

        public getUserWorkCard = (misparIshi: string, cardDate: Date): ng.IPromise<WorkCardResultContainer> => {
        return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetUserWorkCard", { misparIshi: misparIshi, cardDate: cardDate })
            .then((response)=> {
                //Need to objectify since we had a problem with the datetime serialization
                var strResult = response.data.d.toString();
                var obj: WorkCardResultContainer = <WorkCardResultContainer>angular.fromJson(strResult)
                return obj;
                ////var obj= response.data.d;
                ////return obj;
                //return response.data.d;
            }, function (er) {
                this.logError("getUserWorkCard", er);
                throw er;
            });
        }


       //public getSnifOved = (misparIshi:number):any {
       //    return this.$http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvedSnifAndUnit", { iMisparIshi: misparIshi })
       //        .then((response) => {
       //            return response.data.d;
       //        }, function (er) {
       //            this.logError("getSnifOved", er);
       //            throw er;
       //        });
       // }

    ////this.getMisparIshi = function (prefix, cnt, context) {
    ////    return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvdim",
    ////        { prefixText: prefix, count: cnt, contextKey: context });
    ////}

    ////this.getOvedDetails = function (misparIshi, cardDate) {
    ////    return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvedDetails",
    ////        { misparIshi: misparIshi, cardDate: cardDate });
    ////}

        public getWCLastUpdatsDetails = (misparIshi: number, cardDate: Date): ng.IPromise<UserWCUpdateInfo> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetLastUpdate",{ misparIshi: misparIshi, cardDate: cardDate })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getUserWorkCard", er);
                    throw er;
                });
        }

        public getOvdimToUser = (inputstring: string, userId: number): ng.IPromise<EmployeeIdContainer[]> => { 
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvdimToUser",{ inputstring: inputstring, userId: userId })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getOvdimToUser", er);
                    throw er;
                });
        }
        
  
        public getOvdimToUserByName = (inputstring: string, userId: number): ng.IPromise<EmployeeNameContainer[]> => { 
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvdimToUserByName", { inputstring: inputstring, userId: userId })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getOvdimToUserByName", er);
                    throw er;
                });
        }
  
        
        public getEmployeeBasicDetails = (misparIshi: number, cardDate: Date): ng.IPromise<EmployeeBasicDetails> => { 
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetEmpoyeeById",{ iMisparIshi: misparIshi, sCardDate: cardDate })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getEmployeeBasicDetails", er);
                    throw er;
                });
        }

        public getOvedAllDetails = (misparIshi: number, cardDate: Date): ng.IPromise<EmplyeeDetails> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvedAllDetails", { iMisparIshi: misparIshi, sCardDate: cardDate })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getOvedAllDetails", er);
                    throw er;
                });
        }


        public isCardExists = (misparIshi: number, cardDate: Date): ng.IPromise<string> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/IsCardExists", { iMisparIshi: misparIshi, dWorkCard: cardDate })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("isCardExists", er);
                    throw er;
                });
        }

        public getNextErrorCardDate = (misparIshi: number, cardDate: Date): ng.IPromise<string> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetNextErrorCardDate", { iMisparIshi: misparIshi, dWorkCard: cardDate })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getNextErrorCardDate", er);
                    throw er;
                });
        }
     
        public checkOtoNo = (OtoNo: number): ng.IPromise<string> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/CheckOtoNo", { lOtoNo: OtoNo })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("checkOtoNo", er);
                    throw er;
                });
        }

        public getKnisaActualMin = (makat: number, taarich: string, knisa: number): ng.IPromise<string> => {
            return this.$http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetKnisaActualMin", { lMakat: makat, sSidurDate: taarich, iMisaprKnisa: knisa })
                .then((response) => {
                    return response.data.d;
                }, function (er) {
                    this.logError("getKnisaActualMin", er);
                    throw er;
                });
        }
    

    }

    angular.module("modules.workcard").service("IApiProviderService", ApiProviderService);
}