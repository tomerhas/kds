module modules.common {

    interface IDateTimeScope extends ng.IScope {
        date: Date;
        formattedDate: string;
    }

    //class dateTimeController {
    //    constructor(
    //        private $scope: IDateTimeScope,
    //        private $log: ng.ILogService) {
    //        this.$log.debug("dateTimeController: current date" + $scope.date);
    //    }
    //}

    angular.module("modules.common").directive("dateTimeDirective",function() {
        return {
            restrict: "E",
            require: "ngModel",
            scope: {
                date: '='
            },
            templateUrl: "common/componenets/dateTime/dateTimeDirective.tpl.html",
            link: function (scope: IDateTimeScope, elem: JQuery, attributes: any, ngModel: ng.INgModelController) {
                var myScope = scope;
                //You can access myFactory like this.
                elem.bind("change", function (newVal) {
                    var updatedTime = elem.text;

                });

                ngModel.$parsers.push(function (viewValue) {
                    if (viewValue.length > 4) {
                        
                    }
                    return viewValue;
                });

                attributes.origVal = scope.date;
                scope.formattedDate = getFormattedTime(scope.date);

                function getFormattedTime(date) {
                    var h = getHour(date);
                    var m = getMinutes(date);
                    return h + ":" + m;
                }

                function getHour(date) {
                    if(date)
                        return date.getHours();
                    return null;
                }

                function getMinutes(date) {
                    if (date)
                        return date.getMinutes();
                    return null;
                }
            }
        }
    });
}