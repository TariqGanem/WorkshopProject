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
        public double Price { get; set; } 
        public int Quantity { get; set; }
        public Double Rating { get; set; }
        public int AmountOfRating { get; set; }
        public String Category { get; set; }
        public LinkedList<String> keywords { get; set; }
        public ConcurrentDictionary<String, String> Review { get; set; }

        public Product(string id, string name, double price, int quantity, string category , LinkedList<String> kws)
        {
            Review = new ConcurrentDictionary<String, String>();   
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity < 0 ? 0 : quantity;
            Category = category;
            Rating = 0;
            AmountOfRating = 0;
            keywords = kws == null ? new LinkedList<string>() : kws;
        }

        public Boolean PickProduct(int quantity)
        {
            if (quantity > this.Quantity)
                throw new Exception($"There is no enough quantity of {this.Name}");
            this.Quantity -= quantity;
            return true;
        }

        public void addProduct(int quantity)
        {
            this.Quantity += quantity;
        }

        public void addRating(double rating)
        {
            if (rating < 0 | rating > 5)
                throw new Exception($"Invalid Rating for product {this.Name} : {rating}");
            Rating = Rating * AmountOfRating + rating;
            AmountOfRating++;
            Rating /= AmountOfRating;
        }

        public void addReview(String userid , String review)
        {
            Review.AddOrUpdate(userid, review, (k, v) => review);
        }

        public ConcurrentDictionary<String,String> getReviews()
        {
            return Review;
        }

        public void KeyWord(String kw)
        {
            this.keywords.AddLast(kw);
        }
    }
}
