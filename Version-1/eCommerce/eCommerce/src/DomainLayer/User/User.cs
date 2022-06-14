using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects;
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
using System.Threading;

namespace eCommerce.src.DomainLayer.User
{
    public abstract class User
    {
        public String Id { get; set; }
        public Boolean Active { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public List<Offer> PendingOffers { get; set; }
        public List<Offer> AcceptedOffers { get; set; }

        protected User()
        {
            this.Id = Service.GenerateId();
            Active = false;
            ShoppingCart = new ShoppingCart();
            PendingOffers = new List<Offer>();      
            AcceptedOffers = new List<Offer>();    
        }

        public void AddProductToCart(Product product, int productQuantity, Store.Store store)
        {
            try
            {
                Monitor.Enter(product);
                try
                {
                    ShoppingBag sb;
                    Boolean res;
                    try
                    {
                        sb = ShoppingCart.GetShoppingBag(store.Id);
                    }
                    catch (Exception ex)
                    {
                        sb = new ShoppingBag(this.Id, store);
                        res = sb.AddProtuctToShoppingBag(product, productQuantity, AcceptedOffers);
                        ShoppingCart.AddShoppingBagToCart(sb);
                        ShoppingCart.GetTotalShoppingCartPrice(AcceptedOffers);
                        return;
                    }
                    res = sb.AddProtuctToShoppingBag(product, productQuantity, AcceptedOffers);
                    ShoppingCart.GetTotalShoppingCartPrice(AcceptedOffers);
                    return;
                }
                finally
                {
                    Monitor.Exit(product);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                Console.WriteLine("A SynchronizationLockException occurred. Message:");
                Console.WriteLine(SyncEx.Message);
                throw new Exception(SyncEx.Message);
            }
        }

        public ShoppingCart Purchase(IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails, User user , MongoDB.Driver.IClientSessionHandle session = null)
        {
            if (ShoppingCart.ShoppingBags.IsEmpty)
            {
                throw new Exception("The shopping cart is empty\n");
            }

            if (!isValidCartQuantity())
            {
                throw new Exception("Notice - The store is out of stock\n");   
            }

            ShoppingCart result = ShoppingCart.Purchase(paymentDetails, deliveryDetails, AcceptedOffers,this, session);
            if (result != null)
                ShoppingCart = new ShoppingCart();
            bool removeAccatedOffersResult = removeAcceptedOffers(session);
            return result;
        }

        private bool isValidCartQuantity()
        {
            ConcurrentDictionary<String, ShoppingBag> ShoppingBags = ShoppingCart.ShoppingBags;

            foreach (var bag in ShoppingBags)
            {
                ConcurrentDictionary<Product, int> Products = bag.Value.Products;

                foreach (var product in Products)
                {
                    if (product.Key.Quantity < product.Value)
                    {
                        return false;
                    }
                }
            }
            return true; 
        }

        public bool removeAcceptedOffers(MongoDB.Driver.IClientSessionHandle session)
        {
            AcceptedOffers = new List<Offer>();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("AcceptedOffers", Get_DTO_Offers(AcceptedOffers));
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_offer, session: session);

            return true;
        }

        public Offer SendOfferToStore(string storeID, string productID, int amount, double price)
        {
            Offer offer = new Offer(this.Id, productID, amount, price, storeID);
            PendingOffers.Add(offer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("PendingOffers", Get_DTO_Offers(PendingOffers));
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_offer);
            return offer;
        }

        public void UpdateShoppingCart(String storeID, Product product, int quantity)
        {
            ShoppingBag bag = ShoppingCart.GetShoppingBag(storeID);
            bag.UpdateShoppingBag(product, quantity);
        }

        public Offer findPendingOffer(string id)
        {
            foreach (Offer offer in PendingOffers)
                if (offer.Id == id)
                    return offer;
            return null;
        }

        public bool AnswerCounterOffer(string offerID, bool accepted)
        {
            Offer offer = findPendingOffer(offerID);
            if (offer == null)
                throw new Exception("Failed to respond to a counter offer: Failed to locate the offer");
            if (offer.CounterOffer == -1)
                throw new Exception("Failed to respond to a counter offer: The offer is not a counter offer");
            if (accepted)
                return MovePendingOfferToAccepted(offerID);
            bool result = RemovePendingOffer(offerID);
            return true;
        }

        public bool RemovePendingOffer(string id)
        {
            Offer offer = findPendingOffer(id);
            if (offer == null)
               throw new Exception("Failed to remove offer from user: Failed to locate the offer");
            PendingOffers.Remove(offer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("PendingOffers", Get_DTO_Offers(PendingOffers));
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_offer);
            return true;
        }

        public bool MovePendingOfferToAccepted(string id)
        {
            Offer offer = findPendingOffer(id);
            if (offer == null)
                throw new Exception("Failed to move an offer from pending to accepted: Failed to locate the offer");
            bool removeResult = RemovePendingOffer(id);
            AcceptedOffers.Add(offer);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", Id);
            var update_offer = Builders<BsonDocument>.Update.Set("AcceptedOffers", Get_DTO_Offers(AcceptedOffers));
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_offer);
            return true;
        }

        public abstract bool AcceptOffer(string offerID);

        public abstract bool DeclineOffer(string offerID);

        public abstract bool CounterOffer(string offerID);

        public List<Dictionary<string, object>> getUserPendingOffers()
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (Offer offer in PendingOffers)
                list.Add(offer.GetData());
            return list;
        }

        public abstract UserSO getSO();

        public List<DTO_Offer> Get_DTO_Offers(List<Offer> Offers)
        {
            List<DTO_Offer> dto_offers = new List<DTO_Offer>();
            foreach (Offer offer in Offers)
            {
                dto_offers.Add(new DTO_Offer(offer.Id, offer.UserID, offer.ProductID, offer.StoreID, offer.Amount, offer.Price, offer.CounterOffer, offer.acceptedOwners));
            }

            return dto_offers;
        }

        public List<Offer> getAcceptedOffers()
        {
            return AcceptedOffers;
        }
    }
}
