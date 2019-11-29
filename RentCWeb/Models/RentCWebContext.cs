using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentCWeb.Models
{
    public class RentCWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RentCWebContext() : base("name=RentCWebContext")
        {
        }

        public System.Data.Entity.DbSet<RentCWeb.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<RentCWeb.Models.Car> Cars { get; set; }

        public System.Data.Entity.DbSet<RentCWeb.Models.Customer> Customers { get; set; }
    }
}
