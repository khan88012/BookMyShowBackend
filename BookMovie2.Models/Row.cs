using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Models
{
    public class Row
    {
        public string RowId { get; set; }
        public List<SeatInfo> Seats { get; set; }   

       
    }

    public class SeatInfo
    {
        public int SeatNo { get; set; }

        public bool IsBooked { get; set; }

        public string CategorId { get; set; }
    }
}
