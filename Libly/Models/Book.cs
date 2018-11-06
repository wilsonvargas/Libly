using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libly.Models
{
    public class Book
    {
        public Book()
        {
            this.Stocks = new HashSet<Stock>();
        }

        public string Description { get; set; }
        public virtual Genre Genre { get; set; }
        public int GenreId { get; set; }

        [NotMapped]
        public List<SelectListItem> Genres { get; set; }

        public int Id { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public int NumOfStock { get; set; }

        public decimal Rating { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
    }
}
