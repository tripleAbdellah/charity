using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DonorApp.Repositories;
using DonorApp.Models;

namespace DonarApp.Repositories.Mock
{
    public class AllocationsRepository : IAllocationsRepository
    {
        public List<Allocation> allocations = new List<Allocation>();

        public AllocationsRepository()
        {
            allocations.Add(new Allocation("Zakat"));
            allocations.Add(new PricedAllocation("Well", 50.0f));
            allocations.Add(new QurbaniAllocation("Palestine", "Lamb", 200.0f));
            allocations.Add(new OrphanAllocation("Child"));
            allocations.Add(new OrphanAllocation("Family"));
        }

        public IEnumerable<Allocation> GetAllAllocations()
        {
            return allocations.AsEnumerable();
        }
    }
}