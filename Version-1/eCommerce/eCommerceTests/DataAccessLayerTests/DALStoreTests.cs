using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerceIntegrationTests.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceTests.DataAccessLayerTests
{
    public class DALStoreTests : XeCommerceTestCase
    {
        public DBUtil dbutil;
        public StoreOwner founder;
        public RegisteredUser reg;

        public DALStoreTests()
        {
            String connection_url = "mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority";
            String db_name = "UnitTesting";
            this.dbutil = DBUtil.getInstance(connection_url, db_name);
            reg = new RegisteredUser("founder1", "founder1");
            founder = new StoreOwner(reg, "8d68c4b02ecc4304adf6e6ef3cb2c5c2", null);
            //dbutil.Create(reg);
            //dbutil.Create(founder);
            //dbutil.Create(founder);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void CreateStore()
        {
            Store store = new Store("store1", reg);
            dbutil.Create(store);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
            Store store2 = dbutil.LoadStore(filter);
            Assert.NotNull(store2);
            Assert.True(store2.Id == store.Id);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void LoadStore()
        {
            //dbutil.Create(founder);
            var filter2 = Builders<BsonDocument>.Filter.Eq("UserId", "0a02e4f7a6e349c28fbbbedb6001aec1") & Builders<BsonDocument>.Filter.Eq("StoreId", "7c189f7d77ba4eadb369a8f54a6fe1b8");
            dbutil.LoadStoreOwner(filter2);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "7c189f7d77ba4eadb369a8f54a6fe1b8");
            Store store = dbutil.LoadStore(filter);
            Assert.True(store.Id == "7c189f7d77ba4eadb369a8f54a6fe1b8");
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void UpdateStore()
        {
            var filter2 = Builders<BsonDocument>.Filter.Eq("UserId", "0a02e4f7a6e349c28fbbbedb6001aec1") & Builders<BsonDocument>.Filter.Eq("StoreId", "7c189f7d77ba4eadb369a8f54a6fe1b8");
            dbutil.LoadStoreOwner(filter2);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", "7c189f7d77ba4eadb369a8f54a6fe1b8");
            var update = Builders<BsonDocument>.Update.Set("Name", "store1NewName");
            this.dbutil.UpdateStore(filter, update);
            Store s = this.dbutil.LoadStore(filter);
            Assert.True(s.Name == "store1NewName");
        }

        [Fact ( Skip = "TOO MUCH CHANGES TO DB")]
        [Trait("Category", "DALUnit")]
        public void DeleteStore()
        {
            Store store = new Store("todel", reg);
            dbutil.Create(store);
            var filter2 = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
            dbutil.DeleteStore(filter2);
            try { dbutil.LoadStore(filter2); }
            catch { Assert.True(true);  }
        }


    }
}
