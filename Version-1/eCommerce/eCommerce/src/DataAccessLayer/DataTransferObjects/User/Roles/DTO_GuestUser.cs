using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles
{
    public class DTO_GuestUser
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public Boolean Active { get; set; }
        [BsonElement]
        public DTO_ShoppingCart ShoppingCart { get; set; }
        public DTO_GuestUser(string id, DTO_ShoppingCart shoppingCart, bool active)
        {
            _id = id;
            ShoppingCart = shoppingCart;
            Active = active;
        }
    }
}
