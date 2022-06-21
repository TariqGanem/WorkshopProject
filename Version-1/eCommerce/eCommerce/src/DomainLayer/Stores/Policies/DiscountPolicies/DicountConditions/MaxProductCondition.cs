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
    public class MaxProductCondition : AbstractDiscountCondition
    {
        public int MaxQuantity { set; get; }
        public String ProductId { set; get; }

        public MaxProductCondition(String productId, int maxQuantity, String id = "") : base(new Dictionary<string, object>(), id)
        {
            ProductId = productId;
            MaxQuantity = maxQuantity;
        }

        public static IDiscountCondition create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MaxProductCondition: ";
            if (!info.ContainsKey("MaxQuantity"))
                throw new Exception(errorMsg + "MaxQuantity not found");
            int maxQuantity = int.Parse((string)info["MaxQuantity"]);

            if (!info.ContainsKey("ProductId"))
                throw new Exception("ProductId not found");
            String productId = ((string)info["ProductId"]);
            return new MaxProductCondition(productId, maxQuantity);
        }

        public override bool isConditionMet(ConcurrentDictionary<Product, int> products)
        {
            Product myProduct = ContainsProduct(products);
            if (myProduct == null)
                return true;

            return products[myProduct] <= MaxQuantity;
        }

        private Product ContainsProduct(ConcurrentDictionary<Product, int> products)
        {
            foreach (KeyValuePair<Product, int> entry in products)
            {
                if (entry.Key.Id.Equals(ProductId))
                    return entry.Key;
            }
            return null;
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
                { "name", "" + ProductId + " <= " + MaxQuantity},
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("MaxQuantity"))
            {
                MaxQuantity = int.Parse((string)info["MaxQuantity"]);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_discount = Builders<BsonDocument>.Update.Set("MaxQuantity", MaxQuantity);
                DBUtil.getInstance().UpdateMaxProductCondition(filter, update_discount);
            }

            if (info.ContainsKey("ProductId"))
            {
                ProductId = ((string)info["ProductId"]);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_discount = Builders<BsonDocument>.Update.Set("ProductId", ProductId);
                DBUtil.getInstance().UpdateMaxProductCondition(filter, update_discount);
            }

            return true;
        }
    }
}
