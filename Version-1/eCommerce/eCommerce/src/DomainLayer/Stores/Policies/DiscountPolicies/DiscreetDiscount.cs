using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies
{
    public class DiscreetDiscount : AbstractDiscountPolicy
    {
        public String DiscountCode { set; get; }
        public IDiscountPolicy Discount { set; get; }

        public DiscreetDiscount(IDiscountPolicy discount, String discountCode, String id = "") : base(new Dictionary<string, object>(), id)
        {
            Discount = discount;
            DiscountCode = discountCode;
        }

        public static IDiscountPolicy create(Dictionary<string, object> info)
        {
            string errorMsg = "Can't create DiscreetDiscount: ";
            if (!info.ContainsKey("DiscountCode"))
                throw new Exception(errorMsg + "DiscountCode not found");
            String discountCode = ((string)info["DiscountCode"]);

            return new DiscreetDiscount(null, discountCode);
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            if (DiscountCode.Equals(code) && Discount != null)
                return Discount.CalculateDiscount(products, code);
            return new Dictionary<Product, double>();
        }

        public override bool AddDiscount(String id, IDiscountPolicy discount)
        {
            if (Id.Equals(id))
            {
                Discount = discount;
                var update_discount = Builders<BsonDocument>.Update.Set("Discount", discount.Id);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
            }
            if (Discount != null)
                return Discount.AddDiscount(id, discount);
            return false;
        }

        public override IDiscountPolicy RemoveDiscount(String id)
        {
            if (Discount != null)
                return Discount.RemoveDiscount(id);
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
                { "name", "Discreet with a code " + DiscountCode},
                { "children", new Dictionary<String, object>[0] }
            };

            if (Discount != null)
            {
                IDictionary<string, object> discountDataResult = Discount.GetData();
                dict["children"] = new IDictionary<String, object>[1] { discountDataResult};

            }
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

            if (info.ContainsKey("DiscountCode"))
            {
                DiscountCode = ((string)info["DiscountCode"]);
                var update_discount = Builders<BsonDocument>.Update.Set("DiscountCode", DiscountCode);
                DBUtil.getInstance().UpdatePolicy(this, update_discount);
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
