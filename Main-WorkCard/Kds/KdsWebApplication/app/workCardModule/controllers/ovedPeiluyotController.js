workCardApp.controller('ovedPeiluyotController',
    function($scope, apiProvider, workCardStateService) {
        $scope.name = "peiluyot"
        $scope.updateRes = "";

        $scope.selectedCountry = {};
        $scope.countries = [];
            //[{ name: 'Afghanistan', code: 'AF' },
            //                 { name: 'Aland Islands', code: 'AX' },
            //                 { name: 'Albania', code: 'AL' },
            //                 { name: 'Algeria', code: 'DZ' },
            //                 { name: 'American Samoa', code: 'AS' }];


        $scope.addMoreItems = function () {
            $scope.countries.push({ name: 'Bitem', code: 'Bcode' });
        }

        $scope.inputChanged = function (str) {
            //$scope.$broadcast('angucomplete-alt:clearInput');
            var promise = apiProvider.getWCLastUpdatsDetails("75757", "2015-10-10");
            promise.then(function (payload) {
                if (str === "7") {
                    $scope.countries.push({ name: '710', code: 'Bcode' });
                    $scope.countries.push({ name: '711', code: 'Bcode' });
                    $scope.countries.push({ name: '712', code: 'Bcode' });
                }
            }, function (errorPayload) {
               
            });
        }

        $scope.$on("ovedPeiluyot-changed", function (event, args) {
            
            updateProps();
        });
        //$scope.$watch(function () { return workCardStateService.cardGlobalData }, function (newVal, oldVal) {
        //    updateProps();
        //}, true);

        var updateProps = function () {
            $scope.updateRes = workCardStateService.cardGlobalData.ovedPeiluyot;
        }

    });