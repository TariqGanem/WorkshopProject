using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            // FOR TESTNG PURPOSES
            RealAdapter api = new RealAdapter();

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
            return;
        }
    }
}
