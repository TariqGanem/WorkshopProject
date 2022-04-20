using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ShoppingBasketService
    {
        public String Id { get; }
        public String UserId { get; }
        public String StoreId { get; }
        public Dictionary<ProductService, int> Products { get; }  //<productDAL , quantity>
        public Double TotalBagPrice { get; }


        //Constructor 
        public ShoppingBasketService(String bagID, String userID, String storeID, IDictionary<ProductService, int> products, Double totalBagPrice)
        {
            Id = bagID;
            UserId = userID;
            StoreId = storeID;
            Products = new Dictionary<ProductService, int>(products);
            TotalBagPrice = totalBagPrice;
        }
    }
}