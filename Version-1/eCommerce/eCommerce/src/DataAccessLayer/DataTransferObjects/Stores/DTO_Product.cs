﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.Stores
{
    public class DTO_Product
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public String Name { get; set; }
        [BsonElement]
        public Double Price { get; set; }
        [BsonElement]
        public Double Rate { get; set; }
        [BsonElement]
        public int NumberOfRates { get; set; }
        [BsonElement]
        public int Quantity { get; set; }
        [BsonElement]
        public String Category { get; set; }
        [BsonElement]
        public LinkedList<String> KeyWords { get; set; }
        public DTO_Product(string id, string name, double price, int quantity, string category, double rating, int numberOfRates, LinkedList<string> keywords)
        {
            _id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
            Rate = rating;
            NumberOfRates = numberOfRates;
            KeyWords = keywords;
        }
    }
}
