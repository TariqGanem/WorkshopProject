using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.DomainLayer.Store
{
    public class Product
    {
        public String Id { get; }
        public String Name { get; set; }
        public Double Price { get; set; }
        public Double Rate { get; set; }
        public int NumberOfRates { get; set; }
        public int Quantity { get; set; }
        public String Category { get; set; }
        public LinkedList<String> KeyWords { get; set; }

        public Product(string name, double price, string category, int quantity, LinkedList<String> kws = null)
        {
            Id = Service.GenerateId();
            Name = name;
            Price = price;
            Category = category;
            Quantity = quantity;
            KeyWords = kws == null ? new LinkedList<string>() : kws;
        }

        public void AddKeyWord(String kw)
        {
            this.KeyWords.AddLast(kw);
        }

        public void AddRating(Double rate)
        {
            if (rate < 1 || rate > 5)
            {
                throw new Exception($"Product { Name } could not be rated. Please use number between 1 to 5");
            }
            else
            {
                NumberOfRates += 1;
                Rate = (Rate + rate) / (Double)NumberOfRates;
            }
        }
    }
}
