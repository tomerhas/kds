app.factory("apiProvider",
    function ($http) {
        var getSnifOved = function (misparIshi) {
            return $http.post("../../Modules/WebServices/wsGeneral.asmx/GetOvedSnifAndUnit",
                                { iMisparIshi: misparIshi });
        }

        return {
            getSnifOved: getSnifOved
        }
    });