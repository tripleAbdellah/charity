using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class Allocation
    {
        public string Type { get; set; }
        public float Amount { get; set; }

        public Allocation(string Type)
        {
            this.Type = Type;
        }

        public Allocation(string Type, float Amount)
        {
            this.Type = Type;
            this.Amount = Amount;
        }

    }
}