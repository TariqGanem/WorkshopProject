using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.User;
using eCommerceIntegrationTests.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceTests.DataAccessLayerTests.Users
{
    public class DALRegisteredUserTests : XeCommerceTestCase
    {
        public DBUtil dbutil;

        public DALRegisteredUserTests()
        {
            String connection_url = "mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority";
            String db_name = "UnitTesting";
            this.dbutil = DBUtil.getInstance(connection_url, db_name);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void CreateRegUser()
        {
            RegisteredUser reg = new RegisteredUser("user1", "pass1");
            dbutil.Create(reg);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", reg.Id);
            RegisteredUser reg1 = dbutil.LoadRegisteredUser(filter);
            Assert.True(reg.Id == reg1.Id);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void LoadRegUser()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "ef10e932687140e69bf45195b082241b");
            RegisteredUser reg1 = dbutil.LoadRegisteredUser(filter);
            Assert.NotNull(reg1);
            Assert.True(reg1.UserName == "user1" && reg1._password == "pass1");
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void UpdateRegUser()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "ef10e932687140e69bf45195b082241b");
            var update = Builders<BsonDocument>.Update.Set("_password", "newpassword");
            dbutil.UpdateRegisteredUser(filter, update);
            RegisteredUser reg1 = dbutil.LoadRegisteredUser(filter);
            Assert.NotNull(reg1);
            Assert.True(reg1.UserName == "user1" && reg1._password == "newpassword");
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void DeleteRegUser()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "40b067665b404585b90482d4361600ff");
            dbutil.DeleteRegisteredUser(filter);
            // check on data base the actual deletion
        }
    }
}
