using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public string? LocationId { get; set; }

        public string? LocationName { get; set; }

        public ICollection<Theatre> Theatres { get; set; }

    }
}
