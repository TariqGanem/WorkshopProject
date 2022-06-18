using eCommerce.src;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User.Roles;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using eCommerce.src.DomainLayer.Notifications;

namespace eCommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            return;
        }
        /*
        static void Main(string[] args)
        {
            //DBUtil.getInstance("mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority", "TestScenario1").clearDB();
            //return;
            eCommerceSystem ecom = new eCommerceSystem();
            ecom.systemFacade.userFacade.RegisteredUsers.TryGetValue("a7ae753ed12045659260781aaaf9ab8c", out RegisteredUser reg2);
            Notification noti = new Notification("test", "testmsg",false);
            Notification noti2 = new Notification("test", "testmsg2",false);
            reg2.PendingNotification.AddLast(noti);
            reg2.PendingNotification.AddLast(noti2);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", reg2.Id);
            var update_notification = Builders<BsonDocument>.Update.Set("PendingNotification", reg2.getPendingNotificationsDTO());
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_notification); 
        }
        public static void Scenario1_checkAddtocart()
        {
            eCommerceSystem ecom = new eCommerceSystem();
            Result res = ecom.AddProductToCart("8fbbf3df965146a584abd4d45cf6f628", "d329edb3df6540ef8a6dd20c33b92625", 1, "3f28395f92104aeab4039f248b92b22d");            
            res = ecom.AddProductToCart("8fbbf3df965146a584abd4d45cf6f628", "d329edb3df6540ef8a6dd20c33b92625", 1, "3f28395f92104aeab4039f248b92b22d");
            if (res.ErrorOccured)
            {
                //Console.WriteLine("\n1.!!!!!\n");
                Console.WriteLine(res.ErrorMessage);
            }
            return;
        }
        public static void ContScenaro1_2()
        {
            eCommerceSystem ecom = new eCommerceSystem();
            Result res = ecom.AddProductToCart("8fbbf3df965146a584abd4d45cf6f628", "d329edb3df6540ef8a6dd20c33b92625", 5, "3f28395f92104aeab4039f248b92b22d");
            if (res.ErrorOccured)
            {
                Console.WriteLine("\n1.!!!!!\n");
                Console.WriteLine(res.ErrorMessage);
            }
            IDictionary<String, Object>  paymentDetails = new Dictionary<String, Object>
                    {
                     { "card_number", "2222333344445555" },
                     { "month", "4" },
                     { "year", "2021" },
                     { "holder", "Israel Israelovice" },
                     { "ccv", "262" },
                     { "id", "20444444" }
                    };
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>
                    {
                     { "name", "Israel Israelovice" },
                     { "address", "Rager Blvd 12" },
                     { "city", "Beer Sheva" },
                     { "country", "Israel" },
                     { "zip", "8458527" }
                    };
            Result<ShoppingCartSO> shp = ecom.Purchase("8fbbf3df965146a584abd4d45cf6f628", paymentDetails, deliveryDetails);
            if (shp.ErrorOccured)
                Console.Out.WriteLine(shp.ErrorMessage);
            return;
        }
        public static void ContScenario1_1()
        {
            eCommerceSystem ecom = new eCommerceSystem();
            String productid = ecom.AddProductToStore("8fbbf3df965146a584abd4d45cf6f628", "3f28395f92104aeab4039f248b92b22d", "Bamba", 30, 20, "food").Value;
            ecom.AddProductRatingInStore("8fbbf3df965146a584abd4d45cf6f628", "3f28395f92104aeab4039f248b92b22d", productid, 4);
            return;

        }
        public static void Scenario1()
        {
           // String adminId = "-777";
            eCommerceSystem ecom = new eCommerceSystem();
            RegisteredUserSO u1 = ecom.Register("u1", "pass").Value;
            RegisteredUserSO founderId = ecom.Login("u1", "pass").Value;
            StoreService storeid = ecom.OpenNewStore("s1", founderId.Id).Value;
            ecom.AddStoreRating(u1.Id, storeid.Id, 2.2);
            return;
        }
        public static void Scenario2()
        {
            String adminId = "-777";
            eCommerceSystem ecom = new eCommerceSystem();
            // register users u1,u2,u3,u4,u5,u6

            RegisteredUserSO u1 = ecom.Register("u1", "pass").Value;
            RegisteredUserSO u2 = ecom.Register("u2", "pass").Value;
            RegisteredUserSO u3 = ecom.Register("u3", "pass").Value;
            RegisteredUserSO u4 = ecom.Register("u4", "pass").Value;
            RegisteredUserSO u5 = ecom.Register("u5", "pass").Value;
            RegisteredUserSO u6 = ecom.Register("u6", "pass").Value;

            // make u1 admin
            ecom.AddSystemAdmin(adminId, u1.Id);

            // u1,2,3,4,5,6 login
            ecom.Login("u1", "pass");
            RegisteredUserSO founderId = ecom.Login("u2", "pass").Value;
            RegisteredUserSO managerid = ecom.Login("u3", "pass").Value;
            RegisteredUserSO storeowner1 = ecom.Login("u4", "pass").Value;
            RegisteredUserSO storeowner2 = ecom.Login("u5", "pass").Value;
            ecom.Login("u6", "pass");

            // u2 open store s1
            StoreService storeid = ecom.OpenNewStore("s1", founderId.Id).Value;

            // u2 add item “Bamba” to store s1 with cost 30 and quantity 20
            String pro = ecom.AddProductToStore(founderId.Id, storeid.Id, "Bamba", 30, 20, "food").Value;

            // u2 appoints u3 to a store manager with permission to manage inventory.
            ecom.AddStoreManager(managerid.Id, founderId.Id, storeid.Id);
            LinkedList<int> per = new LinkedList<int>();
            per.AddLast(0);
            per.AddLast(1);
            per.AddLast(2);
            ecom.SetPermissions(storeid.Id, managerid.Id, founderId.Id, per);

            // u2 appoint u4, u5 to store s1 owner
            ecom.AddStoreOwner(storeowner1.Id, founderId.Id, storeid.Id);
            ecom.AddStoreOwner(storeowner2.Id, founderId.Id, storeid.Id);

            // u5 logs off
            ecom.Logout(storeowner2.Id);

            //api.AddProductToCart(founderId, pro, 1, storeid);

            // close store - to test notifications in db
            ecom.CloseStore(founderId.Id, storeid.Id);
            return;
        }

        public static void testcode()
        {
            throw new Exception("testcode");
            // FOR TESTNG PURPOSES

            /*
            String store_owner;
            String store_id;
            String manager_id;
            Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
            store_id = storeId.Value;
            store_owner = userId.Value;
            Result<bool> reguserId2 = api.Register("Manager@gmail.com", "ManagerPassword");
            Result<String> userId2 = api.Login("Manager@gmail.com", "ManagerPassword");
            manager_id = userId2.Value;
            Result<bool> managerRes = api.AddStoreManager(manager_id, store_owner, store_id);
            LinkedList<int> permission = new LinkedList<int>();
            permission.AddLast(0);
            permission.AddLast(1);
            Result<String> addProdRes = api.AddProductToStore(manager_id, store_id, "Product_name", 10, 10, "Category");
            //Assert.True(addProdRes.ErrorOccured);
            Result<bool> permRes = api.SetPermissions(store_id, manager_id, store_owner, permission);
            System.Console.WriteLine(permRes.ErrorMessage);
            //Assert.True(permRes.ErrorOccured == false);
            addProdRes = api.AddProductToStore(manager_id, store_id, "Product_name", 10, 10, "Category");
            //Assert.False(addProdRes.ErrorOccured);
            */
            /*
            String store_owner;
            String store_id;
            Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
            store_id = storeId.Value;
            store_owner = userId.Value;
            Result<bool> reguserId2 = api.Register("Manager@gmail.com", "ManagerPassword");
            Result<String> userId2 = api.Login("Manger@gmail.com", "ManagerPassword");
            String user_id = userId2.Value;
            Result<bool> managerRes = api.AddStoreManager(user_id, store_owner, store_id);
            System.Console.WriteLine(managerRes.ErrorMessage);
            //Assert.True(!managerRes.ErrorOccured);
            */
            /*
            String store_owner;
            String store_id;
            String user_id;

            Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
            store_id = storeId.Value;
            store_owner = userId.Value;
            Result<bool> reguserId2 = api.Register("Buyer@gmail.com", "BuyerPassword");
            Result<String> userId2 = api.Login("Buyer@gmail.com", "BuyerPassword");
            user_id = userId2.Value;
            Result<String> productId = api.AddProductToStore(store_owner, store_id, "CODBO2", 300, 10, "VideoGames");
            System.Console.WriteLine(productId.Value);
            //Assert.False(productId.ErrorOccured);
            IDictionary<String, Object> dictonary = new Dictionary<String, Object>() { { "Name", "new_name" } };
            Result<bool> editRes = api.EditProductDetails(store_owner, store_id, productId.Value, dictonary);
            System.Console.WriteLine(editRes.ErrorMessage);
            */
            //Assert.False(editRes.ErrorOccured);
            //Assert.True(!api.SearchProduct(dictonary).ErrorOccured);
            /*
            String user_id;
            String store_id;
            String product_id;
            Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
            Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
            store_id = storeId.Value;
            user_id = userId.Value;
            Result<String> productId = api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
            product_id = productId.Value;

            Result<String> buyer = api.Login(); // Guest User
            String buyer_id = buyer.Value;
            api.AddProductToCart(buyer_id, product_id, 10, store_id);
            List<String> buyer_shopping_bags = api.GetUserShoppingCart(buyer_id).Value;
            Console.WriteLine(buyer_shopping_bags.Count);
            Double dbl = api.GetTotalShoppingCartPrice(buyer_id).Value;
            Result<Dictionary<String, int>> shopping_bag = api.GetUserShoppingBag(buyer_id, buyer_shopping_bags[0]);
            System.Console.WriteLine(shopping_bag.ErrorMessage);
            System.Console.WriteLine(store_id);
            */
            /*
                String store_id;
                String user_id;
                String product_id;
                String buyer_id;
                Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
                Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
                Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
                store_id = storeId.Value;
                user_id = userId.Value;
                Result<String> productId = api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
                product_id = productId.Value;
                buyer_id = api.Login().Value;
                api.AddProductToCart(buyer_id, product_id, 5, store_id);
                bool update_success = api.UpdateShoppingCart(buyer_id, store_id, product_id, 2).Value;
                //Assert.True(update_success);
                List<String> shopping_bags = api.GetUserShoppingCart(buyer_id).Value;
                Dictionary<String, int> shopping_bag = api.GetUserShoppingBag(buyer_id, store_id).Value;
                //Assert.True(shopping_bag.Count == 1); // check number of added products in shopping bag
                int PotentialQuantity;
                bool quanbool = shopping_bag.TryGetValue(product_id, out PotentialQuantity);
                System.Console.WriteLine(PotentialQuantity);
            */
            //Assert.True(quanbool);
            //Assert.True(PotentialQuantity == 10); // check quantity in shopping bad
            //return;
            //Assert.True(shopping_bag.Count == 1);
            //int PotentialQuantity;
            //shopping_bag.TryGetValue(product_id, out PotentialQuantity);
            //Assert.True(shopping_bag.ContainsKey(product_id));
            //Assert.True(PotentialQuantity == 10);
            /*
                String store_id;
                String user_id;
                String product_id;
                //String buyer_id;
                Result<bool> reguserId = api.Register("WantToOpenAStore@gmail.com", "StringPassword");
                Result<String> userId = api.Login("WantToOpenAStore@gmail.com", "StringPassword");
                Result<String> storeId = api.OpenNewStore("AmazonWanaBe", userId.Value);
                store_id = storeId.Value;
                user_id = userId.Value;
                Result<String> productId = api.AddProductToStore(user_id, store_id, "CODBO2", 300, 10, "VideoGames");
                product_id = productId.Value;
                Result<String> buyer = api.Login("Buyer@gmail.com", "StrongPassword"); // Reg User
                String buyer_id = buyer.Value;
                api.AddProductToCart(buyer_id, product_id, 10, store_id);
                Result<List<String>> buyer_shopping_bags = api.GetUserShoppingCart(buyer_id);
                //Assert.True(buyer_shopping_bags.ErrorOccured);
                //Assert.True(buyer_shopping_bags.Value.Count == 1);
                Dictionary<String, int> shopping_bag = api.GetUserShoppingBag(buyer_id, store_id).Value;
                //Assert.True(shopping_bag.Count == 1); // check number of added products in shopping bag
                int PotentialQuantity;
                shopping_bag.TryGetValue(product_id, out PotentialQuantity);
                //Assert.True(shopping_bag.ContainsKey(product_id)); // check productId in shopping bag
                //Assert.True(PotentialQuantity == 10); // check quantity in shopping bad
                //Assert.True(buyer_shopping_bags.Value.Count == 1);
            */
            /*
            Console.Out.WriteLine("testtest");
            Product p = new Product("product", 10, "test", 5);
            DBUtil dbu = DBUtil.getInstance("mongodb+srv://Workshop:Workshop@workshopproject.frdmk.mongodb.net/?retryWrites=true&w=majority" , "TestingDB");
            dbu.Create(p);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", p.Id);
            Product p2 = dbu.LoadProduct(filter);
            Console.Out.WriteLine(p2.Id == p.Id);
            */


        //}

    }
}
