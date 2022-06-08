using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects
{
    public class DTO_DiscountTargetProducts
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public List<String> Products { get; set; }      // List<ProductID>

        public DTO_DiscountTargetProducts(string id, List<string> products)
        {
            _id = id;
            Products = products;
        }
    }
}
