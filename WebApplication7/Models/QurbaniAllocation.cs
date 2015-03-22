using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class QurbaniAllocation : PricedAllocation
    {
        public string Country { get; set; }
        public string Animal { get; set; }

        public QurbaniAllocation(string Country, string Animal, float UnitPrice)
            : base("Qurbani", UnitPrice)
        {
            this.Country = Country;
            this.Animal = Animal;
        }

        public QurbaniAllocation(string Country, string Animal, float UnitPrice, int Units) : base("Qurbani", UnitPrice, Units)
        {
            this.Country = Country;
            this.Animal = Animal;
        }
    }
}