using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects
{
    public class DTO_MinBagPriceCondition
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public Double MinPrice { get; set; }

        public DTO_MinBagPriceCondition(string id, double minPrice)
        {
            _id = id;
            MinPrice = minPrice;
        }
    }
}
