﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer.DataTransferObjects
{
    public class DTO_VisibleDiscount
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement]
        public String ExpirationDate { get; set; }
        [BsonElement]
        public ConcurrentDictionary<String, String> Target { get; }     // <type , id>
        [BsonElement]
        public Double Percentage { get; set; }

        public DTO_VisibleDiscount(string id, string expirationDate, ConcurrentDictionary<string, string> target, double percentage)
        {
            _id = id;
            ExpirationDate = expirationDate;
            Target = target;
            Percentage = percentage;
        }
    }
}
