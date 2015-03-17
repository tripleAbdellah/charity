using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DonorApp.Models;

namespace DonorApp.Repositories
{
    public interface IDonorsRepository
    {
        //   throws NoDonorsFoundException
    	IEnumerable<Donor> GetAllDonors();
        //   throws NoDonorsFoundException
        IEnumerable<Donor> GetDonors(SearchDonorRequest searchRequest);
        //   throws DonorNotFoundException
        Donor GetDonor(int donorID);

        //   throws InvalidDonorException
        Donor AddDonor(Donor item);
        //   throws DonorNotFoundException, InvalidDonorException
        void UpdateDonor(int donorID, Donor item);
        //   throws DonorNotFoundException
		void DeleteDonor(int donorID);
    }
}