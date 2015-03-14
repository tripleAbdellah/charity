using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class ShoppingCategory
    {
        public int itemID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string weight { get; set; }
        public string dimensions { get; set; }
        public IEnumerable<ShoppingSubCategory> shoppingSubCategoryList { get; set; }  
    }
}
