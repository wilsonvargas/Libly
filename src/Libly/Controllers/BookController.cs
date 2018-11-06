using Libly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Libly.Controllers
{
    public class BookController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Create()
        {
            List<SelectListItem> genresSelectList = new List<SelectListItem>();
            Book book = new Book();
            var genres = db.Genres.OrderBy(x => x.Name).ToList();
            genres.ForEach(x => genresSelectList.Add(new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }));
            book.Genres = genresSelectList;
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                for (int i = 0; i < book.NumOfStock; i++)
                {
                    Stock stock = new Stock()
                    {
                        IsAvailable = true,
                        Book = book
                    };
                    db.Stocks.Add(stock);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            var book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            throw new HttpException(404, "Book not found");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var book = db.Books.Find(Convert.ToInt32(id));
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new HttpException(404, "Book not found");
        }

        public ActionResult Details(int id)
        {
            var book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }

            throw new HttpException(404, "Book not found!");
        }

        public ActionResult Edit(int id)
        {
            List<SelectListItem> selectedItems = new List<SelectListItem>();
            var book = db.Books.Find(id);
            if (book != null)
            {
                var genres = db.Genres.ToList();
                genres.ForEach(x => selectedItems.Add(new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }));
                book.Genres = selectedItems;
                return View(book);
            }
            throw new HttpException(404, "Book not found!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        public ActionResult List(string search, int? pageNumber)
        {
            if (search != null)
            {
                return View(db.Books.Where(x => x.Title.Contains(search) || x.Writer.Contains(search) || x.Genre.Name.Contains(search)).ToList().ToPagedList(pageNumber ?? 1, 3));
            }
            else
            {
                return View(db.Books.ToList().ToPagedList(pageNumber ?? 1, 5));
            }
        }
    }
}
