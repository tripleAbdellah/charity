using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DonorApp.Models;

namespace DonorApp.Repositories
{
    public interface IAllocationsRepository
    {
        //   throws NoAllocationsFoundException
        IEnumerable<Allocation> GetAllAllocations();

    }
}