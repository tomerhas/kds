workCardApp.service("apiProvider",
    function ($http, $location,$log) {
        this.root = $location.host() + ":" + $location.port();

        this.getLookupsContainer = function () {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetWorkCardLookups", {})
                    .then(function (response) {
                        return { container: response.data.d }
                    }, function (er) {
                        logError("getLookupsContainer",error);
                        throw er;
                    });
        }

        this.getUserWorkCard = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetUserWorkCard", { misparIshi: misparIshi, cardDate: cardDate })
                 .then(function (response) {
                     //Need to objectify since we had a problem with the datetime serialization
                     var obj = angular.fromJson(response.data.d)
                     return obj
                 }, function (er) {
                     logError("getUserWorkCard", error);
                     throw er;
                 });
        }

        
        var logError = function (methodName, error) {
            $log.error(methodName + " returned an error: " + error.data.Message + ". " + error.data.StackTrace);
        }

        this.getSnifOved = function (misparIshi) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvedSnifAndUnit",
                                { iMisparIshi: misparIshi });
        }

        this.getMisparIshi = function (prefix, cnt, context) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvdim",
                                { prefixText: prefix, count: cnt, contextKey: context });
        }

        this.getOvedDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvedDetails",
                                 { misparIshi: misparIshi, cardDate: cardDate });
        }

      

        this.getWCLastUpdatsDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetLastUpdate",
                                 { misparIshi: misparIshi, cardDate: cardDate });
        }

        this.getOvdimToUser = function (inputstring, userId) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvdimToUser",
                                 { inputstring: inputstring, userId: userId });
        }

        this.getOvdimToUserByName = function (inputstring, userId) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvdimToUserByName",
                                 { inputstring: inputstring, userId: userId });
        }

        this.getEmployeeBasicDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetEmpoyeeById",
                                 { iMisparIshi: misparIshi, sCardDate: cardDate });
        }

        this.getOvedAllDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvedAllDetails",
                                 { iMisparIshi: misparIshi, sCardDate: cardDate });
        }

        this.isCardExists = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/IsCardExists",
                                 { iMisparIshi: misparIshi, dWorkCard: cardDate });
        }

        
        this.getNextErrorCardDate = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetNextErrorCardDate",
                                 { iMisparIshi: misparIshi, dWorkCard: cardDate });
        }
        
        this.checkOtoNo = function (OtoNo) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/CheckOtoNo",
                                 { lOtoNo: OtoNo });
        }

        this.getKnisaActualMin = function (makat,taarich,knisa) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetKnisaActualMin",
                                 { lMakat: makat, sSidurDate:taarich, iMisaprKnisa:knisa});  
        }
        
    });