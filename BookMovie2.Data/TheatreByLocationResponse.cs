using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Keyless]
    [Table("GetTheatresByLocation")]
    public class TheatreByLocationResponse
    {
        public string? TheatreId { get; set; }

        public string? TheatreName { get; set; }
       
        public string? LocationName { get; set; }
    }
}
