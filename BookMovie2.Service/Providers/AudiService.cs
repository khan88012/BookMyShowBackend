using AutoMapper;
using BookMovie2.Data;
using BookMovie2.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Movie = BookMovie2.Models.Movie;
using Audi = BookMovie2.Models.Audi;
using Theatre = BookMovie2.Models.Theatre;
using BookMovie2.Service.Contracts;
using System.Security.Cryptography;
using BookMovie2.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookMovie2.Service.Providers
{
    public class AudiService : IAudiService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _repository;

        public AudiService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork;
        }


        //public void AddAudis(Audi audi)
        //{
        //    try
        //    {

        //        _context.Audis.Add(_mapper.Map<Data.Audi>(audi));
        //        _context.SaveChanges();
        //    }
        //    catch (Exception) { throw; }

        //}
        public void AddAudis(Audi audi)
        {
            try
            {
                _repository.Audi.Add(_mapper.Map<Data.Audi>(audi));
                _repository.Save();
           
            }
            catch (Exception) { throw; }

        }





        //public IEnumerable<Audi> GetAudis()
        //{
        //    try
        //    {
        //        List<Data.Audi> audis = _context.Audis.ToList();

        //        return _mapper.Map<List<Audi>>(audis);
        //    }
        //    catch (Exception) { throw; }
        //}
        public IEnumerable<Audi> GetAudis()
        {
            try
            {
                return _mapper.Map<List<Audi>>(_repository.Audi.GetAll().ToList());
            }
            catch (Exception) { throw; }
        }



        //public IEnumerable<Theatre> GetTheatersByMovieId(string id)

        //{
        //    try
        //    {
        //        List<Theatre> theatres = new List<Theatre>();
        //        List<string> theatreIds = _context.Audis.Where(x => x.MovieId == id).Select(u => u.TheatreId).Distinct().ToList();
        //        foreach (string theatre in theatreIds)
        //        {
        //            Data.Theatre tempTheatre = _context.Theatres.SingleOrDefault(x => x.TheatreId == theatre);

        //            if (tempTheatre != null)
        //                theatres.Add(
        //                    new Theatre()
        //                    {
        //                        TheatreId = tempTheatre.TheatreId,
        //                        TheatreName = tempTheatre.TheatreName,
        //                        LocationId = tempTheatre.LocationId
        //                    });

        //        }
        //        return theatres;
        //    }
        //    catch (Exception) { throw; }

        //}

        public IEnumerable<TheatreByMovieResponse> GetTheatersByMovieId(string id)

        {
            try
            {
                return _repository.TheatreByMovieResponse.Get(x => x.MovieId == id).ToList();
            }
            catch (Exception) { throw; }

        }




        public object BookMovie(BookingRequest bookingRequest)
        {
            bool seatAvailability = true;
            int price = 0;
            try
            {
                List<Row> rows = new List<Row>();
                string? audiId = _repository.Audi.Get(x => x.MovieId == bookingRequest.MovieId
                                                            && x.TheatreId == bookingRequest.TheatreId
                                                            && x.ShowDate == bookingRequest.date
                                                            && x.TimeSlot == bookingRequest.timeSlot).Select(u => u.Id).FirstOrDefault();
                if (audiId != null)
                {// what if seats are not there start
                     List<Data.Seat> seatList = _repository.Seat.Get(x => x.AudiId == audiId).ToList();
                    foreach(AudiRow selectedSeat in bookingRequest.SelectedSeats)
                    {
                       seatAvailability = CheckSeatAvailability(selectedSeat , seatList);
                    }
                    // what if seats are not there end
                    if (seatAvailability)
                    {
                        bookingRequest.SelectedSeats.ForEach( s =>
                        {
                            rows.Add(GetRows(audiId, s.Row));
                        });
                        bookingRequest.SelectedSeats.ForEach(selectedSeats =>
                        {
                            Row row = rows.SingleOrDefault(r => r.RowId == selectedSeats.Row);
                            if (row != null)
                            {
                                row.Seats.ForEach(s =>
                                {
                                    if (selectedSeats.SeatNOs.Any(x => x == s.SeatNo))
                                    {
                                       price+= BookSeat(audiId, row.RowId, s.SeatNo, s.CategorId);
                                    };
                                });
                            }
                        });
                        if(price>0)
                        {
                            _repository.Booking.Add(new Data.Booking()
                            {
                                BookingId= Guid.NewGuid(),
                                UserId = bookingRequest.UserId,
                                MovieId = bookingRequest.MovieId,
                                ShowDate = bookingRequest.date,
                                TimeSlot = bookingRequest.timeSlot,
                                TheatreId = bookingRequest.TheatreId,
                                AudiId = bookingRequest.AudiId,
                                BookedSeats = JsonSerializer.Serialize(bookingRequest.SelectedSeats).ToString(),
                                Status = "Booked",
                                BookingDate = DateTime.Now,
                                Price = price,
                            });
                            _repository.Save();
                            return new { response = "Seats booked." };
                        }


                    }
                   
                }

                return new { response = "Seats not available." };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //public IEnumerable<Row> GetSeatRows(string audiId)
        //{
        //    try
        //    {
        //        List<Row> rows = new();

        //        IEnumerable<IGrouping<string, Data.Seat>> seatLists = _context.Seats.Where(x => x.AudiId == audiId).GroupBy(i => i.Row).ToList();

        //        foreach (IGrouping<string, Data.Seat> seatList in seatLists)
        //        {
        //            List<SeatInfo> seats = seatList.Where(x => x.Row == seatList.Key).Select(u => new SeatInfo { SeatNo = u.SeatNo, IsBooked = u.IsBooked }).ToList();
        //            rows.Add(new Row()
        //            {
        //                RowId = seatList.Key,
        //                Seats = seats
        //            });
        //        }

        //        return rows;
        //    }
        //    catch (Exception) { throw; }
        //}
        public IEnumerable<Row> GetSeatRows(string audiId)
        {
            try
            {
                List<Row> rows = new();

                IEnumerable<IGrouping<string, Data.Seat>> seatLists = _repository.Seat.Get( x => x.AudiId == audiId).GroupBy(i => i.Row).ToList();

                foreach (IGrouping<string, Data.Seat> seatList in seatLists)
                {
                    List<SeatInfo> seats = seatList.Where(x => x.Row == seatList.Key).Select(u => new SeatInfo { SeatNo = u.SeatNo, IsBooked = u.IsBooked , CategorId=u.CategoryId}).ToList();
                    rows.Add(new Row()
                    {
                        RowId = seatList.Key,
                        Seats = seats
                    });
                }

                return rows;
            }
            catch (Exception) { throw; }
        }


        //old code for GetSeatRows


        //public IEnumerable<Row> GetSeatRows(string audiId)
        //{
        //    try
        //    {
        //        List<Row> rows = new();



        //        List<string> rowIds = _context.Seats.Where(x => x.AudiId == audiId).Select(u => u.Row).Distinct().ToList();




        //        foreach (string rowId in rowIds)
        //        {
        //            List<SeatInfo> seats = _context.Seats.Where(x => x.Row == rowId && x.AudiId == audiId)
        //                                                 .Select(u => new SeatInfo { SeatNo = u.SeatNo, IsBooked = u.IsBooked })
        //                                                 .ToList();
        //            rows.Add(new Row()
        //            {
        //                RowId = rowId,
        //                Seats = seats
        //            });
        //        }
        //        return rows;
        //    }
        //    catch (Exception) { throw; }
        //}




        //public Dictionary<DateTime, List<Show>> GetShows(string movieId, string theatreId)
        //{
        //    try
        //    {
        //        Dictionary<DateTime, List<Show>> shows = new();

        //        List<DateTime> showsDates = _context.Audis.Where(x => x.MovieId == movieId
        //                                                                && x.TheatreId == theatreId)
        //                                                                .Select(u => u.ShowDate).Distinct().ToList();
        //        foreach (DateTime date in showsDates)
        //        {
        //            List<Show> showsList = _context.Audis.Where(x => x.ShowDate == date)
        //                                                                .Select(u => new Show() { ShowTime = u.TimeSlot }).ToList();
        //            shows.Add(date, showsList);


        //        }
        //        return shows;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public Dictionary<DateTime, List<Show>> GetShows(string movieId, string theatreId)
        {
            try
            {
                Dictionary<DateTime, List<Show>> shows = new();

                List<DateTime> showsDates = _repository.Audi.Get(x => x.MovieId == movieId
                                                                        && x.TheatreId == theatreId)
                                                                        .Select(u => u.ShowDate).Distinct().ToList();
                foreach (DateTime date in showsDates)
                {
                    List<Show> showsList = _repository.Audi.Get(x => x.ShowDate == date)
                                                                        .Select(u => new Show() { ShowTime = u.TimeSlot }).ToList();
                    shows.Add(date, showsList);


                }
                return shows;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //public IEnumerable<Show> GetShowsByDate( DateTime date)
        //{
        //    try
        //    {
        //        return _context.Audis.Where(x => x.ShowDate == date)
        //                                        .Select(u => new Show() { ShowTime = u.TimeSlot }).ToList();
        //    }
        //    catch (Exception) { throw; }

        //}
        public IEnumerable<Show> GetShowsByDate(DateTime date)
        {
            try
            {
                return _repository.Audi.Get(x => x.ShowDate == date)
                                                .Select(u => new Show() { ShowTime = u.TimeSlot }).ToList();
            }
            catch (Exception) { throw; }

        }
        //public IEnumerable<Movie> GetMoviesByTheatreId(string id)
        //{
        //    try
        //    {
        //        List<Data.Movie> movies = new();

        //        List<string> movieIds = _context.Audis.Where(x => x.TheatreId == id).Select(u => u.MovieId).Distinct().ToList();

        //        foreach (string movieId in movieIds)
        //        {
        //            movies.Add(_context.Movies.SingleOrDefault(x => x.MovieId == movieId));
        //        }
        //        return _mapper.Map<List<Movie>>(movies);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public IEnumerable<Movie> GetMoviesByTheatreId(string id)
        {
            try
            {
                List<Data.Movie> movies = new();

                List<string> movieIds = _repository.Audi.Get(x => x.TheatreId == id).Select(u => u.MovieId).Distinct().ToList();

                foreach (string movieId in movieIds)
                {
                    movies.Add(_repository.Movie.FirstOrDefault(x => x.MovieId == movieId));
                }
                return _mapper.Map<List<Movie>>(movies);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public string CancelBooking(int userId, string bookingId)
        //{
        //    try
        //    {
        //        bool isUpdated = false;

        //        Data.Booking booking = _context.Bookings.SingleOrDefault(x => x.BookingId == bookingId && x.UserId == userId);
        //        if (booking != null)
        //        {
        //            List<AudiRow> audiRows = JsonSerializer.Deserialize<List<AudiRow>>(booking.BookedSeats);
        //            if (audiRows.Count > 0)
        //            {
        //                audiRows.ForEach(audiRow =>
        //                {
        //                    isUpdated = UpdateSeat(booking.AudiId, audiRow);
        //                });
        //            }

        //            if (isUpdated)
        //            {
        //                booking.Status = " Cancelled ";
        //                _context.SaveChanges();
        //                return " Ticekts cancelled. ";
        //            }
        //        }

        //        return " Could not cancel.";
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public string CancelBooking(int userId, Guid bookingId)
        {
            try
            {
                bool isUpdated = false;
                Data.Booking booking = _repository.Booking.FirstOrDefault(x => x.BookingId == bookingId && x.UserId == userId);
                if (booking != null)
                {
                    List<AudiRow> audiRows = JsonSerializer.Deserialize<List<AudiRow>>(booking.BookedSeats);
                    if (audiRows.Count > 0)
                    {
                        audiRows.ForEach(audiRow =>
                        {
                            isUpdated = UpdateSeat(booking.AudiId, audiRow);
                        });
                    }

                    if (isUpdated)
                    {
                        booking.Status = " Cancelled ";
                        _repository.Save();
                        return " Ticekts cancelled. ";
                    }
                }

                return " Could not cancel.";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region PrivateMethods

        //private bool UpdateSeat(string audiId, AudiRow audiRow)
        //{
        //    foreach(int seatNo in audiRow.SeatNOs)
        //    {
        //        Data.Seat seat = _context.Seats.SingleOrDefault(h => h.AudiId==audiId && h.Row == audiRow.Row && h.SeatNo == seatNo);
        //        if(seat != null)
        //        {
        //            seat.IsBooked = false;
        //            _context.SaveChanges();
        //            return true;
                   
        //        }
             
        //    }
        //    return false;
        //}
        private bool UpdateSeat(string audiId, AudiRow audiRow)
        {
            foreach (int seatNo in audiRow.SeatNOs)
            {
                Data.Seat seat = _repository.Seat.FirstOrDefault(h => h.AudiId == audiId && h.Row == audiRow.Row && h.SeatNo == seatNo);
                if (seat != null)
                {
                    seat.IsBooked = false;
                    _repository.Save();
                    return true;

                }

            }
            return false;
        }

        //private int BookSeat(string audiId, string rowId, int seatNo, string category)
        //{
        //    Data.Seat seat = _context.Seats.SingleOrDefault(h => h.AudiId == audiId && h.Row == rowId && h.SeatNo == seatNo);
        //    if (seat != null)
        //    {
        //        int price = _context.Categories.Where(u => u.Id == category).Select(u => u.Price).FirstOrDefault();
        //        seat.IsBooked = true;
        //        _context.SaveChanges();
        //         return price;
        //    }
        //    return 0;
        //}

        private int BookSeat(string audiId, string rowId, int seatNo, string category)
        {
            Data.Seat seat = _repository.Seat.FirstOrDefault(h => h.AudiId == audiId && h.Row == rowId && h.SeatNo == seatNo);
            if (seat != null)
            {
                int price = _repository.Category.Get(u => u.Id == category).Select(u => u.Price).FirstOrDefault();
                seat.IsBooked = true;
                _repository.Save();
                return price;
            }
            return 0;
        }

        private bool CheckSeatAvailability(AudiRow rowSeat , List<Data.Seat> seatList)
        {
            bool available = true;
            seatList.ForEach(seat =>
            {
                if (seat.Row == rowSeat.Row && seat.SeatNo == rowSeat.SeatNOs.FirstOrDefault(x => x == seat.SeatNo))
                {
                   if(seat.IsBooked == true)
                        available= false;
                }
            });
            return available;
        }

        private Row GetRows(string audiId, string row)
        {
            List<SeatInfo> seats = _repository.Seat.Get(x => x.Row == row && x.AudiId == audiId)
                                     .Select(u => new SeatInfo { SeatNo = u.SeatNo, IsBooked = u.IsBooked , CategorId = u.CategoryId})
                                     .ToList();

            return new Row() { RowId = row , Seats = seats  };
        }

        public IEnumerable<ShowDateTime> GetShowDatesandTimings(string movieId, string theatreId)
        {
            return _repository.Audi.Get(x => x.MovieId == movieId && x.TheatreId == theatreId).Select(u => new ShowDateTime { ShowDate = u.ShowDate , ShowTime=u.TimeSlot , AudiId =u.Id});
        }




        #endregion


    }
}

