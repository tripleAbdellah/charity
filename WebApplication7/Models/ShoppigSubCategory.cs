using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class ShoppingSubCategory
    {
        public int unitID { get; set; }
        public string unitName { get; set; }
        public string description { get; set;}
        public decimal unitPrice { get; set; } 
    }
}
