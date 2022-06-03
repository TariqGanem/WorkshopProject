using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User
{
    public class DTO_PurchasedShoppingBag
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public String UserId { get; set; }
        [BsonElement]
        public String StoreId { get; set; }
        [BsonElement]
        public LinkedList<DTO_PurchasedProduct> Products { get; set; }
        [BsonElement]
        public Double TotalBagPrice { get; set; }

        public DTO_PurchasedShoppingBag(String id, String userId, String storeId, LinkedList<DTO_PurchasedProduct> products, Double totalBagPrice)
        {
            _id = id;
            UserId = userId;
            StoreId = storeId;
            Products = products;
            TotalBagPrice = totalBagPrice;
        }
    }
}
