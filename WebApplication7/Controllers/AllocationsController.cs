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
    [RoutePrefix("api/allocations")]
    public class AllocationsController : ApiController
    {
        static readonly IAllocationsRepository repository = new DonarApp.Repositories.Mock.AllocationsRepository();

        [HttpGet]
        [Route("", Name = "GetAllocations")]
        public IEnumerable<Allocation> GetAllAllocations()
        {
            IEnumerable<Allocation> Allocations = repository.GetAllAllocations();
            return Allocations;
        }
        
        
        //To retrieve orphan details donor_code is required, otherwise Orphans are not able to be retrieved 

        [HttpGet]
        [Route("{DonorCode:int}", Name = "GetAllocationsByDonorCode")]
        public IEnumerable<Allocation> GetAllAllocations(int DonorCode)
        {
            IEnumerable<Allocation> Allocations = null; 
            try
            {
               Allocations = repository.GetAllAllocations(DonorCode);
            }
            catch (NoOrphansFoundException)
            {
                //not sure what to do; because not all Donors have orphans
                //perhaps return the allocations without the orphans detail? 
                return Allocations; 
              
            }

            return Allocations;
        }
        
        
    }
}
