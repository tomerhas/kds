app.factory("workCardStateService",
    function () {
        var cardGlobalData = { ovedDetails: {}, ovedPeiluyot: {}};

        return {
            cardGlobalData: cardGlobalData
        }
    });