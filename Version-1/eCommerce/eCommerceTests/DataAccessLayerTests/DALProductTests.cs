using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer.Store;
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
    public class DALProductTests : XeCommerceTestCase
    {
        public DBUtil dbutil;

        public DALProductTests()
        {
            String connection_url = "mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority";
            String db_name = "UnitTesting";
            this.dbutil = DBUtil.getInstance(connection_url, db_name);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void CreateProduct()
        {
            Product p = new Product("product1", 100,"category1",10);
            this.dbutil.Create(p);
            var filter3 = Builders<BsonDocument>.Filter.Eq("_id", p.Id);
            Product p1 = this.dbutil.LoadProduct(filter3);
            Assert.True(p.Id == p1.Id && p.Name == p1.Name & p.Quantity == p1.Quantity & p.Price == p1.Price);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void LoadProduct()
        {
            var filter3 = Builders<BsonDocument>.Filter.Eq("_id", "9df00cbd63f846a28947498c6193641c");
            Product p = this.dbutil.LoadProduct(filter3);
            Assert.NotNull(p);
        }

        [Fact]
        [Trait("Category", "DALUnit")]
        public void UpdateProduct()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "9df00cbd63f846a28947498c6193641c");
            var update = Builders<BsonDocument>.Update.Set("Name", "productsNewName");
            this.dbutil.UpdateProduct(filter, update);
            Product p = this.dbutil.LoadProduct(filter);
            Assert.True(p.Name == "productsNewName");


        }





    }
}
