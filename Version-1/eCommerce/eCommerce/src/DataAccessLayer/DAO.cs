using eCommerce.src.ServiceLayer.ResultService;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DataAccessLayer
{
    public class DAO<T>
    {
        public IMongoCollection<BsonDocument> Documents { get; set; }

        public DAO(IMongoDatabase db , string dbName)
        {
            Documents = db.GetCollection<BsonDocument>(dbName);
        }
        public void Create(T dto)
        {
            try
            {
                var doc = dto.ToBsonDocument();
                Documents.InsertOne(doc);
            }
            catch (MongoWriteException e)
            {
                Console.WriteLine(e.ToString());
                Logger.GetInstance().LogError(e.ToString());
            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
            }
        }

        public T Delete(FilterDefinition<BsonDocument> filter)
        {
            try
            {
                BsonDocument deletedDocument = Documents.FindOneAndDelete(filter);
                T dto = JsonConvert.DeserializeObject<T>(deletedDocument.ToJson());
                return dto;
            }
            catch (MongoWriteException e)
            {
                Console.WriteLine(e.ToString());
                Logger.GetInstance().LogError(e.ToString());
                return default(T);
            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                return default(T);
            }
        }

        public T Load(FilterDefinition<BsonDocument> filter)
        {
            try
            {
                var Document = Documents.Find(filter).FirstOrDefault();
                var json = Document.ToJson();
                if (json.StartsWith("{ \"_id\" : ObjectId(")) { json = "{" + json.Substring(47); }
                T dto = JsonConvert.DeserializeObject<T>(json);
                return dto;
            }
            catch (MongoWriteException e)
            {
                Console.WriteLine(e.ToString());
                Logger.GetInstance().LogError(e.ToString());
                return default(T);

            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                return default(T);

            }
        }
        public void Update(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, Boolean upsert = false)
        {
            if (upsert)
                Documents.UpdateOne(filter, update, new UpdateOptions() { IsUpsert = upsert });
            else Documents.UpdateOne(filter, update);
        }
    }
}
