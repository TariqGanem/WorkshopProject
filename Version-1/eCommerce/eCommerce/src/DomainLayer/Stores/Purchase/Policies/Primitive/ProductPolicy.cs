using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies.Primitive
{
    internal class ProductPolicy : PrimitivePolicy
    {
        public string ProductId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public ProductPolicy(string productId, int min = -1, int max = -1, string id = "") : base(id)
        {
            if (min == -1 && max == -1)
                throw new Exception("One of min or max should be given a value.");
            this.ProductId = productId;
            if (min == -1)
                this.Min = 0;
            this.Min = min;
            if (max == -1)
                this.Max = int.MaxValue;
            this.Max = max;
        }

        public override PrimitivePolicy Create(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("ProductId"))
                throw new Exception("ProductId not found in Keys!");
            string productId = ((JsonElement)info["ProductId"]).GetString();

            if (!info.ContainsKey("Min") && !info.ContainsKey("Max"))
                throw new Exception("Max or Min not found in Keys!");

            int max = -1;
            int min = -1;
            if (info.ContainsKey("Max"))
                max = ((JsonElement)info["Max"]).GetInt32();
            if (info.ContainsKey("Min"))
                min = ((JsonElement)info["Max"]).GetInt32();

            return new ProductPolicy(productId, min, max);
        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            foreach (KeyValuePair<Product, int> entry in bag)
            {
                if (entry.Key.Id.Equals(ProductId))
                {
                    Product product = entry.Key;
                    if (product != null)
                    {
                        int quantity = 0;
                        bag.TryGetValue(product, out quantity);
                        return quantity >= Min && quantity <= Max;
                    }
                }
            }
            return false;
        }
    }
}
