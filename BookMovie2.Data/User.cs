using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    //user table
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String? UserName { get; set; }

        public String? Password { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
