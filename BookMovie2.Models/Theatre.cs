using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Models
{
 
    public class Theatre
    {
    
        public string TheatreId { get; set; }

        public string? TheatreName { get; set; }
        public string? LocationId { get; set; }


    }
}
