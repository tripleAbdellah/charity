using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DonorApp.Repositories;
using DonorApp.Models;

namespace DonorApp.Controllers
{
    [RoutePrefix("api/donors/{id:int}/donations")]
    public class DonationsController : ApiController
    {
        static readonly IDonationsRepository repository = new DonationsRepository();

        [HttpGet]
        [Route("", Name = "GetDonations")]
        public HttpResponseMessage GetDonations(int id)
        {
            try
            {
                IEnumerable<Donation> donations = repository.GetDonations(id);
                return Request.CreateResponse<IEnumerable<Donation>>(HttpStatusCode.OK, donations);
            }
            catch (DonorNotFoundException dnfe)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (NoDonationsFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        [HttpPost]
        [Route("", Name = "Donate")]
        public HttpResponseMessage Donate(int id, Donation donation)
        {
            try
            {
                repository.AddDonation(id, donation);
                return Request.CreateResponse(HttpStatusCode.Created);

            }
            catch (InvalidDonationException ide)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
