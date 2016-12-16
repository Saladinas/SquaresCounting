(function () {
    'use strict';

    var app = angular.module('squaresApp', ['ui.router', 'ui.bootstrap']);

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
           .state('home', {
               template: '<h1>Devbridge Squares task</h1>' + '<span>by Lukas Jasmontas</span>',
               url: '/'
           });

        $stateProvider
            .state('lists', {
                url: '/lists',
                templateUrl: 'app/Partials/Lists.html',
                controller: 'ListsCtrl as Ctrl',
                resolve: {
                    lists: ['PointsListsService', function (ListsService) {
                        return ListsService.getPointsLists();
                    }]
                }
            });

        $stateProvider
            .state('lists.squares', {
                url: '/:id/squares',
                templateUrl: 'app/Partials/Squares.html',
                controller: 'SquaresCtrl as Ctrl',
                resolve: {
                    pointsList: ['PointsListsService', '$stateParams', function (PointsListsService, $stateParams) {
                        return PointsListsService.getPointsList($stateParams.id);
                    }],
                    squares: ['PointsService', '$stateParams', function (PointsService, $stateParams) {
                        return PointsService.getSquares($stateParams.id, 1, 10);
                    }]
                }
            });

        $stateProvider
            .state('lists.details', {
                url: '/:id/lists',
                templateUrl: 'app/Partials/Points.html',
                controller: 'PointsCtrl as Ctrl',
                resolve: {
                    points: ['PointsService', '$stateParams', function (PointsService, $stateParams) {
                        return PointsService.getPoints($stateParams.id, 1, 10);
                    }],
                    pointsList: ['PointsListsService', '$stateParams', function (PointsListsService, $stateParams) {
                        return PointsListsService.getPointsList($stateParams.id);
                    }]
                }
            });
    });

})();