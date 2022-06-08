using eCommerce.src.DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.Offer
{
    public class Offer
    {
        public string Id { get; set; }
        public string UserID { get; }
        public string ProductID { get; }
        public string StoreID { get; }
        public int Amount { get; }
        public double Price { get; }
        public double CounterOffer { get; set; }
        public List<string> acceptedOwners { get; }

        public Offer(string userID, string productID, int amount, double price, string storeID, string id = "", double counterOffer = -1, List<string> acceptedOwners = null)
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.UserID = userID;
            this.ProductID = productID;
            this.StoreID = storeID;
            this.Amount = amount;
            this.Price = price;
            this.CounterOffer = counterOffer;
            this.acceptedOwners = acceptedOwners;
            if (acceptedOwners == null)
                this.acceptedOwners = new List<string>();
        }

        public Offer(string id, string userID, string productID, string storeID, int amount, double price, double counterOffer, List<string> acceptedOwners)
        {
            Id = id;
            UserID = userID;
            ProductID = productID;
            StoreID = storeID;
            Amount = amount;
            Price = price;
            CounterOffer = counterOffer;
            this.acceptedOwners = acceptedOwners;
        }

        public DTO_Offer getDTO()
        {
            return new DTO_Offer(this.Id, this.UserID, this.ProductID, this.StoreID, this.Amount, this.Price, this.CounterOffer, this.acceptedOwners);
        }



        private bool didAllOwnersAccept(List<string> allOwners)
        {
            foreach (string id in allOwners)
                if (!acceptedOwners.Contains(id))
                    return false;
            return true;
        }

        public OfferResponse AcceptedResponse(string ownerID, List<string> allOwners)
        {
            if (acceptedOwners.Contains(ownerID))
                throw new Exception("Failed to response to an offer: Owner Can't accept an offer more than once");
            acceptedOwners.Add(ownerID);
            if (didAllOwnersAccept(allOwners))
                return OfferResponse.Accepted;
            else
                return OfferResponse.None;
        }

        public Dictionary<string, object> GetData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>() {
                { "Id", Id },
                { "Product", ProductID },
                { "User", UserID },
                { "Store", StoreID },
                { "Amount", Amount },
                { "Price", Price },
                { "CounterOfferPrice", CounterOffer},
            };
            return data;
        }
    }
}
