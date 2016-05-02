module modules.common {

    interface dateTimeScope extends ng.IScope {
        dateOrig: string;
        formattedDate: string;
        hourChanged();
    }

  

    class dateTimeFormatedDirective implements ng.IDirective {

        public restrict: string;
        public templateUrl: string;
        public scope: any;

        constructor() {
            this.restrict = "E";
            this.templateUrl = "common/directives/dateTime/dateTimeFormatedDirective.tpl.html";
            this.scope = {
                dateOrig: "=",
            };
        }

        public link($scope: dateTimeScope, element: JQuery, attributes: ng.IAttributes) {
            

        }

        public compile(templateElement: ng.IAugmentedJQuery, templateAttributes: ng.IAttributes, transclude: ng.ITranscludeFunction): ng.IDirectivePrePost {
            return null;
        }
        

        static Instance = (): ng.IDirective=>{
            return new dateTimeFormatedDirective();
        }
    }

    angular.module("modules.common").directive("dateTimeFormatedDirective", dateTimeFormatedDirective.Instance);
}