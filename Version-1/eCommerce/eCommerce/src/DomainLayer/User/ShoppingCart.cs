using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer.Objects;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class ShoppingCart
    {
        public string Id { get; private set; }
        public ConcurrentDictionary<String, ShoppingBag> ShoppingBags { get; set; }  // <StoreId, ShoppingBag>
        public Double TotalCartPrice { get; set; }

        public ShoppingCart()
        {
            Id = Service.GenerateId();
            ShoppingBags = new ConcurrentDictionary<string, ShoppingBag>();
            TotalCartPrice = 0;
        }

        public ShoppingCart(ShoppingCart shoppingCart)
        {
            Id = shoppingCart.Id;
            ShoppingBags = shoppingCart.ShoppingBags;
            TotalCartPrice = shoppingCart.TotalCartPrice;
        }

        public ShoppingCart(String id , ConcurrentDictionary<String, ShoppingBag> sb , double tcp)
        {
            Id = id;
            ShoppingBags = sb;
            TotalCartPrice = tcp;
        }

        public ShoppingBag GetShoppingBag(string storeId)
        {
            if (ShoppingBags.TryGetValue(storeId, out ShoppingBag shoppingBag))
                return shoppingBag;
            throw new Exception($"Shopping bag not found for the store id: {storeId}.");
        }

        public Boolean AddShoppingBagToCart(ShoppingBag shoppingBag)
        {
            return ShoppingBags.TryAdd(shoppingBag.Store.Id, shoppingBag);
        }

        public Double GetTotalShoppingCartPrice(List<Offer> offers, MongoDB.Driver.IClientSessionHandle session = null)
        {
            Double sum = 0;
            String userId = "";
            foreach (ShoppingBag bag in ShoppingBags.Values)
            {
                userId = bag.Id;
                sum += bag.GetTotalPrice(offers);
            }
            this.TotalCartPrice = sum;
            if (userId != "")
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);
                var update_shoppingcart = Builders<BsonDocument>.Update.Set("ShoppingCart", getDTO());
                DBUtil.getInstance().UpdateRegisteredUser(filter, update_shoppingcart, session: session);
            }

            return sum;
        }

        public ShoppingCartSO getSO()
        {
            return new ShoppingCartSO(this);
        }
        public DTO_ShoppingCart getDTO()
        {
            ConcurrentDictionary<string, DTO_ShoppingBag> bags = new ConcurrentDictionary<string, DTO_ShoppingBag>();
            foreach (var sb in this.ShoppingBags)
            {
                bags.TryAdd(sb.Key, sb.Value.getDTO());
            }
            return new DTO_ShoppingCart(this.Id, bags, this.TotalCartPrice);
        }

        internal ShoppingCart Purchase(IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails, List<Offer> acceptedOffers, User user, MongoDB.Driver.IClientSessionHandle session = null)
        {
            if (!checkInventory(this.ShoppingBags))
                throw new Exception("A bag in the Shopping cart contains more of a product than the store can supply");

            if (!AdheresToPolicy(user))
                throw new Exception("A bag in the Shopping cart doesn't adhere to it's respective store's policy");

            Double amount = GetTotalShoppingCartPrice(acceptedOffers, session);

            int paymentId = Proxy.Pay(amount, paymentDetails);

            if (paymentId == -1)
            {
                throw new Exception("Attempt to purchase the shopping cart failed due to error in payment details\n");

            }

            int deliverySuccess = Proxy.Deliver(deliveryDetails);
            if (deliverySuccess == -1)
            {
                IDictionary<String, Object> refundDetails = new Dictionary<String, Object>();
                refundDetails.Add("transaction_id", paymentId.ToString());
                int refundSuccess = Proxy.CancelTransaction(refundDetails);
                if (refundSuccess == -1)
                    throw new Exception("Attempt to purchase the shopping cart failed due to error in delivery details and refund failed\n");
                throw new Exception("Attempt to purchase the shopping cart failed due to error in delivery details\n");
            }
            ShoppingCart copy = new ShoppingCart(this);

            return copy;
        }

        private bool checkInventory(ConcurrentDictionary<string, ShoppingBag> shoppingBags)
        {
            foreach (ShoppingBag bag in shoppingBags.Values)
            {
                foreach (KeyValuePair<Product, int> p in bag.Products)
                {
                    if (p.Key.Quantity < p.Value)
                        return false;
                }
            }
            return true;
        }

        public bool AdheresToPolicy(User user)
        {
            foreach (ShoppingBag bag in ShoppingBags.Values)
            {
                if (bag.AdheresToPolicy(user) == false)
                    return false;
            }
            return true;

           
        }
    }
}
