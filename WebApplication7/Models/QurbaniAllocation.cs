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
        //AAA: the user can enter (free text) notes for Qurbani donation
        public string QurbaniNotes { get; set; }
        //AAA: the user can enter another name (on Behalf of) for the Qurbani donations
        public string QurbaniOnBehalf { get; set; }

        public QurbaniAllocation(string Country, string Animal, float UnitPrice)
            : base("Qurbani", UnitPrice)
        {
            this.Country = Country;
            this.Animal = Animal;
        }

        public QurbaniAllocation(string Country, string Animal, float UnitPrice, int Units, String QurbaniNotes, String QurbaniOnBehalf) : base("Qurbani", UnitPrice, Units)
        {
            this.Country = Country;
            this.Animal = Animal;
            this.QurbaniNotes = QurbaniNotes;
            this.QurbaniOnBehalf = QurbaniOnBehalf; 
        }
    }
}
