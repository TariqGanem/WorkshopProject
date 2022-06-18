using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Client.Code
{
    public class UserHandler
    {
        public UserHandler() { }
        public string GuestLogin()
        {
            string param = "";
            return system.SendApi("GuestLogin", param);
        }

        public string Login(string username, string password)
        {
            string param = string.Format("username={0}&password={1}", username, password);
            return (system.SendApi("Login", param));
        }

        public bool Logout(string username)
        {
            string param = string.Format("username={0}", username);
            return bool.Parse(system.SendApi("Logout", param));
        }

        public string Register(string username, string password)
        {
            string param = string.Format("username={0}&password={1}", username, password);
            return (system.SendApi("Register", param));
        }

        public bool Purchase(string userName, string card_number, string month, string year,
            string holder, string ccv, string id, string name, string address, string city, string country, string zip)
        {
            string param = string.Format("userName={0}&card_number={1}&month={2}&year={3}&holder={4}" +
                "&ccv={5}&id={6}&name={7}&address={8}&city={9}&country={10}&zip={11}", userName, card_number,month,year,holder,ccv,id,name,address,city,country,zip);
            return bool.Parse(system.SendApi("Purchase", param));
        }

        public bool UpdateCart(string userId, string storeId, string productId, int newAmount)
        {
            string param = string.Format("userId={0}&storeId={1}&productId={2}&newAmount={3}", userId, storeId,productId,newAmount);
            return bool.Parse(system.SendApi("UpdateCart", param));
        }

        public DataSet getNotifications(string userid)
        {
            string param = string.Format("userid={0}", userid);
            string str = system.SendApi("getNotifications", param);
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            string[] notis = str.Split(',');
            DataTable t1 = new DataTable("Notifications");
            t1.Columns.Add("id");
            t1.Columns.Add("msg");
            for (int i = 0; i < notis.Length ; i++)
            {
                try
                {
                    t1.Rows.Add(i, notis[i].Replace("\n","\r\n")); // test
                }
                catch
                { }
            }
            DataSet set = new DataSet("Notification");
            set.Tables.Add(t1);
            return set;
        }

        public bool AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            string param = string.Format("userId={0}&productId={1}&quantity={2}&storeId={3}", userId, productId,quantity,storeId);
            return bool.Parse(system.SendApi("AddProductToCart", param));
        }

        public bool RemoveProductFromCart(string userId, string storeId, string productId)
        {
            string param = string.Format("userId={0}&storeId={1}&productId={2}", userId, storeId,productId);
            return bool.Parse(system.SendApi("RemoveProductFromCart", param));
        }

        public bool AddProductToStore(string userId, string storeId, string productName, int price, int quantity,
            string category)
        {
            string param = string.Format("userId={0}&storeId={1}&productName={2}&price={3}&quantity={4}&category={5}", userId, storeId,productName,price,quantity,category);
            return bool.Parse(system.SendApi("AddProductToStore", param));
        }

        public bool RemoveProductFromStore(String userID, String storeID, String productID)
        {
            string param = string.Format("userID={0}&storeID={1}&productID={2}", userID, storeID,productID);
            return bool.Parse(system.SendApi("RemoveProductFromStore", param));
        }

        public String OpenShop(string storeName, string userId)
        {
            string param = string.Format("storeName={0}&userId={1}", storeName, userId);
            return system.SendApi("OpenShop", param);
        }

        public bool CloseShop(string storeId, string userId)
        {
            string param = string.Format("storeId={0}&userId={1}", storeId, userId);
            return bool.Parse(system.SendApi("CloseShop", param));
        }

        public bool makeNewOwner(string newOwnerId, string currentOwnerId, string storeId)
        {
            string param = string.Format("newOwnerId={0}&currentOwnerId={1}&storeId={2}", newOwnerId, currentOwnerId,storeId);
            return bool.Parse(system.SendApi("makeNewOwner", param));
        }

        public bool AddStoreManager(string storeId, string currentOwnerId, string newManagerId)
        {
            string param = string.Format("storeId={0}&currentOwnerId={1}&newManagerId={2}", storeId, currentOwnerId,newManagerId);
            return bool.Parse(system.SendApi("AddStoreManager", param));
        }

        public bool removeManager(string currentOwnerId, string storeId, string ManagerToRemove)
        {
            string param = string.Format("currentOwnerId={0}&storeId={1}&ManagerToRemove={2}", currentOwnerId, storeId,ManagerToRemove);
            return bool.Parse(system.SendApi("removeManager", param));
        }

        public bool removeOwner(string currentOwnerId, string storeId, string OwnerToRemove)
        {
            string param = string.Format("currentOwnerId={0}&storeId={1}&OwnerToRemove={2}", currentOwnerId, storeId,OwnerToRemove);
            return bool.Parse(system.SendApi("removeOwner", param));
        }

        public bool isStoreOwner(string userid,string storeid)
        {
            string param = string.Format("userid={0}&storeid={1}", userid, storeid);
            return bool.Parse(system.SendApi("isStoreOwner", param));
        }

        public DataSet SearchProduct(string keyword)
        {
            string param = string.Format("keyword={0}", keyword);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("SearchProduct", param).ToString());
            DataTable t1 = new DataTable("products");
            t1.Columns.Add("productId");
            t1.Columns.Add("Name");
            t1.Columns.Add("price");
            t1.Columns.Add("catagory");
            t1.Columns.Add("quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++) {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }
            DataSet d1 = new DataSet("products");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet SearchStore(string name)
        {
            string param = string.Format("name={0}", name);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("SearchStore", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("storeId");
            t1.Columns.Add("StoreName");
            t1.Columns.Add("StoreFounder");
            t1.Columns.Add("Rate");
            t1.Columns.Add("NumbeOfRates");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("stores");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3],jarray[i][4]);
            }

            DataSet d1 = new DataSet("Stores");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getUserShoppingCart(string userid)
        {
            string param = string.Format("userid={0}", userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getUserShoppingCart", param).ToString());
            DataTable t1 = new DataTable("ShoppingCart");
            t1.Columns.Add("storeid");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("shoppingcart");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i= 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2] , jarray[i][3]);
            }

            DataSet d1 = new DataSet("shoppingcart");
            d1.Tables.Add(t1);
            return d1;
        }
        
        public DataSet getUserPurchaseHistory(string userid)
        {
            string param = string.Format("userid={0}", userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getUserPurchaseHistory", param).ToString());
            DataTable t1 = new DataTable("ShoppingCart");
            t1.Columns.Add("storeid");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("shoppinghistory");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("shoppinghistory");
            d1.Tables.Add(t1);
            return d1;
        }

        public bool reOpenStore(string storeid, string userid)
        {
            string param = string.Format("storeid={0}&userid={1}", storeid, userid);
            return bool.Parse(system.SendApi("reOpenStore", param));
        }

        public bool editProductDetail(string userID, string storeID, string productID,
            string param , string editto)
        {
            string par = string.Format("userID={0}&storeID={1}&productID={2}&param={3}&editto={4}", userID, storeID,productID, param,editto);
            return bool.Parse(system.SendApi("editProductDetail", par));
        }

        public bool SetPermissions(string storeID, string managerID, string ownerID, String Permissions) // check permissions format before server req
        {
            string param = string.Format("storeID={0}&managerID={1}&ownerID={2}&Permissions={3}", storeID, managerID,ownerID, Permissions);
            return bool.Parse(system.SendApi("SetPermissions", param));
        }

        public bool RemovePermissions(string storeID, string managerID, string ownerID, String permissions) // check permissions format before server req
        {
            string param = string.Format("storeID={0}&managerID={1}&ownerID={2}&permissions={3}", storeID, managerID,ownerID,permissions);
            return bool.Parse(system.SendApi("RemovePermissions", param));
        }

        public DataSet GetStoreStaff(string ownerID, string storeID)
        {
            string param = string.Format("ownerID={0}&storeID={1}",ownerID,storeID);
            String str = system.SendApi("GetStoreStaff", param);
            if (str.Substring(0, 6).Equals("Error:"))
                return new DataSet("storestaff");
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            string[] notis = str.Split(',');
            DataTable t1 = new DataTable("StoreStaff");
            t1.Columns.Add("id");
            t1.Columns.Add("Username");
            for (int i = 1; i < notis.Length ; i++)
            {
                try
                {
                    notis[i] = notis[i].TrimEnd();
                    notis[i].TrimStart();
                    t1.Rows.Add(notis[i], this.getUsernameFromId(notis[i].Substring(1,32)));
                }
                catch
                { }
            }
            DataSet set = new DataSet("StoreStaff");
            set.Tables.Add(t1);
            return set;
        }

        public bool AddStoreRating(String userid, String storeid, double rate)
        {
            string param = string.Format("userid={0}&storeid={1}&rate={2}", userid, storeid,rate);
            return bool.Parse(system.SendApi("AddStoreRating", param));
        }

        public bool addProductRating(String userid, String storeid, String productid, double rate)
        {
            string param = string.Format("userid={0}&storeid={1}&productid={2}&rate={3}", userid, storeid,productid,rate);
            return bool.Parse(system.SendApi("addProductRating", param));
        }

        public DataSet getAllStores()
        {
            string param = "";
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getAllStores", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("storeId");
            t1.Columns.Add("StoreName");
            t1.Columns.Add("StoreFounder");
            t1.Columns.Add("Rate");
            t1.Columns.Add("NumbeOfRates");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("stores");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1],getUsernameFromId(jarray[i][2].ToString()), jarray[i][3] , jarray[i][4]);
            }

            DataSet d1 = new DataSet("Stores");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet GetAllProductByStoreIDToDisplay(string storeID)
        {
            string param = string.Format("storeID={0}", storeID);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetAllProductByStoreIDToDisplay", param).ToString());
            DataTable t1 = new DataTable("Products");
            t1.Columns.Add("productId");
            t1.Columns.Add("Name");
            t1.Columns.Add("price");
            t1.Columns.Add("catagory");
            t1.Columns.Add("quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3] , jarray[i][4]);
            }

            DataSet d1 = new DataSet("Products");
            d1.Tables.Add(t1);
            return d1;
        }
        
        // get permissions ?

        public String getTotalShoppingCartPrice(string userid)
        {
            string param = string.Format("userid={0}", userid);
            return system.SendApi("getTotalShoppingCartPrice", param);
        }

        public DataSet GetUserPurchaseHistory(string admin, string userid)
        {
            string param = string.Format("admin={0}&userid={1}", admin , userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetUserPurchaseHistory", param).ToString());
            DataTable t1 = new DataTable("ShoppingCart");
            t1.Columns.Add("storeid");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Quantity");

            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }

            DataSet d1 = new DataSet("purchasehistory");
            d1.Tables.Add(t1);
            return d1;
        }

        public string AddSystemAdmin(string admin, string email)
        {
            string param = string.Format("admin={0}&email={1}", admin , email);
            return system.SendApi("AddSystemAdmin", param);
        }

        public string RemoveSystemAdmin(string admin, string email)
        {
            string param = string.Format("admin={0}&email={1}", admin , email);
            return system.SendApi("RemoveSystemAdmin", param);
        }

        public bool ResetSystem(string admin)
        {
            string param = string.Format("admin={0}", admin);
            return bool.Parse(system.SendApi("ResetSystem", param));
        }

        public bool isAdminUser(string userid)
        {
            string param = string.Format("userid={0}", userid);
            return bool.Parse(system.SendApi("isAdminUser", param));
        }

        public bool isRegisteredUser(string userid)
        {
            string param = string.Format("userid={0}", userid);
            return bool.Parse(system.SendApi("isRegisteredUser", param));
        }

        public String getProductId(string storeid , string productname)
        {
            string param = string.Format("storeid={0}&productname={1}", storeid , productname);
            string str = system.SendApi("getProductId", param);
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            return str;
        }

        public DataSet getStoresIManage(string userid)
        {
            string param = string.Format("userid={0}", userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getStoresIManage", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("storeId");
            t1.Columns.Add("StoreName");
            t1.Columns.Add("StoreFounder");
            t1.Columns.Add("Rate");
            t1.Columns.Add("NumbeOfRates");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }

            DataSet d1 = new DataSet("StoresIManage");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getStoresIOwn(string userid)
        {
            string param = string.Format("userid={0}", userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getStoresIOwn", param).ToString());
            DataTable t1 = new DataTable("Stores");
            t1.Columns.Add("storeId");
            t1.Columns.Add("StoreName");
            t1.Columns.Add("StoreFounder");
            t1.Columns.Add("Rate");
            t1.Columns.Add("NumbeOfRates");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }

            DataSet d1 = new DataSet("StoresIOwn");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getAllProductsInSystem()
        {
            string param = "";
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getAllProductsInSystem", param).ToString());
            DataTable t1 = new DataTable("ProductsInSystem");
            t1.Columns.Add("productId");
            t1.Columns.Add("Name");
            t1.Columns.Add("price");
            t1.Columns.Add("catagory");
            t1.Columns.Add("quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
                return new DataSet("products");
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }
            DataSet d1 = new DataSet("ProductsInSystem");
            d1.Tables.Add(t1);
            return d1;
        }

        public String getStoreIdByProductId(string productId)
        {
            string param = string.Format("productId={0}", productId);
            string str = system.SendApi("getStoreIdByProductId", param);
            return str;
        }

        public string getUserIdByUsername(string username)
        {
            string param = string.Format("username={0}", username);
            string str = system.SendApi("getUserIdByUsername", param);
            return str;
        }

        public string getUsernameFromId(string userid)
        {
            string param = string.Format("userid={0}", userid);
            string str = system.SendApi("getUsernameFromId", param);
            return str;
        }

        public static string getID(string id)
        {
            return id.Substring(1, 32);
        }
        // offers + policy funcs XD

















        /*
        public DataSet GetAllNotifications(string userId)
        {
            string param = string.Format("userId={0}", userId);
            JArray arr = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetAllNotifications", param).ToString());
            DataTable t1 = new DataTable("Notifications");
            t1.Columns.Add("id");
            t1.Columns.Add("msg");
            if (arr != null)
            {
                for (int i = 0; i < arr.Count && arr[i] != null; i++)
                {
                    try
                    {
                        t1.Rows.Add(i, arr[i]);
                    }
                    catch
                    { }
                }
            }
            DataSet set = new DataSet("Notification");
            set.Tables.Add(t1);
            return set;
        }
        */
    }
}