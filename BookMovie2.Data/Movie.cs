using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Movie")]
    public class Movie
    {

        [Key]
        public string MovieId { get; set; }

        public string? MovieName { get; set; }

        public string? Genre { get; set; }
        public ICollection<Audi>? Theatres { get; set; }



    }
}
