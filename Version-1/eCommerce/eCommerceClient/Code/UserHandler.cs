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


        //==========================================================
        //login and registering issues
        public string GuestLogin()
        {
            string param = "";

            return system.SendApi("Login", param);
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

        //==========================================================
        public bool MakeNewManger(string storeId, string currentOwnerId, string newManagerId)
        {
            string param = string.Format("storeId={0}&currentOwnerId={1}&newManagerId={2}", storeId, currentOwnerId, newManagerId);
            return Boolean.Parse(system.SendApi("AddStoreManager", param));
        }

        public bool MakeNewOwner(string newOwnerId, string currentOwnerId, string storeId)
        {
            string param = string.Format("newOwnerId={0}&currentOwnerId={1}&storeId={2}", newOwnerId, currentOwnerId, storeId);
            return Boolean.Parse(system.SendApi("makeNewOwner", param));
        }

        public bool removeOwner(string apointerid, string storeName, string apointeeid)
        {
            string param = string.Format("apointerid={0}&storeName={1}&apointeeid={2}", apointerid, storeName, apointeeid);
            return Boolean.Parse(system.SendApi("removeOwner", param));

        }

        public bool removeManager(string currentOwnerId, string storeId, string ManagerToRemove)
        {
            string param = string.Format("currentOwnerId={0}&storeId={1}&ManagerToRemove={2}", currentOwnerId, storeId, ManagerToRemove);
            return Boolean.Parse(system.SendApi("removeManager", param));

        }

        public bool IsOwner(string storeId, string userId)
        {
            string param = string.Format("storeId={0}&userId={1}", storeId, userId);
            return bool.Parse(system.SendApi("IsOwner", param));
        }

        //===========================================================
        public double GetTotalCart(string userName)
        {
            string param = string.Format("userName={0}", userName);
            return double.Parse(system.SendApi("GetTotalCart", param));
        }

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
    }
}