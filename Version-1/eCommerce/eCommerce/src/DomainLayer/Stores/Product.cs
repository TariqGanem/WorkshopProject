using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.ServiceLayer.Objects;
using MongoDB.Driver;
using MongoDB.Bson;
using eCommerce.src.DataAccessLayer;

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
        public NotificationPublisher NotificationPublisher { get; set; }


        public Product(string name, double price, string category, int quantity, LinkedList<String> kws = null)
        {
            Id = Service.GenerateId();
            Name = name;
            Price = price;
            Category = category;
            Quantity = quantity;
            KeyWords = kws == null ? new LinkedList<string>() : kws;
            NotificationPublisher = null;
        }

        public Product(string id, string name, double price, int productQuantity, string category , LinkedList<String> kws = null)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            Quantity = productQuantity;
            KeyWords = kws == null ? new LinkedList<string>() : kws;
            NotificationPublisher = null;
        }

        public void AddKeyWord(String kw)
        {
            this.KeyWords.AddLast(kw);
        }

        public void AddRating(Double rate)
        {
            if (rate < 0 || rate > 5)
            {
                throw new Exception($"Product { Name } could not be rated. Please use number between 1 to 5");
            }
            else
            {
                NumberOfRates += 1;
                Rate = (Rate + rate) / (Double)NumberOfRates;
                // db
                var filter = Builders<BsonDocument>.Filter.Eq("_id", this.Id);
                var update_offer = Builders<BsonDocument>.Update.Set("Rate", Rate).Set("NumberOfRates", NumberOfRates);
                DBUtil.getInstance().UpdateProduct(filter, update_offer);
            }
        }

        public bool UpdatePurchasedProductQuantity(int quantity)
        {
            if (this.NotificationPublisher == null)
            {
                throw new Exception("Error: No Notification Publisher set for this product\n");
            }
            Quantity = Quantity - quantity;
            return NotificationPublisher.notifyStorePurchase(this, quantity);
        }
        public ProductService getSO()
        {
            return new ProductService(this.Id, this.Name, this.Price, this.Quantity, this.Category);
        }
        public DTO_Product getDTO()
        {
            return new DTO_Product(Id, Name, Price, Quantity, Category, Rate, NumberOfRates,this.KeyWords);
        }
    }
}
