//Service for holding applicative state
workCardApp.service("workCardStateService",
    function () {
        this.cardGlobalData = { ovedDetails: {}, workCardResult: {} };
        this.lookupsContainer = {};
    
    });