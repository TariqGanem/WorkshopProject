using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Types
{
    public class VisibleDiscount : DiscountType
    {
        public DateTime ExpirationDate { get; }
        public Policies.DiscountLevel.IDiscountLevel Target { get; }
        public Double Percentage { get; }

        public VisibleDiscount(DateTime expirationDate, Policies.DiscountLevel.IDiscountLevel target, Double percentage, String id = "") : base(id)
        {
            ExpirationDate = expirationDate;
            Target = target;
            if (percentage > 100)
                Percentage = 100;
            else if (percentage < 0)
                Percentage = 0;
            else
                Percentage = percentage;
        }

        public override bool AddDiscount(string id, IDiscountType discount)
        {
            if (this.Id.Equals(id))
                throw new Exception("Cant Add Discount");
            return true;
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            if (DateTime.Now.CompareTo(ExpirationDate) >= 0)
                throw new Exception($"Discount {this.Id} Expired !");

            List<Product> targetProducts = Target.getProductsForDiscount(products);
            Dictionary<Product, Double> resultDictionary = new Dictionary<Product, Double>();
            foreach (Product product in targetProducts)
            {
                resultDictionary.Add(product, Percentage);
            }
            return resultDictionary;
        }

        public override bool RemoveDiscount(string id)
        {
            return true;
        }
    }
}
