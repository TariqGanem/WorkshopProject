using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies.Primitive
{
    internal class QuantityPolicy : PrimitivePolicy
    {
        public string ProductId { get; set; }
        public string Category { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public QuantityPolicy(string productId = null, int min = -1, int max = -1, string category = null, string id = "") : base(id)
        {
            if (min == -1 && max == -1)
                throw new Exception("One of Min or Max should be given a value.");
            if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(category))
                throw new Exception("One of productId or Category should be given a value.");
            if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(category))
                throw new Exception("One of productId or Category should be given a value but not both!.");

            if (!string.IsNullOrEmpty(productId))
            {
                this.ProductId = productId;
                this.Category = null;
            }

            if (!string.IsNullOrEmpty(category))
            {
                this.ProductId = null;
                this.Category = category;
            }

            if (min == -1)
                this.Min = 0;
            this.Min = min;
            if (max == -1)
                this.Max = int.MaxValue;
            this.Max = max;
        }

        public override PrimitivePolicy Create(Dictionary<string, object> info, IPurchasePolicy policy = null)
        {
            if (!info.ContainsKey("ProductId") && !info.ContainsKey("Category"))
                throw new Exception("ProductId and Category are not found in Keys!");

            if (info.ContainsKey("ProductId") && info.ContainsKey("Category"))
                throw new Exception("ProductId and Category can't be together in Keys!");

            string productId = null;
            string category = null;
            if (info.ContainsKey("ProductId"))
                productId = ((JsonElement)info["ProductId"]).GetString();
            if (info.ContainsKey("Category"))
                category = ((JsonElement)info["Category"]).GetString();

            if (!info.ContainsKey("Min") && !info.ContainsKey("Max"))
                throw new Exception("Max or Min not found in Keys!");

            int max = -1;
            int min = -1;
            if (info.ContainsKey("Max"))
                max = ((JsonElement)info["Max"]).GetInt32();
            if (info.ContainsKey("Min"))
                min = ((JsonElement)info["Max"]).GetInt32();

            return new QuantityPolicy(productId, min, max, category);
        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            if (String.IsNullOrEmpty(Category))
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
            else if (String.IsNullOrEmpty(ProductId))
            {
                foreach (KeyValuePair<Product, int> entry in bag)
                {
                    if (entry.Key.Category.Equals(Category))
                    {
                        Product product = entry.Key;
                        if (product != null)
                        {
                            int quantity = 0;
                            bag.TryGetValue(product, out quantity);
                            if (!(quantity >= Min && quantity <= Max))
                                return false;
                        }
                    }
                }
            }

            return false || Max == 0;
        }
    }
}
