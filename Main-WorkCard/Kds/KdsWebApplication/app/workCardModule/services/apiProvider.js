workCardApp.factory("apiProvider",
    function ($http,$location) {
        var root = $location.host()+":" + $location.port();
        var getSnifOved = function (misparIshi) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvedSnifAndUnit",
                                { iMisparIshi: misparIshi });
        }

        var getMisparIshi = function (prefix, cnt, context) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvdim",
                                { prefixText: prefix, count: cnt, contextKey: context });
        }

        var getOvedDetails = function (misparIshi,cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvedDetails",
                                 { misparIshi: misparIshi, cardDate: cardDate });
        }

        var getEmployeePeiluyot = function (misparIshi,cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetEmployeePeiluyot",
                                 { misparIshi: misparIshi, cardDate: cardDate });
        }

        var getWCLastUpdatsDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetLastUpdate",
                                 { misparIshi: misparIshi, cardDate: cardDate });
        }

        return {
            getSnifOved: getSnifOved,
            getMisparIshi: getMisparIshi,
            getOvedDetails: getOvedDetails,
            getEmployeePeiluyot: getEmployeePeiluyot,
            getWCLastUpdatsDetails: getWCLastUpdatsDetails
        }
    });