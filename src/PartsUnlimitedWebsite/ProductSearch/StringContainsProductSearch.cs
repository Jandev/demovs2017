using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PartsUnlimited.Models;
using PartsUnlimited.ViewModels;

namespace PartsUnlimited.ProductSearch
{
    public class StringContainsProductSearch : IProductSearch
    {
        private readonly IPartsUnlimitedContext _context;

        public StringContainsProductSearch(IPartsUnlimitedContext context)
        {
            _context = context;
        }

		// TODO: Change this to return List of ProductViewModel?
        public async Task<IEnumerable<ProductViewModel>> Search(string query)
        {
			try
			{
				var cleanQuery = Depluralize(query);

				var q = await _context.Products
					.Where(p => p.Title.ToLower().Contains(cleanQuery)).ToListAsync();

			    var productViewModelCollection = new List<ProductViewModel>();
			    foreach (var product in q)
			    {
                    productViewModelCollection.Add(new ProductViewModel
                    {
                        Product = product
                    });
			    }
			    return productViewModelCollection;
			}
			catch
			{
				return new List<ProductViewModel>();
			}
        }

		public string Depluralize(string query)
		{
			if (query.EndsWith("ies"))
			{
				query = query.Substring(0, query.Length - 3) + "y";
			}
			else if (query.EndsWith("es"))
			{
				query = query.Substring(0, query.Length - 1);
			}
			else if (query.EndsWith("s"))
			{
				query = query.Substring(1, query.Length);
			}
			return query.ToLowerInvariant();
		}
	}
}
