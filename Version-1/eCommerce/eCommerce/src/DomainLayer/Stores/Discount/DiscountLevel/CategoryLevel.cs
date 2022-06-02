using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.DiscountLevel
{
    public class CategoryLevel : IDiscountLevel
    {
        public List<string> Categories { get; }

        public CategoryLevel(List<string> categories)
        {
            Categories = categories;
        }
        public List<Product> getProductsForDiscount(ConcurrentDictionary<Product, int> products)
        {
            List<Product> result = new List<Product>();
            foreach (KeyValuePair<Product, int> entry in products)
            {
                if (Categories.Contains(entry.Key.Category))
                    result.Add(entry.Key);
            }
            return result;
        }
    }
}
