using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies
{
    public class ConditionalDiscount : AbstractDiscountPolicy
    {
        public IDiscountCondition Condition { set; get; }
        public IDiscountPolicy Discount { set; get; }

        public ConditionalDiscount(IDiscountPolicy discount, IDiscountCondition condition, String id = "") : base(
            new Dictionary<string, object>(), id)
        {
            Condition = condition;
            Discount = discount;
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            return new ConditionalDiscount(null, null);
        }

        public override Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products,
            string code = "")
        {
            if (Condition == null || Discount == null)
                return new Dictionary<Product, Double>();

            bool isEligible = Condition.isConditionMet(products);
            if (isEligible)
            {
                return Discount.CalculateDiscount(products, code);
            }

            return new Dictionary<Product, Double>();
        }

        public override bool AddDiscount(String id, IDiscountPolicy discount)
        {
            if (Id == id)
            {
                Discount = discount;
                var update_discount = Builders<BsonDocument>.Update.Set("Discount", discount.Id);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            else if (Discount != null)
                return Discount.AddDiscount(id, discount);

            return false;
        }

        public override IDiscountPolicy RemoveDiscount(String id)
        {
            if (Discount.Id.Equals(id))
            {
                IDiscountPolicy oldDiscount = Discount;
                Discount = null;
                var update_discount = Builders<BsonDocument>.Update.Set("Discount", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return oldDiscount;
            }
            else if (Discount != null)
                return Discount.RemoveDiscount(id);

            return null;
        }

        public override bool AddCondition(string id, IDiscountCondition condition)
        {
            if (Id == id)
            {
                Condition = condition;
                var update_discount = Builders<BsonDocument>.Update.Set("Condition", getConditonData(condition));
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            else if (Condition != null)
                return Condition.AddCondition(id, condition);

            return false;
        }
        
        private ConcurrentDictionary<string,string> getConditonData(IDiscountCondition condition)
        {
            ConcurrentDictionary<String, String> list = new ConcurrentDictionary<String, String>(); //<id , type>
            string[] type = condition.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];
            list.TryAdd(condition.Id, discount_type);
            return list;
        }

        public override IDiscountCondition RemoveCondition(string id)
        {
            if (Condition == null)
                return null;
            if (Condition.Id.Equals(id))
            {
                IDiscountCondition oldCondition = Condition;
                Condition = null;
                var update_discount = Builders<BsonDocument>.Update.Set("Condition", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return oldCondition;
            }
            return Condition.RemoveCondition(id);
        }

        public override IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Conditional"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<String, object>> children = new List<IDictionary<string, object>>();
            if (Condition != null)
            {
                IDictionary<string, object> conditionDataResult = Condition.GetData();
                children.Add(conditionDataResult);
            }

            if (Discount != null)
            {
                IDictionary<string, object> discountDataResult = Discount.GetData();
                children.Add(discountDataResult);
            }
            dict["children"] = children.ToArray();

            return dict;
        }

        public override bool EditDiscount(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                if (Discount != null)
                    return Discount.EditDiscount(info, id);
                return false;
            }

            return true;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (Discount != null)
                return Discount.EditCondition(info, id);
            return false;
        }

    }
}
