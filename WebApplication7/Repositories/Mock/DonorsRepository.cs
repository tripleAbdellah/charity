using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DonorApp.Repositories;
using DonorApp.Models;

namespace DonorApp.Repositories.Mock
{
    public class DonorsRepository : IDonorsRepository
    {
        private static int Counter;
        private static int NextId { 
            get {
                return ++Counter;
            }
        }

        public Dictionary<int, Donor> donors = new Dictionary<int, Donor>();

        //   throws NoDonorsFoundException
        public IEnumerable<Donor> GetAllDonors()
        {
            if (donors.Count == 0)
            {
                throw new NoDonorsFoundException();
            }
            else
            {
                return donors.Values.AsEnumerable();
            }
        }

        //   throws NoDonorsFoundException
        public IEnumerable<Donor> GetDonors(SearchDonorRequest searchRequest)
        {
            List<Donor> matches = donors.Values.ToList().FindAll(e => ( searchRequest.Email == null || searchRequest.Email.Equals(e.Email) ) 
                && ( searchRequest.PostCode == null || searchRequest.PostCode.Equals(e.PostCode) )
                && ( searchRequest.PhoneNumber == null || searchRequest.PhoneNumber.Equals(e.TelephoneHome) ) );
            if (matches.Count == 0)
            {
                throw new NoDonorsFoundException();
            }
            else
            {
                return matches.AsEnumerable();
            }
        }

        //   throws DonorNotFoundException
        public Donor GetDonor(int donorID)
        {
            if (!donors.ContainsKey(donorID))
            {
                throw new DonorNotFoundException();
            }
            else
            {
                return donors[donorID];
            }
        }

        //   throws InvalidDonorException
        public int AddDonor(Donor aDonor)
        {
            if (aDonor == null)
            {
                throw new InvalidDonorException();
            }
            else 
            {
                aDonor.Code = NextId;
                donors.Add(aDonor.Code, aDonor);
                return aDonor.Code;
            }
        }

        //   throws DonorNotFoundException
        public void DeleteDonor(int donorID)
        {
            bool removed = donors.Remove(donorID);
            if (!removed)
            {
                throw new DonorNotFoundException();
            }
        }

        //   throws DonorNotFoundException, InvalidDonorException
        public void UpdateDonor(int donorID, Donor donor)
        {
            if (donor == null)
            {
                throw new InvalidDonorException();
            }
            else
            {
                if (!donors.ContainsKey(donorID))
                {
                    throw new DonorNotFoundException();
                }
                else
                {
                    donors[donorID] = donor;
                }
            }
        }
    }
}