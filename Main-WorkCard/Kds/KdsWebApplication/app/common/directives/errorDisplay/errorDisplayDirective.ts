module modules.common{

    interface errorScope extends ng.IScope {
        errorMessage: string;
        displayError: boolean;
        
    }

    class errorDisplayController {
        
        constructor(private $scope: errorScope) {   
            
        } 
    }

    angular.module("modules.common").directive("errorDisplayDirective", function () {
        return {
            restrict: "E",
            templateUrl: "common/directives/errorDisplay/errorDisplayDirective.tpl.html",
            scope: {
                errorMessage: "=",
                displayError:"="
            }
            ,controller: errorDisplayController
        }
    });
}