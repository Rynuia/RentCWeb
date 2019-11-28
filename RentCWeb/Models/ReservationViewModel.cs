using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentCWeb.Models
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Enter Car ID")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Enter Client ID")]
        public int CostumerID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Starting Date")]
        public System.DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Ending Date")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Enter City")]
        public string Location { get; set; }


        public byte ReservStatsID { get; set; }

        //Custom Attributes

        [Required(ErrorMessage = "Enter Plate")]
        public string Plate { get; set; }
    }
}