angular
    .module('squaresApp')
    .controller('ListCreation', function ($uibModalInstance, PointsListsService) {
        this.pointsList = {};

        this.close = function () {
            $uibModalInstance.dismiss();
        };

        this.createList = function () {
            PointsListsService.createPointsList(this.pointsList)
                .then((response) => {
                    $uibModalInstance.close();
                }, (response) => {
                    this.errorMessages = response.data;
                });
        }

    })
