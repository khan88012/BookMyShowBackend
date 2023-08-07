using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Models
{
  
    public class Audi
    {
        public string Id { get; set; }
        public string MovieId { get; set; }

   
        public string TheatreId { get; set; }

    
        public DateTime ShowDate { get; set; }

        public string? TimeSlot { get; set; }

    }
}
