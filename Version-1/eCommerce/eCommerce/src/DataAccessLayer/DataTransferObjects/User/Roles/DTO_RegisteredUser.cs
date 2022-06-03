using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles
{
    public class DTO_RegisteredUser
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public Boolean Active { get; set; }
        [BsonElement]
        public DTO_ShoppingCart ShoppingCart { get; set; }
        [BsonElement]
        public string UserName { get; set; }
        [BsonElement]
        public string _password { get; set; }
        [BsonElement]
        public DTO_History History { get; set; }
        [BsonElement]
        public LinkedList<DTO_Notification> PendingNotification { get; set; }

        public DTO_RegisteredUser(String id, DTO_ShoppingCart shoppingCart, String username, String password, Boolean loggedIn, DTO_History history, LinkedList<DTO_Notification> pendingNotification)
        {
            _id = id;
            ShoppingCart = shoppingCart;
            UserName = username;
            _password = password;
            Active = loggedIn;
            History = history;
            PendingNotification = pendingNotification;
        }
    }
}
