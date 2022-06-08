using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions
{
    public class DiscountConditionAnd : AbstractDiscountCondition
    {
        public List<IDiscountCondition> Conditions { get; }

        public DiscountConditionAnd(String id = "") : base(new Dictionary<string, object>(), id)
        {
            Conditions = new List<IDiscountCondition>();
        }

        public static IDiscountCondition create(Dictionary<string, object> info)
        {
            return new DiscountConditionAnd();
        }

        public DiscountConditionAnd(List<IDiscountCondition> conditions, String id = "") : base(new Dictionary<string, object>(), id)
        {
            Conditions = conditions;
            if (Conditions == null)
                Conditions = new List<IDiscountCondition>();
        }

        public override bool isConditionMet(ConcurrentDictionary<Product, int> products)
        {
            foreach (IDiscountCondition myCondition in Conditions)
            {
                bool result = myCondition.isConditionMet(products);
                if (!result)
                    return result;
            }
            return true;
        }

        public override bool AddCondition(string id, IDiscountCondition condition)
        {
            if (Id.Equals(id))
            {
                Conditions.Add(condition);
                var update_discount = Builders<BsonDocument>.Update.Set("Conditions", ConvertDiscountToIDs());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            foreach (IDiscountCondition myCondition in Conditions)
            {
                bool result = myCondition.AddCondition(id, condition);
                if (result)
                    return result;
            }
            return false;
        }

        public override IDiscountCondition RemoveCondition(string id)
        {
            IDiscountCondition toBeRemoved = getCondition(id);
            if (toBeRemoved != null)
            {
                Conditions.Remove(toBeRemoved);
                var update_discount = Builders<BsonDocument>.Update.Set("Conditions", ConvertDiscountToIDs());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return toBeRemoved;
            }
            foreach (IDiscountCondition myCondition in Conditions)
            {
                IDiscountCondition result = myCondition.RemoveCondition(id);
                if (result != null)
                    return result;
            }
            return null;
        }

        private IDiscountCondition getCondition(String id)
        {
            foreach (IDiscountCondition myCondition in Conditions)
            {
                if (myCondition.Id.Equals(id))
                    return myCondition;
            }
            return null;
        }

        public override IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "And"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> conditionsList = new List<IDictionary<string, object>>();
            foreach (IDiscountCondition myCondition in Conditions)
            {
                IDictionary<string, object> conditionResult = myCondition.GetData();
                conditionsList.Add(conditionResult);
            }
            dict["children"] = conditionsList.ToArray();
            return dict;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                foreach (IDiscountCondition myCondition in Conditions)
                {
                    bool result = myCondition.EditCondition(info, id);
                    if (result)
                        return result;
                }
                return false;
            }
            return true;
        }


        private ConcurrentDictionary<String, String> ConvertDiscountToIDs()
        {
            ConcurrentDictionary<String, String> list = new ConcurrentDictionary<String, String>();    //<id , type>

            foreach (var discount in Conditions)
            {
                string[] type = discount.GetType().ToString().Split('.');
                string discount_type = type[type.Length - 1];
                list.TryAdd(discount.Id, discount_type);
            }

            return list;
        }
    }
}
