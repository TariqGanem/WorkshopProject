using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions
{
    public class MinBagPriceCondition : AbstractDiscountCondition
    {
        public Double MinPrice { set; get; }

        public MinBagPriceCondition(Double minPrice, String id = "") : base(new Dictionary<string, object>(), id)
        {
            MinPrice = minPrice;
        }

        public static IDiscountCondition create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MinBagPriceCondition: ";
            if (!info.ContainsKey("MinPrice"))
                throw new Exception(errorMsg + "MinPrice not found");
            Double minPrice = int.Parse((string)info["MinPrice"]);

            return new MinBagPriceCondition(minPrice);
        }

        public override bool isConditionMet(ConcurrentDictionary<Product, int> products)
        {
            double priceAcc = 0;
            foreach (KeyValuePair<Product, int> entry in products)
            {
                priceAcc += entry.Key.Price * entry.Value;
                if (priceAcc >= MinPrice)
                    return true;
            }
            return false;
        }

        public override bool AddCondition(string id, IDiscountCondition condition)
        {
            return false;
        }

        public override IDiscountCondition RemoveCondition(string id)
        {
            return null;
        }

        public override IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Bag price >= " + MinPrice},
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("MinPrice"))
            {
                MinPrice = int.Parse((string)info["MinPrice"]);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_discount = Builders<BsonDocument>.Update.Set("MinPrice", MinPrice);
                DBUtil.getInstance().UpdateMinBagPriceCondition(filter, update_discount);
            }
            return true;
        }
    }
}
