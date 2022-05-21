using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies.Primitive
{
    internal class AgePolicy : PrimitivePolicy
    {
        public int MinAge { get; set; }

        public AgePolicy(int minAge, string id = "") : base(id)
        {
            if (minAge < 0)
                throw new Exception("minimum Age cant be negative!");

            MinAge = minAge;

        }
        public override PrimitivePolicy Create(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("Age"))
                throw new Exception("Age must be in the Keys!");
            int age = ((JsonElement)info["Age"]).GetInt32();
            return new AgePolicy(age);
        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            //return user.Age >= MinAge;
            throw new NotImplementedException();
        }
    }
}
