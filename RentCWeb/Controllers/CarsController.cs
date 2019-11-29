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
    public class CarsController : Controller
    {
        private rentcweb_dbEntities db = new rentcweb_dbEntities();

        // GET: Cars
        public ViewResult Index(string sortorder, string searchstring)
        {
            ViewBag.carIDSort = String.IsNullOrEmpty(sortorder) ? "carid_desc" : "";
            ViewBag.plateSort = sortorder == "plate" ? "plate_desc" : "plate";
            ViewBag.manuSort = sortorder == "manu" ? "manu_desc" : "manu";
            ViewBag.modelSort = sortorder == "model" ? "model_desc" : "model";
            ViewBag.ppdSort = sortorder == "ppd" ? "ppd_desc" : "ppd";
            ViewBag.citySort = sortorder == "city" ? "city_desc" : "city";

            var cars = from c in db.Cars select c;

            if (!String.IsNullOrEmpty(searchstring))
            {
                cars = cars.Where(c => c.Plate.Contains(searchstring));
            }

            switch (sortorder)
            {
                case "carID_desc":
                    cars = cars.OrderByDescending(c => c.CarID);
                    break;
                case "plate":
                    cars = cars.OrderBy(c => c.Plate);
                    break;
                case "plate_desc":
                    cars = cars.OrderByDescending(c => c.Plate);
                    break;
                case "manu":
                    cars = cars.OrderBy(c => c.Manufacturer);
                    break;
                case "bd_desc":
                    cars = cars.OrderByDescending(c => c.Manufacturer);
                    break;
                case "model":
                    cars = cars.OrderBy(c => c.Model);
                    break;
                case "model_desc":
                    cars = cars.OrderByDescending(c => c.Model);
                    break;
                case "ppd":
                    cars = cars.OrderBy(c => c.PricePerDay);
                    break;
                case "ppd_desc":
                    cars = cars.OrderByDescending(c => c.PricePerDay);
                    break;
                case "city":
                    cars = cars.OrderBy(c => c.Location);
                    break;
                case "city_desc":
                    cars = cars.OrderByDescending(c => c.Location);
                    break;
                default:
                    cars = cars.OrderBy(c => c.CarID);
                    break;
            }

            return View(cars.ToList());
        }
    

        // GET: Cars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
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
