using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Store;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class BuyNow : IPurchasePolicyType
    {
        public AndPolicy Policy { get; set; }
        public string Id { get; }

        public BuyNow(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.Policy = new AndPolicy();
        }

        public BuyNow(AndPolicy policy, string id)
        {
            Policy = policy;
            Id = id;
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            if (this.Policy.IsConditionMet(bag, user))
                return true;
            throw new Exception("Policy conditions not met");
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            return this.Policy.AddPolicy(policy, id);
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            if (Policy.Id.Equals(id))
            {
                IPurchasePolicy temp = this.Policy;
                this.Policy = new AndPolicy();
                var update_discount = Builders<BsonDocument>.Update.Set("Policy", Policy.Id);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                DBUtil.getInstance().UpdateBuyNowPolicy(filter, update_discount);
                return temp;
            }
            return Policy.RemovePolicy(id);
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "BuyNow"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> children = new List<IDictionary<string, object>>();
            IDictionary<string, object> PolicyResult = Policy.GetData();
            children.Add(PolicyResult);
            dict["children"] = children.ToArray();
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id.Equals(id))
               throw new Exception("Can't edit the main purchase type");
            if (Policy.Id.Equals(id))
                throw new Exception("Can't edit the main 'And node'");
            return Policy.EditPolicy(info, id);
        }

        public DTO_BuyNow getDTO()
        {
            return new DTO_BuyNow(this.Id, new DTO_AndPolicy(this.Policy.Id, Policy.getPolicis_dto()));
        }
    }
}
