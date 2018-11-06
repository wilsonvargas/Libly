using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libly.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Books = new HashSet<Book>();
        }

        public virtual ICollection<Book> Books { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
