using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects
{
    public class SystemInfo
    {
        [BsonElement]
        public String Date { get; set; }
        [BsonElement]
        public int GuestUsers { get; set; }
        [BsonElement]
        public int RegisteredUsers { get; set; }
        [BsonElement]
        public int ManagersNotOwners { get; set; }
        [BsonElement]
        public int Owners { get; set; }
        [BsonElement]
        public int Admins { get; set; }

        public SystemInfo(String date, int guestUsers, int registeredUsers, int managersNotOwners, int owners, int admins)
        {
            Date = date;
            GuestUsers = guestUsers;
            RegisteredUsers = registeredUsers;
            ManagersNotOwners = managersNotOwners;
            Owners = owners;
            Admins = admins;
        }
    }
}
