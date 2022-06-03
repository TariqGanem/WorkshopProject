using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ServiceLayer.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class History
    {
        public LinkedList<ShoppingBag> ShoppingBags { get; }

        public History()
        {
            ShoppingBags = new LinkedList<ShoppingBag>();
        }

        public void AddPurchasedShoppingCart(ShoppingCart shoppingCart)
        {
            ConcurrentDictionary<String, ShoppingBag> bags = shoppingCart.ShoppingBags;
            foreach (ShoppingBag bag in bags.Values)
            {
                ShoppingBags.AddLast(bag);
            }
        }

        public void AddPurchasedShoppingBag(ShoppingBag shoppingBag)
        {
            ShoppingBags.AddLast(shoppingBag);
        }

        public DTO_History getDTO()
        {
            LinkedList<DTO_PurchasedShoppingBag> hsp_dto = new LinkedList<DTO_PurchasedShoppingBag>();
            foreach (var sb in ShoppingBags)
            {
                LinkedList<DTO_PurchasedProduct> products_dto = new LinkedList<DTO_PurchasedProduct>();
                foreach (var tup in sb.Products)
                {
                    Product p = tup.Key;
                    DTO_PurchasedProduct hp_dto = new DTO_PurchasedProduct(p.Id, p.Name, p.Price, tup.Value, p.Category );
                    products_dto.AddLast(hp_dto);
                }
                hsp_dto.AddLast(new DTO_PurchasedShoppingBag(sb.Id, sb.UserId, sb.Store.Id, products_dto, sb.TotalBagPrice));
            }
            return new DTO_History(hsp_dto);
        }
    }
}
