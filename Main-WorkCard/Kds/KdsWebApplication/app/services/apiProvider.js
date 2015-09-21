app.factory("apiProvider",
    function ($http) {
        var getSnifOved = function (misparIshi) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvedSnifAndUnit",
                                { iMisparIshi: misparIshi });
        }

        var getMisparIshi = function (prefix, cnt, context) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvdim",
                                { prefixText: prefix, count: cnt, contextKey: context });
        }

        var getOvedDetails = function (misparIshi,cardDate) {
            return $http.post("../../Modules/WebServices/wsWorkCard.asmx/GetOvedDetails",
                                 { iMisparIshi: misparIshi, dCardDate: cardDate });
        }

        return {
            getSnifOved: getSnifOved,
            getMisparIshi: getMisparIshi,
            getOvedDetails: getOvedDetails
        }
    });