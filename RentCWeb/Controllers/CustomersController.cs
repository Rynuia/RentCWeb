using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentCWeb.Models;

namespace RentCWeb.Controllers
{
    public class CustomersController : Controller
    {
        private rentcweb_dbEntities db = new rentcweb_dbEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string sortorder, string searchstring)
        {
            ViewBag.cIDSort = String.IsNullOrEmpty(sortorder) ? "cID_desc" : "";
            ViewBag.nameSort = sortorder == "name" ? "name_desc" : "name";
            ViewBag.bdSort = sortorder == "bd" ? "bd_desc" : "bd";
            ViewBag.citySort = sortorder == "city" ? "city_desc" : "city";
            ViewBag.zipSort = sortorder == "zip" ? "zip_desc" : "zip";

            var customers = from c in db.Customers select c;

            if (!String.IsNullOrEmpty(searchstring))
            {
                customers = customers.Where(c => c.Name.Contains(searchstring));
            }

            switch (sortorder)
            {
                case "cID_desc":
                    customers = customers.OrderByDescending(c => c.CustomerID);
                    break;
                case "name":
                    customers = customers.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.Name);
                    break;
                case "bd":
                    customers = customers.OrderBy(c => c.BirthDate);
                    break;
                case "bd_desc":
                    customers = customers.OrderByDescending(c => c.BirthDate);
                    break;
                case "city":
                    customers = customers.OrderBy(c => c.Location);
                    break;
                case "city_desc":
                    customers = customers.OrderByDescending(c => c.Location);
                    break;
                case "zip":
                    customers = customers.OrderBy(c => c.Zip);
                    break;
                case "zip_desc":
                    customers = customers.OrderByDescending(c => c.Zip);
                    break;
                default:
                    customers = customers.OrderBy(c => c.CustomerID);
                    break;
            }

            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerID,Name,BirthDate,Location,Zip")] Customer customer)
        {
            bool datepass;
            bool zippass;
            bool allpass = true;

            DateTime datecheck = customer.BirthDate;
            int year = DateTime.Now.Year - datecheck.Year;
            int month = DateTime.Now.Month - datecheck.Month;
            int day = DateTime.Now.Day - datecheck.Day;

            if (year < 18 || year == 18 && month < 0 || year == 18 && month == 0 && day < 0)
            {
                allpass = false;
                datepass = false;
            }
            else datepass = true;

            string zipcheck = customer.Zip.ToString();

            if (zipcheck.Length == 5 || zipcheck.Length == 0)
            {
                zippass = true;
            }
            else
            {
                zippass = false;
                allpass = false;
            }

            string datestr = "";
            string zipstr = "";

            if (datepass == false)
            {
                datestr = "Client should be at least 18 years old.";
            }

            if (zippass == false)
            {
                zipstr = "ZIP field should be either empty, or in the correct US ZIP Code format: XXXXX.";
            }

            ViewBag.DatePass = datestr;
            ViewBag.ZipPass = zipstr;

            if (allpass == true)
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerID,Name,BirthDate,Location,Zip")] Customer customer)
        {
            bool datepass;
            bool zippass;
            bool allpass = true;

            DateTime datecheck = customer.BirthDate;
            int year = DateTime.Now.Year - datecheck.Year;
            int month = DateTime.Now.Month - datecheck.Month;
            int day = DateTime.Now.Day - datecheck.Day;

            if (year < 18 || year == 18 && month < 0 || year == 18 && month == 0 && day < 0)
            {
                allpass = false;
                datepass = false;
            }
            else datepass = true;

            string zipcheck = customer.Zip.ToString();

            if (zipcheck.Length == 5 || zipcheck.Length == 0)
            {
                zippass = true;
            }
            else
            {
                zippass = false;
                allpass = false;
            }

            string datestr = "";
            string zipstr = "";

            if (datepass == false)
            {
                datestr = "Client should be at least 18 years old.";
            }

            if (zippass == false)
            {
                zipstr = "ZIP field should be either empty, or in the correct US ZIP Code format: XXXXX.";
            }

            ViewBag.DatePass = datestr;
            ViewBag.ZipPass = zipstr;


            if (allpass == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);

            foreach (var rent in db.Reservations.Where(r => r.CustomerID == id))
            {
                db.Reservations.Remove(rent);
            }

            db.Customers.Remove(customer);

            

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
