angular
    .module('squaresApp')
    .controller('ListsCtrl', function (lists, $uibModal, $state, PointsListsService) {
        this.lists = lists;

        this.createList = function () {
            var modalInstance = $uibModal.open({
                templateUrl: 'app/Modals/ListCreation/ListCreation.html',
                controller: 'ListCreation as Ctrl'
            });
            modalInstance.result.then(function () {
                $state.go('lists', {}, { reload: true });
            }, function () {
            });
        };

        this.removeList = function (list) {
            var modalInstance = $uibModal.open({
                templateUrl: 'app/Modals/ListDeletion/ListDeletion.html',
                controller: 'ListDeletion as Ctrl',
                resolve: {
                    list: function () {
                        return list;
                    }
                }
            });
            modalInstance.result.then(function () {
                $state.go('lists', {}, { reload: true });
            }, function () {
            });
        };

    });
