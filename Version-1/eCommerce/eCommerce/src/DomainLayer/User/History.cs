using eCommerce.src.DataAccessLayer;
using eCommerce.src.DataAccessLayer.DataTransferObjects.Stores;
using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public History(LinkedList<ShoppingBag> sb)
        {
            this.ShoppingBags = sb;
        }

        public void AddPurchasedShoppingCart(ShoppingCart shoppingCart , MongoDB.Driver.IClientSessionHandle session = null)
        {
            ConcurrentDictionary<String, ShoppingBag> bags = shoppingCart.ShoppingBags;
            String userId = "";
            foreach (var bag in bags)
            {
                userId = bag.Value.Id;
                ShoppingBags.AddLast(bag.Value);
            }

            var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);
            var update_history = Builders<BsonDocument>.Update.Set("History", getDTO());
            DBUtil.getInstance().UpdateRegisteredUser(filter, update_history, session:session);
        }

        public void AddPurchasedShoppingBag(ShoppingBag shoppingBag , MongoDB.Driver.IClientSessionHandle session = null)
        {
            ShoppingBags.AddLast(shoppingBag);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", shoppingBag.Store.Id);
            var update_history = Builders<BsonDocument>.Update.Push("History.ShoppingBags", GetDTO_HistoryShoppingBag(shoppingBag.getSO()));
            DBUtil.getInstance().UpdateStore(filter, update_history, session);
        }

        public DTO_PurchasedShoppingBag GetDTO_HistoryShoppingBag(ShoppingBagSO sb)
        {
            LinkedList<DTO_PurchasedProduct> products_dto = new LinkedList<DTO_PurchasedProduct>();
            foreach (var tup in sb.Products)
            {
                ProductService p = tup.Key;
                DTO_PurchasedProduct hp_dto = new DTO_PurchasedProduct(p.Id, p.Name, p.Price, tup.Value, p.Category);
                products_dto.AddLast(hp_dto);
            }
            return new DTO_PurchasedShoppingBag(sb.Id, sb.UserId, sb.StoreId, products_dto, sb.TotalPrice);
        }
        public UserHistorySO getSO()
        {
            return new UserHistorySO(this);
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
