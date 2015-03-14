using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DonorApp.Models;

namespace DonorApp.Controllers
{
    public class AllocationController : ApiController
    {
        static readonly IAllocationRepository repository = new AllocationRepository();

        public HttpResponseMessage GetShoppingCategoryList()
        {
            IEnumerable<ShoppingCategory> shoppingCategoryList;
            shoppingCategoryList = repository.GetShoppingCategoryList();
            var response = Request.CreateResponse<IEnumerable<ShoppingCategory>>(HttpStatusCode.Created, shoppingCategoryList);

            string uri = Url.Link("DefaultApi", null);
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
  
}