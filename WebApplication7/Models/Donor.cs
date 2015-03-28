using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class Donor
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string LanguageSpoken { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string TelephoneHome { get; set; }
        public string TelephoneWork { get; set; }
        public string TelephoneMobile { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public bool CommunicationByEmail { get; set; }
        public bool GAD { get; set; }
        public bool GADVerbal { get; set; }
        public string GADName { get; set; }
        public DateTime GADDate { get; set; }
    }
}
