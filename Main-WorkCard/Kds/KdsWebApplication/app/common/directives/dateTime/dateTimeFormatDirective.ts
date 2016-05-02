module modules.common {

    interface dateTimeScope extends ng.IScope {
       // dateOrig: Date;
        dateOrig: string;
        formattedDate: string;
        hourChanged();
    }

    class dateTimeDisplayController {

        constructor(private $scope: dateTimeScope) {
          //  (<any>$("#timeInputDirective")).mask("99:99");
            this.registerToDateChange();
            this.$scope.hourChanged = this.hourChanged;
        }

        registerToDateChange = () => {
            this.$scope.$watch(() => { return this.$scope.dateOrig }, (newVal: any, oldVal: any) => {
                //var minutesInDate = this.getMinutes(newVal);
                //var minutesInFormatted = this.getMinutesInFormattedDate();
                //var hoursInDate = this.getHour(newVal);
                //var hoursInFormatted = this.getHoursInFormattedDate();
                //if ((minutesInDate.toString() !== minutesInFormatted.toString()) || (hoursInDate.toString() !== hoursInFormatted.toString())) {
                    this.$scope.formattedDate = this.getFormattedTime(newVal);
                //}
            });
        }

        hourChanged = () => {
            ////console.log("hour changed to " + this.$scope.formattedDate);
            ////this.$scope.dateOrig.setHours(this.getHoursInFormattedDate());
            ////this.$scope.dateOrig.setMinutes(this.getMinutesInFormattedDate());
            ////this.$scope.dateOrig = new Date(this.$scope.dateOrig.getTime());
            ////console.log("date after change: " + this.$scope.dateOrig);
            var hr = this.getHoursInFormattedDate();
            var sc = this.getMinutesInFormattedDate();
            if (!(Number(hr)) || !(Number(sc))  || +hr > 23 || +sc > 59) {
                this.$scope.formattedDate = this.getFormattedTime(this.$scope.dateOrig.toString());
                alert("שעה לא תקינה");
            }
            else     
                this.$scope.dateOrig = this.$scope.dateOrig.toString().split('T')[0] + "T" + hr + ":" + sc + ":00";
        }

        getHoursInFormattedDate = (): string => {
            if (this.$scope.formattedDate) {
                var hm = this.$scope.formattedDate.split(':');
                var minutes: string = hm[0]
                return minutes;
            }
            else {
                return "";
            }
        }

        getMinutesInFormattedDate = (): string => {
            if (this.$scope.formattedDate) {
                var hm = this.$scope.formattedDate.split(':');
                var minutes: string = hm[1]
                return minutes;
            }
            else {
                return "";
            }
        }

        //getHoursInFormattedDate = (): number => {
        //    if (this.$scope.formattedDate) {
        //        var hm = this.$scope.formattedDate.split(':');
        //        var minutes: number = +hm[0]
        //        return minutes;
        //    }
        //    else {
        //        return -1;
        //    }
        //}

        //getMinutesInFormattedDate = (): number => {
        //    if (this.$scope.formattedDate){
        //        var hm = this.$scope.formattedDate.split(':');
        //        var minutes: number = +hm[1]
        //        return minutes;
        //    }
        //    else{
        //        return -1;
        //    }
        //}

        getFormattedTime = (date): string=> {
            var time = date.split('T')[1].split(':');
            var h = time[0];
            var m = GeneralFunctions.padLeft(time[1], 2, "0");
          //   var h = GeneralFunctions.padLeft(this.getHour(date).toString(), 2, "0");// this.getHour(date); //
          //   var m = GeneralFunctions.padLeft(this.getMinutes(date).toString(), 2, "0");// this.getMinutes(date);//
            return h + ":" + m;
        }

        getHour=(date)=> {
            return date.getHours();
        }

        getMinutes=(date)=> {
            return date.getMinutes();
        }
    }

    angular.module("modules.common").directive("dateTimeFormatDirective", function () {
        return {
            restrict: "E",
            templateUrl: "common/directives/dateTime/dateTimeFormatDirective.tpl.html",
            scope: {
                dateOrig: "=",
            }
            , controller: dateTimeDisplayController
        }
    });
}