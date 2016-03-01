mainApp.directive('eggedDateTime', ["$rootScope",function ($rootScope) {
    return {
        restrict: 'EA',
        replace: true,
        require: "ngModel",
        link: link
    }

    function link(scope, element, attrs, ngModel) {
        //scope.$watch(attrs.ngModel, function (newValue, oldValue) {
        //    var date = new Date(newValue);
        //    if (date == null) {
        //        ngModel.$modelValue = oldValue;
        //    }
        //    else
        //    {
        //        //ngModel.$modelValue=newValue;
        //        var formatted = getFormattedTime(date);
        //        ngModel.$setViewValue(formatted);
        //        //ngModel.$render();
        //    }
        //});

        ngModel.$parsers.push(function (viewValue) {
            if (viewValue.length > 4) {
                var date = ngModel.$modelValue;
                var shaa = viewValue.split(':');
                if (typeof date == 'string')
                    date = new Date(date);

                date.setHours(shaa[0]);
                date.setMinutes(shaa[1]);

                return date.toJSON();
                //ngModel.$modelValue = date;
                //ngModel.$render();
                //ngModel.$setViewValue(fullTime.split(":")[0] + ":" + fullTime.split(":")[1]);
                //ngModel.$render();
            }
           return viewValue;
        });

        ngModel.$formatters.push(function (mv) {

            var res = "";
            if (typeof mv == 'string') {
                mv = new Date(mv)
                ngModel.$modelValue = mv;
                ngModel.$render();
                res = getFormattedTime(mv);
            }
            return res;

        });
    }

    function getFormattedTime(date)
    {
        var h = getHour(date);
        var m = getMinutes(date);
        return h + ":" + m;
    }

    function getHour(date)
    {
        return date.getHours();
    }

    function getMinutes(date) {
        return date.getMinutes();
    }
}]);