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
    }
}