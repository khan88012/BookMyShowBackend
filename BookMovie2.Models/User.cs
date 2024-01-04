using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String? UserName { get; set; }

        public String? Password { get; set; }
    }
}
