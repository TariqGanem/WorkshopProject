using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User
{
    public class DTO_Notification
    {
        [BsonElement]
        public String Message { get; set; }
        [BsonElement]
        public String Date { get; set; }
        [BsonElement]
        public Boolean isOpened { get; set; }
        [BsonElement]
        public Boolean isStoreStaff { get; set; }
        [BsonElement]
        public String ClientId { get; set; }
        public DTO_Notification(String message, String date, Boolean isOpened, Boolean isStoreStaff, String clientId)
        {
            Message = message;
            Date = date;
            this.isOpened = isOpened;
            this.isStoreStaff = isStoreStaff;
            ClientId = clientId;
        }
    }
}
