using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DonorApp.Models
{
    public class Donation
    {
        public int DonorId { get; set; }
        public Allocation Allocation { get; set; }
        public Card Card { get; set; }

        public Donation(int DonorId, Allocation Allocation, Card Card)
        {
            this.DonorId = DonorId;
            this.Allocation = Allocation;
            this.Card = Card;
        }
    }
}
