using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies
{
    internal class ConditionalPolicy : LogicPolicy
    {
        public ConditionalPolicy(string id = "") : base(id)
        {

        }
        public ConditionalPolicy(List<LogicPolicy> policies, string id = "") : base(policies, id)
        {
            if (policies.Count < 2)
                throw new Exception("In Conditional Policy 2 or more polices are needed, (pre-condition and condition)!");
        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            /*            bool binary_pred = true;
                        bool res = true;
                        for (int i = Policies.Count - 1; i >= 1; i--)
                        {
                            if (!Policies[i].IsSatisfiedCond(bag, user) &&
                                Policies[i - 1].IsSatisfiedCond(bag, user))
                                binary_pred = false;
                            res
                        }
                        return res;*/
            int i = Policies.Count - 1;
            return IsSatisfiedCond(bag, user, Policies[i].IsSatisfiedCond(bag, user), true, i);
        }
        public bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user, bool p, bool q, int i)
        {
            //q if p , (p=>q)
            bool _p = true;
            if (i < 2)
            {
                if (p && !q)
                    return false;
                return true;
            }
            // TODO: LATER WE IMPLEMENT THIS RECURSIVELY!

            else if (p && !IsSatisfiedCond(bag, user, Policies[i].IsSatisfiedCond(bag, user), q, i - 1)) { return false; } else { return true; }
        }
    }
}
