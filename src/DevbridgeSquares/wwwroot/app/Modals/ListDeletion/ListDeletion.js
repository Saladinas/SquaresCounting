angular
    .module('squaresApp')
    .controller('ListDeletion', function ($uibModalInstance, PointsListsService, list) {
        this.list = list;

        this.close = function () {
            $uibModalInstance.dismiss();
        };

        this.delete = function () {
            PointsListsService.deletePointsList(this.list.id)
                .then((response) => {
                    $uibModalInstance.close();
                }, (response) => {
                });
        }

    })
