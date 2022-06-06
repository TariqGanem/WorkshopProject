using eCommerce.src.DataAccessLayer;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src
{
    public static class Initialize
    {
        public static void Initializer()
        {
            //String adminId = "e035ddf3301245119e39a6f2a81143da";
            DBUtil dbutil;
            String connection_url = "mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority";
            String db_name = "Scenario1";
            dbutil = DBUtil.getInstance(connection_url, db_name); // db initializer
            String extSystemURL = "https://cs-bgu-wsep.herokuapp.com/";  
            Proxy.getInstance(); // external system init - MOCK
            // Proxy.getInstance(extSystemURL); // external system init
        }
    }
}
