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
    public class ConditionalPolicy : IPurchasePolicy
    {
        public IPurchasePolicy PreCond { get; set; }
        public IPurchasePolicy Cond { get; set; }
        public string Id { get; }

        public ConditionalPolicy(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.PreCond = null;
            this.Cond = null;
        }

        public ConditionalPolicy(IPurchasePolicy preCond, IPurchasePolicy cond, string id = "") : this(id)
        {
            this.PreCond = preCond;
            this.Cond = cond;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            return new ConditionalPolicy();
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            if (this.PreCond == null || this.Cond == null)
                return false;
            if (this.PreCond.IsConditionMet(bag, user))
                if (!this.Cond.IsConditionMet(bag, user))
                    return false;
            return true;
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            if (this.Id.Equals(id))
            {
                if (this.PreCond == null)
                {
                    this.PreCond = policy;
                    var update_discount = Builders<BsonDocument>.Update.Set("PreCond", PreCond.Id);
                    DBUtil.getInstance().UpdatePolicy(this, update_discount);
                }
                else if (this.Cond == null)
                {
                    this.Cond = policy;
                    var update_discount = Builders<BsonDocument>.Update.Set("Cond", Cond.Id);
                    DBUtil.getInstance().UpdatePolicy(this, update_discount);
                }
                else
                    throw new Exception("Cannot add a policy to this type of policy");
            }
            else
            {
                if (this.PreCond != null)
                {
                    bool curr = this.PreCond.AddPolicy(policy, id);
                    if (curr)
                        return true;
                }
                if (this.Cond != null)
                {
                    bool curr = this.Cond.AddPolicy(policy, id);
                    if (curr)
                        return true;
                }
            }
            return false;
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            if (PreCond != null && PreCond.Id.Equals(id))
            {
                IPurchasePolicy temp = PreCond;
                PreCond = null;
                var update_discount = Builders<BsonDocument>.Update.Set("PreCond", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return temp;
            }
            else if (Cond != null && Cond.Id.Equals(id))
            {
                IPurchasePolicy temp = Cond;
                Cond = null;
                var update_discount = Builders<BsonDocument>.Update.Set("Cond", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return temp;
            }

            IPurchasePolicy res = PreCond.RemovePolicy(id);
            if (res != null)
                return res;

            res = Cond.RemovePolicy(id);
            if (res != null)
                return res;
            return null;
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Conditional"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> children = new List<IDictionary<string, object>>();
            if (PreCond != null)
            {
                IDictionary<string, object> PreCondResult = PreCond.GetData();
                children.Add(PreCondResult);
            }
            if (Cond != null)
            {
                IDictionary<string, object> CondResult = Cond.GetData();
                children.Add(CondResult);
            }
            dict["children"] = children.ToArray();
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                if (PreCond != null)
                {
                    bool result = PreCond.EditPolicy(info, id);
                    if (result)
                        return result;
                }
                if (Cond != null)
                {
                    bool result = Cond.EditPolicy(info, id);
                    if (result)
                        return result;
                }
                return false;
            }
            return true;
        }

        public DTO_ConditionalPolicy getDTO()
        {
            List<IPurchasePolicy> list = new List<IPurchasePolicy>();
            list.Add(this.PreCond);
            ConcurrentDictionary<String, String> PreCond = getPoliciesIDs(list);

            List<IPurchasePolicy> list2 = new List<IPurchasePolicy>();
            list2.Add(this.Cond);
            ConcurrentDictionary<String, String> Cond = getPoliciesIDs(list2);


            return new DTO_ConditionalPolicy(this.Id, PreCond, Cond);
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
    }
}
