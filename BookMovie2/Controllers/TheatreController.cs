using BookMovie2.Models;
using BookMovie2.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMovie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatreController 
    {
        private readonly ITheatreService _theatreService;
        public TheatreController(ITheatreService  theatreService)
        {
                _theatreService = theatreService;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Theatre> Get() {
         
            try
            {
                return _theatreService.GetTheatres();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public void Post(Theatre theatre)
        {
            try
            {
                if(theatre != null)
                _theatreService.AddTheatres(theatre);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet("GetTheatresByLocation")]
        public IEnumerable<Data.TheatreByLocationResponse> GetTheatresByLocation(string location)
        {
            try 
            {
                if(location !=null)
                {
                    return _theatreService.GetTheatresByLocation(location);
                }
                return new List<Data.TheatreByLocationResponse>();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }


    }
}
