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
    public class OrPolicy : IPurchasePolicy
    {
        public List<IPurchasePolicy> Policies { get; }
        public string Id { get; }

        public OrPolicy(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.Policies = new List<IPurchasePolicy>();
        }
        public OrPolicy(List<IPurchasePolicy> policies, string id = "") : this(id)
        {
            if (policies != null)
                this.Policies = policies;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            return new OrPolicy();
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            if (Policies.Count == 0)
            {
                return true;
            }
            foreach (IPurchasePolicy policy in Policies)
            {
                if (policy.IsConditionMet(bag, user))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            if (this.Id.Equals(id))
            {
                Policies.Add(policy);
                var update_discount = Builders<BsonDocument>.Update.Set("Policies", getPolicis_dto());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            foreach (IPurchasePolicy p in Policies)
            {
                bool curr = p.AddPolicy(policy, id);
                if (curr)
                    return true;
            }
            return false;

        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            IPurchasePolicy policy = Policies.Find(policylocal => policylocal.Id.Equals(id));
            if (policy != null)
            {
                Policies.Remove(policy);
                var update_discount = Builders<BsonDocument>.Update.Set("Policies", getPolicis_dto());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return policy;
            }

            foreach (IPurchasePolicy curr in Policies)
            {
                IPurchasePolicy res = curr.RemovePolicy(id);
                if (res != null)
                    return res;
            }
            return null;
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Or"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> children = new List<IDictionary<string, object>>();
            foreach (IPurchasePolicy myPolicy in Policies)
            {
                IDictionary<string, object> purchasePolicyResult = myPolicy.GetData();
                children.Add(purchasePolicyResult);
            }
            dict["children"] = children.ToArray();
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                foreach (IPurchasePolicy myDiscount in Policies)
                {
                    bool result = myDiscount.EditPolicy(info, id);
                    if (result)
                        return result;
                }
                return false ;
            }

            return true;
        }

        public DTO_OrPolicy getDTO()
        {
            return new DTO_OrPolicy(this.Id, getPoliciesIDs(this.Policies));
        }

        private ConcurrentDictionary<String, String> getPoliciesIDs(List<IPurchasePolicy> list)
        {
            ConcurrentDictionary<String, String> Policies = new ConcurrentDictionary<String, String>();
            foreach (IPurchasePolicy policy in list)
            {
                string[] type = policy.GetType().ToString().Split('.');
                string policy_type = type[type.Length - 1];
                Policies.TryAdd(policy_type, policy.Id);
            }
            return Policies;
        }

        public ConcurrentDictionary<String, String> getPolicis_dto()
        {
            ConcurrentDictionary<String, String> policies_dto = new ConcurrentDictionary<String, String>();
            foreach (IPurchasePolicy policy in Policies)
            {
                string[] type = policy.GetType().ToString().Split('.');
                string policy_type = type[type.Length - 1];
                policies_dto.TryAdd(policy.Id, policy_type);
            }

            return policies_dto;
        }
    }
}
