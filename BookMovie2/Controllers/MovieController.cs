using AutoMapper;
using BookMovie2.Data.Contracts;
using BookMovie2.Models;
using BookMovie2.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMovie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController 
    {
        private readonly IMovieService _movieService;
       
    
        public MovieController( IMovieService movieService)
        {
            _movieService = movieService;
        
        }

        [HttpGet ]
        public IEnumerable<Movie> Get()
        {
            try
            {
                return _movieService.GetMovies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
       
        }
        //[HttpGet]
        //public IEnumerable<Movie> Get()
        //{
        //    try
        //    {
        //        return _movieService.GetMovies();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);

        //    }

        //}

        [HttpGet ("GetMovieById")]
        public Movie GetById(string id)
        {
            try 
            {
                if(id != null)
                {
                    return _movieService.GetMovieById(id);
                }
                return new Movie();
            }
            catch(Exception ex )
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPost]
        public void Post(Movie movie)
        {
            try
            {
                if(movie != null)
                {
                    _movieService.AddMovie(movie);

                }
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
         

        }
        [HttpPut]
        public string Put(Movie movie)
        {
            try
            {
                if(movie != null)
                {
                    return _movieService.UpdateMovie(movie);
                }
                return new string(" Cannot modify the movie");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
       
        }
        [HttpDelete]
        public string Delete(string id)
        {
            try
            {
                if(id != null)
                {
                    return _movieService.RemoveMovie(id);

                }
                return new string(" Cannot be deleted! ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }


    }
}


