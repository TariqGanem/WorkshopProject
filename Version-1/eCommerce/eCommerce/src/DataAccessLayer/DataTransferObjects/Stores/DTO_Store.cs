using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects.Stores
{
    public class DTO_Store
    {
        [BsonId]
        public String _id { get; set; }
        [BsonElement]
        public String Name { get; set; }
        [BsonElement]
        public String Founder { get; set; }
        [BsonElement]
        public LinkedList<String> Owners { get; set; }
        [BsonElement]
        public LinkedList<String> Managers { get; set; }
        [BsonElement]
        public LinkedList<String> InventoryManager { get; set; }     
        [BsonElement]
        public DTO_History History { get; set; }
        [BsonElement]
        public Double Rate { get; set; }
        [BsonElement]
        public int NumberOfRates { get; set; }
        [BsonElement]
        public Boolean Active { get; set; }

        public DTO_Store(String id, String name, String founder, LinkedList<String> owners, LinkedList<String> managers, LinkedList<String> inventoryManager, DTO_History history,
                            Double rating, int numberOfRates, Boolean isActive)
        {
            _id = id;
            Name = name;
            Founder = founder;
            Owners = owners;
            Managers = managers;
            InventoryManager = inventoryManager;
            History = history;
            Rate = rating;
            NumberOfRates = numberOfRates;
            Active = isActive;
        }
    }
}
