//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentCWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Reservation
    {
        [Required(ErrorMessage = "Enter Car ID")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Enter Client ID")]
        public int CustomerID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Starting Date")]
        public System.DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Ending Date")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Enter City")]
        [StringLength(50)]
        public string Location { get; set; }

        [Key]
        public int ReservationID { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
