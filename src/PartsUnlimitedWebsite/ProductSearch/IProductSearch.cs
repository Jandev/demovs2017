using System.Collections.Generic;
using System.Threading.Tasks;
using PartsUnlimited.Models;
using PartsUnlimited.ViewModels;

namespace PartsUnlimited.ProductSearch
{
    public interface IProductSearch
    {
        Task<IEnumerable<ProductViewModel>> Search(string query);
    }
}
