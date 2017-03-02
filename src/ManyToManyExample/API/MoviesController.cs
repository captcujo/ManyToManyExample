using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToManyExample.Data;
using ManyToManyExample.ViewModels;
using ManyToManyExample.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToManyExample.API
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            this._db = db;
        }
        
        [HttpGet("{id}")]
        public MovieWithActors Get(int id)
        {
            MovieWithActors movieWithActors = (from m in _db.Movies
                                               where m.Id == id
                                               select new MovieWithActors
                                               {
                                                   Id = m.Id,
                                                   Title = m.Title,
                                                   Director = m.Director,
                                                   Actors = (from ma in _db.MovieActors
                                                             where ma.MovieId == m.Id
                                                             select ma.Actor).ToList()
                                               }).FirstOrDefault();
            return movieWithActors;
        }

        [HttpGet]
        public List<Movie> Get()
        {
            List<Movie> movies = (from m in _db.Movies
                                  select new Movie
                                  {
                                      Id = m.Id,
                                      Title = m.Title,
                                      Director = m.Director,
                                      MovieActors = m.MovieActors
                                  }).ToList();
            return movies;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MovieWithActors movie)
        {
            if(movie == null)
            {
                return BadRequest();
            }
            else if(movie.Id == 0)
            {
                _db.Add(new Movie {Title = movie.Title,Director = movie.Director });

                _db.SaveChanges();

                return Ok();
            }
            else
            {
                foreach(Actor actor in movie.Actors)
                {
                    _db.MovieActors.Add(new MovieActor { MovieId = movie.Id, ActorId = actor.Id});
                }
                              
                _db.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Movie movieToDelete = (from m in _db.Movies
                                   where m.Id == id
                                   select m).FirstOrDefault();

            _db.Remove(movieToDelete);
            _db.SaveChanges();

            return Ok();
        }
    }
}
