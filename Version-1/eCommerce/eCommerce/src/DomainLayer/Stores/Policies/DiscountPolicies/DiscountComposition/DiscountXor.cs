using eCommerce.src.DataAccessLayer;
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
    public class DiscountXor : AbstractDiscountPolicy
    {
        public IDiscountPolicy Discount1 { set; get; }
        public IDiscountPolicy Discount2 { set; get; }
        public IDiscountCondition ChoosingCondition { set; get; }

        public DiscountXor(IDiscountPolicy discount1, IDiscountPolicy discount2, IDiscountCondition choosingCondition, String Id = "") : base(new Dictionary<string, object>(), Id)
        {
            Discount1 = discount1;
            Discount2 = discount2;
            ChoosingCondition = choosingCondition;
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            return new DiscountXor(null, null, null);
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            if (Discount1 == null || Discount2 == null || ChoosingCondition == null)
                return new Dictionary<Product, double>();

            Dictionary<Product, double> result1 = Discount1.CalculateDiscount(products, code);
            Dictionary<Product, double> result2 = Discount2.CalculateDiscount(products, code);

            if (result1 == null)
                return new Dictionary<Product, double>();
            if (result2 == null)
                return new Dictionary<Product, double>();

            if (result1.Count == 0)
                return result2;
            if (result2.Count == 0)
                return result1;

            bool conditionResult = ChoosingCondition.isConditionMet(products);
            if (conditionResult)
                return result1;
            return result2;
        }

        public override bool AddDiscount(string id, IDiscountPolicy discount)
        {
            if (Id.Equals(id))
            {
                if (Discount1 == null)
                {
                    Discount1 = discount;
                    var update_discount = Builders<BsonDocument>.Update.Set("Discount1", discount.Id);
                    DBUtil.getInstance().UpdatePolicy(this, update_discount);
                }

                else if (Discount2 == null)
                {
                    Discount2 = discount;
                    var update_discount = Builders<BsonDocument>.Update.Set("Discount2", discount.Id);
                    DBUtil.getInstance().UpdatePolicy(this, update_discount);
                }
                else
                    throw new Exception("Can't add a discount to a full xor");


                return true;
            }

            bool result = Discount1.AddDiscount(id, discount);
            if (result)
                return result;
            return Discount2.AddDiscount(id, discount);
        }

        public override IDiscountPolicy RemoveDiscount(string id)
        {
            IDiscountPolicy oldPolicy = null;
            if (Discount1 != null && Discount1.Id.Equals(id))
            {
                oldPolicy = Discount1;
                Discount1 = null;
                var update_discount = Builders<BsonDocument>.Update.Set("Discount1", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return oldPolicy;
            }
            if (Discount2 != null && Discount2.Id.Equals(id))
            {
                oldPolicy = Discount2;
                Discount2 = null;
                var update_discount = Builders<BsonDocument>.Update.Set("Discount1", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return oldPolicy;
            }

            IDiscountPolicy result = Discount1.RemoveDiscount(id);
            if (result != null)
                return result;
            return Discount2.RemoveDiscount(id);
        }

        public override bool AddCondition(string id, IDiscountCondition condition)
        {
            if (Id == id)
            {
                ChoosingCondition = condition;
                var update_discount = Builders<BsonDocument>.Update.Set("ChoosingCondition", getConditonData(condition));
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return true;
            }
            if (ChoosingCondition != null)
            {
                bool choosingResult = ChoosingCondition.AddCondition(id, condition);
                if (choosingResult)
                    return choosingResult;
            }
            bool result = Discount1.AddCondition(id, condition);
            if (result)
                return result;
            return Discount2.AddCondition(id, condition);
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
            if (ChoosingCondition != null && ChoosingCondition.Id == id)
            {
                IDiscountCondition oldCondition = ChoosingCondition;
                ChoosingCondition = null;
                var update_discount = Builders<BsonDocument>.Update.Set("ChoosingCondition", "");
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
                return oldCondition;
            }
            if (ChoosingCondition != null)
            {
                IDiscountCondition choosingResult = ChoosingCondition.RemoveCondition(id);
                if (choosingResult != null)
                    return choosingResult;
            }
            IDiscountCondition result = Discount1.RemoveCondition(id);
            if (result != null)
                return result;
            return Discount2.RemoveCondition(id);
        }

        public override IDictionary<string, object> GetData()
        {
            /*IDictionary<string, object> dict = new Dictionary<string, object>() { 
                {"type","DiscountXor" },
                {"Discount1",null },
                {"Discount2",null },
                {"ChoosingCondition",null}

            };*/
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Xor"},
                { "children", new Dictionary<String, object>[0] }
            };
            List<IDictionary<string, object>> children = new List<IDictionary<string, object>>();
            if (ChoosingCondition != null)
            {
                IDictionary<string, object> ChoosingConditionResult = ChoosingCondition.GetData();
                children.Add(ChoosingConditionResult);
            }
            if (Discount1 != null)
            {
                IDictionary<string, object> discount1Result = Discount1.GetData();
                children.Add(discount1Result);
            }
            if (Discount2 != null)
            {
                IDictionary<string, object> discount2Result = Discount2.GetData();
                children.Add(discount2Result);
            }
            dict["children"] = children.ToArray();
            return dict;
        }

        public override bool EditDiscount(Dictionary<string, object> info, string id)
        {
            if (Id != id)
            {
                bool result = Discount1.EditDiscount(info, id);
                if (result)
                    return result;
                return Discount2.EditDiscount(info, id);
            }

            return true;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (ChoosingCondition != null)
            {
                bool choosingResult = ChoosingCondition.EditCondition(info, id);
                if (choosingResult)
                    return choosingResult;
            }
            bool result = Discount1.EditCondition(info, id);
            if (result)
                return result;
            return Discount2.EditCondition(info, id);
        }
    }
}
