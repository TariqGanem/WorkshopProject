using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets
{
    public class DiscountTargetProducts : IDiscountTarget
    {
        public String Id { get; set; }
        public List<string> ProductIds { get; }

        public DiscountTargetProducts(List<string> productIds)
        {
            Id = Service.GenerateId();
            ProductIds = productIds;
        }

        // for loading from db
        public DiscountTargetProducts(string id, List<string> productIds)
        {
            Id = id;
            ProductIds = productIds;
        }

        public static IDiscountTarget create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create DiscountTargetProducts: ";
            if (!info.ContainsKey("ProductIds"))
                throw new Exception(errorMsg + "ProductIds not found");
            List<string> productIds = createProductsList((JsonElement)info["ProductIds"]);
            return new DiscountTargetProducts(productIds);
        }

        private static List<string> createProductsList(JsonElement productsElement)
        {
            List<string> products = JsonSerializer.Deserialize<List<string>>(productsElement.GetRawText());
            return products;
        }

        public List<Product> getTargets(ConcurrentDictionary<Product, int> products)
        {
            List<Product> result = new List<Product>();
            foreach (KeyValuePair<Product, int> entry in products)
            {
                if (ProductIds.Contains(entry.Key.Id))
                    result.Add(entry.Key);
            }
            return result;
        }

        public String GetData()
        {
            String answer = "";
            foreach (String productId in ProductIds)
                answer += productId + ", ";
            if (ProductIds.Count > 0)
                answer = answer.Substring(0, answer.Length - 2);
            return answer;
        }

        public string getId()
        {
            return this.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is DiscountTargetProducts products &&
                   Id == products.Id &&
                   EqualityComparer<List<string>>.Default.Equals(ProductIds, products.ProductIds);
        }
    }
}
