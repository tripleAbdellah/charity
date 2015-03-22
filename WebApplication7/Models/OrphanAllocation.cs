using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class OrphanAllocation : Allocation
    {
        /*
        enum OrphanType
        {
            Child, Family
        }
        */

        public string OrphanType { get; set; }

        public OrphanAllocation(string OrphanType)
            : base("Orphan")
        {
            this.OrphanType = OrphanType;
        }

        public OrphanAllocation(string OrphanType, float Amount) : base("Orphan", Amount)
        {
            this.OrphanType = OrphanType;
        }
    }
}