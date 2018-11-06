using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libly.Models
{
    public class Stock
    {
        public Stock()
        {
            this.Rentals = new HashSet<Rental>();
        }

        public virtual Book Book { get; set; }
        public int BookId { get; set; }
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
