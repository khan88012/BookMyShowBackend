using BookMovie2.Data;
using BookMovie2.Models;
using BookMovie2.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie = BookMovie2.Models.Movie;
using Audi = BookMovie2.Models.Audi;
using Theatre = BookMovie2.Models.Theatre;
using Microsoft.AspNetCore.Authorization;

namespace BookMovie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudiController
    {
        private readonly IAudiService _AudiService;
        public AudiController(IAudiService audiService)
        {
            _AudiService = audiService;
        }

        [HttpGet]
        public IEnumerable<Audi> Get()
        {
            try
            {
                return _AudiService.GetAudis();

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [HttpPost]
        public void Post(Audi audi)
        {
            try
            {
                if(audi != null)
                _AudiService.AddAudis(audi);

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        [Authorize]
        [HttpGet("GetTheatresByMovieId")]
        public IEnumerable<TheatreByMovieResponse> GetTheatersByMovieId(string id)
        {
            try
            {
                if(id != null) {
                     return _AudiService.GetTheatersByMovieId(id);
                }
                return new List<TheatreByMovieResponse>();

            }
            catch (Exception ex) {
                throw new Exception(ex.Message);

            }

        }



        [HttpPost("BookMovie")]
        public object BookMovie(BookingRequest bookingRequest)
        {
            try
            {
                if(bookingRequest != null)
                {
                    return _AudiService.BookMovie(bookingRequest);

                }
                return new string(" Request cannot be completed!");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        [HttpGet("GetShows")]
        public Dictionary<DateTime, List<Show>> GetShows(string movieId, string theatreId)
        {
            try
            {
                return _AudiService.GetShows(movieId, theatreId);

            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetShowDatesandTimings")]
        [Authorize]
        public IEnumerable<ShowDateTime> GetShowDates(string movieId, string theatreId)
        {
            try
            {
                return _AudiService.GetShowDatesandTimings(movieId, theatreId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetShowsByDate")]
        public IEnumerable<Show> GetShowsByDate( DateTime date)
        {
            try
            {
                return _AudiService.GetShowsByDate(date);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetMoviesByTheatreId")]
        public IEnumerable<Movie> GetMoviesByTheatreId(string id)
        {
            try
            {
                if(id != null)
                {
                    return _AudiService.GetMoviesByTheatreId(id);

                }

                return new List<Movie>();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetSeatsByAudiId")]
        public IEnumerable<Row> GetSeatRows(string audiId)
        {
            try
            {
                if(audiId != null)
                {
                    return _AudiService.GetSeatRows(audiId);

                }
                return new List<Row>();
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }

        [HttpPost("CancelBooking")]
        public string CancelBooking(int userId, Guid bookingId)
        {
           return _AudiService.CancelBooking(userId, bookingId);
        }
    }
}
