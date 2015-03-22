using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class PricedAllocation : Allocation
    {
        public float UnitPrice { get; set; }
        int _Units;
        public int Units { 
            get
            {
                return _Units;
            }
            set 
            {
                _Units = value;
                Amount = UnitPrice * _Units;
            }
        }

        public PricedAllocation(string Type, float UnitPrice)
            : base(Type)
        {
            this.UnitPrice = UnitPrice;
        }

        public PricedAllocation(string Type, float UnitPrice, int Units) : base(Type, UnitPrice * Units)
        {
            this.UnitPrice = UnitPrice;
            this.Units = Units;
        }
    }
}