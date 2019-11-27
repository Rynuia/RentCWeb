using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCWeb.Models
{
    public class ReservationViewModel
    {
        public int CarID { get; set; }
        public int CostumerID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Location { get; set; }
        public byte ReservStatsID { get; set; }
        public string Plate { get; set; }
    }
}