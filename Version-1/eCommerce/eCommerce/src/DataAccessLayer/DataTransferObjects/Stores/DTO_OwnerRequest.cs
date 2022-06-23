using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.Stores
{
    public class DTO_OwnerRequest
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public String UserID { get; set; }
        [BsonElement]
        public String StoreID { get; set; }
        [BsonElement]
        public String AppointedBy { get; set; }
        [BsonElement]
        public List<String> acceptedOwners { get; }

        public DTO_OwnerRequest(string id, string userID, string storeID, string appointedBy, List<string> acceptedOwners)
        {
            _id = id;
            UserID = userID;
            StoreID = storeID;
            this.acceptedOwners = acceptedOwners;
            this.AppointedBy = appointedBy;
        }
    }
}
