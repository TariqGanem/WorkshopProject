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

        public string Purchase(string userName, string card_number, string month, string year,
            string holder, string ccv, string id, string name, string address, string city, string country, string zip)
        {
            string param = string.Format("userName={0}&card_number={1}&month={2}&year={3}&holder={4}" +
                "&ccv={5}&id={6}&name={7}&address={8}&city={9}&country={10}&zip={11}", userName, card_number, month, year, holder, ccv, id, name, address, city, country, zip);
            return system.SendApi("Purchase", param);
        }

        public string UpdateCart(string userId, string storeId, string productId, int newAmount) 
        {
            string param = string.Format("userId={0}&storeId={1}&productId={2}&newAmount={3}", userId, storeId, productId, newAmount);
            return system.SendApi("UpdateCart", param);
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
            for (int i = 0; i < notis.Length; i++)
            {
                try
                {
                    t1.Rows.Add(i, notis[i].Replace(Environment.NewLine, "<BR />")); // test
                }
                catch
                { }
            }
            DataSet set = new DataSet("Notification");
            set.Tables.Add(t1);
            return set;
        }

        public string AddProductToCart(string userId, string productId, int quantity, string storeId) 
        {
            string param = string.Format("userId={0}&productId={1}&quantity={2}&storeId={3}", userId, productId, quantity, storeId);
            return system.SendApi("AddProductToCart", param);
        }

        public string RemoveProductFromCart(string userId, string storeId, string productId) 
        {
            string param = string.Format("userId={0}&storeId={1}&productId={2}", userId, storeId, productId);
            return system.SendApi("RemoveProductFromCart", param);
        }

        public string AddProductToStore(string userId, string storeId, string productName, int price, int quantity,
            string category)
        {
            string param = string.Format("userId={0}&storeId={1}&productName={2}&price={3}&quantity={4}&category={5}", userId, storeId, productName, price, quantity, category);
            return system.SendApi("AddProductToStore", param);
        }

        public string RemoveProductFromStore(String userID, String storeID, String productID) 
        {
            string param = string.Format("userID={0}&storeID={1}&productID={2}", userID, storeID, productID);
            return system.SendApi("RemoveProductFromStore", param);
        }

        public String OpenShop(string storeName, string userId)
        {
            string param = string.Format("storeName={0}&userId={1}", storeName, userId);
            return system.SendApi("OpenShop", param);
        }

        public string CloseShop(string storeId, string userId) 
        {
            string param = string.Format("storeId={0}&userId={1}", storeId, userId);
            return system.SendApi("CloseShop", param);
        }

        public string makeNewOwner(string newOwnerId, string currentOwnerId, string storeId) 
        {
            string param = string.Format("newOwnerId={0}&currentOwnerId={1}&storeId={2}", newOwnerId, currentOwnerId, storeId);
            return system.SendApi("makeNewOwner", param);
        }

        public string AddStoreManager(string storeId, string currentOwnerId, string newManagerId) 
        {
            string param = string.Format("storeId={0}&currentOwnerId={1}&newManagerId={2}", storeId, currentOwnerId, newManagerId);
            return system.SendApi("AddStoreManager", param);
        }

        public string removeManager(string currentOwnerId, string storeId, string ManagerToRemove)
        {
            string param = string.Format("currentOwnerId={0}&storeId={1}&ManagerToRemove={2}", currentOwnerId, storeId, ManagerToRemove);
            return system.SendApi("removeManager", param);
        }

        public string removeOwner(string currentOwnerId, string storeId, string OwnerToRemove) 
        {
            string param = string.Format("currentOwnerId={0}&storeId={1}&OwnerToRemove={2}", currentOwnerId, storeId, OwnerToRemove);
            return system.SendApi("removeOwner", param);
        }

        public bool isStoreOwner(string userid, string storeid) 
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
            t1.Columns.Add("quantity");
            t1.Columns.Add("category");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("products");
                d2.Tables.Add(t1);
                return d2;
            }
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
            {
                DataSet d2 = new DataSet("Stores");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
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
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("shoppingcart");
            d1.Tables.Add(t1);
            return d1;
        }

        public string getStoreIdByStoreName(string storename)
        {
            string param = string.Format("storename={0}", storename);
            return system.SendApi("getStoreIdByStoreName", param);
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
            {
                DataSet d2 = new DataSet("shoppinghistory");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("shoppinghistory");
            d1.Tables.Add(t1);
            return d1;
        }

        public string reOpenStore(string storeid, string userid) 
        {
            string param = string.Format("storeid={0}&userid={1}", storeid, userid);
            return system.SendApi("reOpenStore", param);
        }

        public string editProductDetail(string userID, string storeID, string productID, 
            string param, string editto)
        {
            string par = string.Format("userID={0}&storeID={1}&productID={2}&param={3}&editto={4}", userID, storeID, productID, param, editto);
            return system.SendApi("editProductDetail", par);
        }

        public string SetPermissions(string storeID, string managerID, string ownerID, String Permissions) 
        {
            string param = string.Format("storeID={0}&managerID={1}&ownerID={2}&Permissions={3}", storeID, managerID, ownerID, Permissions);
            return system.SendApi("SetPermissions", param);
        }

        public string RemovePermissions(string storeID, string managerID, string ownerID, String permissions) 
        {
            string param = string.Format("storeID={0}&managerID={1}&ownerID={2}&permissions={3}", storeID, managerID, ownerID, permissions);
            return system.SendApi("RemovePermissions", param);
        }

        public DataSet GetStoreStaff(string ownerID, string storeID)
        {
            string param = string.Format("ownerID={0}&storeID={1}", ownerID, storeID);
            String str = system.SendApi("GetStoreStaff", param);
            if (str.Substring(0, 6).Equals("Error:"))
                return new DataSet("storestaff");
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            string[] notis = str.Split(',');
            DataTable t1 = new DataTable("StoreStaff");
            t1.Columns.Add("id");
            t1.Columns.Add("Username");
            for (int i = 1; i < notis.Length; i++)
            {
                try
                {
                    notis[i] = notis[i].TrimEnd();
                    notis[i].TrimStart();
                    t1.Rows.Add(notis[i], this.getUsernameFromId(notis[i].Substring(1, 32)));
                }
                catch
                { }
            }
            DataSet set = new DataSet("StoreStaff");
            set.Tables.Add(t1);
            return set;
        }

        public string AddStoreRating(String userid, String storeid, double rate) 
        {
            string param = string.Format("userid={0}&storeid={1}&rate={2}", userid, storeid, rate);
            return system.SendApi("AddStoreRating", param);
        }

        public string addProductRating(String userid, String storeid, String productid, double rate) 
        {
            string param = string.Format("userid={0}&storeid={1}&productid={2}&rate={3}", userid, storeid, productid, rate);
            return system.SendApi("addProductRating", param);
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
            {
                DataSet d2 = new DataSet("Stores");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], getUsernameFromId(jarray[i][2].ToString()), jarray[i][3], jarray[i][4]);
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
            t1.Columns.Add("quantity");
            t1.Columns.Add("category");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("Products");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4]);
            }

            DataSet d1 = new DataSet("Products");
            d1.Tables.Add(t1);
            return d1;
        }


        public String getTotalShoppingCartPrice(string userid)
        {
            string param = string.Format("userid={0}", userid);
            return system.SendApi("getTotalShoppingCartPrice", param);
        }

        public DataSet GetUserPurchaseHistory(string admin, string userid)
        {
            string param = string.Format("admin={0}&userid={1}", admin, userid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetUserPurchaseHistory", param).ToString());
            DataTable t1 = new DataTable("ShoppingCart");
            t1.Columns.Add("storeid");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Quantity");

            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("purchaseHistory");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("purchasehistory");
            d1.Tables.Add(t1);
            return d1;
        }

        public string AddSystemAdmin(string admin, string email)
        {
            string param = string.Format("admin={0}&email={1}", admin, email);
            return system.SendApi("AddSystemAdmin", param);
        }

        public string RemoveSystemAdmin(string admin, string email)
        {
            string param = string.Format("admin={0}&email={1}", admin, email);
            return system.SendApi("RemoveSystemAdmin", param);
        }

        public bool ResetSystem(string admin , string filepath)
        {
            string param = string.Format("admin={0}&filepath={1}", admin, filepath);
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

        public String getProductId(string storeid, string productname)
        {
            string param = string.Format("storeid={0}&productname={1}", storeid, productname);
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
            {
                DataSet d2 = new DataSet("StoresIManage");
                d2.Tables.Add(t1);
                return d2;
            }
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
            {
                DataSet d2 = new DataSet("StoresIOwn");
                d2.Tables.Add(t1);
                return d2;
            }
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
            t1.Columns.Add("quantity");
            t1.Columns.Add("category");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("Products");
                d2.Tables.Add(t1);
                return d2;
            }
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

        public DataSet GetStorePurchaseHistory(string ownerID, string storeID, Boolean isSysAdmin = false)
        {
            string param = string.Format("ownerID={0}&storeID={1}&isSysAdmin={2}", ownerID, storeID, isSysAdmin);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetStorePurchaseHistory", param).ToString());
            DataTable t1 = new DataTable("storehistory");
            t1.Columns.Add("storeid");
            t1.Columns.Add("Name");
            t1.Columns.Add("Price");
            t1.Columns.Add("Quantity");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("storepurchasehistory");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }

            DataSet d1 = new DataSet("storehistory");
            d1.Tables.Add(t1);
            return d1;
        }

        public string BanUser(string userid, string adminid)
        {
            string param = string.Format("userid={0}&adminid={1}", userid, adminid);
            string str = system.SendApi("BanUser", param);
            return str;
        }

        public string CloseStoreAdmin(string storeid)
        {
            string param = string.Format("storeid={0}", storeid);
            string str = system.SendApi("CloseStoreAdmin", param);
            return str;
        }
        // offers + policy funcs XD

        public string SendOfferToStore(string storeID, string userID, string productID, int amount, double price) 
        {
            string param = string.Format("storeID={0}&userID={1}&productID={2}&amount={3}&price={4}", storeID, userID, productID, amount, price);
            return system.SendApi("SendOfferToStore", param);
        }

        
        public string AnswerCounterOffer(string userID, string offerID, bool accepted) 
        {
            string param = string.Format("userID={0}&offerID={1}&accepted={2}",userID,offerID,accepted);
            return system.SendApi("AnswerCounterOffer", param);
        }

        public string SendOfferResponseToUser(string storeID, string ownerID, string userID, string offerID, bool accepted, double counterOffer) 
        {
            string param = string.Format("storeID={0}&ownerID={1}&userID={2}&offerID={3}&accepted={4}&counterOffer={5}", storeID, ownerID, userID,offerID,accepted,counterOffer);
            return system.SendApi("SendOfferResponseToUser", param);
        }

        public DataSet getStoreOffers(string storeID)
        {
            string param = string.Format("storeID={0}", storeID);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getStoreOffers", param).ToString());
            DataTable t1 = new DataTable("storeoffers");
            t1.Columns.Add("OfferId");
            t1.Columns.Add("ProductId");
            t1.Columns.Add("UserId");
            t1.Columns.Add("StoreId");
            t1.Columns.Add("Amount");
            t1.Columns.Add("Price");
            t1.Columns.Add("CounterOfferPrice");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("storeoffers");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4], jarray[i][5], jarray[i][6]);
            }

            DataSet d1 = new DataSet("storeoffers");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getUserOffers(string userId)
        {
            string param = string.Format("userId={0}", userId);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getUserOffers", param).ToString());
            DataTable t1 = new DataTable("useroffers");
            t1.Columns.Add("OfferId");
            t1.Columns.Add("ProductId");
            t1.Columns.Add("UserId");
            t1.Columns.Add("StoreId");
            t1.Columns.Add("Amount");
            t1.Columns.Add("Price");
            t1.Columns.Add("CounterOfferPrice");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("useroffers");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3], jarray[i][4], jarray[i][5], jarray[i][6]);
            }

            DataSet d1 = new DataSet("useroffers");
            d1.Tables.Add(t1);
            return d1;
        }

        // policies
            //Data
        public DataSet getDiscountPolicies(string storeid)
        {
            string param = string.Format("storeid={0}", storeid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getDiscountPolicies", param).ToString());
            DataTable t1 = new DataTable("StoreDiscoutPolicies");
            t1.Columns.Add("DiscountId");
            t1.Columns.Add("Type");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("StoreDiscoutPolicies");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1]);
            }

            DataSet d1 = new DataSet("StoreDiscoutPolicies");
            d1.Tables.Add(t1);
            return d1;
        }

        public DataSet getPruchasePolicies(string storeid)
        {
            string param = string.Format("storeid={0}", storeid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getPruchasePolicies", param).ToString());
            DataTable t1 = new DataTable("StorePurchasePolicies");
            t1.Columns.Add("PurchaseId");
            t1.Columns.Add("Type");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("StorePurchasePolicies");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1]);
            }

            DataSet d1 = new DataSet("StorePurchasePolicies");
            d1.Tables.Add(t1);
            return d1;
        }
            
            // Discount FUNCS
        public string AddDiscountPolicyVisibleDiscount(string storeId, string ExpirationDate, string Percentage, string Target) // fixing
        {
            string param = string.Format("storeId={0}&ExpirationDate={1}&Percentage={2}&Target={3}", storeId, ExpirationDate, Percentage, Target);
            return system.SendApi("AddDiscountPolicyVisibleDiscount", param);
        }

        public string AddDiscountPolicyDiscreetDiscount(string storeId, string DiscountCode)
        {
            string param = string.Format("storeId={0}&DiscountCode={1}", storeId, DiscountCode);
            return system.SendApi("AddDiscountPolicyDiscreetDiscount", param);
        }

        public string AddDiscountPolicyConditionalDiscount(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyConditionalDiscount", param);
        }

        public string AddDiscountPolicyDiscountAddition(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountAddition", param);
        }

        public string AddDiscountPolicyDiscountAnd(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountAnd", param);
        }

        public string AddDiscountPolicyDiscountMax(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountMax", param);
        }

        public string AddDiscountPolicyDiscountMin(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountMin", param);
        }

        public string AddDiscountPolicyDiscountOr(string storeId)
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountOr", param);
        }

        public string AddDiscountPolicyDiscountXor(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddDiscountPolicyDiscountXor", param);
        }

        public string AddDiscountPolicyVisibleDiscount(string storeId, string ExpirationDate, string Percentage, string Target , string id) 
        {
            string param = string.Format("storeId={0}&ExpirationDate={1}&Percentage={2}&Target={3}&id={4}", storeId, ExpirationDate, Percentage, Target,id);
            return system.SendApi("AddDiscountPolicyVisibleDiscount", param);
        }

        public string AddDiscountPolicyDiscreetDiscount(string storeId, string DiscountCode,string id) 
        {
            string param = string.Format("storeId={0}&DiscountCode={1}&id={2}", storeId, DiscountCode,id);
            return system.SendApi("AddDiscountPolicyDiscreetDiscount", param);
        }

        public string AddDiscountPolicyConditionalDiscount(string storeId, string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddDiscountPolicyConditionalDiscount", param);
        }

        public string AddDiscountPolicyDiscountAddition(string storeId, string id)
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddDiscountPolicyDiscountAddition", param);
        }

        public string AddDiscountPolicyDiscountAnd(string storeId, string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("AddDiscountPolicyDiscountAnd", param);
        }

        public string AddDiscountPolicyDiscountMax(string storeId, string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddDiscountPolicyDiscountMax", param);
        }

        public string AddDiscountPolicyDiscountMin(string storeId, string id)
        { 
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("AddDiscountPolicyDiscountMin", param);
        }

        public string AddDiscountPolicyDiscountOr(string storeId, string id)
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddDiscountPolicyDiscountOr", param);
        }

        public string AddDiscountPolicyDiscountXor(string storeId , string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddDiscountPolicyDiscountXor", param);
        }

        public string RemoveDiscountPolicy(string storeId, String id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("RemoveDiscountPolicy", param);
        }

           // Conditions

        public string AddDiscountConditionDiscountConditionAnd(string storeId, String id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("AddDiscountConditionDiscountConditionAnd", param);
        }

        public string AddDiscountConditionDiscountConditionOr(string storeId, String id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("AddDiscountConditionDiscountConditionOr", param);
        }

        public string AddDiscountConditionMaxProductCondition(string storeId, string MaxQuantity, string ProductId, string id)
        {
            string param = string.Format("storeId={0}&MaxQuantity={1}&ProductId={2}&id={3}", storeId, MaxQuantity, ProductId, id);
            return system.SendApi("AddDiscountConditionMaxProductCondition", param);
        }

        public string AddDiscountConditionMinProductCondition(string storeId, string MinQuantity, string ProductId, string id) 
        {
            string param = string.Format("storeId={0}&MinQuantity={1}&ProductId={2}&id={3}", storeId, MinQuantity, ProductId, id);
            return system.SendApi("AddDiscountConditionMinProductCondition", param);
        }

        public string AddDiscountConditionMinBagPriceCondition(string storeId, string MinPrice, string id) 
        {
            string param = string.Format("storeId={0}&MinPrice={1}&id={2}", storeId, MinPrice, id);
            return system.SendApi("AddDiscountConditionMinBagPriceCondition", param);
        }

        public string RemoveDiscountCondition(string storeId, String id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("RemoveDiscountCondition", param);
        }

            // Purchase Funcs

        public string AddPurchasePolicyAndPolicy(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddPurchasePolicyAndPolicy", param);
        }

        public string AddPurchasePolicyOrPolicy(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddPurchasePolicyOrPolicy", param);
        }

        public string AddPurchasePolicyConditionalPolicy(string storeId) 
        {
            string param = string.Format("storeId={0}", storeId);
            return system.SendApi("AddPurchasePolicyConditionalPolicy", param);
        }

        public string AddPurchasePolicyMaxProductPolicy(string storeId, string ProductId, string Max) 
        {
            string param = string.Format("storeId={0}&ProductId={1}&Max={2}", storeId, ProductId, Max);
            return system.SendApi("AddPurchasePolicyMaxProductPolicy", param);
        }

        public string AddPurchasePolicyMinProductPolicy(string storeId, string ProductId, string Min) 
        {
            string param = string.Format("storeId={0}&ProductId={1}&Min={2}", storeId, ProductId, Min);
            return system.SendApi("AddPurchasePolicyMinProductPolicy", param);
        }

        public string AddPurchasePolicyMinAgePolicy(string storeId, string Age) 
        {
            string param = string.Format("storeId={0}&Age={1}", storeId, Age);
            return system.SendApi("AddPurchasePolicyMinAgePolicy", param);
        }

        public string AddPurchasePolicyRestrictedHoursPolicy(string storeId, string StartRestrict, string EndRestrict , string ProductId) 
        {
            string param = string.Format("storeId={0}&StartRestrict={1}&EndRestrict={2}&ProductId={3}", storeId, StartRestrict, EndRestrict, ProductId);
            return system.SendApi("AddPurchasePolicyRestrictedHoursPolicy", param);
        }

        public string AddPurchasePolicyAndPolicy(string storeId , string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddPurchasePolicyAndPolicy", param);
        }

        public string AddPurchasePolicyOrPolicy(string storeId , string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddPurchasePolicyOrPolicy", param);
        }

        public string AddPurchasePolicyConditionalPolicy(string storeId,string id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId,id);
            return system.SendApi("AddPurchasePolicyConditionalPolicy", param);
        }

        public string AddPurchasePolicyMaxProductPolicy(string storeId, string ProductId, string Max, string id) 
        {
            string param = string.Format("storeId={0}&ProductId={1}&Max={2}&id={3}", storeId, ProductId, Max,id);
            return system.SendApi("AddPurchasePolicyMaxProductPolicy", param);
        }

        public string AddPurchasePolicyMinProductPolicy(string storeId, string ProductId, string Min, string id) 
        {
            string param = string.Format("storeId={0}&ProductId={1}&Min={2}&id={3}", storeId, ProductId, Min,id);
            return system.SendApi("AddPurchasePolicyMinProductPolicy", param);
        }

        public string AddPurchasePolicyMinAgePolicy(string storeId, string Age, string id) 
        {
            string param = string.Format("storeId={0}&Age={1}&id={2}", storeId, Age,id);
            return system.SendApi("AddPurchasePolicyMinAgePolicy", param);
        }

        public string AddPurchasePolicyRestrictedHoursPolicy(string storeId, string StartRestrict, string EndRestrict, string ProductId, string id)
        { 
            string param = string.Format("storeId={0}&StartRestrict={1}&EndRestrict={2}&ProductId={3}&id={4}", storeId, StartRestrict, EndRestrict, ProductId,id);
            return system.SendApi("AddPurchasePolicyRestrictedHoursPolicy", param);
        }

        public string RemovePurchasePolicy(string storeId, String id) 
        {
            string param = string.Format("storeId={0}&id={1}", storeId, id);
            return system.SendApi("RemovePurchasePolicy", param);
        }

        // ---- policies done

        // owner req
        public string  SendOwnerApp(string storeID, string owner, string appointee) 
        {
            string param = string.Format("storeID={0}&owner={1}&appointee={2}", storeID, owner,appointee);
            return system.SendApi("SendOwnerApp", param);
        }

        public string SendOwnerRequestResponseToUser(string storeID, string ownerID, string offerID, bool accepted) 
        {
            string param = string.Format("storeID={0}&ownerID={1}&offerID={2}&accepted={3}", storeID, ownerID, offerID,accepted);
            return system.SendApi("SendOwnerRequestResponseToUser", param);
        }

        public DataSet getOwnerRequests(string storeid)
        {
            string param = string.Format("storeid={0}", storeid);
            JArray jarray = (JArray)JsonConvert.DeserializeObject(system.SendApi("getOwnerRequests", param).ToString());
            DataTable t1 = new DataTable("StoreOwnerReqs");
            t1.Columns.Add("ReqId");
            t1.Columns.Add("FutureOwnerId");
            t1.Columns.Add("StoreId");
            t1.Columns.Add("AppointedBy");
            if (jarray[0][0].ToString().Substring(0, 6).Equals("Error:"))
            {
                DataSet d2 = new DataSet("StoreOwnerReqs");
                d2.Tables.Add(t1);
                return d2;
            }
            for (int i = 1; i < jarray.Count; i++)
            {
                t1.Rows.Add(jarray[i][0], jarray[i][1], jarray[i][2], jarray[i][3]);
            }
            DataSet d1 = new DataSet("StoreOwnerReqs");
            d1.Tables.Add(t1);
            return d1;
        }
        // --- 

        // system info
        public DataSet getUsersVisitsInDate(string date)
        {
            string param = string.Format("date={0}", date);
            string str = system.SendApi("getUsersVisitsInDate", param);
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            string[] notis = str.Split(',');
            DataTable t1 = new DataTable("UserVisits");
            t1.Columns.Add("Date");
            t1.Columns.Add("Regs");
            t1.Columns.Add("Guests");
            t1.Columns.Add("Managers");
            t1.Columns.Add("Owners");
            t1.Columns.Add("Admins");
            Console.Out.WriteLine(notis[0]);
            if (notis[0].Substring(1,6) == "Error:")
            {
                DataSet d2 = new DataSet("UserVisits");
                d2.Tables.Add(t1);
                return d2;
            }
            
            t1.Rows.Add(notis[0],notis[1],notis[2],notis[3],notis[4],notis[5]);
            DataSet set = new DataSet("UserVisits");
            set.Tables.Add(t1);
            return set;
        }

        public DataSet getUsersVisitsInDateChart(string date)
        {
            string param = string.Format("date={0}", date);
            string str = system.SendApi("getUsersVisitsInDate", param);
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            string[] notis = str.Split(',');
            DataTable t1 = new DataTable("UserVisits");
            t1.Columns.Add("Regs");
            t1.Columns.Add("Guests");
            t1.Columns.Add("Managers");
            t1.Columns.Add("Owners");
            t1.Columns.Add("Admins");
            Console.Out.WriteLine(notis[0]);
            if (notis[0].Substring(1, 6) == "Error:")
            {
                DataSet d2 = new DataSet("UserVisits");
                d2.Tables.Add(t1);
                return d2;
            }

            t1.Rows.Add(notis[1], notis[2], notis[3], notis[4], notis[5]);
            DataSet set = new DataSet("UserVisits");
            set.Tables.Add(t1);
            return set;
        }
















    }
}