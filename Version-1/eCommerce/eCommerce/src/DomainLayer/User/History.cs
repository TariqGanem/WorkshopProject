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
            LinkedList<DTO_ShoppingBag> hsp_dto = new LinkedList<DTO_ShoppingBag>();
            foreach (var sb in ShoppingBags)
            {
                LinkedList<DTO_Product> products_dto = new LinkedList<DTO_Product>();
                foreach (var tup in sb.Products)
                {
                    Product p = tup.Key;
                    DTO_Product hp_dto = new DTO_Product(p.Id, p.Name, p.Price, tup.Value, p.Category , 0 , 0 , null);
                    products_dto.AddLast(hp_dto);
                }
                ConcurrentDictionary<String,int> tempDic = new ConcurrentDictionary<String,int>();
                foreach (DTO_Product dt_pro in products_dto)
                    tempDic.TryAdd(dt_pro._id, dt_pro.Quantity);
                hsp_dto.AddLast(new DTO_ShoppingBag(sb.Id, sb.UserId, sb.Store.Id, tempDic, sb.TotalBagPrice));
            }
            return new DTO_History(hsp_dto);
        }
    }
}
