using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorApp.Models
{
    public interface IDonorsRepository
    {

    	IEnumerable<Donor> GetAllDonors() throws NoDonorsFoundException;
        IEnumerable<Donor> GetDonors(SearchDonorRequest searchRequest) throws NoDonorsFoundException;
        Donor GetDonor(int donorID) throws DonorNotFoundException;

        Donor AddDonor(Donor item) throws InvalidDonorException;
        void UpdateDonor(int donorID, Donor item) throws DonorNotFoundException, InvalidDonorException;
		void DeleteDonor(int donorID) throws DonorNotFoundException;

    }
}