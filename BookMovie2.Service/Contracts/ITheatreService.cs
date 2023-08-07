using BookMovie2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Contracts
{
    public interface ITheatreService
    {
        IEnumerable<Theatre> GetTheatres();
        void AddTheatres(Theatre theatre);

        IEnumerable<Data.TheatreByLocationResponse> GetTheatresByLocation(string location);


    }
}
