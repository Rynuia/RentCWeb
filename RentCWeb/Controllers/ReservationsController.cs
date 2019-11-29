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
    public class ReservationsController : Controller
    {
        private rentcweb_dbEntities db = new rentcweb_dbEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string sortorder, string searchstring)
        {
            ViewBag.plateSort = String.IsNullOrEmpty(sortorder) ? "plate_desc" : "";
            ViewBag.cIDSort = sortorder == "cID" ? "cID_desc" : "cID";
            ViewBag.sdSort = sortorder == "sd" ? "sd_desc" : "sd";
            ViewBag.edSort = sortorder == "ed" ? "ed_desc" : "ed";
            ViewBag.citySort = sortorder == "city" ? "city_desc" : "city";

            var reservations = from r in db.Reservations select r;
            reservations = db.Reservations.Include(r => r.Car);

            if (!String.IsNullOrEmpty(searchstring))
            {
                reservations = reservations.Where(r => r.Car.Plate.Contains(searchstring));
            }

            switch (sortorder)
            {
                case "plate_desc":
                    reservations = reservations.OrderByDescending(r => r.Car.Plate);
                    break;
                case "cID":
                    reservations = reservations.OrderBy(r => r.CustomerID);
                    break;
                case "cID_desc":
                    reservations = reservations.OrderByDescending(r => r.CustomerID);
                    break;
                case "sd":
                    reservations = reservations.OrderBy(r => r.StartDate);
                    break;
                case "sd_desc":
                    reservations = reservations.OrderByDescending(r => r.StartDate);
                    break;
                case "ed":
                    reservations = reservations.OrderBy(r => r.EndDate);
                    break;
                case "ed_desc":
                    reservations = reservations.OrderByDescending(r => r.EndDate);
                    break;
                case "city":
                    reservations = reservations.OrderBy(r => r.Location);
                    break;
                case "city_desc":
                    reservations = reservations.OrderByDescending(r => r.Location);
                    break;
                default:
                    reservations = reservations.OrderBy(r => r.Car.Plate);
                    break;
            }

            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            //var list = db.Cars.Where(c => db.Reservations.Where(r => r.CarID != c.CarID).Select(r => r.CarID).Contains(c.CarID)).ToList();

            //ViewBag.CarID = new SelectList(db.Cars.Where(c => c.CarID != c.Reservations.), "CarID", "Plate");
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Plate");

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CarID,CustomerID,StartDate,EndDate,Location,ReservationID")] Reservation reservation)
        {
            bool carpass;
            bool cidpass;
            bool datepass;
            bool citypass;
            bool allpass = true;

            int carid = reservation.CarID;
            int cid = reservation.CustomerID;
            string city = reservation.Location;

            //var cidcheck = db.Reservations.Where(r => db.Customers.Any(c => c.CustomerID == r.CustomerID)).ToList();

            var carcheck = db.Reservations.Where(r => r.CarID == carid).ToList();

            if (carcheck.Count != 0)
            {
                carpass = false;
                allpass = false;
            }
            else carpass = true;

            var cidcheck = db.Customers.Where(c => c.CustomerID == cid).ToList();

            if (cidcheck.Count > 0)
            {
                cidpass = true;
            }
            else
            {
                cidpass = false;
                allpass = false;
            }

            if (reservation.EndDate < reservation.StartDate)
            {
                datepass = false;
                allpass = false;
            }
            else datepass = true;

            var citycheck = db.Cars.Where(c => c.CarID == carid && c.Location == city).ToList();

            if (citycheck.Count != 1)
            {
                citypass = false;
                allpass = false;
            }
            else citypass = true;

            string carstr = "";
            string cidstr = "";
            string datestr = "";
            string citystr = "";

            if (carpass == false)
            {
                carstr = "This car is already rented.";
            }

            if (cidpass == false)
            {
                cidstr = "Client ID not found.";
            }

            if (datepass == false)
            {
                datestr = "End Date cannot be before than the Start Date";
            }

            if (citypass == false)
            {
                citystr = "Selected car is not available for this city.";
            }

            if (allpass == true)
            {
                if (ModelState.IsValid)
                {
                    db.Reservations.Add(reservation);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CarPass = carstr;
            ViewBag.CidPass = cidstr;
            ViewBag.DatePass = datestr;
            ViewBag.CityPass = citystr;
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Plate", reservation.CarID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Plate", reservation.CarID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CarID,CustomerID,StartDate,EndDate,Location,ReservationID")] Reservation reservation)
        {
            bool cidpass;
            bool datepass;
            bool citypass;
            bool allpass = true;

            int carid = reservation.CarID;
            int cid = reservation.CustomerID;
            string city = reservation.Location;

            var cidcheck = db.Customers.Where(c => c.CustomerID == cid).ToList();

            if (cidcheck.Count > 0)
            {
                cidpass = true;
            }
            else
            {
                cidpass = false;
                allpass = false;
            }

            if (reservation.EndDate < reservation.StartDate)
            {
                datepass = false;
                allpass = false;
            }
            else datepass = true;

            var citycheck = db.Cars.Where(c => c.CarID == carid && c.Location == city).ToList();

            if (citycheck.Count != 1)
            {
                citypass = false;
                allpass = false;
            }
            else citypass = true;

            string cidstr = "";
            string datestr = "";
            string citystr = "";
            

            if (cidpass == false)
            {
                cidstr = "Client ID not found.";
            }

            if (datepass == false)
            {
                datestr = "End Date cannot be before than the Start Date";
            }

            if (citypass == false)
            {
                citystr = "Selected car is not available for this city.";
            }


            if (allpass == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(reservation).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CidPass = cidstr;
            ViewBag.DatePass = datestr;
            ViewBag.CityPass = citystr;
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Plate", reservation.CarID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservation reservation = await db.Reservations.FindAsync(id);
            db.Reservations.Remove(reservation);
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
