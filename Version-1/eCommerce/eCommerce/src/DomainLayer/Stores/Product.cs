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
        public int AmountOfRating { get; set; }
        public String Category { get; set; }
        public LinkedList<String> KeyWords { get; set; }

        public Product(string name, double price, string category , LinkedList<String> kws = null)
        {
            //Id = id;
            Name = name;
            Price = price;
            Category = category;
            AmountOfRating = 0;
            KeyWords = kws == null ? new LinkedList<string>() : kws;
        }

        public void AddKeyWord(String kw)
        {
            this.KeyWords.AddLast(kw);
        }
    }
}
