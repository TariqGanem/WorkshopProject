using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Client.Code
{
    public class ShopHandler
    {
        public ShopHandler() { }

        public DataSet search(string keyword)
        {
            string param = string.Format("keyword={0}", keyword);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("search", param).ToString());
            DataTable t1 = new DataTable("products");
            t1.Columns.Add("Id");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Catagory");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("products");
            d1.Tables.Add(t1);
            return d1;
        }

        public bool OpenShop(string userId, string shopName)
        {
            string param = string.Format("storeName={0}&userId={1}", userId, shopName);
            return Boolean.Parse(system.SendApi("OpenShop", param));
        }

        public bool CloseStore(string storeId, string ownerId)
        {
            string param = string.Format("storeId={0}&userId={1}", storeId, ownerId);
            return bool.Parse(system.SendApi("CloseStore", param));
        }

        public bool AddItemToStore(string userId, string storeId, string productName, int price, int quantity, string category)
        {
            string param = string.Format("userId={0}&storeId={1}&productName={2}&price={3}&quantity={4}&category={5}", userId, storeId, productName, price, quantity, category);
            return bool.Parse(system.SendApi("AddProductToStore", param));
        }

        public bool AddProductToBasket(string userId, string productId, int quantity, string storeId)
        {
            string param = string.Format("userId={0}&productId={1}&quantity={2}&storeId={3}", userId, productId, quantity, storeId);
            return bool.Parse(system.SendApi("AddProductToCart", param));
        }

        public bool Purchase(string userName, string creditCard)
        {
            string param = string.Format("userName={0}&creditCard={1}", userName, creditCard);
            return bool.Parse(system.SendApi("Purchase", param));

        }

        public bool UpdateCart(string userName, string storeName, string productBarcode, int newAmount)
        {
            string param = string.Format("userName={0}&storeName={1}&productBarcode={2}&newAmount={3}", userName, storeName, productBarcode, newAmount);
            return bool.Parse(system.SendApi("UpdateCart", param));
        }

        public string UpdateProductAmountInStore(string userName, string storeName, string productBarcode, int amount)
        {
            string param = string.Format("userName={0}&storeName={1}&productBarcode={2}&amount={3}", userName, storeName, productBarcode, amount);
            return (system.SendApi("UpdateProductAmountInStore", param));
        }

        public bool remove_item_from_cart(string userId, string storeId, string productId)
        {
            string param = string.Format("userId={0}&storeId={1}&productId={2}", userId, storeId, productId);
            return bool.Parse(system.SendApi("RemoveProductFromCart", param));
        }

        //==================================================================================================================================================================
        //getters
        public DataSet getAllProducts()
        {
            string param = "";
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetStoresProducts", param).ToString());
            DataTable t1 = new DataTable("products");
            t1.Columns.Add("Id");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Catagory");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }
            DataSet d1 = new DataSet("products");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getAllStores()
        {
            string param = "";
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getAllStores", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("Id");
            t1.Columns.Add("Name");
            t1.Columns.Add("Founder");
            t1.Columns.Add("Rate");
            t1.Columns.Add("NumberOfRates");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }

            DataSet d1 = new DataSet("Stores");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetUserStores(string userName)
        {
            string param = string.Format("userName={0}", userName);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetUserStores", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("storeName");
            t1.Columns.Add("ownerName");
            t1.Columns.Add("sellingpolicy");
            t1.Columns.Add("message");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("Stores");
            d1.Tables.Add(t1);
            return d1;

        }
        
        public DataSet GetStoreManagers(string storeId)
        {
            string param = string.Format("storeId={0}", storeId);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetStoreManagers", param).ToString());
            DataTable t1 = new DataTable("manager");
            t1.Columns.Add("username");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i]);
            }

            DataSet d1 = new DataSet("manager");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetStoreOwners(string storeId)
        {
            string param = string.Format("storeId={0}", storeId);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetStoreOwners", param).ToString());
            DataTable t1 = new DataTable("owners");
            t1.Columns.Add("username");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i]);
            }

            DataSet d1 = new DataSet("owners");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetAllUserNamesInSystem()
        {
            string param = "";
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetAllUserNamesInSystem", param).ToString());
            DataTable t1 = new DataTable("Users");
            t1.Columns.Add("username");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i]);
            }

            DataSet d1 = new DataSet("Users");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetStoreProducts(string storeName)
        {
            string param = string.Format("storeName={0}", storeName);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetStoreProducts", param).ToString());
            DataTable t1 = new DataTable("products");
            t1.Columns.Add("productName");
            t1.Columns.Add("barcode");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][2]);
            }
            DataSet d1 = new DataSet("products");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetUserBaskets(string userName)
        {

            string param = string.Format("userName={0}", userName);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetUserBaskets", param).ToString());
            DataTable t1 = new DataTable("products");
            t1.Columns.Add("productName");
            t1.Columns.Add("descerption");
            t1.Columns.Add("barcode");
            t1.Columns.Add("price");
            t1.Columns.Add("catagory");
            t1.Columns.Add("nameShop");
            t1.Columns.Add("Amount");

            for (int i = 0; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4], jarray[i][6], jarray[i][7]);
            }

            DataSet d1 = new DataSet("products");
            d1.Tables.Add(t1);
            return d1;
        }
    }
}