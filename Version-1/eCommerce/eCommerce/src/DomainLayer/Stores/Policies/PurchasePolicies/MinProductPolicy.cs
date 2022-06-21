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
    public class MinProductPolicy : IPurchasePolicy
    {
        public string ProductId { get; set; }
        public int Min { get; set; }
        public string Id { get; }

        public MinProductPolicy(string productId, int min, string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.ProductId = productId;
            this.Min = min;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MinProductPolicy: ";
            if (!info.ContainsKey("ProductId"))
                throw new Exception(errorMsg + "ProductId not found");
            string productId = ((string)info["ProductId"]);

            if (!info.ContainsKey("Min"))
                throw new Exception(errorMsg + "Min not found");
            int min = int.Parse((string)info["Min"]);

            return new MinProductPolicy(productId, min);
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            int count;
            Product product = ContainsProduct(bag);
            if (product == null)
                return true;
            bag.TryGetValue(product, out count);
            return count >= this.Min;
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            if (this.Id.Equals(id))
                throw new Exception("Cannot add a policy to this type of policy");
            return false;
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            return null;
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", ProductId + " >= " + Min },
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("Min"))
            {
                Min = int.Parse((string)info["Min"]);
                var update_discount = Builders<BsonDocument>.Update.Set("Min", Min);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("ProductId"))
            {
                ProductId = ((string)info["ProductId"]);
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

        public DTO_MinProductPolicy getDTO()
        {
            return new DTO_MinProductPolicy(this.Id, this.ProductId, this.Min);
        }
    }
}
