using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(ProductModel product, int quantity = 1)
        {
            CartLine line = lineCollection.Where(item => item.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(ProductModel product) => lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        //public virtual IEnumerable<CartLine> Lines() { return lineCollection; }
        public virtual IEnumerable<CartLine> Lines => lineCollection;

    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}


