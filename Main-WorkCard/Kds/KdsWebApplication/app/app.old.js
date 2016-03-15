var mainApp = angular.module('mainApp', []);
var workCardApp = angular.module('workCard', ['ngRoute', 'mainApp', 'angucomplete-alt', 'kendo.directives']);
//.config(function ($routeProvider, $locationProvider) {
//    $locationProvider.html5Mode({
//        enabled: true,
//        requireBase: false
//    });

//    $routeProvider.
//      when("/",
//       {
//           template:"jj"
//       })
//        .otherwise({

//           redirectTo: '/'

//       });
  
//});
String.prototype.padZero = function (len, c) {
    var s = '', c = c || '0', len = (len || 2) - this.length;
    while (s.length < len) s += c;
    return s + this;
}
Number.prototype.padZero = function (len, c) {
    return String(this).padZero(len, c);
}

//function focusTextById(id) {
//    $("#" + id).select();
//}


//workCardApp.directive('uiMask', function () {
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
//});

//workCardApp.directive('timeMask', function () {
//    return {
//        restrict: 'E',
//        replace: true,
//        require: 'ngModel',
//        scope: {
//            'time': '@'
//        },
//        template: [
//          '<input ng-model="sidur.FullShatHatchala.Value" ui-mask="99:99" placeholder="hh:mm" class="pointer input-small" />'
//        ].join(''),
//        link: function (scope, element, attrs, ngModelController) {
//          //  console.log(scope.time);

//            ngModelController.$formatters.push(function (data) {
//                console.log(moment(data, moment.ISO_8601).format('HH:mm'));
//                //convert data from model format to view format   
//                return moment(data, moment.ISO_8601).format('HH:mm'); //converted
//            });
//        }
//    };
//});

//app.directive('ngAutocomplete', function ($parse) {
//      return {

//          scope: {
//              details: '=',
//              ngAutocomplete: '=',
//              options: '='
//          },

//          link: function (scope, element, attrs, model) {

//              //options for autocomplete
//              var opts

//              //convert options provided to opts
//              var initOpts = function () {
//                  opts = {}
//                  if (scope.options) {
//                      if (scope.options.types) {
//                          opts.types = []
//                          opts.types.push(scope.options.types)
//                      }
//                      if (scope.options.bounds) {
//                          opts.bounds = scope.options.bounds
//                      }
//                      if (scope.options.country) {
//                          opts.componentRestrictions = {
//                              country: scope.options.country
//                          }
//                      }
//                  }
//              }
//              initOpts()

//              //create new autocomplete
//              //reinitializes on every change of the options provided
//              var newAutocomplete = function () {
//                  scope.gPlace = new google.maps.places.Autocomplete(element[0], opts);
//                  google.maps.event.addListener(scope.gPlace, 'place_changed', function () {
//                      scope.$apply(function () {
//                          //              if (scope.details) {
//                          scope.details = scope.gPlace.getPlace();
//                          //              }
//                          scope.ngAutocomplete = element.val();
//                      });
//                  })
//              }
//              newAutocomplete()

//              //watch options provided to directive
//              scope.watchOptions = function () {
//                  return scope.options
//              };
//              scope.$watch(scope.watchOptions, function () {
//                  initOpts()
//                  newAutocomplete()
//                  element[0].value = '';
//                  scope.ngAutocomplete = element.val();
//              }, true);
//          }
//      };
//});
/*
app.directive('autoComplete', function ($timeout) {
    return function (scope, iElement, iAttrs) {
        iElement.autocomplete({
            source: scope[iAttrs.uiItems],
            select: function () {
                $timeout(function () {
                    iElement.trigger('input');
                }, 0);
            }
        });
    };
});*/