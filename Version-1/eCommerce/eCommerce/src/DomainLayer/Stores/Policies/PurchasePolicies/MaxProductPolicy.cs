using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Store;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class MaxProductPolicy : IPurchasePolicy
    {
        public string ProductId { get; set; }
        public int Max { get; set; }
        public string Id { get; }

        public MaxProductPolicy(string productId, int max, string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.ProductId = productId;
            this.Max = max;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MaxProductPolicy: ";
            if (!info.ContainsKey("ProductId"))
                throw new Exception(errorMsg + "ProductId not found");
            string productId = ((JsonElement)info["ProductId"]).GetString();
            if (!info.ContainsKey("Max"))
                throw new Exception(errorMsg + "Max not found");
            int max = ((JsonElement)info["Max"]).GetInt32();
            return new MaxProductPolicy(productId, max);
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            int count;
            Product product = ContainsProduct(bag);
            if (product == null)
                return true;
            bag.TryGetValue(product, out count);
            return count <= this.Max;
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            if (this.Id.Equals(id))
                throw new Exception("Cannot add a policy to this type of policy");
            return false ;
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            return null;
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", ProductId + " <= " + Max },
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("Max"))
            {
                Max = ((JsonElement)info["Max"]).GetInt32();
                var update_discount = Builders<BsonDocument>.Update.Set("Max", Max);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("ProductId"))
            {
                ProductId = ((JsonElement)info["ProductId"]).GetString();
                var update_discount = Builders<BsonDocument>.Update.Set("ProductId", ProductId);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            return true;
        }

        private Product ContainsProduct(ConcurrentDictionary<Product, int> products)
        {
            foreach (KeyValuePair<Product, int> entry in products)
            {
                if (entry.Key.Id.Equals(ProductId))
                    return entry.Key;
            }
            return null;
        }

        public DTO_MaxProductPolicy getDTO()
        {
            return new DTO_MaxProductPolicy(this.Id, this.ProductId, this.Max);
        }
    }
}
