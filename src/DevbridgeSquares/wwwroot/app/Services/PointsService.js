angular.module('squaresApp').service('PointsService', ['$http', function ($http) {

    this.getPoints = function (listId, page, pageSize, sortType, sortReverse) {
        endOfUrl = '';
        if (sortType !== undefined && sortReverse !== undefined) {
            endOfUrl = '&sortType=' + sortType + '&sortReverse=' + sortReverse;
        }
        return $http({
            method: 'GET',
            url: 'api/pointsLists/' + listId + '/points?page=' + page + '&pageSize=' + pageSize + endOfUrl
        }).then((response) => {
            return response.data;
        }, (response) => {
        });
    };

    this.addPoint = function (listId, point) {
        return $http({
            method: 'POST',
            data: point,
            url: 'api/pointsLists/' + listId + '/points'
        });
    };

    this.deletePoint = function (listId, point) {
        return $http({
            method: 'PUT',
            data: point,
            url: 'api/pointsLists/' + listId + '/points'
        });
    };

    this.uploadPoints = function (listId, file) {
        return $http({
            method: 'POST',
            data: file,
            url: 'api/pointsLists/' + listId + '/upload'
        });
    };

    this.getSquares = function (listId) {
        return $http({
            method: 'GET',
            url: 'api/pointsLists/' + listId + '/squares'
        });
    };

    this.getSquares = function (listId, page, pageSize) {
        return $http({
            method: 'GET',
            url: 'api/pointsLists/' + listId + '/squares?page=' + page + '&pageSize=' + pageSize + endOfUrl
        }).then((response) => {
            return response.data;
        }, (response) => {
        });
    };

}]);