using AutoMapper;
using BookMovie2.Data;
using BookMovie2.Data.Contracts;
using BookMovie2.Models;
using BookMovie2.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie = BookMovie2.Models.Movie;

namespace BookMovie2.Service.Providers
{
    public class MovieService : IMovieService
    {
       
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repository;
        public MovieService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
          
            _repository = unitOfWork;
        }


        public Movie GetMovieById(string id)
        {
            try
            {
                Data.Movie movie = _repository.Movie.FirstOrDefault(x => x.MovieId == id);
                if (movie != null)
                    return _mapper.Map<Movie>(movie);
                return new Movie();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Movie> GetMovies()
        {
            try
            {
                IEnumerable<Data.Movie> movies = _repository.Movie.GetAll();
                return _mapper.Map<List<Movie>>(movies);
            }
            catch (Exception) { throw; }

        }

        //public IEnumerable<Movie> GetMovies()
        //{
        //    try
        //    {
        //        List<Data.Movie> movies = _context.Movies.ToList();
        //        return _mapper.Map<List<Movie>>(movies);
        //    }
        //    catch (Exception) { throw; }

        //}




        public void AddMovie(Movie movie)
        {
            try
            {
                _repository.Movie.Add(_mapper.Map<Data.Movie>(movie));
                _repository.Save();
            }
            catch (Exception) { throw; }

        }

        //public void AddMovie(Movie movie)
        //{
        //    try
        //    {
        //        _context.Movies.Add(_mapper.Map<Data.Movie>(movie));
        //        _context.SaveChanges();
        //    }
        //    catch (Exception) { throw; }

        //}

        //public string UpdateMovie(Movie movie)
        //{
        //    try
        //    {
        //        Data.Movie? tempMovie = _context.Movies.SingleOrDefault(x => x.MovieId == movie.MovieId);

        //        if (tempMovie != null)
        //        {
        //            tempMovie.Genre = movie.Genre;
        //            tempMovie.MovieName = movie.MovieName;
        //            _context.SaveChanges();
        //            return "Movie has been updated";

        //        }
        //        return "Movie not found";
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        public string UpdateMovie(Movie movie)
        {
            try
            {
                Data.Movie? tempMovie = _repository.Movie.FirstOrDefault(u => u.MovieId == movie.MovieId);
                if (tempMovie != null)
                {
                    tempMovie.Genre = movie.Genre;
                    tempMovie.MovieName = movie.MovieName;
                    _repository.Save();
                    return "Movie has been updated";
                }
                return "Movie not found";
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public string RemoveMovie(string id)
        //{
        //    try
        //    {
        //        Data.Movie? movie = _context.Movies.SingleOrDefault(x => x.MovieId == id);
        //        if (movie != null)
        //        {
        //            _context.Movies.Remove(movie);
        //            _context.SaveChanges();
        //            return "Movie deleted";
        //        }
        //        return "Movie not found";
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public string RemoveMovie(string id)
        {
            try
            {
                _repository.Movie.Remove(id);
                _repository.Save();

                return "Movie deleted";
            }
            catch (Exception)
            {
                throw;
            }

        }



    }
}
