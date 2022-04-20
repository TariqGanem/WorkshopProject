using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class StoreService
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Founder { get; set; }
        public LinkedList<String> Owners { get; set; }
        public LinkedList<String> Managers { get; set; }
        public StoreHistoryService History { get; set; }
        public Double Rate { get; set; }
        public int NumberOfRates { get; set; }

        public StoreService(string id, string name, String founder, LinkedList<String> owners, LinkedList<String> managers, StoreHistoryService history, double rating, int numberOfRates)
        {
            Id = id;
            Name = name;
            Founder = founder;
            Owners = owners;
            Managers = managers;
            History = history;
            Rate = rating;
            NumberOfRates = numberOfRates;
        }
    }
}
