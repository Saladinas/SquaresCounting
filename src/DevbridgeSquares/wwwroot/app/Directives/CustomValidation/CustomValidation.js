angular
    .module('squaresApp').directive('customValidation', function () {
        return {
            restrict: 'E',
            replace: 'true',
            scope: {
                errors: '='
            },
            templateUrl: 'app/Directives/CustomValidation/CustomValidation.html',
            link: function (scope, elem, attrs) {
                scope.$watch('errors', function (newErrors, oldErrors) {
                    scope.displayingErrors = [];
                    if (typeof newErrors === 'string') {
                        scope.displayingErrors.push(newErrors);
                    } else {
                        angular.forEach(newErrors, function (errorsFamily, errorsFamilyKey) {
                            angular.forEach(errorsFamily, function (errorValue, errorKey) {
                                scope.displayingErrors.push(errorValue);
                            })
                        });
                    }
                }, true);
            }
        }
    });