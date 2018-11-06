using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libly.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Rentals = new HashSet<Rental>();
        }

        public System.DateTime DateOfBith { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string IdentityCard { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public string Salt { get; set; }
    }
}
