using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DonorApp.Models;
using DonorApp.Repositories;

namespace DonorApp.Controllers
{

    [RoutePrefix("api/donors")]
    public class DonorsController : ApiController
    {
        static readonly IDonorsRepository repository = new DonorsRepository();

        [HttpGet]
        [Route("", Name = "SearchDonors")]
        public HttpResponseMessage GetDonors([FromUri] SearchDonorRequest searchRequest)
        {
            try
            {
                if (searchRequest == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                else
                {
                    if (searchRequest.Code > 0)
                    {
                        var response = Request.CreateResponse(HttpStatusCode.RedirectMethod);

                        string uri = Url.Link("GetDonorById", new { id = searchRequest.Code });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                    else
                    {
                        IEnumerable<Donor> donors = repository.GetDonors(searchRequest.validate());
                        return Request.CreateResponse<IEnumerable<Donor>>(HttpStatusCode.OK, donors);
                    }
                }

            }
            catch (NoDonorsFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (InvalidRequestException ire)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetDonorById")]
        public Donor GetDonor(int id)
        {
            try
            {
                return repository.GetDonor(id);
            }
            catch (DonorNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("", Name = "AddDonor")]
        public HttpResponseMessage AddDonor(Donor donor)
        {
            try
            {
                donor = repository.AddDonor(donor);
                var response = Request.CreateResponse<Donor>(HttpStatusCode.Created, donor);
               
                string uri = Url.Link("GetDonorById", new { id = donor.Code });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (InvalidDonorException ide)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

       
        [HttpPut]
        [Route("{donorID:int}", Name = "UpdateDonor")]
        public void UpdateDonor(int donorID, Donor donor)
        {
            try
            {
                repository.UpdateDonor(donorID, donor);
            }
            catch (DonorNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (InvalidDonorException ide)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
           
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteDonor")]
        public void DeleteDonor(int id)
        {
            try
            {
                repository.DeleteDonor(id);
            }
            catch (DonorNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

    }
}
