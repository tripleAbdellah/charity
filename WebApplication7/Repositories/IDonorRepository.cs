using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DonorApp.Models;

namespace DonorApp.Repositories
{
    public interface IDonorRepository
    {
        IEnumerable<Donor> GetDonors(Donor item);
        Donor GetDonor(int donorID);
        Donor AddDonor(Donor item);
        bool Remove(int donorID);
        bool Update(Donor item);
    }
}