using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Types
{
    public interface IDiscountType
    {
        String Id { get; }

        Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "");

        bool AddDiscount(String id, IDiscountType discount);

        bool RemoveDiscount(String id);
    }

    public abstract class DiscountType : IDiscountType
    {
        public string Id { get; }

        public DiscountType(String id = "")
        {
            if (id.Equals(""))
                Id = Service.GenerateId();
            else
                Id = id;
        }

        public abstract Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "");
        public abstract bool AddDiscount(string id, IDiscountType discount);
        public abstract bool RemoveDiscount(string id);
    }
}
