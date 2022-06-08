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
    public class MinProductCondition : AbstractDiscountCondition
    {
        public int MinQuantity { set; get; }
        public String ProductId { set; get; }

        public MinProductCondition(String productId, int minQuantity, String id = "") : base(new Dictionary<string, object>(), id)
        {
            ProductId = productId;
            MinQuantity = minQuantity;
        }

        public static IDiscountCondition create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create MinProductCondition: ";
            if (!info.ContainsKey("MinQuantity"))
                throw new Exception(errorMsg + "MinQuantity not found");
            int minQuantity = ((JsonElement)info["MinQuantity"]).GetInt32();

            if (!info.ContainsKey("ProductId"))
                throw new Exception("ProductId not found");
            String productId = ((JsonElement)info["ProductId"]).GetString();

            return new MinProductCondition(productId, minQuantity);
        }

        public override bool isConditionMet(ConcurrentDictionary<Product, int> products)
        {
            Product myProduct = ContainsProduct(products);
            if (myProduct == null && MinQuantity == 0)
                return true;
            if (products.ContainsKey(myProduct) && products[myProduct] >= MinQuantity)
                return true;
            return false ;
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
                { "name", "" + ProductId + " >= " + MinQuantity},
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("MinQuantity"))
            {
                MinQuantity = ((JsonElement)info["MinQuantity"]).GetInt32();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_discount = Builders<BsonDocument>.Update.Set("MinQuantity", MinQuantity);
                DBUtil.getInstance().UpdateMinProductCondition(filter, update_discount);
            }

            if (info.ContainsKey("ProductId"))
            {
                ProductId = ((JsonElement)info["ProductId"]).GetString();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
                var update_discount = Builders<BsonDocument>.Update.Set("ProductId", ProductId);
                DBUtil.getInstance().UpdateMinProductCondition(filter, update_discount);
            }

            return true;
        }
    }
}
