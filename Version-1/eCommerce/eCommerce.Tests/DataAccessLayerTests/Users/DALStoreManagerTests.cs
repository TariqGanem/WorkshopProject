using eCommerce.src.DataAccessLayer;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTests.DataAccessLayerTests.Users
{
    public class DALStoreManagerTests : XeCommerceTestCase
    {
        public DBUtil dbutil;

        public DALStoreManagerTests()
        {
            String connection_url = "mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority";
            String db_name = "UnitTesting";
            this.dbutil = DBUtil.getInstance(connection_url, db_name);
        }
    }
}
