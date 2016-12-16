angular.module('squaresApp').service('PointsListsService', ['$http', function ($http) {

    this.getPointsLists = function () {
        return $http({
            method: 'GET',
            url: 'api/pointsLists'
        }).then((response) => {
            return response.data;
        }, (response) => {
        });
    };

    this.getPointsList = function (listId) {
        return $http({
            method: 'GET',
            url: 'api/pointsLists/' + listId
        }).then((response) => {
            return response.data;
        }, (response) => {
        });
    };


    this.createPointsList = function (pointList) {
        return $http({
            data: pointList,
            method: 'PUT',
            url: 'api/pointsLists'
        });
    };

    this.deletePointsList = function (listId) {
        return $http({
            method: 'DELETE',
            url: 'api/pointsLists/' + listId
        });
    };

    this.clearList = function (listId) {
        return $http({
            method: 'POST',
            url: 'api/pointsLists/clear/' + listId
        });
    };

    var saveByteArray = (function () {
        var a = document.createElement("a");
        document.body.appendChild(a);
        a.style = "display: none";
        return function (data, name) {
            var blob = new Blob(data, { type: "octet/stream" }),
                url = window.URL.createObjectURL(blob);
            a.href = url;
            a.download = name;
            a.click();
            window.URL.revokeObjectURL(url);
        };
    }());

    this.downloadList = function (listId) {
        return $http({
            method: 'GET',
            url: 'api/pointsLists/' + listId + '/download'
        }).then((response) => {
            saveByteArray([response.data], 'DevbridgeSquares.txt');
            return response.data;
        }, (response) => {
        });

    };

}]);