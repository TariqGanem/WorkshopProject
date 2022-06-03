using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User
{
    public class DTO_History
    {
        [BsonElement]
        public LinkedList<DTO_ShoppingBag> ShoppingBags { get; set; }

        public DTO_History(LinkedList<DTO_ShoppingBag> shoppingBags)
        {
            ShoppingBags = shoppingBags;
        }
    }
}
