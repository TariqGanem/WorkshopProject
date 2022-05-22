using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.DiscountLevel
{
    public interface IDiscountLevel
    { 
        List<Product> getProductsForDiscount(ConcurrentDictionary<Product, int> products);
    }
}
