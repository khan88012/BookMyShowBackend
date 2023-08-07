using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public Guid? BookingId { get; set; }

        public int UserId { get; set; }

        public string MovieId { get; set; }
        public string TheatreId { get; set; }

        public string AudiId { get; set; }

        public DateTime ShowDate { get; set; }

        public String? TimeSlot { get; set; }

        public string BookedSeats { get; set; }

        public string Status { get; set; }

        public DateTime? BookingDate { get; set; }

        public int Price { get; set; }

    }
}
