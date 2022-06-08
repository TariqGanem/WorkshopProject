using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Store;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class RestrictedHoursPolicy : IPurchasePolicy
    {
        public DateTime StartRestrict { get; set; }
        public DateTime EndRestrict { get; set; }
        public string ProductId { get; set; }
        public string Id { get; }

        public RestrictedHoursPolicy(DateTime startRestrict, DateTime endRestrict, string productId, string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.StartRestrict = startRestrict;
            this.EndRestrict = endRestrict;
            this.ProductId = productId;
        }

        public static IPurchasePolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create RestrictedHoursPolicy: ";
            if (!info.ContainsKey("StartRestrict"))
                throw new Exception(errorMsg + "StartRestrict not found");
            DateTime startRestrict = createDateTime((JsonElement)info["StartRestrict"]);

            if (!info.ContainsKey("EndRestrict"))
                throw new Exception(errorMsg + "EndRestrict not found");
            DateTime endRestrict = createDateTime((JsonElement)info["EndRestrict"]);

            if (!info.ContainsKey("ProductId"))
                throw new Exception(errorMsg + "ProductId not found");
            string productId = ((JsonElement)info["ProductId"]).GetString();

            return new RestrictedHoursPolicy(startRestrict, endRestrict, productId);
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            DateTime now = DateTime.Now;
            return  now > EndRestrict && now < StartRestrict;
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            if (this.Id.Equals(id))
                throw new Exception("Cannot add a policy to this type of policy");
            return false;
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            return null;
        }

        public IDictionary<string, object> GetData()
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() {
                { "id", Id },
                { "name", "Restricted Hours - from " + DateTimeToHour(StartRestrict) + " to " + DateTimeToHour(EndRestrict) + " for product " + ProductId},
                { "children", new Dictionary<String, object>[0] }
            };
            return dict;
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("StartRestrict"))
            {
                StartRestrict = createDateTime((JsonElement)info["StartRestrict"]);
                var update_discount = Builders<MongoDB.Bson.BsonDocument>.Update.Set("StartRestrict", StartRestrict.ToString());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("EndRestrict"))
            {
                EndRestrict = createDateTime((JsonElement)info["EndRestrict"]);
                var update_discount = Builders<BsonDocument>.Update.Set("EndRestrict", EndRestrict.ToString());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("ProductId"))
            {
                ProductId = ((JsonElement)info["ProductId"]).GetString();
                var update_discount = Builders<BsonDocument>.Update.Set("ProductId", ProductId);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            return true;
        }

        private static DateTime createDateTime(JsonElement timeElement)
        {
            String timeString = timeElement.GetString();
            DateTime time = DateTime.ParseExact(timeString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            return time;
        }

        public DTO_RestrictedHoursPolicy getDTO()
        {
            return new DTO_RestrictedHoursPolicy(this.Id, this.StartRestrict.ToString(), this.EndRestrict.ToString(), this.ProductId);
        }

        private String DateTimeToHour(DateTime date)
        {
            return date.Hour + ":" + date.Minute + ":" + date.Second;
        }
    }
}
