using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition
{
    public class DiscountAddition : AbstractDiscountPolicy
    {
        public List<IDiscountPolicy> Discounts { get; }

        public DiscountAddition(String id = "") : base(new Dictionary<string, object>(), id)
        {
            Discounts = new List<IDiscountPolicy>();
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            return new DiscountAddition();
        }

        public DiscountAddition(List<IDiscountPolicy> discounts, String id = "") : base(new Dictionary<string, object>(), id)
        {
            Discounts = discounts;
            if (Discounts == null)
                Discounts = new List<IDiscountPolicy>();
        }

        public override Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            Dictionary<Product, Double> result = new Dictionary<Product, Double>();
            foreach (IDiscountPolicy discountPolicy in Discounts)
            {
                Dictionary<Product, Double> discountResult = discountPolicy.CalculateDiscount(products, code);
                if (discountResult == null)
                    continue;
                foreach (KeyValuePair<Product, Double> entry in discountResult)
                {
                    if (result.ContainsKey(entry.Key))
                        if (result[entry.Key] + entry.Value > 100)
                            result[entry.Key] = 100;
                        else
                            result[entry.Key] = result[entry.Key] + entry.Value;
                    else
                        result[entry.Key] = entry.Value;
                }
            }
            return result;
        }

        public override bool AddDiscount(String id, IDiscountPolicy discount)
        {
            if (Id.Equals(id))
            {
                Discounts.Add(discount);
                var update_discount = Builders<BsonDocument>.Update.Set("Discounts", ConvertDiscountToIDs());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                bool result = myDiscount.AddDiscount(id, discount);
                if (result)
                    return result;
            }
            return false;
        }

        public override IDiscountPolicy RemoveDiscount(String id)
        {
            IDiscountPolicy toBeRemoved = getDiscount(id);
            if (toBeRemoved != null)
            {
                Discounts.Remove(toBeRemoved);
                var update_discount = Builders<BsonDocument>.Update.Set("Discounts", ConvertDiscountToIDs());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return toBeRemoved;
            }
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                IDiscountPolicy result = myDiscount.RemoveDiscount(id);
                if (result != null)
                    return result;
            }
            return null;
        }

        private IDiscountPolicy getDiscount(String id)
        {
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                if (myDiscount.Id.Equals(id))
                    return myDiscount;
            }
            return null;
        }

        public override bool AddCondition(string id, IDiscountCondition condition)
        {
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                bool result = myDiscount.AddCondition(id, condition);
                if (result)
                    return result;
            }
            return false;
        }

        public override IDiscountCondition RemoveCondition(string id)
        {
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                IDiscountCondition result = myDiscount.RemoveCondition(id);
                if (result != null)
                    return result;
            }
            return null;
        }

        public override IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Add"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> children = new List<IDictionary<string, object>>();
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                IDictionary<string, object> discountResult = myDiscount.GetData();
                children.Add(discountResult);
            }
            dict["children"] = children.ToArray();
            return dict;
        }

        public override bool EditDiscount(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                foreach (IDiscountPolicy myDiscount in Discounts)
                {
                    bool result = myDiscount.EditCondition(info, id);
                    if (result)
                        return result;
                }
                return false;
            }

            return true;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            foreach (IDiscountPolicy myDiscount in Discounts)
            {
                bool result = myDiscount.EditCondition(info, id);
                if (result)
                    return result;
            }
            return false;
        }

        public DTO_DiscountAddition getDTO()
        {
            ConcurrentDictionary<String, String> Discounts_dto = new ConcurrentDictionary<string, string>();
            foreach (var dis in Discounts)
            {
                Discounts_dto.TryAdd("DiscountAddition", dis.Id);
            }
            return new DTO_DiscountAddition(this.Id, Discounts_dto);
        }

        public ConcurrentDictionary<String, String> ConvertDiscountToIDs()
        {
            ConcurrentDictionary<String, String> list = new ConcurrentDictionary<String, String>();    //<id , type>

            foreach (var discount in Discounts)
            {
                string[] type = discount.GetType().ToString().Split('.');
                string discount_type = type[type.Length - 1];
                list.TryAdd(discount.Id, discount_type);
            }

            return list;
        }
    }
}
