using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User
{
    public class DTO_ShoppingCart
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public ConcurrentDictionary<String, DTO_ShoppingBag> ShoppingBags { get; set; } 
        [BsonElement]
        public Double TotalCartPrice { get; set; }

        public DTO_ShoppingCart(string id, ConcurrentDictionary<string, DTO_ShoppingBag> shoppingBags, double totalCartPrice)
        {
            _id = id;
            ShoppingBags = shoppingBags;
            TotalCartPrice = totalCartPrice;
        }
    }
}
