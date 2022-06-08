using eCommerce.src.DataAccessLayer.DataTransferObjects;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class Auction : IPurchasePolicyType
    {
        public DateTime ClosingTime { get; }
        public Double StartingPrice { get; }
        public Tuple<Double, String> LastOffer { get; }   

        public string Id { get; set; }

        public Auction(DateTime closingTime, Double startingPrice)
        {
            Id = Service.GenerateId();
            ClosingTime = closingTime;
            StartingPrice = startingPrice;
            LastOffer = new Tuple<Double, String>(-1, null);
        }

        public Auction(string id, String closingTime, double startingPrice, Tuple<double, string> lastOffer)
        {
            Id = id;
            ClosingTime = DateTime.Parse(closingTime);
            StartingPrice = startingPrice;
            LastOffer = lastOffer;
        }

        public double CalculatePrice(Product product, int quantity)
        {
            throw new System.NotImplementedException();
        }

        public bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            throw new NotImplementedException();
        }

        public bool AddPolicy(IPurchasePolicy policy, string id)
        {
            throw new NotImplementedException();
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetData()
        {
            throw new NotImplementedException();
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            throw new NotImplementedException();
        }

        public DTO_Auction getDTO()
        {
            return new DTO_Auction(this.Id, this.ClosingTime.ToString(), this.StartingPrice, this.LastOffer.Item1, this.LastOffer.Item2);
        }
    }
}
