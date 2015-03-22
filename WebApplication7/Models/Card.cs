using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DonorApp.Models
{
    public class Card
    {
        public string Number { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set;}
        public string Name { get; set; }
        public int CVV { get; set; }
        public Card(string Number, int ExpiryYear, int ExpiryMonth, string Name, int CVV)
        {
            this.Number = Number;
            try
            {
                DateTime Expiry = new DateTime(ExpiryYear, ExpiryMonth, 1);
                this.ExpiryYear = Expiry.Year;
                this.ExpiryMonth = Expiry.Month;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new InvalidCardException("The card's expiry year [ " + ExpiryYear + " ] or month [ " + ExpiryMonth + " ] is invalid");
            }
            this.Name = Name;
        }
    }
}
