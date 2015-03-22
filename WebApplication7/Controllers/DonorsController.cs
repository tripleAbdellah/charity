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
        //static readonly IDonorsRepository repository = new DonorsRepository();
        static readonly IDonorsRepository repository = new DonorApp.Repositories.Mock.DonorsRepository();

        [HttpGet]
        [Route("", Name="SearchDonors")]
        public HttpResponseMessage GetDonors([FromUri] SearchDonorRequest searchRequest)
        {
        	try
        	{
        		if (searchRequest == null) 
        		{
                    IEnumerable<Donor> donors = repository.GetAllDonors();
                    return Request.CreateResponse<IEnumerable<Donor>>(HttpStatusCode.OK, donors);
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
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (InvalidRequestException ire)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name="GetDonorById")]
        public HttpResponseMessage GetDonor(int id)
        {
        	try
        	{
                Donor donor = repository.GetDonor(id);
                return Request.CreateResponse<Donor>(HttpStatusCode.OK, donor);
            }
            catch (DonorNotFoundException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        
		[HttpPost]
		[Route("", Name="AddDonor")]
        public HttpResponseMessage AddDonor(Donor donor)
        {
        	try
        	{
            	//donor = repository.AddDonor(donor);
                var donorID = repository.AddDonor(donor);
                var response = Request.CreateResponse(HttpStatusCode.Created);

	            //string uri = Url.Link("GetDonorById", new { id = donor.CODE });
                string uri = Url.Link("GetDonorById", new { id = donorID });
                response.Headers.Location = new Uri(uri);
	            return response;
            }
            catch (InvalidDonorException ide)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("{id:int}", Name="UpdateDonor")]
        public HttpResponseMessage UpdateDonor(int id, Donor donor)
        {
        	try
        	{
            	repository.UpdateDonor(id, donor);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (DonorNotFoundException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (InvalidDonorException ide)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteDonor")]
        public HttpResponseMessage DeleteDonor(int id)
        {
        	try
        	{
            	repository.DeleteDonor(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (DonorNotFoundException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}