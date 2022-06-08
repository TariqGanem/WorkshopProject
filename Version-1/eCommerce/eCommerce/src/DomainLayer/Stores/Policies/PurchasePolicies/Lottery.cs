using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class Lottery : IPurchasePolicyType
    {
        public Double Price { get; }
        public ConcurrentDictionary<String, Double> Participants { get; set; }  // <UserID, winning %>

        public string Id { get; set; }

        public Lottery(double price)
        {
            Id = Service.GenerateId();
            Price = price;
            Participants = new ConcurrentDictionary<string, double>();
        }

        public Lottery(string id, double price, ConcurrentDictionary<string, double> participants)
        {
            Id = id;
            Price = price;
            Participants = participants;
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

        public DTO_Lottery getDTO()
        {
            return new DTO_Lottery(this.Id, this.Price, this.Participants);
        }
    }
}
