using Libly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libly.Controllers
{
    public class RentalController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        private int userId;

        // GET: Rental
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                userId = Convert.ToInt32(Session["UserID"].ToString());
                var rentals = db.Rentals.Where(x => x.CustomerId == userId).ToList();
                return View(rentals);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public ActionResult Rent(int id)
        {
            Rental rental = new Rental();
            var stock = db.Books.Where(x => x.Id == id).ToList().FirstOrDefault().
                Stocks.Where(x => x.IsAvailable).ToList().FirstOrDefault();
            rental.Stock = stock;
            return View(rental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rent(Rental rental)
        {
            if (Session["UserID"] != null)
            {
                userId = Convert.ToInt32(Session["UserID"].ToString());
                rental.Customer = db.Customers.Where(x => x.Id == userId).ToList().FirstOrDefault();
                rental.RentalDate = DateTime.Now.Date;
                rental.Status = "Borrowed";
                var stock = db.Books.Where(x => x.Id == rental.Id).ToList().FirstOrDefault().
                Stocks.Where(x => x.IsAvailable).ToList().FirstOrDefault();
                stock.IsAvailable = false;
                rental.Stock = stock;
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public ActionResult Return(int id)
        {
            if (Session["UserID"] != null)
            {
                var rental = db.Rentals.Where(x => x.Id == id).ToList().FirstOrDefault();
                return View(rental);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(Rental rental)
        {
            var rent = db.Rentals.Where(x => x.Id == rental.Id).ToList().FirstOrDefault();
            rent.Stock.IsAvailable = true;
            rent.Status = "Returned";
            rent.ReturnedDate = DateTime.Now.Date;
            db.Entry(rent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
