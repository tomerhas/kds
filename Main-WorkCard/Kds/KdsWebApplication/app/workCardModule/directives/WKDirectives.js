workCardApp.directive('time', TimeDirective);
//workCardApp.directive('hboTabs', tabs);
//workCardApp.directive('uiMask', TimeMask);

//http://jsfiddle.net/FvLQN/1074/
//function tabs() {
//    return {
//        restrict: 'A',
//        link: function(scope, elm, attrs) {
//            var jqueryElm = $(elm[0]);
//            $(jqueryElm).tabs()
//        }
//    };
//}


function TimeDirective() {
    return {
        require: 'ngModel',
        link: link
    };

    function link(scope, element, attrs, ngModel) {
        ngModel.$parsers.push(function (viewValue) {
            if (viewValue.length > 4) {
                var date = ngModel.$modelValue;
                var shaa = viewValue.split(':');
                if (typeof date == 'string')
                    date = new Date(date);

                date.setHours(shaa[0]);
                date.setMinutes(shaa[1]);
              
                ngModel.$modelValue = date.toJSON();
            }
            return ngModel.$modelValue;
        });

        ngModel.$formatters.push(function (mv) {

            if (typeof mv == 'string') {
                mv = new Date(mv)
            }
                var h = mv.getHours();
                var m = mv.getMinutes();
                return h.padZero(2) + ":" + m.padZero(2);
          
        });
    }
}



//function TimeMask() {
//    return {
//        require: 'ngModel',
//        scope: {
//            uiMask: 'evaluate'
//        },
//        link: function ($scope, element, attrs, controller) {
//            controller.$render = function () {
//                var _ref;
//                element.val((_ref = controller.$viewValue) != null ? _ref : '');
//                return $(element).mask($scope.uiMask);
//            };
//            controller.$parsers.push(function (value) {
//                var isValid;
//                isValid = element.data('mask-isvalid');
//                controller.$setValidity('mask', isValid);
//                if (isValid) {
//                    return element.mask();
//                } else {
//                    return null;
//                }
//            });
//            return element.bind('blur', function () {
//                return $scope.$apply(function () {
//                    return controller.$setViewValue($(element).mask());
//                });
//            });
//        }
//    };
//}
