angular
    .module('squaresApp')
    .controller('SquaresCtrl', function (pointsList, squares, PointsService) {
        this.pointsList = pointsList;
        this.squares = squares.items;
        this.itemsPerPage = 10;
        this.itemsPerPageSelections = ['5', '10', '20', '50'];

        this.changePaging = (itemsPerPage) => {
            PointsService.getSquares(this.pointsList.id, 1, itemsPerPage)
            .then((response) => {
                this.squares = response.items;
                this.pagingParameters = response.pager;
            }, (response) => {
                this.errorMessages = response.data;
            });
        };

        this.pageChanged = () => {
            PointsService.getSquares(this.pointsList.id, this.pagingParameters.currentPage, this.itemsPerPage, this.sortType, this.sortReverse)
            .then((response) => {
                this.squares = response.items;
                this.pagingParameters = response.pager;
            }, (response) => {
                this.errorMessages = response.data;
            });
        };
    });
