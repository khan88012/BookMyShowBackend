using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data
{
    [Table("Category")]
    public class Category
    {
        public string? Id { get; set; }

        public string? CategoryName { get; set; }

        public int Price { get; set; }
    }
}
