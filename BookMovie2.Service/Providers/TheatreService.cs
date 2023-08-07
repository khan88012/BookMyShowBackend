using AutoMapper;
using BookMovie2.Data;
using BookMovie2.Data.Contracts;
using BookMovie2.Models;
using BookMovie2.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Theatre = BookMovie2.Models.Theatre;

namespace BookMovie2.Service.Providers
{
    public class TheatreService : ITheatreService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _repository;

        public TheatreService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = unitOfWork;   
         
        }
        //public void AddTheatres(Theatre theatre)
        //{
        //    try
        //    {
        //        Data.Theatre theatreDb = new Data.Theatre()
        //        {
        //            TheatreId = theatre.TheatreId,
        //            TheatreName = theatre.TheatreName,
        //            LocationId = theatre.LocationId,

        //        };

        //        _context.Theatres.Add(theatreDb);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}




        //public IEnumerable<Theatre> GetTheatres()
        //{
        //    try
        //    {
        //        List<Data.Theatre> theatres = _context.Theatres.ToList();
        //        return _mapper.Map<List<Theatre>>(theatres);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public void AddTheatres(Theatre theatre)
        {
            try
            {
                _repository.Theatre.Add(_mapper.Map<Data.Theatre>(theatre));
                _repository.Save();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IEnumerable<Theatre> GetTheatres()
        {
            try
            {
                return _mapper.Map<List<Theatre>>(_repository.Theatre.GetAll());
            }
            catch (Exception)
            {
                throw;
            }

        }

        //public IEnumerable<TheatreResponse> GetTheatresByLocation(string location)
        //{

        //    try
        //    {
        //     //   List<TheatreResponse> theatres = _repository.Theatre.Get(x => x.LocationId == location);
        //        List<TheatreResponse> theatres = _context.TheatresByLocations.Where(x => x.LocationName == location).ToList();

        //        return theatres;
        //    }
        //    catch (Exception) { throw; }
        //}

        public IEnumerable<TheatreByLocationResponse> GetTheatresByLocation(string location)
        {

            try
            {
                List<TheatreByLocationResponse> theatres = _repository.TheatreByLocationResponse.Get(x => x.LocationName == location).ToList();

                return theatres;
            }
            catch (Exception) { throw; }
        }

        //public IEnumerable<TheatreResponse> GetTheatresByLocation(string location)
        //{

        //    try
        //    {
        //        List<TheatreResponse> theatres = _context.TheatresByLocations.Where(x => x.LocationName == location).ToList();

        //        return theatres;  
        //    }
        //    catch (Exception) { throw; }
        //}












        // old code GetTheatresByLocation
        //public IEnumerable<TheatreResponse> GetTheatresByLocation(string location)
        //{

        //    try
        //    {

        //        List<TheatreResponse> theatres = (from t in _context.Theatres
        //                                          join l in _context.Locations
        //                                          on t.LocationId equals l.LocationId
        //                                          where l.LocationName == location
        //                                          select new TheatreResponse()
        //                                          {
        //                                              TheatreId = t.TheatreId,
        //                                              TheatreName = t.TheatreName,
        //                                              LocationName = l.LocationName,
        //                                          }
        //                        ).ToList();


        //       // List<Data.Theatre> theatres = _context.Theatres.Where(x => x.Location == location).ToList();
        //        return theatres;  //_mapper.Map<List<Theatre>>(theatres);
        //    }
        //    catch (Exception) { throw; }
        //}

        //public void AddJson(Models.JsonObject jsonObject)
        //{
        //    JsonDemo data = new JsonDemo()
        //    {
        //        Data = JsonSerializer.Serialize(jsonObject).ToString(),
        //    };

        //    var d = JsonSerializer.Deserialize<Data.JsonObject>(data.Data);

        //    _context.JsonDemos.Add(data);

        //    _context.SaveChanges();


        //}


    }
}
