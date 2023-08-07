using BookMovie2.Data;
using BookMovie2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Contracts
{
    public interface IAudiService
    {
        IEnumerable<Models.Audi> GetAudis();
        void AddAudis(Models.Audi audi);

        IEnumerable<TheatreByMovieResponse> GetTheatersByMovieId(string id);


        Dictionary<DateTime, List<Show>> GetShows(string movieId, string theatreId);

        object BookMovie(BookingRequest bookingRequest);

        IEnumerable<Show> GetShowsByDate( DateTime date);
        IEnumerable<ShowDateTime> GetShowDatesandTimings(string movieId, string  theatreId);

        IEnumerable<Models.Movie> GetMoviesByTheatreId(string id);
        IEnumerable<Row> GetSeatRows(string  AudiId);
        string CancelBooking(int userId, Guid bookingId);

    }
}
