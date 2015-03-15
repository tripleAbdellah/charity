using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DonorApp.Models;

namespace DonorApp.Controllers
{

	[Route("api/donors")]
    public class DonorsResource : ApiController
    {
        static readonly IDonorRepository repository = new DonorRepository();

        [HttpGet]
        [Route(Name="GetAllDonors")]
        public IEnumerable<Donor> GetAllDonors()
        {
        	try
        	{
            	return repository.GetAllDonors();
            }
            catch (NoDonorsFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route(Name="SearchDonors")]
        public IEnumerable<Donor> GetDonors([FromUri] SearchDonorRequest searchRequest)
        {
        	try
        	{
        		if (searchRequest.getCode() > 0) 
        		{
        			//	redirect
        		}
            	return repository.GetDonors(searchRequest);
            }
            catch (NoDonorsFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name="GetDonorById")]
        public Donor GetDonor(int donorID)
        {
        	try
        	{
            	return repository.GetDonor(donorID);
            }
            catch (DonorNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

		[HttpPost]
		[Route(Name="AddDonor")]
        public HttpResponseMessage AddDonor(Donor donor)
        {
        	try
        	{
            	donor = repository.AddDonor(donor);
            	var response = Request.CreateResponse<Donor>(HttpStatusCode.Created, donor);

	            string uri = Url.Link("GetDonorById", new { donorID = donor.CODE });
	            response.Headers.Location = new Uri(uri);
	            return response;
            }
            catch (InvalidDonorException ide)
            {
            	throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("{id:int}", Name="UpdateDonor")]
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
        [Route(Name="DeleteDonor")]
        public void DeleteDonor(int donorID)
        {
        	try
        	{
            	repository.DeleteDonor(donorID);
            }
            catch (DonorNotFoundException e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}