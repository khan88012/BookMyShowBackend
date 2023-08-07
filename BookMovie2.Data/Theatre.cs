using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Theatre")]
    public class Theatre
    {
        [Key]
        public string TheatreId { get; set; }

        public string? TheatreName { get; set; }
        public string? LocationId { get; set; }


        public ICollection<Audi>? Movies { get; set; }
      
    }
}
