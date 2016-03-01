workCardApp.directive('inputMaxLengthNumber', inputMaxLengthNumber);
//workCardApp.directive('time', TimeDirective);
//workCardApp.directive('psDatetimePicker', psDatetimePicker);
workCardApp.directive('timeegged', convertDateToTime);
//workCardApp.directive('autoComplete', autoComplete);
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


function inputMaxLengthNumber() {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs, ngModelCtrl) {
            function fromUser(text) {
                var maxlength = Number(attrs.maxlength);
                if (String(text).length > maxlength) {
                    ngModelCtrl.$setViewValue(ngModelCtrl.$modelValue);
                    ngModelCtrl.$render();
                    return ngModelCtrl.$modelValue;
                }
                return text;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
}




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


function psDatetimePicker() {
    var format = 'hh:mm';

    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attributes, ctrl) {
            element.datetimepicker({
                format: format
            });
            var picker = element.data("DateTimePicker");

            ctrl.$formatters.push(function (value) {
                var date = moment(value);
                if (date.isValid()) {
                    return date.format(format);
                }
                return '';
            });

            element.on('change', function (event) {
                scope.$apply(function () {
                    var date = picker.getDate();
                    ctrl.$setViewValue(date.valueOf());
                });
            });
        }
    };
}


function convertDateToTime() {
    return {
        restrict: 'A',
        terminal: true,
        require: "?ngModel",
        link: function (scope, element, attrs, ngModel) {
            scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                if (newValue == null || newValue == "" ) return;
                else if (newValue.length == 5) {
                    return;
                }
                else {
                    var fullTime = newValue.split("T")[1];
                    if (fullTime != null) {
                        ngModel.$setViewValue(fullTime.split(":")[0] + ":" + fullTime.split(":")[1]);
                        ngModel.$render();
                       // ngModel.$modelValue = new Date();
                    }
                }
                
            });
        }
    };
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
