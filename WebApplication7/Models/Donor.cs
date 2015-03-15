using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class Donor
    {
        public int CODE { get; set; }
        public string NAME { get; set; }
        public double TYPE { get; set; }
        public string ADD1 { get; set; }
        public string ADD2 { get; set; }
        public string CITY { get; set; }
        public string PCODE { get; set; }
        public string EMAIL { get; set; }
        public string COUNTY { get; set; }
        public string COUNTRY { get; set; }
        public string TEL { get; set; }
        public string TEL_WORK { get; set; }
        public string MOBILE { get; set; }
        public string NTUSERWHOADDED { get; set; }
        public string TITLE { get; set; }
        public bool ERECEIPT { get; set; }
        public bool GAD { get; set; }
    }
}
