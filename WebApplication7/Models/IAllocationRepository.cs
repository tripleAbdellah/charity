using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorApp.Models
{
    public interface IAllocationRepository
    {
        IEnumerable<ShoppingCategory> GetShoppingCategoryList();
    }
}