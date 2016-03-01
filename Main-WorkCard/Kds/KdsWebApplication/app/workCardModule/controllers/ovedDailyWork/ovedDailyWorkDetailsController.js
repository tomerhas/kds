workCardApp.controller('ovedDailyWorkDetailsController',
    function ($rootScope, $scope, workCardStateService) {
        var vm = this;
        vm.DayDetails = {};
        vm.TachgrafList = {};
        vm.LinaList = {};
        vm.HalbashaList = {};
        vm.HashlameLeYomList = {};
        vm.btnHashlamaForDayImage;
        vm.ChangeHalbashaSelect = ChangeHalbashaSelect;
        vm.HashlamaForDay_Click = HashlamaForDay_Click;
        vm.color;
        vm.val = 1;
        vm.HalbashaVal = 0;
        vm.HashlamaCBVal = 0;
      //  vm.DisabledControls = DisabledControls;
       
        activate();

        function activate() {
            UpdateLists();

            vm.DayDetails = workCardStateService.cardGlobalData.workCardResult.DayDetails;
            vm.HalbashaVal = vm.DayDetails.Halbasha.Value;
            vm.HashlamaCBVal = vm.DayDetails.HashlamaForDay.Value;

            if(vm.DayDetails.HashlamaForDay.Value >0)
                vm.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox.jpg";
            else vm.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox-empty.jpg";

           
            if ($rootScope.DisablePage)
                $rootScope.EnablePage('disabled');

        }
   
        function UpdateLists() {
            if (workCardStateService.lookupsContainer.container) {
                vm.TachgrafList = workCardStateService.lookupsContainer.container.TachografList;
                vm.LinaList = workCardStateService.lookupsContainer.container.LinaList;
                vm.HalbashaList = workCardStateService.lookupsContainer.container.HalbashaList;
                vm.HashlameLeYomList = workCardStateService.lookupsContainer.container.HashlameLeYomList;
            }
        }

        function ChangeHalbashaSelect() {
            if(vm.HalbashaVal ==0)
                vm.HalbashaVal = vm.DayDetails.Halbasha.Value;
        }

        function HashlamaForDay_Click() {
            if (vm.HashlamaCBVal == "0") {
                vm.HashlamaCBVal = "1";
                vm.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox.jpg";
            }
            else {
                vm.HashlamaCBVal = "0";

                vm.btnHashlamaForDayImage = "../../../../../Images/allscreens-checkbox-empty.jpg";
            }
            $rootScope.DisabledControls();
        }
        
    });
/*
workCardApp.controller('ovedDailyWorkDetailsController',
    function($scope, apiProvider,workCardStateService) {

        $scope.name = "daily work"
        $scope.updateRes = "";
        
        //In order to watch a property on the service,  the first param of the watch must be a method that returns the property and not to watch the property itself
        $scope.$watch(function () { return workCardStateService.cardGlobalData }, function (newVal, oldVal) {
            updateProps();
        },true);

        var updateProps = function () {
            //$scope.updateRes = workCardStateService.cardGlobalData.ovedPeiluyot;
        }
    });*/