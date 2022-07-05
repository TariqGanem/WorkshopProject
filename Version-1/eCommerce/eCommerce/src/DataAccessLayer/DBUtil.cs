﻿using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.Objects;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerce.src.DomainLayer.Stores.Policies;
using eCommerce.src.DomainLayer.Stores.OwnerAppointmennt;
using System.Globalization;

namespace eCommerce.src.DataAccessLayer
{
    public enum ConnectionStatus
    {
        OK = 0,
        Error = 1
    }
    public class DBUtil
    {
        private static DBUtil Instance = null;
        public MongoClient dbClient;
        public IMongoDatabase db;
        public static ConnectionStatus connectionStatus = ConnectionStatus.OK;


        public DAO<DTO_RegisteredUser> DAO_RegisteredUser;
        public DAO<DTO_Product> DAO_Product;
        public DAO<DTO_StoreManager> DAO_StoreManager;
        public DAO<DTO_StoreOwner> DAO_StoreOwner;
        public DAO<DTO_Store> DAO_Store;
        public DAO<DTO_SystemAdmins> DAO_SystemAdmins;
        public DAO<DTO_Auction> DAO_Auction;
        public DAO<DTO_Lottery> DAO_Lottery;
        public DAO<DTO_MaxProductPolicy> DAO_MaxProductPolicy;
        public DAO<DTO_MinAgePolicy> DAO_MinAgePolicy;
        public DAO<DTO_MinProductPolicy> DAO_MinProductPolicy;
        public DAO<DTO_Offer> DAO_Offer;
        public DAO<DTO_RestrictedHoursPolicy> DAO_RestrictedHoursPolicy;
        public DAO<DTO_AndPolicy> DAO_AndPolicy;
        public DAO<DTO_OrPolicy> DAO_OrPolicy;
        public DAO<DTO_BuyNow> DAO_BuyNow;
        public DAO<DTO_ConditionalPolicy> DAO_ConditionalPolicy;
        public DAO<DTO_VisibleDiscount> DAO_VisibleDiscount;
        public DAO<DTO_DiscountTargetCategories> DAO_DiscountTargetCategories;
        public DAO<DTO_DiscountTargetProducts> DAO_DiscountTargetProducts;
        public DAO<DTO_DiscreetDiscount> DAO_DiscreetDiscount;
        public DAO<DTO_ConditionalDiscount> DAO_ConditionalDiscount;
        public DAO<DTO_MinProductCondition> DAO_MinProductCondition;
        public DAO<DTO_MinBagPriceCondition> DAO_MinBagPriceCondition;
        public DAO<DTO_MaxProductCondition> DAO_MaxProductCondition;
        public DAO<DTO_DiscountConditionOr> DAO_DiscountConditionOr;
        public DAO<DTO_DiscountConditionAnd> DAO_DiscountConditionAnd;
        public DAO<DTO_DiscountXor> DAO_DiscountXor;
        public DAO<DTO_DiscountOr> DAO_DiscountOr;
        public DAO<DTO_DiscountMin> DAO_DiscountMin;
        public DAO<DTO_DiscountMax> DAO_DiscountMax;
        public DAO<DTO_DiscountAnd> DAO_DiscountAnd;
        public DAO<DTO_DiscountAddition> DAO_DiscountAddition;
        public DAO<DTO_OwnerRequest> DAO_OwnerRequest;
        public DAO<SystemInfo> DAO_SysInfo;



        public ConcurrentDictionary<String, RegisteredUser> RegisteredUsers;
        public ConcurrentDictionary<String, GuestUser> GuestUsers;
        public ConcurrentDictionary<String, Product> Products;
        public ConcurrentDictionary<String, LinkedList<StoreManager>> StoreManagers;
        public ConcurrentDictionary<String, LinkedList<StoreOwner>> StoreOwners;
        public ConcurrentDictionary<String, Store> Stores;
        public ConcurrentDictionary<String, Auction> Policy_Auctions;
        public ConcurrentDictionary<String, Lottery> Policy_Lotterys;
        public ConcurrentDictionary<String, MaxProductPolicy> Policy_MaxProductPolicys;
        public ConcurrentDictionary<String, MinAgePolicy> Policy_MinAgePolicys;
        public ConcurrentDictionary<String, MinProductPolicy> Policy_MinProductPolicys;
        public ConcurrentDictionary<String, Offer> Policy_Offers;
        public ConcurrentDictionary<String, RestrictedHoursPolicy> Policy_RestrictedHoursPolicys;
        public ConcurrentDictionary<String, AndPolicy> Policy_AndPolicys;
        public ConcurrentDictionary<String, OrPolicy> Policy_OrPolicys;
        public ConcurrentDictionary<String, BuyNow> Policy_BuyNows;
        public ConcurrentDictionary<String, ConditionalPolicy> Policy_ConditionalPolicys;
        public ConcurrentDictionary<String, VisibleDiscount> Discount_VisibleDiscounts;
        public ConcurrentDictionary<String, DiscountTargetCategories> Discount_DiscountTargetCategories;
        public ConcurrentDictionary<String, DiscountTargetProducts> Discount_DiscountTargetProducts;
        public ConcurrentDictionary<String, DiscreetDiscount> Discount_DiscreetDiscounts;
        public ConcurrentDictionary<String, ConditionalDiscount> Discount_ConditionalDiscounts;
        public ConcurrentDictionary<String, MinProductCondition> Discount_MinProductConditions;
        public ConcurrentDictionary<String, MinBagPriceCondition> Discount_MinBagPriceConditions;
        public ConcurrentDictionary<String, MaxProductCondition> Discount_MaxProductConditions;
        public ConcurrentDictionary<String, DiscountConditionOr> Discount_DiscountConditionOrs;
        public ConcurrentDictionary<String, DiscountConditionAnd> Discount_DiscountConditionAnds;
        public ConcurrentDictionary<String, DiscountXor> Discount_DiscountXors;
        public ConcurrentDictionary<String, DiscountOr> Discount_DiscountOrs;
        public ConcurrentDictionary<String, DiscountMin> Discount_DiscountMins;
        public ConcurrentDictionary<String, DiscountMax> Discount_DiscountMaxs;
        public ConcurrentDictionary<String, DiscountAnd> Discount_DiscountAnds;
        public ConcurrentDictionary<String, DiscountAddition> Discount_DiscountAdditions;
        public ConcurrentDictionary<String, OwnerRequest> OwnerRequests;


        private DBUtil(String connection_url , String db_name)
        {
            try
            {
                //String dbName = "XMart";

                this.dbClient = new MongoClient(connection_url);
                db = dbClient.GetDatabase(db_name);

                DAO_RegisteredUser = new DAO<DTO_RegisteredUser>(db, "Users");
                DAO_Product = new DAO<DTO_Product>(db, "Products");
                DAO_StoreManager = new DAO<DTO_StoreManager>(db, "StoreStaff");
                DAO_StoreOwner = new DAO<DTO_StoreOwner>(db, "StoreStaff");
                DAO_Store = new DAO<DTO_Store>(db, "Stores");
                DAO_SystemAdmins = new DAO<DTO_SystemAdmins>(db, "SystemAdmins");
                DAO_Auction = new DAO<DTO_Auction>(db, "PurchasePolicies");
                DAO_Lottery = new DAO<DTO_Lottery>(db, "PurchasePolicies");
                DAO_MaxProductPolicy = new DAO<DTO_MaxProductPolicy>(db, "PurchasePolicies");
                DAO_MinAgePolicy = new DAO<DTO_MinAgePolicy>(db, "PurchasePolicies");
                DAO_MinProductPolicy = new DAO<DTO_MinProductPolicy>(db, "PurchasePolicies");
                DAO_Offer = new DAO<DTO_Offer>(db, "PurchasePolicies");
                DAO_RestrictedHoursPolicy = new DAO<DTO_RestrictedHoursPolicy>(db, "PurchasePolicies");
                DAO_AndPolicy = new DAO<DTO_AndPolicy>(db, "PurchasePolicies");
                DAO_OrPolicy = new DAO<DTO_OrPolicy>(db, "PurchasePolicies");
                DAO_BuyNow = new DAO<DTO_BuyNow>(db, "PurchasePolicies");
                DAO_ConditionalPolicy = new DAO<DTO_ConditionalPolicy>(db, "PurchasePolicies");
                DAO_VisibleDiscount = new DAO<DTO_VisibleDiscount>(db, "DiscountPolicies");
                DAO_DiscountTargetCategories = new DAO<DTO_DiscountTargetCategories>(db, "DiscountPolicies");
                DAO_DiscountTargetProducts = new DAO<DTO_DiscountTargetProducts>(db, "DiscountPolicies");
                DAO_DiscreetDiscount = new DAO<DTO_DiscreetDiscount>(db, "DiscountPolicies");
                DAO_ConditionalDiscount = new DAO<DTO_ConditionalDiscount>(db, "DiscountPolicies");
                DAO_MinProductCondition = new DAO<DTO_MinProductCondition>(db, "DiscountPolicies");
                DAO_MinBagPriceCondition = new DAO<DTO_MinBagPriceCondition>(db, "DiscountPolicies");
                DAO_MaxProductCondition = new DAO<DTO_MaxProductCondition>(db, "DiscountPolicies");
                DAO_DiscountConditionOr = new DAO<DTO_DiscountConditionOr>(db, "DiscountPolicies");
                DAO_DiscountConditionAnd = new DAO<DTO_DiscountConditionAnd>(db, "DiscountPolicies");
                DAO_DiscountXor = new DAO<DTO_DiscountXor>(db, "DiscountPolicies");
                DAO_DiscountOr = new DAO<DTO_DiscountOr>(db, "DiscountPolicies");
                DAO_DiscountMin = new DAO<DTO_DiscountMin>(db, "DiscountPolicies");
                DAO_DiscountMax = new DAO<DTO_DiscountMax>(db, "DiscountPolicies");
                DAO_DiscountAnd = new DAO<DTO_DiscountAnd>(db, "DiscountPolicies");
                DAO_DiscountAddition = new DAO<DTO_DiscountAddition>(db, "DiscountPolicies");
                DAO_OwnerRequest = new DAO<DTO_OwnerRequest>(db, "OwnerRequests");
                DAO_SysInfo = new DAO<SystemInfo>(db, "SystemVisits");


                RegisteredUsers = new ConcurrentDictionary<String, RegisteredUser>();
                GuestUsers = new ConcurrentDictionary<String, GuestUser>();
                Products = new ConcurrentDictionary<String, Product>();
                StoreManagers = new ConcurrentDictionary<String, LinkedList<StoreManager>>();
                StoreOwners = new ConcurrentDictionary<String, LinkedList<StoreOwner>>();
                Stores = new ConcurrentDictionary<String, Store>();
                Policy_Auctions = new ConcurrentDictionary<String, Auction>();
                Policy_Lotterys = new ConcurrentDictionary<String, Lottery>();
                Policy_MaxProductPolicys = new ConcurrentDictionary<String, MaxProductPolicy>();
                Policy_MinAgePolicys = new ConcurrentDictionary<String, MinAgePolicy>();
                Policy_MinProductPolicys = new ConcurrentDictionary<String, MinProductPolicy>();
                Policy_Offers = new ConcurrentDictionary<String, Offer>();
                Policy_RestrictedHoursPolicys = new ConcurrentDictionary<String, RestrictedHoursPolicy>();
                Policy_AndPolicys = new ConcurrentDictionary<String, AndPolicy>();
                Policy_OrPolicys = new ConcurrentDictionary<String, OrPolicy>();
                Policy_BuyNows = new ConcurrentDictionary<String, BuyNow>();
                Policy_ConditionalPolicys = new ConcurrentDictionary<String, ConditionalPolicy>();
                Discount_VisibleDiscounts = new ConcurrentDictionary<String, VisibleDiscount>();
                Discount_DiscountTargetCategories = new ConcurrentDictionary<String, DiscountTargetCategories>();
                Discount_DiscountTargetProducts = new ConcurrentDictionary<String, DiscountTargetProducts>();
                Discount_DiscreetDiscounts = new ConcurrentDictionary<String, DiscreetDiscount>();
                Discount_ConditionalDiscounts = new ConcurrentDictionary<String, ConditionalDiscount>();
                Discount_MinProductConditions = new ConcurrentDictionary<String, MinProductCondition>();
                Discount_MinBagPriceConditions = new ConcurrentDictionary<String, MinBagPriceCondition>();
                Discount_MaxProductConditions = new ConcurrentDictionary<String, MaxProductCondition>();
                Discount_DiscountConditionOrs = new ConcurrentDictionary<String, DiscountConditionOr>();
                Discount_DiscountConditionAnds = new ConcurrentDictionary<String, DiscountConditionAnd>();
                Discount_DiscountXors = new ConcurrentDictionary<String, DiscountXor>();
                Discount_DiscountOrs = new ConcurrentDictionary<String, DiscountOr>();
                Discount_DiscountMins = new ConcurrentDictionary<String, DiscountMin>();
                Discount_DiscountMaxs = new ConcurrentDictionary<String, DiscountMax>();
                Discount_DiscountAnds = new ConcurrentDictionary<String, DiscountAnd>();
                Discount_DiscountAdditions = new ConcurrentDictionary<String, DiscountAddition>();
                OwnerRequests = new ConcurrentDictionary<string, OwnerRequest>();
            }
            catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Environment.Exit(1);
            }
        }

