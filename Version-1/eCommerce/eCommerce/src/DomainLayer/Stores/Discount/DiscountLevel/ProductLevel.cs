using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.DiscountLevel
{
    public class ProductLevel : IDiscountLevel
    {
        public List<Product> Products { get; }

        public ProductLevel(List<Product> products)
        {
            Products = products;
        }
        public List<Product> getProductsForDiscount(ConcurrentDictionary<Product, int> products)
        {
            List<Product> result = new List<Product>();
            foreach (Product product in Products)
            {
                if (products.ContainsKey(product))
                    result.Add(product);
            }
            return result;
        }
    }
}
