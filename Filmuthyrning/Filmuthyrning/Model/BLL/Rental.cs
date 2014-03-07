using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filmuthyrning.Model.BLL
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int MovieID { get; set; }
        public int CustomerID { get; set; }
        public string RentalDate { get; set; }
    }
}