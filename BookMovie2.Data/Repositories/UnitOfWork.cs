using BookMovie2.Data.Contracts;
using BookMovie2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IUserRepository _user;


        public Lazy<GenericRepository<Theatre>> theatre;
        public Lazy<GenericRepository<TheatreByLocationResponse>> theatreByLocationResponse;
        public Lazy<GenericRepository<TheatreByMovieResponse>> theatreByMovieResponse;
        public Lazy<GenericRepository<Movie>> movie;
        public Lazy<GenericRepository<Audi>> audi;
        public Lazy<GenericRepository<Seat>> seat;
        public Lazy<GenericRepository<Booking>> booking;
        public Lazy<GenericRepository<Category>> category;




        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            theatre = new Lazy<GenericRepository<Theatre>>(() => new GenericRepository<Theatre>(_context));
            theatreByLocationResponse = new Lazy<GenericRepository<TheatreByLocationResponse>>(() => new GenericRepository<TheatreByLocationResponse>(_context));
            theatreByMovieResponse = new Lazy<GenericRepository<TheatreByMovieResponse>>(() => new GenericRepository<TheatreByMovieResponse>(_context));
            movie = new Lazy<GenericRepository<Movie>>(() => new GenericRepository<Movie>(_context));
            audi = new Lazy<GenericRepository<Audi>>(() => new GenericRepository<Audi>(_context));
            seat = new Lazy<GenericRepository<Seat>>(() => new GenericRepository<Seat>(_context));
            booking = new Lazy<GenericRepository<Booking>>(() => new GenericRepository<Booking>(_context));
            category = new Lazy<GenericRepository<Category>>(() => new GenericRepository<Category>(_context));
        }


        public GenericRepository<Theatre> Theatre => theatre.Value;  //actual initilization happens here when value is accessed
                                                                     //gets the lazily initialized value of the current Lazy<T> instance.
        public GenericRepository<TheatreByLocationResponse> TheatreByLocationResponse => theatreByLocationResponse.Value;

        public GenericRepository<TheatreByMovieResponse> TheatreByMovieResponse => theatreByMovieResponse.Value;
        public GenericRepository<Movie> Movie => movie.Value;
        public GenericRepository<Audi> Audi => audi.Value;
        public GenericRepository<Seat> Seat => seat.Value;
        public GenericRepository<Booking> Booking => booking.Value;
        public GenericRepository<Category> Category => category.Value;












        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }





        //public IAudiRepository Audi
        //{
        //    get
        //    {
        //        if (_audi == null)
        //        {
        //            _audi = new AudiRepository(_context);
        //        }
        //        return _audi;
        //    }

        //}

        //public GenericRepository<Theatre> Theatre
        //{
        //    get
        //    {
        //        return new GenericRepository<Theatre>(_context);
        //    }
        //}


        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
