using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies
{
    internal abstract class LogicPolicy
    {
        public string Id { get; }
        public List<LogicPolicy> Policies { get; }

        public abstract bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user);

        protected LogicPolicy(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.Policies = new List<LogicPolicy>();
        }
        protected LogicPolicy(List<LogicPolicy> policies, string id = "") : this(id)
        {
            if (policies != null)
                this.Policies = policies;
        }
        public bool AddPolicy(LogicPolicy policy)
        {
            LogicPolicy p = Policies.Find(p => p.Id.Equals(policy.Id));
            if (p != null)
            {
                throw new Exception("Policy already exists, therefore can't be added!");
            }

            Policies.Add(policy);
            return true;
        }

        public LogicPolicy RemovePolicy(string id)
        {
            LogicPolicy policy = Policies.Find(policy => policy.Id.Equals(id));
            if (policy != null)
            {
                Policies.Remove(policy);
                return policy;
            }
            throw new Exception("Policy not found, therefore can't be removed!");
        }
    }
}
