using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.Stores
{
    public class DTO_PurchasedProduct
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public String Name { get; set; }
        [BsonElement]
        public Double Price { get; set; }
        [BsonElement]
        public int ProductQuantity { get; set; }   
        [BsonElement]
        public String Category { get; set; }

        public DTO_PurchasedProduct(String id, String name, Double price, int productQuantity, String category)
        {
            _id = id;
            Name = name;
            Price = price;
            ProductQuantity = productQuantity;
            Category = category;
        }
    }
}
