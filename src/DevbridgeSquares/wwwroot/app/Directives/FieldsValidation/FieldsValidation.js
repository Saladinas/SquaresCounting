angular
    .module('squaresApp').directive('fieldsValidation', function () {
        return {
            restrict: 'E',
            replace: 'true',
            scope: {
                errors: '=',
                field: '@'
            },
            templateUrl: 'app/Directives/FieldsValidation/FieldsValidation.html',
            link: function (scope, elem, attrs) {
                formGroupElement = elem.parent().parent();
                scope.$watch('errors', function (newErrors, oldErrors) {
                    formGroupElement.removeClass('has-error');
                    scope.displayingErrors = [];
                    angular.forEach(newErrors, function (errorsFamily, errorsFamilyKey) {
                        if (errorsFamilyKey == scope.field) {
                            angular.forEach(errorsFamily, function (errorValue, errorKey) {
                                scope.displayingErrors.push(errorValue);
                                formGroupElement.addClass('has-error');
                            })
                        };
                    });
                });
            }
        }
    });