using BookMovie2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Contracts
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovieById(string id);
        void AddMovie(Movie movie);
        string UpdateMovie(Movie movie);

        string RemoveMovie(string id);

    }
}
