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
        public UserHistorySO History { get; set; }
        public Double Rate { get; set; }
        public int NumberOfRates { get; set; }

        public StoreService(string id, string name, String founder, LinkedList<String> owners, LinkedList<String> managers, UserHistorySO history, double rating, int numberOfRates)
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

        public string[] ToStringArray()
        {
            string[] output = new string[5];
            output[0] = Id;
            output[1] = Name;
            output[2] = Founder;
            output[3] = Rate.ToString();
            output[4] = NumberOfRates.ToString();
            return output;
        }
    }
}
