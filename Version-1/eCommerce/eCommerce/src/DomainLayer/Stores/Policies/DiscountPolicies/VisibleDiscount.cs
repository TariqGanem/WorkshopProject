using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies
{
    public class VisibleDiscount : AbstractDiscountPolicy
    {
        public DateTime ExpirationDate { get; set; }
        public IDiscountTarget Target { get; set; }
        public Double Percentage { get; set; }

        public VisibleDiscount(DateTime expirationDate, IDiscountTarget target, Double percentage, String id = "") : base(new Dictionary<string, object>(), id)
        {
            ExpirationDate = expirationDate;
            Target = target;
            if (percentage > 100)
                Percentage = 100;
            else if (percentage < 0)
                Percentage = 0;
            else
                Percentage = percentage;
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create VisibleDiscount: ";
            if (!info.ContainsKey("ExpirationDate"))
                throw new Exception(errorMsg + "ExpirationDate not found");
            DateTime expirationDate = createDateTime((JsonElement)info["ExpirationDate"]);

            if (!info.ContainsKey("Percentage"))
                throw new Exception(errorMsg + "Percentage not found");
            Double percentage = ((JsonElement)info["Percentage"]).GetDouble();

            if (!info.ContainsKey("Target"))
                throw new Exception(errorMsg + "Target not found");

            IDiscountTarget targetResult = createTarget((JsonElement)info["Target"]);
            return new VisibleDiscount(expirationDate, targetResult, percentage);
        }

        private static DateTime createDateTime(JsonElement timeElement)
        {
            String timeString = timeElement.GetString();
            DateTime time = DateTime.ParseExact(timeString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            return time;
        }

        private static IDiscountTarget createTarget(JsonElement targetElement)
        {
            Dictionary<string, object> targetDict = JsonSerializer.Deserialize<Dictionary<string, object>>(targetElement.GetRawText());
            return createTarget(targetDict);
        }

        public override Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            //if the discount is expired
            if (DateTime.Now.CompareTo(ExpirationDate) >= 0)
                return new Dictionary<Product, Double>();

            List<Product> targetProducts = Target.getTargets(products);
            Dictionary<Product, Double> resultDictionary = new Dictionary<Product, Double>();
            foreach (Product product in targetProducts)
            {
                resultDictionary.Add(product, Percentage);
            }
            return resultDictionary;
        }

        public override bool AddDiscount(String id, IDiscountPolicy discount)
        {
            if (Id.Equals(id))
                throw new Exception("Can't add a discount to a visible discount with an id ");
            return false;
        }

        public override IDiscountPolicy RemoveDiscount(String id)
        {
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
                { "name", "Visible expires on " + ExpirationDate.ToString() + ", " +Percentage + "% on "},
                { "children", new Dictionary<String, object>[0] }
            };

            if (Target != null)
            {
                String targetDataResult = Target.GetData();
                dict["name"] = dict["name"] + targetDataResult;
            }

            return dict;
        }

        private static IDiscountTarget createTarget(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("type"))
                throw new Exception("Can't create a target without a type");

            string type = ((JsonElement)info["type"]).ToString();
            switch (type)
            {
                case "DiscountTargetShop":
                    return DiscountTargetShop.create(info);
                case "DiscountTargetCategories":
                    return DiscountTargetCategories.create(info);
                case "DiscountTargetProducts":
                    return DiscountTargetProducts.create(info);
                default:
                    return null;
            }
        }

        public override bool EditDiscount(Dictionary<string, object> info, string id)
        {
            if (Id != id)
                return false;

            if (info.ContainsKey("ExpirationDate"))
            {
                //ExpirationDate = (DateTime)info["ExpirationDate"];
                ExpirationDate = createDateTime((JsonElement)info["ExpirationDate"]);
                var update_discount = Builders<BsonDocument>.Update.Set("ExpirationDate", ExpirationDate.ToString());
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("Percentage"))
            {
                Percentage = ((JsonElement)info["Percentage"]).GetDouble();
                var update_discount = Builders<BsonDocument>.Update.Set("Percentage", Percentage);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }

            if (info.ContainsKey("Target"))
            {
                IDiscountTarget targetResult = createTarget((JsonElement)info["Target"]);
            }
            return true;
        }

        public override bool EditCondition(Dictionary<string, object> info, string id)
        {
            return false;
        }
    }
}
