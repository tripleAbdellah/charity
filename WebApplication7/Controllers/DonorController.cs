using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DonorApp.Models;

namespace DonorApp.Controllers
{
    public class DonorController : ApiController
    {
        static readonly IDonorRepository repository = new DonorRepository();
        [HttpGet]
        public Donor GetDonorByName(string donorID)
        {
            Donor donor = repository.Get(donorID);
            if (donor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return donor;
        }
        [HttpGet]
        public Donor GetDonor(int donorID)
        {
            Donor donor = repository.Get(donorID);
            if (donor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return donor;
        }

        public IEnumerable<Donor> GetCustomersByCountry(string country)
        {
            return repository.GetAll().Where(
                d => string.Equals(d.COUNTRY, country, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostDonor(Donor donor)
        {
            donor = repository.AddDonor(donor);
            var response = Request.CreateResponse<Donor>(HttpStatusCode.Created, donor);

            string uri = Url.Link("DefaultApi", new { donorID = donor.CODE });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutDonor(int donorID, Donor donor)
        {
            donor.CODE = donorID;
            if (!repository.Update(donor))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteDonor(int donorID)
        {
            Donor donor = repository.Get(donorID);

            if (donor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(donorID);
        }
    }
}