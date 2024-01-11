using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Models
{

 
    public class Movie
    {
      
        
        public string? MovieId { get; set; }

        public string? MovieName { get; set; }

        public string? Genre { get; set; }

        public string? moviePoster { get; set; }



    }
}
