namespace ManyToManyExample.Controllers
{

    export class HomeController
    {
        public message = 'Hello from the home page!';

        public movies;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService)
        {
            this.$http.get('/api/movies').then((response) =>
            {
                this.movies = response.data;
            })
        }

        public deleteMovie(id:number)
        {
            this.$http.delete('/api/movies/' + id).then((response) =>
            {
                this.$state.go('home');
            })
        }
    }

    export class AddMovieController
    {
        public actors;

        public movie;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService)
        {
            this.$http.get('/api/actors').then((response) =>
            {
                this.actors = response.data;
            })
        }

        public addMovie()
        {
            this.$http.post('/api/movies/', this.movie).then((response) =>
            {
                this.$state.go('home');
            })
        }

    }

    export class AddActorsController
    {
        public actors;

        public movie;

        constructor(private $http: ng.IHttpService, private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService)
        {
            this.$http.get('/api/actors').then((response) =>
            {
                this.actors = response.data;
            })

            let movieId = this.$stateParams['id'];

            this.$http.get('/api/movies/' + movieId).then((response) =>
            {
                this.movie = response.data;
            })

        }

        public addActor(movieId:string,actorId:string)
        {
            console.log("controller.movie.movieActors.actorId = ", actorId);
           
             this.$http.post('/api/movies', this.movie).then((response) =>
            {
                this.$state.go('home');
            })
        }

    }

    export class AboutController
    {
        public message = 'Hello from the about page!';
    }

}
