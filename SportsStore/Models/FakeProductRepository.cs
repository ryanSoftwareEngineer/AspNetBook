using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository /*: IProductRepository*/
    {
        public IQueryable<ProductModel> Products => new List<ProductModel> {
            new ProductModel { Name = "Football", Price = 25 },
            new ProductModel { Name = "Surf board", Price = 179 },
            new ProductModel { Name = "Running shoes", Price = 95 }
        }.AsQueryable<ProductModel>();

        public void SaveProduct(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}