using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorApp.Models;

namespace DonorApp.Repositories
{
    public interface IAllocationRepository
    {
        IEnumerable<ShoppingCategory> GetShoppingCategoryList();
    }
}