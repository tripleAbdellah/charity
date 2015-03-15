using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorApp.Models
{
    public class SearchDonorRequest
    {
    	public int Code { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }

}