        public static DBUtil getInstance()
        {
            return Instance;
        }

        public static DBUtil getInstance(String connection_url , String db_name)
        {
            if (Instance == null)
            {
                Instance = new DBUtil(connection_url , db_name);
            }
            return Instance;
        }

        public MongoClient getMongoClient()
        {
            return this.dbClient;
        }
        private ConcurrentDictionary<String, String> getPoliciesIDs(List<IPurchasePolicy> list)
        {
            ConcurrentDictionary<String, String> Policies = new ConcurrentDictionary<String, String>(); //<id , type>
            foreach (IPurchasePolicy policy in list)
            {
                if (policy == null)
                    continue;
                string[] type = policy.GetType().ToString().Split('.');
                string policy_type = type[type.Length - 1];
                AddPolicyToDB(policy_type, policy);
                Policies.TryAdd(policy.Id, policy_type);
            }
            return Policies;
        }
        private ConcurrentDictionary<String, String> getDiscountsIDs(IDiscountTarget discount)
        {
            ConcurrentDictionary<String, String> Discounts = new ConcurrentDictionary<String, String>();    //<id , type>

            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];
            AddDiscountToDB(discount_type, discount);
            Discounts.TryAdd(discount.getId(), discount_type);

            return Discounts;
        }
        private ConcurrentDictionary<String, String> getDiscountsIDs(IDiscountPolicy discount)
        {
            ConcurrentDictionary<String, String> Discounts = new ConcurrentDictionary<String, String>();    //<id , type>

            if (discount == null)
                return Discounts;
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];
            AddDiscountToDB(discount_type, discount);
            Discounts.TryAdd(discount.Id, discount_type);

            return Discounts;
        }
        private ConcurrentDictionary<String, String> getDiscountsIDs(IDiscountCondition discount)
        {
            ConcurrentDictionary<String, String> Discounts = new ConcurrentDictionary<String, String>();    //<id , type>

            if (discount == null)
                return Discounts;
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];
            AddDiscountToDB(discount_type, discount);
            Discounts.TryAdd(discount.Id, discount_type);

            return Discounts;
        }
        private ConcurrentDictionary<String, String> getDiscountsIDs(List<IDiscountCondition> discounts)
        {
            ConcurrentDictionary<String, String> Discounts = new ConcurrentDictionary<String, String>();    //<id , type>

            foreach (IDiscountCondition discountCondition in discounts)
            {
                string[] type = discountCondition.GetType().ToString().Split('.');
                string discount_type = type[type.Length - 1];
                AddDiscountToDB(discount_type, discountCondition);
                Discounts.TryAdd(discountCondition.Id, discount_type);
            }
            return Discounts;
        }
        private ConcurrentDictionary<String, String> getDiscountsIDs(List<IDiscountPolicy> discounts)
        {
            ConcurrentDictionary<String, String> Discounts = new ConcurrentDictionary<String, String>();    //<id , type>

            foreach (IDiscountPolicy discountCondition in discounts)
            {
                string[] type = discountCondition.GetType().ToString().Split('.');
                string discount_type = type[type.Length - 1];
                AddDiscountToDB(discount_type, discountCondition);
                Discounts.TryAdd(discountCondition.Id, discount_type);
            }
            return Discounts;
        }


        private IPurchasePolicy LoadIPurchasePolicy(String tag, string policy_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", policy_id);
            switch (tag)
            {
                case "AndPolicy":
                    return LoadAndPolicy(filter);

                case "ConditionalPolicy":
                    return LoadConditionalPolicy(filter);

                case "MaxProductPolicy":
                    return LoadMaxProductPolicy(filter);


                case "MinAgePolicy":
                    return LoadMinAgePolicy(filter);


                case "MinProductPolicy":
                    return LoadMinProductPolicy(filter);


                case "OrPolicy":
                    return LoadOrPolicy(filter);


                case "RestrictedHoursPolicy":
                    return LoadRestrictedHoursPolicy(filter);
            }

            return null;
        }
        private IDiscountTarget LoadIDiscountTarget(String tag, string target_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", target_id);
            switch (tag)
            {
                case "DiscountTargetCategories":
                    return LoadDiscountTargetCategories(filter);

                case "DiscountTargetProducts":
                    return LoadDiscountTargetProducts(filter);

            }

            return null;
        }
        private IDiscountPolicy LoadIDiscountPolicy(String tag, string target_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", target_id);
            switch (tag)
            {
                case "ConditionalDiscount":
                    return LoadConditionalDiscount(filter);

                case "DiscountAddition":
                    return LoadDiscountAddition(filter);

                case "DiscountAnd":
                    return LoadDiscountAnd(filter);

                case "DiscountMax":
                    return LoadDiscountMax(filter);

                case "DiscountOr":
                    return LoadDiscountOr(filter);

                case "DiscountMin":
                    return LoadDiscountMin(filter);

                case "DiscountXor":
                    return LoadDiscountXor(filter);

                case "DiscreetDiscount":
                    return LoadDiscreetDiscount(filter);

                case "VisibleDiscount":
                    return LoadVisibleDiscount(filter);

            }

            return null;
        }
        private IDiscountCondition LoadIDiscountCondition(String tag, string discount_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount_id);
            switch (tag)
            {
                case "DiscountConditionAnd":
                    return LoadDiscountConditionAnd(filter);

                case "DiscountConditionOr":
                    return LoadDiscountConditionOr(filter);

                case "MaxProductCondition":
                    return LoadMaxProductCondition(filter);

                case "MinBagPriceCondition":
                    return LoadMinBagPriceCondition(filter);

                case "MinProductCondition":
                    return LoadMinProductCondition(filter);
            }

            return null;
        }

        public void Delete(IPurchasePolicy purchasePolicy)
        {
            string[] type = purchasePolicy.GetType().ToString().Split('.');
            string policy_type = type[type.Length - 1];

            DeleteIPurchasePolicy(policy_type, purchasePolicy.Id);
        }
        public void Delete(IDiscountPolicy discount)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            DeleteIDiscountPolicy(discount_type, discount.Id);
        }
        public void Delete(IDiscountCondition discount)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            DeleteIDiscountCondition(discount_type, discount.Id);
        }

        private void DeleteIDiscountTarget(String type, string discount_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount_id);
            switch (type)
            {
                case "DiscountTargetCategories":
                    DeleteDiscountTargetCategories(filter);
                    break;

                case "DiscountTargetProducts":
                    DeleteDiscountTargetProducts(filter);
                    break;
            }
        }
        private void DeleteIPurchasePolicy(String type, string policy_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", policy_id);
            switch (type)
            {
                case "AndPolicy":
                    DeleteAndPolicy(filter);
                    break;

                case "Auction":
                    DeleteAuctionPolicy(filter);
                    break;

                case "BuyNow":
                    DeleteBuyNowPolicy(filter);
                    break;

                case "ConditionalPolicy":
                    DeleteConditionalPolicy(filter);
                    break;

                case "Lottery":
                    DeleteLotteryPolicy(filter);
                    break;

                case "MaxProductPolicy":
                    DeleteMaxProductPolicy(filter);
                    break;

                case "MinAgePolicy":
                    DeleteMinAgePolicy(filter);
                    break;

                case "MinProductPolicy":
                    DeleteMinProductPolicy(filter);
                    break;

                case "Offer":
                    DeleteOfferPolicy(filter);
                    break;

                case "OrPolicy":
                    DeleteOrPolicy(filter);
                    break;

                case "RestrictedHoursPolicy":
                    DeleteRestrictedHoursPolicy(filter);
                    break;
            }
        }
        private void DeleteIDiscountPolicy(String type, string discount_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount_id);
            switch (type)
            {
                case "ConditionalDiscount":
                    DeleteConditionalDiscount(filter);
                    break;

                case "DiscountAddition":
                    DeleteDiscountAddition(filter);
                    break;

                case "DiscountAnd":
                    DeleteDiscountAnd(filter);
                    break;

                case "DiscountMax":
                    DeleteDiscountMax(filter);
                    break;

                case "DiscountOr":
                    DeleteDiscountOr(filter);
                    break;

                case "DiscountMin":
                    DeleteDiscountMin(filter);
                    break;

                case "DiscountXor":
                    DeleteDiscountXor(filter);
                    break;

                case "DiscreetDiscount":
                    DeleteDiscreetDiscount(filter); ;
                    break;

                case "VisibleDiscount":
                    DeleteVisibleDiscount(filter);
                    break;

            }
        }
        private void DeleteIDiscountCondition(String type, string discount_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount_id);
            switch (type)
            {
                case "DiscountConditionAnd":
                    DeleteDiscountConditionAnd(filter);
                    break;

                case "DiscountConditionOr":
                    DeleteDiscountConditionOr(filter);
                    break;

                case "MaxProductCondition":
                    DeleteMaxProductCondition(filter);
                    break;

                case "MinBagPriceCondition":
                    DeleteMinBagPriceCondition(filter);
                    break;

                case "MinProductCondition":
                    DeleteMinProductCondition(filter);
                    break;

            }
        }

        private void AddPolicyToDB(String type, IPurchasePolicy policy)
        {
            switch (type)
            {
                case "AndPolicy":
                    Create((AndPolicy)policy);
                    break;

                case "Auction":
                    Create((Auction)policy);
                    break;

                case "BuyNow":
                    Create((BuyNow)policy);
                    break;

                case "ConditionalPolicy":
                    Create((ConditionalPolicy)policy);
                    break;

                case "Lottery":
                    Create((Lottery)policy);
                    break;

                case "MaxProductPolicy":
                    Create((MaxProductPolicy)policy);
                    break;

                case "MinAgePolicy":
                    Create((MinAgePolicy)policy);
                    break;

                case "MinProductPolicy":
                    Create((MinProductPolicy)policy);
                    break;

                case "Offer":
                    Create((Offer)policy);
                    break;

                case "OrPolicy":
                    Create((OrPolicy)policy);
                    break;

                case "RestrictedHoursPolicy":
                    Create((RestrictedHoursPolicy)policy);
                    break;
            }
        }
        private void AddDiscountToDB(String type, IDiscountTarget discount)
        {
            switch (type)
            {
                case "DiscountTargetCategories":
                    Create((DiscountTargetCategories)discount);
                    break;

                case "DiscountTargetProducts":
                    Create((DiscountTargetProducts)discount);
                    break;
            }
        }
        private void AddDiscountToDB(String type, IDiscountPolicy discount)
        {
            switch (type)
            {
                case "ConditionalDiscount":
                    Create((ConditionalDiscount)discount);
                    break;

                case "DiscountAddition":
                    Create((DiscountAddition)discount);
                    break;

                case "DiscountAnd":
                    Create((DiscountAnd)discount);
                    break;

                case "DiscountMax":
                    Create((DiscountMax)discount);
                    break;

                case "DiscountOr":
                    Create((DiscountOr)discount);
                    break;

                case "DiscountMin":
                    Create((DiscountMin)discount);
                    break;

                case "DiscountXor":
                    Create((DiscountXor)discount);
                    break;

                case "DiscreetDiscount":
                    Create((DiscreetDiscount)discount);
                    break;

                case "VisibleDiscount":
                    Create((VisibleDiscount)discount);
                    break;

            }
        }
        private void AddDiscountToDB(String type, IDiscountCondition discount)
        {
            switch (type)
            {
                case "DiscountConditionAnd":
                    Create((DiscountConditionAnd)discount);
                    break;

                case "DiscountConditionOr":
                    Create((DiscountConditionOr)discount);
                    break;

                case "MaxProductCondition":
                    Create((MaxProductCondition)discount);
                    break;

                case "MinBagPriceCondition":
                    Create((MinBagPriceCondition)discount);
                    break;

                case "MinProductCondition":
                    Create((MinProductCondition)discount);
                    break;
            }
        }

        // 2DTO
        public DTO_ShoppingCart Get_DTO_ShoppingCart(User user)
        {
            ConcurrentDictionary<String, DTO_ShoppingBag> dto_sb = new ConcurrentDictionary<String, DTO_ShoppingBag>();
            foreach (var sb in user.ShoppingCart.ShoppingBags)
            {
                ConcurrentDictionary<String, int> dto_products = new ConcurrentDictionary<string, int>();
                foreach (var p in sb.Value.Products)
                {
                    dto_products.TryAdd(p.Key.Id, p.Value);
                }
                dto_sb.TryAdd(sb.Key, new DTO_ShoppingBag(sb.Value.Id, sb.Value.UserId, sb.Value.Store.Id, dto_products, sb.Value.TotalBagPrice));
            }
            DTO_ShoppingCart dto_sc = new DTO_ShoppingCart(user.ShoppingCart.Id, dto_sb, user.ShoppingCart.TotalCartPrice);

            return dto_sc;
        }

        private DTO_ShoppingBag Get_DTO_ShoppingBag(ShoppingBag sb)
        {
            ConcurrentDictionary<String, int> dto_products = new ConcurrentDictionary<string, int>();
            foreach (var p in sb.Products)
            {
                dto_products.TryAdd(p.Key.Id, p.Value); //<Product id , quantity>
            }
            return new DTO_ShoppingBag(sb.Id, sb.UserId, sb.Store.Id, dto_products, sb.TotalBagPrice);
        }

        public DTO_History Get_DTO_History(History history)
        {
            LinkedList<DTO_PurchasedShoppingBag> dto_sb = new LinkedList<DTO_PurchasedShoppingBag>();
            foreach (ShoppingBag bag in history.ShoppingBags)
            {
                LinkedList<DTO_PurchasedProduct> dto_products = new LinkedList<DTO_PurchasedProduct>();
                foreach (KeyValuePair<Product, int> tuple in bag.Products)
                {
                    dto_products.AddLast(new DTO_PurchasedProduct(tuple.Key.Id, tuple.Key.Name, tuple.Key.Price, tuple.Value, tuple.Key.Category));
                }

                DTO_PurchasedShoppingBag dto_bag = new DTO_PurchasedShoppingBag(bag.Id, bag.UserId, bag.Store.Id, dto_products, bag.TotalBagPrice);
                dto_sb.AddLast(dto_bag);
            }
            return new DTO_History(dto_sb);
        }

        private LinkedList<DTO_Notification> Get_DTO_Notifications(LinkedList<Notification> pendingNotifications)
        {
            LinkedList<DTO_Notification> dto_pendingNotifications = new LinkedList<DTO_Notification>();
            foreach (Notification n in pendingNotifications)
            {
                dto_pendingNotifications.AddLast(new DTO_Notification(n.Message, n.Date.ToString(), n.isOpened, n.isStoreStaff, n.ClientId));
            }

            return dto_pendingNotifications;
        }

        public LinkedList<String> Get_DTO_ManagerList(LinkedList<StoreManager> list)
        {
            LinkedList<String> managers = new LinkedList<String>();

            foreach (StoreManager manager in list)
            {
                managers.AddLast(manager.User.Id);
            }

            return managers;
        }

        public LinkedList<String> Get_DTO_OwnerList(LinkedList<StoreOwner> list)
        {
            LinkedList<String> owners = new LinkedList<String>();

            foreach (StoreOwner owner in list)
            {
                owners.AddLast(owner.User.Id);
            }

            return owners;
        }
        // 2OBJ

        private List<Offer> ToObject(List<DTO_Offer> dto, User user)
        {
            List<Offer> Offers = new List<Offer>();
            if (dto != null)
            {
                foreach (DTO_Offer offer in dto)
                {
                    Offers.Add(new Offer(offer.UserID, offer.ProductID, offer.Amount, offer.Price, offer.StoreID, offer._id, offer.CounterOffer, offer.acceptedOwners));
                }
            }
            return Offers;
        }

        private List<OwnerRequest> ToObject(List<DTO_OwnerRequest> dto, User user)
        {
            List<OwnerRequest> Offers = new List<OwnerRequest>();
            if (dto != null)
            {
                foreach (DTO_OwnerRequest offer in dto)
                {
                    Offers.Add(new OwnerRequest(offer.UserID, offer.StoreID,offer.AppointedBy, offer._id, offer.acceptedOwners));
                }
            }
            return Offers;
        }

        private ShoppingCart ToObject(DTO_ShoppingCart dto, User user)
        {
            ConcurrentDictionary<String, ShoppingBag> sb = new ConcurrentDictionary<String, ShoppingBag>();
            foreach (var bag in dto.ShoppingBags)
            {
                ConcurrentDictionary<Product, int> products = new ConcurrentDictionary<Product, int>();
                foreach (var p in bag.Value.Products)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", p.Key);
                    products.TryAdd(LoadProduct(filter), p.Value);
                }
                Store store = LoadStore(Builders<BsonDocument>.Filter.Eq("_id", bag.Value.StoreId));
                sb.TryAdd(bag.Key, new ShoppingBag(bag.Key, user.Id , store , products, bag.Value.TotalBagPrice));
            }
            ShoppingCart sc = new ShoppingCart(dto._id, sb, dto.TotalCartPrice);
            return sc;
        }
        private History ToObject(DTO_History dto)
        {
            LinkedList<ShoppingBag> shoppingBags = new LinkedList<ShoppingBag>();
            foreach (DTO_PurchasedShoppingBag bag in dto.ShoppingBags)
            {
                ConcurrentDictionary<Product, int> products = new ConcurrentDictionary<Product, int>();
                foreach (DTO_PurchasedProduct p in bag.Products)
                {
                    Product toadd = new Product(p._id, p.Name, p.Price, p.ProductQuantity, p.Category);
                    products[toadd] = p.ProductQuantity;
                }
                Store store = getStoreById(bag.StoreId);
                shoppingBags.AddLast(new ShoppingBag(bag._id, bag.UserId, store, products, bag.TotalBagPrice));
            }
            return new History(shoppingBags);
        }

        private LinkedList<Notification> ToObject(LinkedList<DTO_Notification> dto_list)
        {
            LinkedList<Notification> pendingNotifications = new LinkedList<Notification>();
            foreach (DTO_Notification n in dto_list)
            {
                pendingNotifications.AddLast(new Notification(n.ClientId, n.Message, n.isStoreStaff, n.isOpened, n.Date));
            }
            return pendingNotifications;
        }

        private Store getStoreById(String Id)
        {
            Stores.TryGetValue(Id, out Store s);
            return s;
        }

        // load all DTOS
        public List<RegisteredUser> LoadAllRegisterUsers()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_RegisteredUser.collection.Find(filter).ToList();
            List<DTO_RegisteredUser> registerUsersDTO = new List<DTO_RegisteredUser>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_RegisteredUser dto = JsonConvert.DeserializeObject<DTO_RegisteredUser>(json);
                registerUsersDTO.Add(dto);
            }
            List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
            foreach (DTO_RegisteredUser dto in registerUsersDTO)
            {
                RegisteredUser registerUser = LoadRegisteredUser(Builders<BsonDocument>.Filter.Eq("_id", dto._id));
                registeredUsers.Add(registerUser);
            }
            return registeredUsers;
        }

        public LinkedList<String> LoadAllSystemAdmins()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_SystemAdmins.collection.Find(filter).ToList();
            List<DTO_SystemAdmins> systemAdminDTO = new List<DTO_SystemAdmins>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_SystemAdmins dto = JsonConvert.DeserializeObject<DTO_SystemAdmins>(json);
                systemAdminDTO.Add(dto);
            }
            if (systemAdminDTO.Count > 0)
            {
                return systemAdminDTO[0].SystemAdmins;
            }
            else { return null; }
        }

        public List<Store> LoadAllStores()
        {
            //load all stores dto
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = DAO_Store.collection.Find(filter).ToList();
            List<DTO_Store> storesDTOs = new List<DTO_Store>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_Store dto = JsonConvert.DeserializeObject<DTO_Store>(json);
                storesDTOs.Add(dto);
            }
            List<Store> stores = new List<Store>();
            foreach (DTO_Store dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("_id", dto._id);
                Store s = LoadStore(f);
                stores.Add(s);
            }
            return stores;
        }

        public ConcurrentDictionary<String,StoreOwner> loadAllStoreOwnerForStore(FilterDefinition<BsonDocument> storefilter)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = this.DAO_StoreOwner.collection.Find(filter).ToList();
            List<DTO_StoreOwner> storesDTOs = new List<DTO_StoreOwner>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_StoreOwner dto = JsonConvert.DeserializeObject<DTO_StoreOwner>(json);
                storesDTOs.Add(dto);
            }
            ConcurrentDictionary<String,StoreOwner> owners = new ConcurrentDictionary<String, StoreOwner>();
            foreach (DTO_StoreOwner dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("UserId", dto.UserId);
                StoreOwner s = LoadStoreOwner(f);
                owners.TryAdd(dto.UserId, s);
            }
            return owners;
        }

        public ConcurrentDictionary<String, StoreManager> LoadAllManagersForStore(FilterDefinition<BsonDocument> storefilter)
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            List<BsonDocument> docs = this.DAO_StoreManager.collection.Find(filter).ToList();
            List<DTO_StoreManager> storesDTOs = new List<DTO_StoreManager>();
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                DTO_StoreManager dto = JsonConvert.DeserializeObject<DTO_StoreManager>(json);
                storesDTOs.Add(dto);
            }
            ConcurrentDictionary<String, StoreManager> owners = new ConcurrentDictionary<String, StoreManager>();
            foreach (DTO_StoreManager dto in storesDTOs)
            {
                var f = Builders<BsonDocument>.Filter.Eq("UserId", dto.UserId);
                StoreManager s = LoadStoreManager(f);
                owners.TryAdd(dto.UserId, s);
            }
            return owners;
        }

        // reg user
        public void Create(RegisteredUser ru)
        {
            DAO_RegisteredUser.Create(new DTO_RegisteredUser(ru.Id, Get_DTO_ShoppingCart(ru), ru.UserName, Cryptography.encrypt(ru._password), ru.Active, Get_DTO_History(ru.History), Get_DTO_Notifications(ru.PendingNotification), Get_DTO_Offers(ru.PendingOffers), Get_DTO_Offers(ru.AcceptedOffers)));
            RegisteredUsers.TryAdd(ru.Id, ru);
        }

        public RegisteredUser LoadRegisteredUser(FilterDefinition<BsonDocument> filter)
        {
            RegisteredUser ru;
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            if (dto != null && RegisteredUsers.TryGetValue(dto._id, out ru))
            {
                return ru;
            }
            string pass = Cryptography.Decrypt(dto._password);
            DTO_History temp = dto.History;
            ru = new RegisteredUser(dto._id, dto.UserName, pass, dto.Active, ToObject(dto.History), ToObject(dto.PendingNotification));
            ru.ShoppingCart = ToObject(dto.ShoppingCart, ru);
            ru.AcceptedOffers = ToObject(dto.AcceptedOffers, ru);
            ru.PendingOffers = ToObject(dto.PendingOffers, ru);
            RegisteredUsers.TryAdd(ru.Id, ru);
            return ru;
        }

        public void UpdateRegisteredUser(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, Boolean upsert = false, MongoDB.Driver.IClientSessionHandle session = null)
        {
            DAO_RegisteredUser.Update(filter, update, upsert , session);
        }

        public void DeleteRegisteredUser(FilterDefinition<BsonDocument> filter)
        {
            DTO_RegisteredUser deletedRegisteredUser = DAO_RegisteredUser.Delete(filter);
            if (!(deletedRegisteredUser is null))
            {
                RegisteredUsers.TryRemove(deletedRegisteredUser._id, out RegisteredUser ru);
            }
        }

        public void Load_RegisteredUserHistory(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.History = ToObject(dto.History);
        }
        public void Load_RegisteredUserNotifications(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.PendingNotification = ToObject(dto.PendingNotification);
        }
        public void Load_RegisteredUserShoppingCart(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.ShoppingCart = ToObject(dto.ShoppingCart, user);
        }

        // store manager
        public void Create(StoreManager sm)
        {
            DAO_StoreManager.Create(new DTO_StoreManager(sm.GetId(), sm.Permission.functionsBitMask, sm.AppointedBy.GetId(), sm.Store.Id));
            LinkedList<StoreManager> list;
            if (StoreManagers.ContainsKey(sm.GetId()))
            {
                StoreManagers.TryGetValue(sm.GetId(), out list);
                list.AddLast(sm);
            }
            else
            {
                list = new LinkedList<StoreManager>();
                list.AddLast(sm);
                StoreManagers.TryAdd(sm.GetId(), list);
            }
        }

        public StoreManager LoadStoreManager(FilterDefinition<BsonDocument> filter)
        {
            StoreManager sm;
            LinkedList<StoreManager> list;
            DTO_StoreManager dto = DAO_StoreManager.Load(filter);       

            bool listExists = StoreManagers.TryGetValue(dto.UserId, out list);
            if (listExists)
            {
                foreach (StoreManager manager in list)
                {
                    if (manager.Store.Id == dto.UserId)
                    {
                        return manager;
                    }
                }

            }

            return null;
        }

        public void UpdateStoreManager(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_StoreManager.Update(filter, update);
        }

        public void DeleteStoreManager(FilterDefinition<BsonDocument> filter)
        {
            DTO_StoreManager deletedStoreManager = DAO_StoreManager.Delete(filter);
            StoreManager sm = null;
            LinkedList<StoreManager> list;
            if (!(deletedStoreManager is null))
            {
                if (StoreManagers.TryGetValue(deletedStoreManager.UserId, out list))
                {
                    foreach (StoreManager manager in list)
                    {
                        if (manager.Store.Id == deletedStoreManager.UserId)
                        {
                            sm = manager;
                        }
                    }
                }

                list.Remove(sm);
                if (list.Count == 0)
                {
                    StoreManagers.TryRemove(sm.GetId(), out _);
                }
            }
        }

        private void AddManagerToIdentityMap(StoreManager manager)
        {
            LinkedList<StoreManager> list;
            bool listExists = StoreManagers.TryGetValue(manager.GetId(), out list);
            if (listExists)
            {
                list.AddLast(manager);
            }
            else
            {
                list = new LinkedList<StoreManager>();
                list.AddLast(manager);
                StoreManagers.TryAdd(manager.GetId(), list);
            }
        }

        // store owner
        public void Create(StoreOwner so)
        {

            String appointedById = "0";
            if (so.AppointedBy != null)
            {
                appointedById = so.AppointedBy.GetId();
            }

            DAO_StoreOwner.Create(new DTO_StoreOwner(so.GetId(), so.StoreId, appointedById, Get_DTO_ManagerList(so.StoreManagers), Get_DTO_OwnerList(so.StoreOwners)));
            LinkedList<StoreOwner> list;
            if (StoreOwners.ContainsKey(so.GetId()))
            {
                StoreOwners.TryGetValue(so.GetId(), out list);
                list.AddLast(so);
            }
            else
            {
                list = new LinkedList<StoreOwner>();
                list.AddLast(so);
                StoreOwners.TryAdd(so.GetId(), list);
            }
        }

        public StoreOwner LoadStoreOwner(FilterDefinition<BsonDocument> filter)
        {
            StoreOwner so;
            LinkedList<StoreOwner> list;
            DTO_StoreOwner dto = DAO_StoreOwner.Load(filter);       

            bool listExists = StoreOwners.TryGetValue(dto.UserId, out list);
            if (listExists)
            {
                foreach (StoreOwner owner in list) { if (owner.StoreId == dto.StoreId) { return owner; } }

            }
            return null;
        }

        public StoreOwner getOwnershipTree(Store store, String founder_id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("UserId", founder_id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
            DTO_StoreOwner founder_dto = DAO_StoreOwner.Load(filter);
            var filter2 = Builders<BsonDocument>.Filter.Eq("_id", founder_dto.UserId);
            StoreOwner founder = new StoreOwner(LoadRegisteredUser(filter2), store.Id, null);

            if (founder_dto.StoreOwners.Count > 0)
            {
                foreach (String owner_id in founder_dto.StoreOwners)
                {
                    StoreOwner storeowner = getOwnershipTree(store, owner_id);
                    storeowner.AppointedBy = founder;
                    founder.StoreOwners.AddLast(storeowner);
                }
            }
            if (founder_dto.StoreManagers.Count > 0)
            {
                foreach (String manager_id in founder_dto.StoreManagers)
                {
                    var manager_filter = Builders<BsonDocument>.Filter.Eq("UserId", manager_id) & Builders<BsonDocument>.Filter.Eq("StoreId", store.Id);
                    DTO_StoreManager manager_dto = DAO_StoreManager.Load(manager_filter);
                    var user_filter = Builders<BsonDocument>.Filter.Eq("_id", manager_id);
                    StoreManager manager = new StoreManager(LoadRegisteredUser(user_filter), store, new Permission(manager_dto.Permission), founder);
                    founder.StoreManagers.AddLast(manager);
                    store.Managers.TryAdd(manager_id, manager);
                    AddManagerToIdentityMap(manager);
                }
            }
            store.Owners.TryAdd(founder_id, founder);
            AddOwnerToIdentityMap(founder);

            return founder;
        }

        public void UpdateStoreOwner(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_StoreOwner.Update(filter, update);
        }

        public void DeleteStoreOwner(FilterDefinition<BsonDocument> filter)
        {
            DTO_StoreOwner deletedStoreOwner = DAO_StoreOwner.Delete(filter); 
            StoreOwner so = null;
            LinkedList<StoreOwner> list;
            if (!(deletedStoreOwner is null))
            {
                if (StoreOwners.TryGetValue(deletedStoreOwner.UserId, out list))
                {
                    foreach (StoreOwner owner in list)
                    {
                        if (owner.StoreId == deletedStoreOwner.StoreId) { so = owner; }
                    }
                }

                list.Remove(so);
                if (list.Count == 0)
                {
                    StoreOwners.TryRemove(so.GetId(), out _);
                }
            }
        }

        private void AddOwnerToIdentityMap(StoreOwner owner)
        {
            LinkedList<StoreOwner> list;
            bool listExists = StoreOwners.TryGetValue(owner.GetId(), out list);
            if (listExists)
            {
                list.AddLast(owner);
            }
            else
            {
                list = new LinkedList<StoreOwner>();
                list.AddLast(owner);
                StoreOwners.TryAdd(owner.GetId(), list);
            }
        }

        // system admin
        public void UpdateSystemAdmins(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, Boolean upsert = false)
        {
            DAO_SystemAdmins.Update(filter, update, upsert);
        }

        // product

        public void Create(Product p)
        {
            DAO_Product.Create(new DTO_Product(p.Id, p.Name, p.Price, p.Quantity, p.Category, p.Rate, p.NumberOfRates, p.KeyWords));
            Products.TryAdd(p.Id, p);
        }

        public Product LoadProduct(FilterDefinition<BsonDocument> filter)
        {
            Product p;
            DTO_Product dto = DAO_Product.Load(filter);
            if (Products.TryGetValue(dto._id, out p))
            {
                return p;
            }

            p = new Product(dto._id, dto.Name, dto.Price, dto.Quantity, dto.Category, dto.KeyWords);
            Products.TryAdd(p.Id, p);
            return p;
        }

        public void UpdateProduct(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update , MongoDB.Driver.IClientSessionHandle session = null)
        {
            DAO_Product.Update(filter, update,session:session);
        }

        public void DeleteProduct(FilterDefinition<BsonDocument> filter)
        {
            DTO_Product deletedProduct = DAO_Product.Delete(filter);
            if (!(deletedProduct is null))
            {
                Products.TryRemove(deletedProduct._id, out Product p);
            }
        }

        // stores
        public void Create(Store s)
        {
            LinkedList<String> owners = new LinkedList<String>();
            LinkedList<String> managers = new LinkedList<String>();
            LinkedList<String> inventory = new LinkedList<String>();

            foreach (var owner in s.Owners) { owners.AddLast(owner.Key); }
            foreach (var manager in s.Managers) { managers.AddLast(manager.Key); }
            foreach (var product in s.InventoryManager.Products) { inventory.AddLast(product.Key); }

            DAO_Store.Create(new DTO_Store(s.Id, s.Name, s.Founder.GetId(), owners, managers, inventory, Get_DTO_History(s.History),
                                            s.Rate, s.NumberOfRates, s.Active, s.PolicyManager.MainDiscount.getDTO(), s.PolicyManager.MainPolicy.getDTO(), s.OfferManager.GetDTO() , s.RequestManager.GetDTO()));
            this.DAO_DiscountAddition.Create(s.PolicyManager.MainDiscount.getDTO());
            this.DAO_BuyNow.Create(s.PolicyManager.MainPolicy.getDTO());
            Stores.TryAdd(s.Id, s);
        }

        public Store LoadStore(FilterDefinition<BsonDocument> filter)
        {
            Store s;
            DTO_Store dto = DAO_Store.Load(filter);
            if (Stores.TryGetValue(dto._id, out s)) { return s; }

            ConcurrentDictionary<String, Product> products = new ConcurrentDictionary<String, Product>();
            NotificationPublisher notificationManager = new NotificationPublisher();

            foreach (String product in dto.InventoryManager)
            {
                var filter3 = Builders<BsonDocument>.Filter.Eq("_id", product);
                Product p = LoadProduct(filter3);
                p.NotificationPublisher = notificationManager;
                products.TryAdd(product, p);
            }

            s = new Store(dto._id, dto.Name, new InventoryManager(products), ToObject(dto.History), dto.Rate, dto.NumberOfRates, notificationManager, dto.Active);
            s.NotificationPublisher.Store = s;

            Stores.TryAdd(s.Id, s);
            DiscountAddition MainDiscount = LoadDiscountAddition(Builders<BsonDocument>.Filter.Eq("_id", dto.MainDiscount._id));
            BuyNow MainPolicy = LoadBuyNowPolicy(Builders<BsonDocument>.Filter.Eq("_id", dto.MainPolicy._id));
            if (MainPolicy is null) { MainPolicy = new BuyNow(); }
            s.PolicyManager = new PolicyManager(MainDiscount, MainPolicy);
            StoreOwner founder = getOwnershipTree(s, dto.Founder);
            s.Founder = founder;

            //s.History = ToObject(dto.History);
            foreach (ShoppingBag shb in s.History.ShoppingBags)
            {
                shb.Store = s;
            }
            s.OfferManager = new OfferManager(Load_StoreOfferManager(dto));
            s.RequestManager = new OwnerRequestManager(Load_StoreRequestManagger(dto));
            // loading staff
            //var filterstaff = Builders<BsonDocument>.Filter.Eq("StoreId", s.Id);
            //s.Managers = LoadAllManagersForStore(filterstaff);
            //s.Owners = loadAllStoreOwnerForStore(filterstaff);
            return s;
        }

        public void UpdateStore(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update , MongoDB.Driver.IClientSessionHandle session = null)
        {
            DAO_Store.Update(filter, update,session:session);
        }

        public void DeleteStore(FilterDefinition<BsonDocument> filter)
        {
            DTO_Store deletedStore = DAO_Store.Delete(filter);
            if (!(deletedStore is null))
            {
                Stores.TryRemove(deletedStore._id, out Store s);
                foreach (string ownerid in deletedStore.Owners)
                {
                    var filter2 = Builders<BsonDocument>.Filter.Eq("UserId", ownerid);
                    this.DeleteStoreOwner(filter2);
                }
                foreach(string productid in deletedStore.InventoryManager)
                {
                    var filter4 = Builders<BsonDocument>.Filter.Eq("_id", productid);
                    this.DeleteProduct(filter4);
                }
                var filter3 = Builders<BsonDocument>.Filter.Eq("_id", deletedStore.MainPolicy._id);
                this.DeleteBuyNowPolicy(filter3);
                filter3 = Builders<BsonDocument>.Filter.Eq("_id", deletedStore.MainDiscount._id);
                this.DeleteDiscountAddition(filter3);
            }
        }

        public void Load_StoreHistory(Store store)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
            DTO_Store dto = DAO_Store.Load(filter);
            store.History = ToObject(dto.History);
        }

        public List<Offer> Load_StoreOfferManager(DTO_Store store)
        {
            List<Offer> offers = new List<Offer>();
            if (store.OfferManager != null)
            {
                foreach (DTO_Offer offer in store.OfferManager)
                {
                    offers.Add(new Offer(offer.UserID, offer.ProductID, offer.Amount, offer.Price, offer.StoreID, offer._id, offer.CounterOffer, offer.acceptedOwners));
                }
            }

            return offers;
        }

        public List<OwnerRequest> Load_StoreRequestManagger(DTO_Store store)
        {
            List<OwnerRequest> offers = new List<OwnerRequest>();
            if (store.OwnerRequestManager != null)
            {
                foreach (DTO_OwnerRequest offer in store.OwnerRequestManager)
                {
                    offers.Add(new OwnerRequest(offer.UserID, offer.StoreID,offer.AppointedBy, offer._id, offer.acceptedOwners));
                }
            }
            return offers;
        }

        // purchase policy
        public void Create(Auction auction)
        {
            DAO_Auction.Create(new DTO_Auction(auction.Id, auction.ClosingTime.ToString(), auction.StartingPrice, auction.LastOffer.Item1, auction.LastOffer.Item2));
            Policy_Auctions.TryAdd(auction.Id, auction);
        }

        public Auction LoadAuctionPolicy(FilterDefinition<BsonDocument> filter)
        {
            Auction a;
            DTO_Auction dto = DAO_Auction.Load(filter);
            if (Policy_Auctions.TryGetValue(dto._id, out a))
            {
                return a;
            }

            a = new Auction(dto._id, dto.ClosingTime, dto.StartingPrice, new Tuple<double, string>(dto.LastOffer_Price, dto.LastOffer_UserId));
            Policy_Auctions.TryAdd(a.Id, a);
            return a;
        }

        public void UpdateAuctionPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_Auction.Update(filter, update);
        }

        public void DeleteAuctionPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_Auction deletedAuction = DAO_Auction.Delete(filter);
            if (!(deletedAuction is null))
            {
                Policy_Auctions.TryRemove(deletedAuction._id, out Auction a);
            }
        }


        public void Create(Lottery lottery)
        {
            DAO_Lottery.Create(new DTO_Lottery(lottery.Id, lottery.Price, lottery.Participants));
            Policy_Lotterys.TryAdd(lottery.Id, lottery);
        }

        public Lottery LoadLotteryPolicy(FilterDefinition<BsonDocument> filter)
        {
            Lottery l;
            DTO_Lottery dto = DAO_Lottery.Load(filter);
            if (Policy_Lotterys.TryGetValue(dto._id, out l))
            {
                return l;
            }

            l = new Lottery(dto._id, dto.Price, dto.Participants);
            Policy_Lotterys.TryAdd(l.Id, l);
            return l;
        }

        public void UpdateLotteryPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_Lottery.Update(filter, update);
        }

        public void DeleteLotteryPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_Lottery deletedLottery = DAO_Lottery.Delete(filter);
            if (!(deletedLottery is null))
            {
                Policy_Lotterys.TryRemove(deletedLottery._id, out Lottery l);
            }
        }


        public void Create(MaxProductPolicy maxProductPolicy)
        {
            DAO_MaxProductPolicy.Create(new DTO_MaxProductPolicy(maxProductPolicy.Id, maxProductPolicy.ProductId, maxProductPolicy.Max));
            Policy_MaxProductPolicys.TryAdd(maxProductPolicy.Id, maxProductPolicy);
        }

        public MaxProductPolicy LoadMaxProductPolicy(FilterDefinition<BsonDocument> filter)
        {
            MaxProductPolicy m;
            DTO_MaxProductPolicy dto = DAO_MaxProductPolicy.Load(filter);
            if (Policy_MaxProductPolicys.TryGetValue(dto._id, out m))
            {
                return m;
            }
            var product_filter = Builders<BsonDocument>.Filter.Eq("_id", dto.Product);
            m = new MaxProductPolicy(LoadProduct(product_filter).Id, dto.Max, dto._id);
            Policy_MaxProductPolicys.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMaxProductPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MaxProductPolicy.Update(filter, update);
        }

        public void DeleteMaxProductPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_MaxProductPolicy deletedMaxProductPolicy = DAO_MaxProductPolicy.Delete(filter);
            if (!(deletedMaxProductPolicy is null))
            {
                Policy_MaxProductPolicys.TryRemove(deletedMaxProductPolicy._id, out MaxProductPolicy m);
            }
        }


        public void Create(MinAgePolicy minAgePolicy)
        {
            DAO_MinAgePolicy.Create(new DTO_MinAgePolicy(minAgePolicy.Id, minAgePolicy.Age));
            Policy_MinAgePolicys.TryAdd(minAgePolicy.Id, minAgePolicy);
        }

        public MinAgePolicy LoadMinAgePolicy(FilterDefinition<BsonDocument> filter)
        {
            MinAgePolicy m;
            DTO_MinAgePolicy dto = DAO_MinAgePolicy.Load(filter);
            if (Policy_MinAgePolicys.TryGetValue(dto._id, out m))
            {
                return m;
            }
            m = new MinAgePolicy(dto.Age, dto._id);
            Policy_MinAgePolicys.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMinAgePolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MinAgePolicy.Update(filter, update);
        }

        public void DeleteMinAgePolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_MinAgePolicy deletedMinAgePolicy = DAO_MinAgePolicy.Delete(filter);
            if (!(deletedMinAgePolicy is null))
            {
                Policy_MinAgePolicys.TryRemove(deletedMinAgePolicy._id, out MinAgePolicy m);
            }
        }


        public void Create(MinProductPolicy minProductPolicy)
        {
            DAO_MinProductPolicy.Create(new DTO_MinProductPolicy(minProductPolicy.Id, minProductPolicy.ProductId, minProductPolicy.Min));
            Policy_MinProductPolicys.TryAdd(minProductPolicy.Id, minProductPolicy);
        }

        public MinProductPolicy LoadMinProductPolicy(FilterDefinition<BsonDocument> filter)
        {
            MinProductPolicy m;
            DTO_MinProductPolicy dto = DAO_MinProductPolicy.Load(filter);
            if (Policy_MinProductPolicys.TryGetValue(dto._id, out m))
            {
                return m;
            }
            var product_filter = Builders<BsonDocument>.Filter.Eq("_id", dto.Product);
            m = new MinProductPolicy(LoadProduct(product_filter).Id, dto.Min, dto._id);
            Policy_MinProductPolicys.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMinProductPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MinProductPolicy.Update(filter, update);
        }

        public void DeleteMinProductPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_MinProductPolicy deletedMinProductPolicy = DAO_MinProductPolicy.Delete(filter);
            if (!(deletedMinProductPolicy is null))
            {
                Policy_MinProductPolicys.TryRemove(deletedMinProductPolicy._id, out MinProductPolicy m);
            }
        }


        public void Create(Offer offer)
        {
            //Temporary change so there aren't any build errors Kfir
            //DAO_Offer.Create(new DTO_Offer(offer.Id, offer.LastOffer.Item1, offer.LastOffer.Item2 , offer.CounterOffer , offer.Accepted));
            //Policy_Offers.TryAdd(offer.Id, offer);
        }

        public Offer LoadOfferPolicy(FilterDefinition<BsonDocument> filter)
        {
            Offer o;
            DTO_Offer dto = DAO_Offer.Load(filter);
            if (Policy_Offers.TryGetValue(dto._id, out o))
            {
                return o;
            }

            //Temporary change so there aren't any build errors Kfir
            //o = new Offer(dto._id , new Tuple<Double , string>(dto.LastOffer_Price , dto.LastOffer_UserId) , dto.CounterOffer , dto.Accepted);
            o = new Offer("U1", "P1", 1, 10, "S1");
            Policy_Offers.TryAdd(o.Id, o);
            return o;
        }

        public void UpdateOfferPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_Offer.Update(filter, update);
        }

        public void UpdateOwnerRequestPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            this.DAO_OwnerRequest.Update(filter, update);
        }

        public void DeleteOfferPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_Offer deletedOffer = DAO_Offer.Delete(filter);
            if (!(deletedOffer is null))
            {
                Policy_Offers.TryRemove(deletedOffer._id, out Offer o);
            }
        }

        public void DeleteOwnerRequestPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_OwnerRequest deletedOffer = this.DAO_OwnerRequest.Delete(filter);
            if (!(deletedOffer is null))
            {
                OwnerRequests.TryRemove(deletedOffer._id, out OwnerRequest o);
            }
        }

        public void Create(RestrictedHoursPolicy restrictedHoursPolicy)
        {
            DAO_RestrictedHoursPolicy.Create(new DTO_RestrictedHoursPolicy(restrictedHoursPolicy.Id, restrictedHoursPolicy.StartRestrict.ToString(), restrictedHoursPolicy.EndRestrict.ToString(), restrictedHoursPolicy.ProductId));
            Policy_RestrictedHoursPolicys.TryAdd(restrictedHoursPolicy.Id, restrictedHoursPolicy);
        }

        public RestrictedHoursPolicy LoadRestrictedHoursPolicy(FilterDefinition<BsonDocument> filter)
        {
            RestrictedHoursPolicy r;
            DTO_RestrictedHoursPolicy dto = DAO_RestrictedHoursPolicy.Load(filter);
            if (Policy_RestrictedHoursPolicys.TryGetValue(dto._id, out r))
            {
                return r;
            }

            var product_filter = Builders<BsonDocument>.Filter.Eq("_id", dto.Product);
            r = new RestrictedHoursPolicy(DateTime.Parse(dto.StartRestrict), DateTime.Parse(dto.EndRestrict), LoadProduct(product_filter).Id, dto._id);
            Policy_RestrictedHoursPolicys.TryAdd(r.Id, r);
            return r;
        }

        public void UpdateRestrictedHoursPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_RestrictedHoursPolicy.Update(filter, update);
        }

        public void DeleteRestrictedHoursPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_RestrictedHoursPolicy deletedRestrictedHoursPolicy = DAO_RestrictedHoursPolicy.Delete(filter);
            if (!(deletedRestrictedHoursPolicy is null))
            {
                Policy_RestrictedHoursPolicys.TryRemove(deletedRestrictedHoursPolicy._id, out RestrictedHoursPolicy r);
            }
        }



        public void Create(AndPolicy andPolicy)
        {
            DAO_AndPolicy.Create(new DTO_AndPolicy(andPolicy.Id, getPoliciesIDs(andPolicy.Policies)));
            Policy_AndPolicys.TryAdd(andPolicy.Id, andPolicy);
        }

        public AndPolicy LoadAndPolicy(FilterDefinition<BsonDocument> filter)
        {
            AndPolicy a;
            DTO_AndPolicy dto = DAO_AndPolicy.Load(filter);
            if (Policy_AndPolicys.TryGetValue(dto._id, out a))
            {
                return a;
            }

            List<IPurchasePolicy> Policies = new List<IPurchasePolicy>();
            foreach (var policy in dto.Policies)
            {
                IPurchasePolicy p = LoadIPurchasePolicy(policy.Value, policy.Key);
                Policies.Add(p);
            }

            a = new AndPolicy(Policies, dto._id);
            Policy_AndPolicys.TryAdd(a.Id, a);
            return a;
        }

        public void UpdateAndPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_AndPolicy.Update(filter, update);
        }

        public void DeleteAndPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_AndPolicy deletedAndPolicy = DAO_AndPolicy.Delete(filter);
            if (!(deletedAndPolicy is null))
            {
                foreach (var policy in deletedAndPolicy.Policies)
                {
                    DeleteIPurchasePolicy(policy.Key, policy.Value);
                }
                Policy_AndPolicys.TryRemove(deletedAndPolicy._id, out AndPolicy a);
            }

        }



        public void Create(OrPolicy orPolicy)
        {
            DAO_OrPolicy.Create(new DTO_OrPolicy(orPolicy.Id, getPoliciesIDs(orPolicy.Policies)));
            Policy_OrPolicys.TryAdd(orPolicy.Id, orPolicy);
        }

        public OrPolicy LoadOrPolicy(FilterDefinition<BsonDocument> filter)
        {
            OrPolicy o;
            DTO_OrPolicy dto = DAO_OrPolicy.Load(filter);
            if (Policy_OrPolicys.TryGetValue(dto._id, out o))
            {
                return o;
            }

            List<IPurchasePolicy> Policies = new List<IPurchasePolicy>();
            foreach (var policy in dto.Policies)
            {
                IPurchasePolicy p = LoadIPurchasePolicy(policy.Value, policy.Key);
                Policies.Add(p);
            }

            o = new OrPolicy(Policies, dto._id);
            Policy_OrPolicys.TryAdd(o.Id, o);
            return o;
        }

        public void UpdateOrPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_OrPolicy.Update(filter, update);
        }

        public void DeleteOrPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_OrPolicy deletedOrPolicy = DAO_OrPolicy.Delete(filter);
            if (!(deletedOrPolicy is null))
            {
                foreach (var policy in deletedOrPolicy.Policies)
                {
                    DeleteIPurchasePolicy(policy.Key, policy.Value);
                }
                Policy_OrPolicys.TryRemove(deletedOrPolicy._id, out OrPolicy o);
            }

        }



        public void Create(BuyNow buyNow)
        {
            DAO_BuyNow.Create(new DTO_BuyNow(buyNow.Id, new DTO_AndPolicy(buyNow.Policy.Id, getPoliciesIDs(buyNow.Policy.Policies))));
            Policy_BuyNows.TryAdd(buyNow.Id, buyNow);
        }

        public BuyNow LoadBuyNowPolicy(FilterDefinition<BsonDocument> filter)
        {
            BuyNow b;
            DTO_BuyNow dto = DAO_BuyNow.Load(filter);
            if (dto is null) { return null; }
            if (Policy_BuyNows.TryGetValue(dto._id, out b))
            {
                return b;
            }

            List<IPurchasePolicy> Policies = new List<IPurchasePolicy>();
            foreach (var policy in dto.Policy.Policies)
            {
                IPurchasePolicy p = LoadIPurchasePolicy(policy.Value, policy.Key);
                Policies.Add(p);
            }

            AndPolicy andPolicy = new AndPolicy(Policies, dto.Policy._id);
            b = new BuyNow(andPolicy, dto._id);
            Policy_BuyNows.TryAdd(b.Id, b);
            return b;
        }

        public void UpdateBuyNowPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_BuyNow.Update(filter, update);
        }

        public void DeleteBuyNowPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_BuyNow deletedBuyNow = DAO_BuyNow.Delete(filter);
            if (!(deletedBuyNow is null))
            {
                foreach (var policy in deletedBuyNow.Policy.Policies)
                {
                    DeleteIPurchasePolicy(policy.Key, policy.Value);
                }
                Policy_BuyNows.TryRemove(deletedBuyNow._id, out BuyNow b);
            }

        }




        public void Create(ConditionalPolicy conditionalPolicy)
        {
            List<IPurchasePolicy> list = new List<IPurchasePolicy>();
            list.Add(conditionalPolicy.PreCond);
            ConcurrentDictionary<String, String> PreCond = getPoliciesIDs(list);    // <id , type>

            List<IPurchasePolicy> list2 = new List<IPurchasePolicy>();
            list2.Add(conditionalPolicy.Cond);
            ConcurrentDictionary<String, String> Cond = getPoliciesIDs(list2);      // <id , type>


            DAO_ConditionalPolicy.Create(new DTO_ConditionalPolicy(conditionalPolicy.Id, PreCond, Cond));
            Policy_ConditionalPolicys.TryAdd(conditionalPolicy.Id, conditionalPolicy);
        }

        public ConditionalPolicy LoadConditionalPolicy(FilterDefinition<BsonDocument> filter)
        {
            ConditionalPolicy c;
            DTO_ConditionalPolicy dto = DAO_ConditionalPolicy.Load(filter);
            if (Policy_ConditionalPolicys.TryGetValue(dto._id, out c))
            {
                return c;
            }
            IPurchasePolicy pre = null;
            foreach (var policy in dto.PreCond)
            {
                pre = LoadIPurchasePolicy(policy.Value, policy.Key);
            }

            IPurchasePolicy cond = null;
            foreach (var policy in dto.Cond)
            {
                cond = LoadIPurchasePolicy(policy.Value, policy.Key);
            }

            c = new ConditionalPolicy(pre, cond, dto._id);
            Policy_ConditionalPolicys.TryAdd(c.Id, c);
            return c;
        }

        public void UpdateConditionalPolicy(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_ConditionalPolicy.Update(filter, update);
        }

        public void DeleteConditionalPolicy(FilterDefinition<BsonDocument> filter)
        {
            DTO_ConditionalPolicy deletedConditionalPolicy = DAO_ConditionalPolicy.Delete(filter);
            if (!(deletedConditionalPolicy is null))
            {
                foreach (var policy in deletedConditionalPolicy.PreCond)
                {
                    DeleteIPurchasePolicy(policy.Key, policy.Value);
                }
                foreach (var policy in deletedConditionalPolicy.Cond)
                {
                    DeleteIPurchasePolicy(policy.Key, policy.Value);
                }

                Policy_ConditionalPolicys.TryRemove(deletedConditionalPolicy._id, out ConditionalPolicy c);
            }
        }

        // discount policy

        public void Create(VisibleDiscount visibleDiscount)
        {
            DAO_VisibleDiscount.Create(new DTO_VisibleDiscount(visibleDiscount.Id, visibleDiscount.ExpirationDate.ToString(), getDiscountsIDs(visibleDiscount.Target), visibleDiscount.Percentage));
            Discount_VisibleDiscounts.TryAdd(visibleDiscount.Id, visibleDiscount);
        }

        public VisibleDiscount LoadVisibleDiscount(FilterDefinition<BsonDocument> filter)
        {
            VisibleDiscount v;
            DTO_VisibleDiscount dto = DAO_VisibleDiscount.Load(filter);
            if (Discount_VisibleDiscounts.TryGetValue(dto._id, out v))
            {
                return v;
            }

            IDiscountTarget Target = null;
            foreach (var discount in dto.Target)
            {
                Target = LoadIDiscountTarget(discount.Value, discount.Key);
            }

            v = new VisibleDiscount(DateTime.Parse(dto.ExpirationDate), Target, dto.Percentage, dto._id);
            Discount_VisibleDiscounts.TryAdd(v.Id, v);
            return v;
        }

        public void UpdateVisibleDiscount(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_VisibleDiscount.Update(filter, update);
        }

        public void DeleteVisibleDiscount(FilterDefinition<BsonDocument> filter)
        {
            DTO_VisibleDiscount deletedVisibleDiscount = DAO_VisibleDiscount.Delete(filter);
            foreach (var discount in deletedVisibleDiscount.Target)
            {
                DeleteIDiscountTarget(discount.Value, discount.Key);
            }
            Discount_VisibleDiscounts.TryRemove(deletedVisibleDiscount._id, out VisibleDiscount v);
        }


        public void Create(DiscountTargetCategories discountTargetCategories)
        {
            DAO_DiscountTargetCategories.Create(new DTO_DiscountTargetCategories(discountTargetCategories.getId(), discountTargetCategories.Categories));
            Discount_DiscountTargetCategories.TryAdd(discountTargetCategories.getId(), discountTargetCategories);
        }

        public DiscountTargetCategories LoadDiscountTargetCategories(FilterDefinition<BsonDocument> filter)
        {
            DiscountTargetCategories d;
            DTO_DiscountTargetCategories dto = DAO_DiscountTargetCategories.Load(filter);
            if (Discount_DiscountTargetCategories.TryGetValue(dto._id, out d))
            {
                return d;
            }

            d = new DiscountTargetCategories(dto.Categories, dto._id);
            Discount_DiscountTargetCategories.TryAdd(dto._id, d);
            return d;
        }

        public void UpdateDiscountTargetCategories(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountTargetCategories.Update(filter, update);
        }

        public void DeleteDiscountTargetCategories(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountTargetCategories deletedDiscountTargetCategories = DAO_DiscountTargetCategories.Delete(filter);
            Discount_DiscountTargetCategories.TryRemove(deletedDiscountTargetCategories._id, out DiscountTargetCategories d);
        }


        public void Create(DiscountTargetProducts discountTargetProducts)
        {
            DAO_DiscountTargetProducts.Create(new DTO_DiscountTargetProducts(discountTargetProducts.getId(), discountTargetProducts.ProductIds));
            Discount_DiscountTargetProducts.TryAdd(discountTargetProducts.getId(), discountTargetProducts);
        }

        public DiscountTargetProducts LoadDiscountTargetProducts(FilterDefinition<BsonDocument> filter)
        {
            DiscountTargetProducts d;
            DTO_DiscountTargetProducts dto = DAO_DiscountTargetProducts.Load(filter);
            if (Discount_DiscountTargetProducts.TryGetValue(dto._id, out d))
            {
                return d;
            }
            d = new DiscountTargetProducts(dto._id, dto.Products);
            Discount_DiscountTargetProducts.TryAdd(dto._id, d);
            return d;
        }

        public void UpdateDiscountTargetProducts(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountTargetProducts.Update(filter, update);
        }

        public void DeleteDiscountTargetProducts(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountTargetProducts deletedDiscountTargetProducts = DAO_DiscountTargetProducts.Delete(filter);
            Discount_DiscountTargetProducts.TryRemove(deletedDiscountTargetProducts._id, out DiscountTargetProducts d);
        }



        public void Create(DiscreetDiscount discreetDiscount)
        {
            DAO_DiscreetDiscount.Create(new DTO_DiscreetDiscount(discreetDiscount.Id, discreetDiscount.DiscountCode, getDiscountsIDs(discreetDiscount.Discount)));
            Discount_DiscreetDiscounts.TryAdd(discreetDiscount.Id, discreetDiscount);
        }

        public DiscreetDiscount LoadDiscreetDiscount(FilterDefinition<BsonDocument> filter)
        {
            DiscreetDiscount d;
            DTO_DiscreetDiscount dto = DAO_DiscreetDiscount.Load(filter);
            if (Discount_DiscreetDiscounts.TryGetValue(dto._id, out d))
            {
                return d;
            }

            IDiscountPolicy Discount = null;
            foreach (var discount in dto.Discount)
            {
                Discount = LoadIDiscountPolicy(discount.Value, discount.Key);
            }

            d = new DiscreetDiscount(Discount, dto.DiscountCode, dto._id);
            Discount_DiscreetDiscounts.TryAdd(d.Id, d);
            return d;
        }

        public void UpdateDiscreetDiscount(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscreetDiscount.Update(filter, update);
        }

        public void DeleteDiscreetDiscount(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscreetDiscount deletedDiscreetDiscount = DAO_DiscreetDiscount.Delete(filter);
            foreach (var discount in deletedDiscreetDiscount.Discount)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }
            Discount_DiscreetDiscounts.TryRemove(deletedDiscreetDiscount._id, out DiscreetDiscount d);
        }



        public void Create(ConditionalDiscount conditionalDiscount)
        {
            DAO_ConditionalDiscount.Create(new DTO_ConditionalDiscount(conditionalDiscount.Id, getDiscountsIDs(conditionalDiscount.Condition), getDiscountsIDs(conditionalDiscount.Discount)));
            Discount_ConditionalDiscounts.TryAdd(conditionalDiscount.Id, conditionalDiscount);
        }

        public ConditionalDiscount LoadConditionalDiscount(FilterDefinition<BsonDocument> filter)
        {
            ConditionalDiscount c;
            DTO_ConditionalDiscount dto = DAO_ConditionalDiscount.Load(filter);
            if (Discount_ConditionalDiscounts.TryGetValue(dto._id, out c))
            {
                return c;
            }

            IDiscountCondition Condition = null;
            foreach (var discount in dto.Condition)
            {
                Condition = LoadIDiscountCondition(discount.Value, discount.Key);
            }

            IDiscountPolicy Discount = null;
            foreach (var discount in dto.Discount)
            {
                Discount = LoadIDiscountPolicy(discount.Value, discount.Key);
            }

            c = new ConditionalDiscount(Discount, Condition, dto._id);
            Discount_ConditionalDiscounts.TryAdd(c.Id, c);
            return c;
        }

        public void UpdateConditionalDiscount(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_ConditionalDiscount.Update(filter, update);
        }

        public void DeleteConditionalDiscount(FilterDefinition<BsonDocument> filter)
        {

            DTO_ConditionalDiscount deletedConditionalDiscount = DAO_ConditionalDiscount.Delete(filter);
            foreach (var condition in deletedConditionalDiscount.Condition)
            {
                DeleteIDiscountCondition(condition.Value, condition.Key);
            }
            foreach (var discount in deletedConditionalDiscount.Discount)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }
            Discount_ConditionalDiscounts.TryRemove(deletedConditionalDiscount._id, out ConditionalDiscount c);

        }


        public void Create(MinProductCondition minProductCondition)
        {
            DAO_MinProductCondition.Create(new DTO_MinProductCondition(minProductCondition.Id, minProductCondition.MinQuantity, minProductCondition.ProductId));
            Discount_MinProductConditions.TryAdd(minProductCondition.Id, minProductCondition);
        }

        public MinProductCondition LoadMinProductCondition(FilterDefinition<BsonDocument> filter)
        {
            MinProductCondition m;
            DTO_MinProductCondition dto = DAO_MinProductCondition.Load(filter);
            if (Discount_MinProductConditions.TryGetValue(dto._id, out m))
            {
                return m;
            }
            m = new MinProductCondition(dto.Product, dto.MinQuantity, dto._id);
            Discount_MinProductConditions.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMinProductCondition(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MinProductCondition.Update(filter, update);
        }

        public void DeleteMinProductCondition(FilterDefinition<BsonDocument> filter)
        {
            DTO_MinProductCondition deletedMinProductCondition = DAO_MinProductCondition.Delete(filter);
            Discount_MinProductConditions.TryRemove(deletedMinProductCondition._id, out MinProductCondition m);
        }



        public void Create(MinBagPriceCondition minBagPriceCondition)
        {
            DAO_MinBagPriceCondition.Create(new DTO_MinBagPriceCondition(minBagPriceCondition.Id, minBagPriceCondition.MinPrice));
            Discount_MinBagPriceConditions.TryAdd(minBagPriceCondition.Id, minBagPriceCondition);
        }

        public MinBagPriceCondition LoadMinBagPriceCondition(FilterDefinition<BsonDocument> filter)
        {
            MinBagPriceCondition m;
            DTO_MinBagPriceCondition dto = DAO_MinBagPriceCondition.Load(filter);
            if (Discount_MinBagPriceConditions.TryGetValue(dto._id, out m))
            {
                return m;
            }

            m = new MinBagPriceCondition(dto.MinPrice, dto._id);
            Discount_MinBagPriceConditions.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMinBagPriceCondition(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MinBagPriceCondition.Update(filter, update);
        }

        public void DeleteMinBagPriceCondition(FilterDefinition<BsonDocument> filter)
        {
            DTO_MinBagPriceCondition deletedMinBagPriceCondition = DAO_MinBagPriceCondition.Delete(filter);
            Discount_MinBagPriceConditions.TryRemove(deletedMinBagPriceCondition._id, out MinBagPriceCondition m);
        }



        public void Create(MaxProductCondition maxProductCondition)
        {
            DAO_MaxProductCondition.Create(new DTO_MaxProductCondition(maxProductCondition.Id, maxProductCondition.MaxQuantity, maxProductCondition.ProductId));
            Discount_MaxProductConditions.TryAdd(maxProductCondition.Id, maxProductCondition);
        }

        public MaxProductCondition LoadMaxProductCondition(FilterDefinition<BsonDocument> filter)
        {
            MaxProductCondition m;
            DTO_MaxProductCondition dto = DAO_MaxProductCondition.Load(filter);
            if (Discount_MaxProductConditions.TryGetValue(dto._id, out m))
            {
                return m;
            }
            m = new MaxProductCondition(dto.Product, dto.MaxQuantity, dto._id);
            Discount_MaxProductConditions.TryAdd(m.Id, m);
            return m;
        }

        public void UpdateMaxProductCondition(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_MaxProductCondition.Update(filter, update);
        }

        public void DeleteMaxProductCondition(FilterDefinition<BsonDocument> filter)
        {
            DTO_MaxProductCondition deletedMaxProductCondition = DAO_MaxProductCondition.Delete(filter);
            Discount_MaxProductConditions.TryRemove(deletedMaxProductCondition._id, out MaxProductCondition m);
        }


        public void Create(DiscountConditionOr discountConditionOr)
        {
            DAO_DiscountConditionOr.Create(new DTO_DiscountConditionOr(discountConditionOr.Id, getDiscountsIDs(discountConditionOr.Conditions)));
            Discount_DiscountConditionOrs.TryAdd(discountConditionOr.Id, discountConditionOr);
        }
        public DiscountConditionOr LoadDiscountConditionOr(FilterDefinition<BsonDocument> filter)
        {
            DiscountConditionOr d;
            DTO_DiscountConditionOr dto = DAO_DiscountConditionOr.Load(filter);
            if (Discount_DiscountConditionOrs.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountCondition> Conditions = new List<IDiscountCondition>();
            foreach (var discount in dto.Conditions)
            {
                Conditions.Add(LoadIDiscountCondition(discount.Value, discount.Key));
            }

            d = new DiscountConditionOr(Conditions, dto._id);
            Discount_DiscountConditionOrs.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountConditionOr(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountConditionOr.Update(filter, update);
        }
        public void DeleteDiscountConditionOr(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountConditionOr deletedDiscountConditionOr = DAO_DiscountConditionOr.Delete(filter);
            foreach (var condition in deletedDiscountConditionOr.Conditions)
            {
                DeleteIDiscountCondition(condition.Value, condition.Key);
            }

            Discount_DiscountConditionOrs.TryRemove(deletedDiscountConditionOr._id, out DiscountConditionOr d);
        }



        public void Create(DiscountConditionAnd discountConditionAnd)
        {
            DAO_DiscountConditionAnd.Create(new DTO_DiscountConditionAnd(discountConditionAnd.Id, getDiscountsIDs(discountConditionAnd.Conditions)));
            Discount_DiscountConditionAnds.TryAdd(discountConditionAnd.Id, discountConditionAnd);
        }
        public DiscountConditionAnd LoadDiscountConditionAnd(FilterDefinition<BsonDocument> filter)
        {
            DiscountConditionAnd d;
            DTO_DiscountConditionAnd dto = DAO_DiscountConditionAnd.Load(filter);
            if (Discount_DiscountConditionAnds.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountCondition> Conditions = new List<IDiscountCondition>();
            foreach (var discount in dto.Conditions)
            {
                Conditions.Add(LoadIDiscountCondition(discount.Value, discount.Key));
            }

            d = new DiscountConditionAnd(Conditions, dto._id);
            Discount_DiscountConditionAnds.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountConditionAnd(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountConditionAnd.Update(filter, update);
        }
        public void DeleteDiscountConditionAnd(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountConditionAnd deletedDiscountConditionAnd = DAO_DiscountConditionAnd.Delete(filter);
            foreach (var condition in deletedDiscountConditionAnd.Conditions)
            {
                DeleteIDiscountCondition(condition.Value, condition.Key);
            }

            Discount_DiscountConditionAnds.TryRemove(deletedDiscountConditionAnd._id, out DiscountConditionAnd d);
        }



        public void Create(DiscountXor discountXor)
        {
            DAO_DiscountXor.Create(new DTO_DiscountXor(discountXor.Id, getDiscountsIDs(discountXor.Discount1), getDiscountsIDs(discountXor.Discount2), getDiscountsIDs(discountXor.ChoosingCondition)));
            Discount_DiscountXors.TryAdd(discountXor.Id, discountXor);
        }
        public DiscountXor LoadDiscountXor(FilterDefinition<BsonDocument> filter)
        {
            DiscountXor d;
            DTO_DiscountXor dto = DAO_DiscountXor.Load(filter);
            if (Discount_DiscountXors.TryGetValue(dto._id, out d))
            {
                return d;
            }
            IDiscountPolicy Discount1 = null;
            foreach (var discount in dto.Discount1)
            {
                Discount1 = LoadIDiscountPolicy(discount.Value, discount.Key);
            }
            IDiscountPolicy Discount2 = null;
            foreach (var discount in dto.Discount2)
            {
                Discount2 = LoadIDiscountPolicy(discount.Value, discount.Key);
            }
            IDiscountCondition ChoosingCondition = null;
            foreach (var discount in dto.ChoosingCondition)
            {
                ChoosingCondition = LoadIDiscountCondition(discount.Value, discount.Key);
            }
            d = new DiscountXor(Discount1, Discount2, ChoosingCondition);
            Discount_DiscountXors.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountXor(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountXor.Update(filter, update);
        }
        public void DeleteDiscountXor(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountXor deletedDiscountXor = DAO_DiscountXor.Delete(filter);
            foreach (var discount in deletedDiscountXor.Discount1)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }
            foreach (var discount in deletedDiscountXor.Discount2)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }
            foreach (var condition in deletedDiscountXor.ChoosingCondition)
            {
                DeleteIDiscountCondition(condition.Value, condition.Key);
            }

            Discount_DiscountXors.TryRemove(deletedDiscountXor._id, out DiscountXor d);
        }



        public void Create(DiscountOr discountOr)
        {
            DAO_DiscountOr.Create(new DTO_DiscountOr(discountOr.Id, getDiscountsIDs(discountOr.Discounts)));
            Discount_DiscountOrs.TryAdd(discountOr.Id, discountOr);
        }
        public DiscountOr LoadDiscountOr(FilterDefinition<BsonDocument> filter)
        {
            DiscountOr d;
            DTO_DiscountOr dto = DAO_DiscountOr.Load(filter);
            if (Discount_DiscountOrs.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountPolicy> Discounts = new List<IDiscountPolicy>();
            foreach (var discount in dto.Discounts)
            {
                Discounts.Add(LoadIDiscountPolicy(discount.Value, discount.Key));
            }

            d = new DiscountOr(Discounts, dto._id);
            Discount_DiscountOrs.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountOr(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountOr.Update(filter, update);
        }
        public void DeleteDiscountOr(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountOr deletedDiscountOr = DAO_DiscountOr.Delete(filter);
            foreach (var discount in deletedDiscountOr.Discounts)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }

            Discount_DiscountOrs.TryRemove(deletedDiscountOr._id, out DiscountOr d);
        }



        public void Create(DiscountMin discountMin)
        {
            DAO_DiscountMin.Create(new DTO_DiscountMin(discountMin.Id, getDiscountsIDs(discountMin.Discounts)));
            Discount_DiscountMins.TryAdd(discountMin.Id, discountMin);
        }
        public DiscountMin LoadDiscountMin(FilterDefinition<BsonDocument> filter)
        {
            DiscountMin d;
            DTO_DiscountMin dto = DAO_DiscountMin.Load(filter);
            if (Discount_DiscountMins.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountPolicy> Discounts = new List<IDiscountPolicy>();
            foreach (var discount in dto.Discounts)
            {
                Discounts.Add(LoadIDiscountPolicy(discount.Value, discount.Key));
            }

            d = new DiscountMin(Discounts, dto._id);
            Discount_DiscountMins.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountMin(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountMin.Update(filter, update);
        }
        public void DeleteDiscountMin(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountMin deletedDiscountMin = DAO_DiscountMin.Delete(filter);
            foreach (var discount in deletedDiscountMin.Discounts)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }

            Discount_DiscountMins.TryRemove(deletedDiscountMin._id, out DiscountMin d);
        }


        public void Create(DiscountMax discountMax)
        {
            DAO_DiscountMax.Create(new DTO_DiscountMax(discountMax.Id, getDiscountsIDs(discountMax.Discounts)));
            Discount_DiscountMaxs.TryAdd(discountMax.Id, discountMax);
        }
        public DiscountMax LoadDiscountMax(FilterDefinition<BsonDocument> filter)
        {
            DiscountMax d;
            DTO_DiscountMax dto = DAO_DiscountMax.Load(filter);
            if (Discount_DiscountMaxs.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountPolicy> Discounts = new List<IDiscountPolicy>();
            foreach (var discount in dto.Discounts)
            {
                Discounts.Add(LoadIDiscountPolicy(discount.Value, discount.Key));
            }

            d = new DiscountMax(Discounts, dto._id);
            Discount_DiscountMaxs.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountMax(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountMax.Update(filter, update);
        }
        public void DeleteDiscountMax(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountMax deletedDiscountMax = DAO_DiscountMax.Delete(filter);
            foreach (var discount in deletedDiscountMax.Discounts)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }

            Discount_DiscountMaxs.TryRemove(deletedDiscountMax._id, out DiscountMax d);
        }


        public void Create(DiscountAnd discountAnd)
        {
            DAO_DiscountAnd.Create(new DTO_DiscountAnd(discountAnd.Id, getDiscountsIDs(discountAnd.Discounts)));
            Discount_DiscountAnds.TryAdd(discountAnd.Id, discountAnd);
        }
        public DiscountAnd LoadDiscountAnd(FilterDefinition<BsonDocument> filter)
        {
            DiscountAnd d;
            DTO_DiscountAnd dto = DAO_DiscountAnd.Load(filter);
            if (Discount_DiscountAnds.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountPolicy> Discounts = new List<IDiscountPolicy>();
            foreach (var discount in dto.Discounts)
            {
                Discounts.Add(LoadIDiscountPolicy(discount.Value, discount.Key));
            }

            d = new DiscountAnd(Discounts, dto._id);
            Discount_DiscountAnds.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountAnd(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountAnd.Update(filter, update);
        }
        public void DeleteDiscountAnd(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountAnd deletedDiscountAnd = DAO_DiscountAnd.Delete(filter);
            foreach (var discount in deletedDiscountAnd.Discounts)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }

            Discount_DiscountAnds.TryRemove(deletedDiscountAnd._id, out DiscountAnd d);
        }



        public void Create(DiscountAddition discountAddition)
        {
            DAO_DiscountAddition.Create(new DTO_DiscountAddition(discountAddition.Id, getDiscountsIDs(discountAddition.Discounts)));
            Discount_DiscountAdditions.TryAdd(discountAddition.Id, discountAddition);
        }
        public DiscountAddition LoadDiscountAddition(FilterDefinition<BsonDocument> filter)
        {
            DiscountAddition d;
            DTO_DiscountAddition dto = DAO_DiscountAddition.Load(filter);
            if (Discount_DiscountAdditions.TryGetValue(dto._id, out d))
            {
                return d;
            }
            List<IDiscountPolicy> Discounts = new List<IDiscountPolicy>();
            foreach (var discount in dto.Discounts)
            {
                Discounts.Add(LoadIDiscountPolicy(discount.Value, discount.Key));
            }

            d = new DiscountAddition(Discounts, dto._id);
            Discount_DiscountAdditions.TryAdd(d.Id, d);
            return d;
        }
        public void UpdateDiscountAddition(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            DAO_DiscountAddition.Update(filter, update);
        }
        public void DeleteDiscountAddition(FilterDefinition<BsonDocument> filter)
        {
            DTO_DiscountAddition deletedDiscountAddition = DAO_DiscountAddition.Delete(filter);
            foreach (var discount in deletedDiscountAddition.Discounts)
            {
                DeleteIDiscountPolicy(discount.Value, discount.Key);
            }

            Discount_DiscountAdditions.TryRemove(deletedDiscountAddition._id, out DiscountAddition d);
        }

        public void Create(IPurchasePolicy purchasePolicy)
        {
            string[] type = purchasePolicy.GetType().ToString().Split('.');
            string policy_type = type[type.Length - 1];

            AddPolicyToDB(policy_type, purchasePolicy);
        }
        public void Create(IDiscountTarget discount)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            AddDiscountToDB(discount_type, discount);
        }
        public void Create(IDiscountPolicy discount)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            AddDiscountToDB(discount_type, discount);
        }
        public void Create(IDiscountCondition discount)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            AddDiscountToDB(discount_type, discount);
        }
        public void UpdatePolicy(IPurchasePolicy purchasePolicy, UpdateDefinition<BsonDocument> update)
        {
            string[] type = purchasePolicy.GetType().ToString().Split('.');
            string policy_type = type[type.Length - 1];

            var filter = Builders<BsonDocument>.Filter.Eq("_id", purchasePolicy.Id);
            switch (policy_type)
            {
                case "AndPolicy":
                    UpdateAndPolicy(filter, update);
                    break;

                case "Auction":
                    UpdateAuctionPolicy(filter, update);
                    break;

                case "BuyNow":
                    UpdateBuyNowPolicy(filter, update);
                    break;

                case "ConditionalPolicy":
                    UpdateConditionalPolicy(filter, update);
                    break;

                case "Lottery":
                    UpdateLotteryPolicy(filter, update);
                    break;

                case "MaxProductPolicy":
                    UpdateMaxProductPolicy(filter, update);
                    break;

                case "MinAgePolicy":
                    UpdateMinAgePolicy(filter, update);
                    break;

                case "MinProductPolicy":
                    UpdateMinProductPolicy(filter, update);
                    break;

                case "Offer":
                    UpdateOfferPolicy(filter, update);
                    break;

                case "OrPolicy":
                    UpdateOrPolicy(filter, update);
                    break;

                case "RestrictedHoursPolicy":
                    UpdateRestrictedHoursPolicy(filter, update);
                    break;
            }
        }

        public void UpdatePolicy(IDiscountTarget discount, UpdateDefinition<BsonDocument> update)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount.getId());
            switch (discount_type)
            {
                case "DiscountTargetCategories":
                    UpdateDiscountTargetCategories(filter, update);
                    break;

                case "DiscountTargetProducts":
                    UpdateDiscountTargetProducts(filter, update);
                    break;
            }
        }
        public void UpdatePolicy(IDiscountPolicy discount, UpdateDefinition<BsonDocument> update)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount.Id);
            switch (discount_type)
            {
                case "ConditionalDiscount":
                    UpdateConditionalDiscount(filter, update);
                    break;

                case "DiscountAddition":
                    UpdateDiscountAddition(filter, update);
                    break;

                case "DiscountAnd":
                    UpdateDiscountAnd(filter, update);
                    break;

                case "DiscountMax":
                    UpdateDiscountMax(filter, update);
                    break;

                case "DiscountOr":
                    UpdateDiscountOr(filter, update);
                    break;

                case "DiscountMin":
                    UpdateDiscountMin(filter, update);
                    break;

                case "DiscountXor":
                    UpdateDiscountXor(filter, update);
                    break;

                case "DiscreetDiscount":
                    UpdateDiscreetDiscount(filter, update); ;
                    break;

                case "VisibleDiscount":
                    UpdateVisibleDiscount(filter, update);
                    break;

            }
        }
        public void UpdatePolicy(IDiscountCondition discount, UpdateDefinition<BsonDocument> update)
        {
            string[] type = discount.GetType().ToString().Split('.');
            string discount_type = type[type.Length - 1];

            var filter = Builders<BsonDocument>.Filter.Eq("_id", discount.Id);
            switch (discount_type)
            {
                case "DiscountConditionAnd":
                    UpdateDiscountConditionAnd(filter, update);
                    break;

                case "DiscountConditionOr":
                    UpdateDiscountConditionOr(filter, update);
                    break;

                case "MaxProductCondition":
                    UpdateMaxProductCondition(filter, update);
                    break;

                case "MinBagPriceCondition":
                    UpdateMinBagPriceCondition(filter, update);
                    break;

                case "MinProductCondition":
                    UpdateMinProductCondition(filter, update);
                    break;

            }
        }

        public List<DTO_Offer> Get_DTO_Offers(List<Offer> offers)
        {
            List<DTO_Offer> dto_offers = new List<DTO_Offer>();
            foreach (Offer offer in offers)
            {
                dto_offers.Add(new DTO_Offer(offer.Id, offer.UserID, offer.ProductID, offer.StoreID, offer.Amount, offer.Price, offer.CounterOffer, offer.acceptedOwners));
            }

            return dto_offers;
        }

        public List<DTO_OwnerRequest> Get_DTO_OwnerRequests(List<OwnerRequest> offers)
        {
            List<DTO_OwnerRequest> dto_offers = new List<DTO_OwnerRequest>();
            foreach (OwnerRequest offer in offers)
            {
                dto_offers.Add(new DTO_OwnerRequest(offer.Id, offer.UserID, offer.StoreID,offer.AppointedBy, offer.acceptedOwners));
            }
            return dto_offers;
        }

        public void Load_RegisteredUserOffers(RegisteredUser user)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            DTO_RegisteredUser dto = DAO_RegisteredUser.Load(filter);
            user.PendingOffers = ToObject(dto.PendingOffers, user);
            user.AcceptedOffers = ToObject(dto.AcceptedOffers, user);
        }

        public void RevertTransaction_Purchase(String userID)
        {
            RegisteredUser user;
            RegisteredUsers.TryGetValue(userID, out user);
            if (user == null)
            {
                return;
            }

            // Revert User - History , ShoppingCart , Notifications
            Load_RegisteredUserHistory(user);
            Load_RegisteredUserShoppingCart(user);
            Load_RegisteredUserOffers(user);
            Load_RegisteredUserNotifications(user);

            ShoppingCart shoppingCart = user.ShoppingCart;
            foreach (var bag in shoppingCart.ShoppingBags)
            {
                Load_StoreHistory(bag.Value.Store);      

                foreach (var product in bag.Value.Products) 
                {
                    Product p = product.Key;
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", p.Id);
                    DTO_Product dto = DAO_Product.Load(filter);
                    p.Quantity = dto.Quantity;
                }
            }
        }

        // system info
        public SystemInfo LoadSystemInfoRecord(string date)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("Date", date);

            List<BsonDocument> docs = DAO_SysInfo.collection.Find(filter).ToList();

            SystemInfo dto = null;
            foreach (BsonDocument doc in docs)
            {
                var json = doc.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                dto = JsonConvert.DeserializeObject<SystemInfo>(json);
            }

            return dto;
        }

        public void Update(SystemInfo monitor)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Date", monitor.Date);
            var update = Builders<BsonDocument>.Update.Set("GuestUsers", monitor.GuestUsers)
                                                    .Set("RegisteredUsers", monitor.RegisteredUsers)
                                                    .Set("ManagersNotOwners", monitor.ManagersNotOwners)
                                                    .Set("Owners", monitor.Owners)
                                                    .Set("Admins", monitor.Admins);
            DAO_SysInfo.Update(filter, update, true);
        }

        public SystemInfo LoadSystemInfo()
        {

            String date = DateTime.Now.Date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            var filter = Builders<BsonDocument>.Filter.Eq("Date", date);

            SystemInfo dto = DAO_SysInfo.Load(filter);
            if (dto is null)
            {
                dto = new SystemInfo(date, 0, 0, 0, 0, 0);
            }
            return dto;

        }

        public void clearDB()
        {
            if (!(Instance is null))
            {
                var emptyFilter = Builders<BsonDocument>.Filter.Empty;
                db.GetCollection<BsonDocument>("Products").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("Stores").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("Users").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("StoreStaff").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("SystemAdmins").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("DiscountPolicies").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("PurchasePolicies").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("OwnerRequests").DeleteMany(emptyFilter);
                db.GetCollection<BsonDocument>("SystemVisits").DeleteMany(emptyFilter);


                RegisteredUsers.Clear();
                GuestUsers.Clear();
                Products.Clear();
                StoreManagers.Clear();
                StoreOwners.Clear();
                Stores.Clear();
                Policy_Auctions.Clear();
                Policy_Lotterys.Clear();
                Policy_MaxProductPolicys.Clear();
                Policy_MinAgePolicys.Clear();
                Policy_MinProductPolicys.Clear();
                Policy_Offers.Clear();
                Policy_RestrictedHoursPolicys.Clear();
                Policy_AndPolicys.Clear();
                Policy_OrPolicys.Clear();
                Policy_BuyNows.Clear();
                Policy_ConditionalPolicys.Clear();
                Discount_VisibleDiscounts.Clear();
                Discount_DiscountTargetCategories.Clear();
                Discount_DiscountTargetProducts.Clear();
                Discount_DiscreetDiscounts.Clear();
                Discount_ConditionalDiscounts.Clear();
                Discount_MinProductConditions.Clear();
                Discount_MinBagPriceConditions.Clear();
                Discount_MaxProductConditions.Clear();
                Discount_DiscountConditionOrs.Clear();
                Discount_DiscountConditionAnds.Clear();
                Discount_DiscountXors.Clear();
                Discount_DiscountOrs.Clear();
                Discount_DiscountMins.Clear();
                Discount_DiscountMaxs.Clear();
                Discount_DiscountAnds.Clear();
                Discount_DiscountAdditions.Clear();
                OwnerRequests.Clear();

            }

        }


    }
}
