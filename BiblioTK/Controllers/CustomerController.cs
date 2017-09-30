using BiblioTK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BiblioTK.Controllers
{
    public class CustomerController : Controller
    {
        private Entities db = new Entities();
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View(db.Customers);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var salt = Crypto.GenerateSalt();
                customer.Password = Crypto.HashPassword(customer.Password + salt);
                customer.Salt = salt;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            return View(customer);
        }


        public ActionResult Edit(int id)
        {

            var customer = db.Customers.Find(id);
            if (customer != null)
            {
                return View(customer);
            }
            throw new HttpException(404, "Customer not found!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Detail(int id)
        {
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var customer = db.Customers.Find(Convert.ToInt32(id));
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            throw new HttpException(404, "Customer not found!");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var value = db.Customers.Where(x => x.Email == customer.Email).ToList().FirstOrDefault();
                if (value != null)
                {
                    if (Crypto.VerifyHashedPassword(value.Password, customer.Password + value.Salt))
                    {
                        System.Web.HttpContext.Current.Session["UserID"] = value.Id.ToString();
                        System.Web.HttpContext.Current.Session["Email"] = value.Email;
                        System.Web.HttpContext.Current.Session["Name"] = value.FirstName + " " + value.LastName;
                        System.Web.HttpContext.Current.Session["isAdmin"] = value.IsAdmin;
                        return RedirectToAction("Index", "Book");
                    }
                }
            }
            return View(customer);
        }

        public ActionResult Logout() {
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Index","Book");
        }
    }
}