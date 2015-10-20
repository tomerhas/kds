//Service for holding applicative state
workCardApp.factory("workCardStateService",
    function () {
        var cardGlobalData = { ovedDetails: {}, ovedPeiluyot: {} };


        return {
            cardGlobalData: cardGlobalData
        }
    });