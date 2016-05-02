workCardApp.controller('mainWorkCardController',

    function ($rootScope, $scope,apiProvider, workCardStateService) {

        var vm = this;

        $rootScope.DisabledControls = DisabledControls;
        $rootScope.DisablePage = false;
        $rootScope.EnablePage = EnablePage;
        $rootScope.focusTextById = focusTextById;
       // $rootScope.maxLengthCheck = maxLengthCheck;

        vm.SetLookupsInCache = SetLookupsInCache;
        activate();

        function activate() {
            SetLookupsInCache();
           // RegisterDisableChanges();
        }

        function SetLookupsInCache() {
         
                apiProvider.getLookupsContainer().then(function (result) {
                    workCardStateService.lookupsContainer = result;
                    $rootScope.$broadcast("workcardLookups-changed");
                }); 
        }

        //function RegisterDisableChanges() {
        //    $scope.$on('disablePage-changed', function (event, args) {
        //       // $log.debug("ovedDailyDetailsController - updating lookups after event changed recieved");
        //        vm.disabledFrame = workCardStateService.disable;
        //    });    
        //}

        function DisabledControls() {
            //get("btnPrevCard").disabled = true;
            //$get("btnNextCard").disabled = true;
            //$get("btnPrevCard").className = "btnPrevDayDis";
            //$get("btnNextCard").className = "btnNextDayDis"
            //$get("btnNextErrCard").disabled = true;
            //$get("btnNextErrCard").className = "btnNextErrorDis";

            $('.cntlDis').attr('disabled', 'disabled');
        }

        function EnablePage(value) {

            // $("input[type=button]").attr("disabled", true);
            // $('#tabsDiv *').prop('disabled', true);
            if (value != '') {
                $('input, textarea, select')
                  .attr('disabled', value);//'disabled');..
                $rootScope.DisablePage = true;
            }
            else {
                $('input, textarea, select').removeAttr("disabled");
                $rootScope.DisablePage = false;
            }

        }
        //function maxLengthCheck(ob) {
        //    if (ob.value.length > ob.maxLength)
        //        ob.value = ob.value.slice(0, ob.maxLength)
        //}
        function focusTextById(id) {
            $("#" + id).select();
        }

       
    });
