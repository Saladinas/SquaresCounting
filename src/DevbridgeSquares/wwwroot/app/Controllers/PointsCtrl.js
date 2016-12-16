angular
    .module('squaresApp')
    .controller('PointsCtrl', function (points, pointsList, $uibModal, PointsService, $state, PointsListsService, $scope) {
        this.pointsList = pointsList;
        this.points = points.items;
        this.pagingParameters = points.pager;
        this.itemsPerPage = 10;
        this.itemsPerPageSelections = ['5', '10', '20', '50'];
        this.newPoint = {};
        this.errorMessages = {};
        this.sortReverse = false;

        this.addPoint = () => {
            if (this.newPoint.x === null || this.newPoint.y === null) {
                this.errorMessages.coordinates = [];
                this.errorMessages.coordinates.push("At least one coordinate value is empty.");
                return this.errorMessages;
            }
            PointsService.addPoint(pointsList.id, this.newPoint)
                .then((response) => {
                    this.pageChanged(false);
                }, (response) => {
                    this.errorMessages = response.data;
                });
        };

        this.clearList = () => {
            PointsListsService.clearList(pointsList.id)
                .then((response) => {
                    this.pageChanged(false);
                }, (response) => {
                    this.errorMessages = response.data;
                });
        };

        this.deletePoint = (point) => {
            PointsService.deletePoint(this.pointsList.id, point)
                .then((response) => {
                    this.pageChanged(false);
                }, (response) => {
                    this.errorMessages = response.data;
                });
        };

        this.pageChanged = (showErrorMessage) => {
            PointsService.getPoints(this.pointsList.id, this.pagingParameters.currentPage, this.itemsPerPage, this.sortType, this.sortReverse)
            .then((response) => {
                if (!response.items.length && response.pager.currentPage > 1) {
                    this.pagingParameters.currentPage--;
                    this.pageChanged(false);
                }
                this.points = response.items;
                this.pagingParameters = response.pager;
                if (!showErrorMessage) {
                    this.errorMessages = {};
                }
                this.newPoint = {};
            }, (response) => {
                this.errorMessages = response.data;
            });
        };

        this.changePaging = (itemsPerPage) => {
            PointsService.getPoints(this.pointsList.id, 1, itemsPerPage)
            .then((response) => {
                this.points = response.items;
                this.pagingParameters = response.pager;
            }, (response) => {
                this.errorMessages = response.data;
            });
        };

        this.sortChanged = function (x) {
            if (this.sortType !== x) {
                this.sortReverse = false;
            }
            this.sortType = x;
            this.sortReverse = !this.sortReverse;
            this.pageChanged(false);
        };

        this.downloadList = function () {
            PointsListsService.downloadList(this.pointsList.id)
            .then((response) => {
            }, (response) => {
                this.errorMessages = response.data;
            });
        };

        $scope.$watch('Ctrl.file', (newFile, oldFIle) => {
            if (angular.isDefined(newFile)) {
                this.sendingFile = {};
                this.sendingFile.base64File = newFile;
                PointsService.uploadPoints(this.pointsList.id, this.sendingFile)
                    .then((response) => {
                        this.pageChanged(false);
                    }, (response) => {
                        this.errorMessages = response.data;
                        this.pageChanged(true);
                    });
            }
        });
    });