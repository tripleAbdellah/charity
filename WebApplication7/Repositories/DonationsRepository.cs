using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DonorApp.Models;

namespace DonorApp.Repositories
{
    public class DonationsRepository : IDonationsRepository
    {
        public IEnumerable<Donation> GetDonations(int id)
        {
            return null;
        }

        public void AddDonation(int id, Donation donation)
        {
            
        }
    }
}
