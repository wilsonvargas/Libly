using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libly.Models
{
    public class Rental
    {
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public System.DateTime RentalDate { get; set; }
        public Nullable<System.DateTime> ReturnedDate { get; set; }
        public string Status { get; set; }
        public virtual Stock Stock { get; set; }
        public int StockId { get; set; }
    }
}
