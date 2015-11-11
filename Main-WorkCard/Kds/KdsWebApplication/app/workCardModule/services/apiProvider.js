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

        var getOvdimToUser = function (inputstring, userId) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetOvdimToUser",
                                 { inputstring: inputstring, userId: userId });
        }

        var getEmployeeBasicDetails = function (misparIshi, cardDate) {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetEmpoyeeById",
                                 { iMisparIshi: misparIshi, sCardDate: cardDate });
        }

        var getSibotLedivuach = function () {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetSibotLedivuach", {});
        }

        var getHariga = function () {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetHariga", {});
        }

        var getPizul = function () {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetPizul", {});
        }

        var getHashlama = function () {
            return $http.post("../../../Modules/WebServices/wsWorkCard.asmx/GetHashlama", {});
        }

        return {
            getSnifOved: getSnifOved,
            getMisparIshi: getMisparIshi,
            getOvedDetails: getOvedDetails,
            getEmployeePeiluyot: getEmployeePeiluyot,
            getWCLastUpdatsDetails: getWCLastUpdatsDetails,
            getOvdimToUser: getOvdimToUser,
            getEmployeeBasicDetails: getEmployeeBasicDetails,
            getSibotLedivuach: getSibotLedivuach,
            getHariga: getHariga,
            getPizul: getPizul,
            getHashlama: getHashlama
        }
    });