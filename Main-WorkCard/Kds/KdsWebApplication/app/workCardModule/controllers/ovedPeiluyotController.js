workCardApp.controller('ovedPeiluyotController',
    function($scope, apiProvider, workCardStateService,$log) {

        var vm = this;
        vm.Sidurim = {};
        vm.NumSidurim = 0;
        vm.ChangeCollapeImg = ChangeCollapeImg;
        vm.SibotDivuachList = {};
        vm.HarigaList = {};
        vm.PizulList = {};
        vm.HshlamaList = {};
        
        vm.datePickerOption = {};

        activate();

        function activate() {
            RegisterToOvedPeiluyotChanged();
            UpdateLists();
          //  $('.timepicker').timepicker();
        }

        function UpdateLists() {
            if (workCardStateService.lookupsContainer.container) {
                vm.SibotDivuachList = workCardStateService.lookupsContainer.container.SibotLedivuachList;
                vm.HarigaList = workCardStateService.lookupsContainer.container.HarigaList;
                vm.PizulList = workCardStateService.lookupsContainer.container.PizulList;
                vm.HshlamaList = workCardStateService.lookupsContainer.container.HashlamaList;
            }
        }

        function SetDateOptions() {
            vm.datePickerOption = {
                change: function (a,b,c)
                {
                    var curVal = this.value();
                    if (curVal == null)
                    {
                        this.value(new Date());
                    }
                }
            }
        };

        function RegisterToOvedPeiluyotChanged()
        {
            $scope.$on('ovedPeiluyot-changed', function (event, args) {
                $log.debug("ovedPeiluyotController recieved broadcast ovedPeiluyot-changed event");
                vm.Sidurim = workCardStateService.cardGlobalData.workCardResult.Sidurim.SidurimList;
                vm.Sidurim.forEach(function (sidur) {
                    sidur.MyFullDate = new Date(sidur.FullShatHatchala.Value);
                });
                vm.NumSidurim = vm.Sidurim.length;
                UpdateLists();
            });
        }

        





        function ChangeCollapeImg(index) {
            if (vm.Sidurim[index].CollapseSrc.Value.indexOf("openArrow") > -1) {
                vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/closeArrow.png";
            }
            else vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/openArrow.png";
        }

        
    });