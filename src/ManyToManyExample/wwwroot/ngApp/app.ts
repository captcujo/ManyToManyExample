namespace ManyToManyExample {

    angular.module('ManyToManyExample', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ManyToManyExample.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: ManyToManyExample.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state('addMovie', {
                url: '/addMovie',
                templateUrl: '/ngApp/views/addMovie.html',
                controller: ManyToManyExample.Controllers.AddMovieController,
                controllerAs: 'controller'
            })
            .state('addActors', {
                url: '/addActors/:id',
                templateUrl: '/ngApp/views/addActors.html',
                controller: ManyToManyExample.Controllers.AddActorsController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    

}
