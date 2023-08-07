using BookMovie2.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        GenericRepository<Category> Category
        {
            get;
        }
        GenericRepository<Booking> Booking
        {
            get;
        }
        GenericRepository<Seat> Seat
        {
            get;
        }

        //  IMovieRepository Movie { get; }

        GenericRepository<Audi> Audi
        {
            get;
        }
        GenericRepository<Movie> Movie
            {
            get;

            }

        GenericRepository<Theatre> Theatre
        {
            get;
        } 
        GenericRepository<TheatreByLocationResponse> TheatreByLocationResponse
        {
            get;
        }
        GenericRepository<TheatreByMovieResponse> TheatreByMovieResponse
        {
            get;
        }




        //on demand repository creation

        void Save();
    }
}
