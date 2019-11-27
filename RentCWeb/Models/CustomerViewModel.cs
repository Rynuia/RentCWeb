using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCWeb.Models
{
    public class CustomerViewModel
    {
        public int CostumerID { get; set; }
        public string Name { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Location { get; set; }
        public Nullable<int> Zip { get; set; }
    }
}