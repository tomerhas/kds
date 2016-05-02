module modules.common {

    interface dateTimeScope extends ng.IScope {
       // dateOrig: Date;
        dateOrig: string;
        formattedDate: string;
        hourChanged();
    }

    class dateTimeDisplayController {

        isFocused:boolean;

        constructor(private $scope: dateTimeScope) {
            this.$scope.hourChanged = this.hourChanged;
            //On init - set the formatted hour prop to be diaplyed in UI
            this.$scope.formattedDate = this.getFormattedTime(this.$scope.dateOrig);
       }
       
        //Will be called on blur by view
        hourChanged = () => {
            console.log("hour change");
            var hr = this.getHoursInFormattedDate();
            var sc = this.getMinutesInFormattedDate();
            if (!(Number(hr)) || !(Number(sc)) || +hr > 23 || +sc > 59) {
                //revert to original hour
                this.$scope.formattedDate = this.getFormattedTime(this.$scope.dateOrig.toString());
                //alert("שעה לא תקינה");
            }
            else     
                this.$scope.dateOrig = this.$scope.dateOrig.toString().split('T')[0] + "T" + hr + ":" + sc + ":00";
        }


        getHoursInFormattedDate = (): string => {
            if (this.$scope.formattedDate) {
                var hm = this.$scope.formattedDate.substring(0,2);
                return hm;
            }
            else {
                return "";
            }
        }
        
        getMinutesInFormattedDate = (): string => {
            if (this.$scope.formattedDate) {
                var minutes: string = this.$scope.formattedDate.substring(2, 4);
                return minutes;
            }
            else {
                return "";
            }
        }
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
            },
            controller: dateTimeDisplayController
        }
    });
}