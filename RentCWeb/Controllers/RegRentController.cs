using RentCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCWeb.Controllers
{
    public class RegRentController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult NewRent()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DoReg(ReservationViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                rentcweb_dbEntities rdb = new rentcweb_dbEntities();

                Reservation res = new Reservation();
                res.Car.Plate = model.Plate;
                res.CarID = model.CarID;
                res.CostumerID = model.CostumerID;
                res.StartDate = model.StartDate;
                res.EndDate = model.EndDate;
                res.Location = model.Location;
            }

            

            return View(model);
        }
    }
}