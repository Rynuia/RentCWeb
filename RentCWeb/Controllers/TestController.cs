using RentCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentCWeb.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            rentcweb_dbEntities db = new rentcweb_dbEntities();

            List<Reservation> list = db.Reservations.ToList();

            ReservationViewModel vm = new ReservationViewModel();

            List<ReservationViewModel> vmlist = list.Select(x => new ReservationViewModel
            {
                CarID = x.CarID,
                CostumerID = x.CostumerID,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Location = x.Location,
                Plate = x.Car.Plate
            }).ToList();

            return View(vmlist);
        }
    }
}