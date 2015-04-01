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
        // AAA: I believe orphan ID is required, potentially a donor can have two children with the same name, therefore ID is required. 
        // Please note, for some reason the table field 'ORN_CODE' is of type float/double in the database. I think we need to cast to int? 
        public int OrphanID { get; set; }
        public string OrphanName { get; set; }
        public string OrphanType { get; set; }

        // AAA: This is required to determine if the Orphan / family is still eligible to retrieve donations. Based on the Due date. 
        public DateTime Due { get; set; }

        /* AAA: Donor can give special gift or pay the regular sponsorship amount
           if the regular sponsorship amount is chosen, then it requires the number of sponsor months
           So there are two types of Orphan donations (special gift or sponsorship). That is why OrphanDonationType is required to distinguish the types. 
         */ 
        public string OrphanDonationType { get; set; }
        public int SponsorMonths { get; set; }
       
        /*There are some remarks (free text) that can be entered by the users, e.g. 'Money donated for Fridge, or schoolkit children and so on'.*/ 
        public string OrphanRemarks { get; set; }


        public OrphanAllocation(int OrphanID, string OrphanName, string OrphanType, DateTime Due)
            : base("Orphan")
        {
            this.OrphanID = OrphanID; 
            this.OrphanName = OrphanName; 
            this.OrphanType = OrphanType;
            this.Due = Due; 
        }


        public OrphanAllocation(int OrphanID, float Amount, string OrphanDonationType ,string OrphanRemarks, int SponsorMonths) : base("Orphan", Amount)
        {
            this.OrphanID = OrphanID;
            this.OrphanDonationType = OrphanDonationType;
            this.OrphanRemarks = OrphanRemarks;
            this.SponsorMonths = SponsorMonths; 
        }

    }
}
