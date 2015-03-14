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
        public Donor GetDonor(int donorID)
        {
            Donor donor = repository.GetDonor(donorID);
            if (donor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return donor;
        }

		[HttpPost]
        public HttpResponseMessage AddDonor(Donor donor)
        {
            donor = repository.AddDonor(donor);
            var response = Request.CreateResponse<Donor>(HttpStatusCode.Created, donor);

            string uri = Url.Link("DefaultApi", new { donorID = donor.CODE });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpGet]
        public IEnumerable<Donor> GetDonors([FromUri] Donor donor)
        {
            IEnumerable<Donor> donors = repository.GetDonors(donor);
            if (donors == null || donors.size() == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return donors;
        }

        [HttpPut]
        public void UpdateDonor(int donorID, Donor donor)
        {
            donor.CODE = donorID;
            if (!repository.Update(donor))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        public void DeleteDonor(int donorID)
        {
            Donor donor = repository.GetDonor(donorID);

            if (donor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(donorID);
        }
    }
}