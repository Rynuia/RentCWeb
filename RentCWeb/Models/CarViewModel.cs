using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCWeb.Models
{
    public class CarViewModel
    {
        public int CarID { get; set; }
        public string Plate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string Location { get; set; }
    }
}