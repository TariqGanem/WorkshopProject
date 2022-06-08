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
    public class DiscountMax : AbstractDiscountPolicy
    {
        public List<IDiscountPolicy> Discounts { get; }

        public DiscountMax(String id = "") : base(new Dictionary<string, object>(), id)
        {
            Discounts = new List<IDiscountPolicy>();
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            return new DiscountMax();
        }

        public DiscountMax(List<IDiscountPolicy> discounts, String id = "") : base(new Dictionary<string, object>(), id)
        {
            Discounts = discounts;
            if (Discounts == null)
                Discounts = new List<IDiscountPolicy>();
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            //if we dont have discounts
            if (Discounts.Count == 0)
                return new Dictionary<Product, double>();

            //calculating all discounts
            List<Dictionary<Product, Double>> discountsResultsList = CalculateAllDiscounts(products, code);

            //choosing the biggest discount
            return ChooseDiscountByResult(discountsResultsList, products);
        }

        private List<Dictionary<Product, Double>> CalculateAllDiscounts(ConcurrentDictionary<Product, int> products, String code = "")
        {
            List<Dictionary<Product, Double>> discountsResultsList = new List<Dictionary<Product, double>>();
            foreach (IDiscountPolicy discount in Discounts)
            {
                Dictionary<Product, Double> discountResultDictionary = discount.CalculateDiscount(products, code);
                if (discountResultDictionary == null)
                    discountResultDictionary = new Dictionary<Product, double>();
                if (discountResultDictionary.Count != 0)
                    discountsResultsList.Add(discountResultDictionary);
            }
            return discountsResultsList;
        }

        private Dictionary<Product, double> ChooseDiscountByResult(List<Dictionary<Product, Double>> discountsResultsList, ConcurrentDictionary<Product, int> products)
        {
            if (discountsResultsList.Count == 0)
                return new Dictionary<Product, double>();
            Dictionary<Product, double> chosenDiscount = discountsResultsList[0];
            Double chosenValue = CalculateDiscountsValue(chosenDiscount, products);

            foreach (Dictionary<Product, double> discount in discountsResultsList)
            {
                Double currDiscountValue = CalculateDiscountsValue(discount, products);
                if (currDiscountValue > chosenValue)
                {
                    chosenValue = currDiscountValue;
                    chosenDiscount = discount;
                }
            }
            return chosenDiscount;
        }

        private Double CalculateDiscountsValue(Dictionary<Product, Double> discountResult, ConcurrentDictionary<Product, int> products)
        {
            Double acc = 0;
            foreach (KeyValuePair<Product, Double> entry in discountResult)
            {
                acc += entry.Value * entry.Key.Price * products[entry.Key];
            }
            return acc;
        }

        public override bool AddDiscount(string id, IDiscountPolicy discount)
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
                { "name", "Max"},
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
        private ConcurrentDictionary<String, String> ConvertDiscountToIDs()
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
