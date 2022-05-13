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
            return;
            //Assert.True(shopping_bag.Count == 1);
            //int PotentialQuantity;
            //shopping_bag.TryGetValue(product_id, out PotentialQuantity);
            //Assert.True(shopping_bag.ContainsKey(product_id));
            //Assert.True(PotentialQuantity == 10);


        }
    }
}
