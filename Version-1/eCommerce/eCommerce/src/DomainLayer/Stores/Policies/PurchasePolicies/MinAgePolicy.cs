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
    public class MinAgePolicy : IPurchasePolicy
    {
        public int Age { get; set; }
        public string Id { get; }

        public MinAgePolicy(int age, string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.Age = age;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MinAgePolicy: ";

            if (!info.ContainsKey("Age"))
                throw new Exception(errorMsg + "Age not found");
            int age = ((JsonElement)info["Age"]).GetInt32();

            return new MinAgePolicy(age);
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            return true;
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
                { "name", "Age >= " + Age },
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("Age"))
            {
                Age = ((JsonElement)info["Age"]).GetInt32();
                var update_discount = Builders<BsonDocument>.Update.Set("Age", Age);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }
            return true;
        }

        public DTO_MinAgePolicy getDTO()
        {
            return new DTO_MinAgePolicy(this.Id, this.Age);
        }
    }
}
