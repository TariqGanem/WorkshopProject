using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores.Policies
{
    public interface IPolicyManager
    {
        double GetTotalBagPrice(ConcurrentDictionary<Product, int> products, string discountCode = "", List<Offer.Offer> offers = null);
        bool AdheresToPolicy(ConcurrentDictionary<Product, int> products, User.User user);
        IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info);
        IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info, String id);
        IDiscountCondition AddDiscountCondition(Dictionary<string, object> info, String id);
        Boolean AddDiscountPolicy(IDiscountPolicy policy);
        Boolean AddDiscountPolicy(IDiscountPolicy policy, String id);
        Boolean AddDiscountCondition(IDiscountCondition condition, String id);
        IDiscountPolicy RemoveDiscountPolicy(String id);
        IDiscountCondition RemoveDiscountCondition(String id);
        Boolean EditDiscountPolicy(Dictionary<string, object> info, String id);
        Boolean EditDiscountCondition(Dictionary<string, object> info, String id);
        IDictionary<string, object> GetDiscountPolicyData();
        IDictionary<string, object> GetPurchasePolicyData();
        IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info);
        Boolean AddPurchasePolicy(IPurchasePolicy policy);
        IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info, string id);
        Boolean AddPurchasePolicy(IPurchasePolicy policy, string id);
        IPurchasePolicy RemovePurchasePolicy(string id);
        bool EditPurchasePolicy(Dictionary<string, object> info, string id);
    }

    public class PolicyManager : IPolicyManager
    {

        public DiscountAddition MainDiscount { get; set; }
        public BuyNow MainPolicy { get; set; }
        public PolicyManager()
        {
            MainDiscount = new DiscountAddition();
            MainPolicy = new BuyNow();
        }

        public PolicyManager(DiscountAddition mainDiscount, BuyNow mainPolicy)
        {
            MainDiscount = mainDiscount;
            MainPolicy = mainPolicy;
        }

        public double GetTotalBagPrice(ConcurrentDictionary<Product, int> products, string discountCode = "", List<Offer.Offer> offers = null)
        {
            if (offers == null)
                offers = new List<Offer.Offer>();
            Dictionary<Product, Double> discountsResult = MainDiscount.CalculateDiscount(products, discountCode);
            Dictionary<Product, Double> discounts = discountsResult;
            Double price = 0;
            foreach (KeyValuePair<Product, int> entry in products)
            {
                double discount = 0;
                if (discounts.ContainsKey(entry.Key))
                    discount = discounts[entry.Key];
                price += calculateProductPrice(entry.Key.Id, entry.Value, entry.Key.Price, discount, offers);
            }
            return price;
        }

        private double calculateProductPrice(string productId, int amount, double basePrice, double discount, List<Offer.Offer> offers)
        {
            double price = 0;
            foreach (Offer.Offer offer in offers)
            {
                if (productId.Equals(offer.ProductID) && amount >= offer.Amount)
                {
                    amount -= offer.Amount;
                    if (offer.CounterOffer == -1)
                        price += offer.Price;
                    else
                        price += offer.CounterOffer;
                }
            }

            price += amount * basePrice * (100 - discount) / 100;
            return price;
        }

        public bool AdheresToPolicy(ConcurrentDictionary<Product, int> products, User.User user)
        {
            return MainPolicy.IsConditionMet(products, user);
        }

        public IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info)
        {
            IDiscountPolicy discountResult = CreateDiscount(info);
            bool res = AddDiscountPolicy(discountResult);
            return  discountResult;
        }

        public IDiscountPolicy AddDiscountPolicy(Dictionary<string, object> info, String id)
        {
            IDiscountPolicy discountResult = CreateDiscount(info);
            bool res = AddDiscountPolicy(discountResult, id);
            return discountResult;
        }

        public IDiscountCondition AddDiscountCondition(Dictionary<string, object> info, String id)
        {
            IDiscountCondition discountConditionResult = CreateDiscountCondition(info);
            bool res = AddDiscountCondition(discountConditionResult, id);
            return discountConditionResult;
        }

        public bool AddDiscountPolicy(IDiscountPolicy discount)
        {
            bool result = MainDiscount.AddDiscount(MainDiscount.Id, discount);
            if (result)
                return true;
            else throw new Exception($"The discount could not be added");
        }

        public bool AddDiscountPolicy(IDiscountPolicy discount, String id)
        {
            bool result = MainDiscount.AddDiscount(id, discount);
            if (result)
                return true;
            else throw new Exception($"The discount addition failed because the discount with an id ${id} was not found");
        }

        public bool AddDiscountCondition(IDiscountCondition condition, String id)
        {
            bool result = MainDiscount.AddCondition(id, condition);
            if (result)
                return true;
            else throw new Exception($"The discount condition addition failed because the discount condition with an id ${id} was not found");
        }

        public IDiscountPolicy RemoveDiscountPolicy(string id)
        {
            IDiscountPolicy result = MainDiscount.RemoveDiscount(id);
            if (result != null)
                return result;
            else throw new Exception($"The discount removal failed because the discount with an id ${id} was not found");
        }

        public IDiscountCondition RemoveDiscountCondition(string id)
        {
            IDiscountCondition result = MainDiscount.RemoveCondition(id);
            if (result != null)
                return result;
            else throw new Exception($"The discount condition removal failed because the discount condition with an id ${id} was not found");
        }

        public IDictionary<string, object> GetDiscountPolicyData()
        {
            return MainDiscount.GetData();
        }

        public IDictionary<string, object> GetPurchasePolicyData()
        {
            return MainPolicy.GetData();
        }

        public IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info)
        {
            IPurchasePolicy result = CreatePurchasePolicy(info);
            IPurchasePolicy policy = result;
            bool res = AddPurchasePolicy(policy);
            return policy;
        }

        public Boolean AddPurchasePolicy(IPurchasePolicy policy)
        {
            bool result = MainPolicy.AddPolicy(policy, MainPolicy.Policy.Id);
            if (result)
                return true;
            else throw new Exception($"The policy could not be added");
        }

        public IPurchasePolicy AddPurchasePolicy(Dictionary<string, object> info, String id)
        {
            IPurchasePolicy result = CreatePurchasePolicy(info);
            IPurchasePolicy policy = result;
            bool res = AddPurchasePolicy(policy, id);
            return policy;
        }

        public bool AddPurchasePolicy(IPurchasePolicy policy, String id)
        {
            bool result = MainPolicy.AddPolicy(policy, id);
            if (result)
                return true;
            else throw new Exception($"The policy addition failed because the policy with an id ${id} was not found");
        }

        public IPurchasePolicy RemovePurchasePolicy(string id)
        {
            IPurchasePolicy result = MainPolicy.RemovePolicy(id);
            if (result != null)
                return result;
            else throw new Exception($"The policy removal failed because the policy with an id ${id} was not found");
            
        }

        private IPurchasePolicy CreatePurchasePolicy(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("type"))
                throw new Exception("Can't create a purchase Policy without a type");

            string type = ((string)info["type"]);
            switch (type)
            {
                case "AndPolicy":
                    return AndPolicy.create(info);
                case "OrPolicy":
                    return OrPolicy.create(info);
                case "ConditionalPolicy":
                    return ConditionalPolicy.create(info);
                case "MaxProductPolicy":
                    return MaxProductPolicy.create(info);
                case "MinProductPolicy":
                    return MinProductPolicy.create(info);
                case "MinAgePolicy":
                    return MinAgePolicy.create(info);
                case "RestrictedHoursPolicy":
                    return RestrictedHoursPolicy.create(info);

                default:
                    throw new Exception("Can't recognise this purchase policy type: " + type);
            }
        }

        public bool EditPurchasePolicy(Dictionary<string, object> info, string id)
        {
            bool result = MainPolicy.EditPolicy(info, id);  
            if (result)
                return true;
            else throw new Exception($"The Purchase policy edit failed because the policy with an id ${id} was not found");
        }

        private IDiscountPolicy CreateDiscount(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("type"))
                throw new Exception("Can't create a discount without a type");

            string type = (string)info["type"];
            switch (type)
            {
                case "VisibleDiscount":
                    return VisibleDiscount.create(info);
                case "DiscreetDiscount":
                    return DiscreetDiscount.create(info);
                case "ConditionalDiscount":
                    return ConditionalDiscount.create(info);
                case "DiscountAddition":
                    return DiscountAddition.create(info);
                case "DiscountAnd":
                    return DiscountAnd.create(info);
                case "DiscountMax":
                    return DiscountMax.create(info);
                case "DiscountMin":
                    return DiscountMin.create(info);
                case "DiscountOr":
                    return DiscountOr.create(info);
                case "DiscountXor":
                    return DiscountXor.create(info);

                default:
                    throw new Exception("Can't recognise this discount type: " + type);
            }
        }

        private IDiscountCondition CreateDiscountCondition(Dictionary<string, object> info)
        {
            if (!info.ContainsKey("type"))
                throw new Exception("Can't create a condition without a type");

            string type = ((string)info["type"]);
            switch (type)
            {
                case "DiscountConditionAnd":
                    return DiscountConditionAnd.create(info);
                case "DiscountConditionOr":
                    return DiscountConditionOr.create(info);
                case "MaxProductCondition":
                    return MaxProductCondition.create(info);
                case "MinProductCondition":
                    return MinProductCondition.create(info);
                case "MinBagPriceCondition":
                    return MinBagPriceCondition.create(info);


                default:
                    throw new Exception("Can't recognise this condition type: " + type);
            }
        }

        public bool EditDiscountPolicy(Dictionary<string, object> info, string id)
        {
            bool result = MainDiscount.EditDiscount(info, id);
            if (result)
                return true;
            else throw new Exception($"The discount edit failed because the discount with an id ${id} was not found");
        }

        public bool EditDiscountCondition(Dictionary<string, object> info, string id)
        {
            bool result = MainDiscount.EditCondition(info, id);
            if (result)
                return true;
            else throw new Exception($"The discount condition edit failed because the discount condition with an id ${id} was not found");
        }

    }
}
