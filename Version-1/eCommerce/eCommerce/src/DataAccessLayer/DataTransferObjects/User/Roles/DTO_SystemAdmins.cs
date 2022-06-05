using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles
{
    public class DTO_SystemAdmins
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public LinkedList<String> SystemAdmins { get; set; }
        public DTO_SystemAdmins(LinkedList<string> admins)
        {
            _id = "";
            SystemAdmins = admins;
        }
    }
}
