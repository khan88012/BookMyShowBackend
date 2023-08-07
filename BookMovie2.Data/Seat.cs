using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Seat")]
    public class Seat
    {
        public int Id { get; set; }
        public string AudiId { get; set; }

        public string Row { get; set; }

        public int SeatNo { get; set; }

        public bool IsBooked { get; set; }

        public string CategoryId { get; set; }


    }
}
