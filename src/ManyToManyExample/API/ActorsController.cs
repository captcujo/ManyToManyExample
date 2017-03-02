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
    public class ActorsController : Controller
    {
        private ApplicationDbContext _db;

        public ActorsController(ApplicationDbContext db)
        {
            this._db = db;
        }

        [HttpGet("{id}")]
        public ActorWithMovies Get(int id)
        {
            ActorWithMovies actorWithMovies = (from a in _db.Actors
                                               where a.Id == id
                                               select new ActorWithMovies
                                               {
                                                   Id = a.Id,
                                                   FirstName = a.FirstName,
                                                   LastName = a.LastName,
                                                   Movies = (from ma in _db.MovieActors
                                                             where ma.ActorId == a.Id
                                                             select ma.Movie).ToList()
                                               }).FirstOrDefault();
            return actorWithMovies;
        }

        [HttpGet]
        public List<Actor> Get()
        {
            List<Actor> actors = (from a in _db.Actors
                                  select new Actor
                                  {
                                      Id = a.Id,
                                      FirstName = a.FirstName,
                                      LastName = a.LastName
                                  }).ToList();
            return actors;
        }
    }
}
