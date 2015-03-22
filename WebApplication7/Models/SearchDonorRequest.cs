using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DonorApp.Models
{
    public class SearchDonorRequest
    {
        public int Code { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        //public int PhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public SearchDonorRequest validate()
        {
            //if (Code == 0 && PostCode == null && Email == null && PhoneNumber == 0)
            if ( Code == 0 
                && (PostCode == null || PostCode.Trim().Length == 0) 
                && (Email == null || Email.Trim().Length == 0)
                && (PhoneNumber == null || PhoneNumber.Trim().Length == 0) )
            {
                throw new InvalidRequestException();
            }
            return this;
        }
    }
}
