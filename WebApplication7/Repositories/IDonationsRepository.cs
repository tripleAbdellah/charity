using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DonorApp.Models;

namespace DonorApp.Repositories
{
    public interface IDonationsRepository
    {
        //   throws DonorNotFoundException, NoDonationsFoundException
        IEnumerable<Donation> GetDonations(int donorID);

        //   throws DonorNotFoundException, InvalidDonationException
        void AddDonation(int donorID, Donation donation);
    }
}
