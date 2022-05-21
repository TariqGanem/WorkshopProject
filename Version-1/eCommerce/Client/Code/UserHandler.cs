﻿using Newtonsoft.Json;
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

        public string Register(string username, string password)
        {
            string param = string.Format("username={0}&password={1}", username, password);
            return (system.SendApi("Register", param));
        }

        public double GetTotalCart(string userName)
        {
            string param = string.Format("userName={0}", userName);
            return double.Parse(system.SendApi("GetTotalCart", param));
        }

        public string Login(string username, string password)
        {
            string param = string.Format("username={0}&password={1}", username, password);
            return (system.SendApi("Login", param));
        }

        public string GuestLogin()
        {
            string param = "";
            return long.Parse(system.SendApi("GuestLogin", param));
        }

        public bool Logout(string userName)
        {
            string param = string.Format("username={0}", userName);
            return bool.Parse(system.SendApi("Logout", param));
        }

        public bool IsOwner(string storeName, string ownerName)
        {
            string param = string.Format("storeName={0}&ownerName={1}", storeName, ownerName);
            return bool.Parse(system.SendApi("IsOwner", param));
        }

        public string MakeNewManger(string storeName, string apointerid, string apointeeid, int permissions)
        {
            string param = string.Format("storeName={0}&apointerid={1}&apointeeid={2}&permissions={3}", storeName, apointerid, apointeeid, permissions);
            return (system.SendApi("MakeNewManger", param));
        }

        public string MakeNewOwner(string storeName, string apointerid, string apointeeid, int permissions)
        {
            string param = string.Format("storeName={0}&apointerid={1}&apointeeid={2}&permissions={3}", storeName, apointerid, apointeeid, permissions);
            return (system.SendApi("MakeNewOwner", param));
        }

        public string removeOwner(string apointerid, string storeName, string apointeeid)
        {
            string param = string.Format("apointerid={0}&storeName={1}&apointeeid={2}", apointerid, storeName, apointeeid);
            return (system.SendApi("removeOwner", param));

        }

        public string removeManager(string apointerid, string storeName, string apointeeid)
        {
            string param = string.Format("apointerid={0}&storeName={1}&apointeeid={2}", apointerid, storeName, apointeeid);
            return (system.SendApi("removeManager", param));

        }

        public DataSet GetAllNotifications(string userName)
        {
            string param = string.Format("userName={0}", userName);
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

        public DataSet GetAllUserNotificationsoffer(string userName)
        {
            string param = string.Format("userName={0}", userName);
            JArray arr = (JArray)JsonConvert.DeserializeObject(system.SendApi("GetAllUserNotificationsoffer", param).ToString());
            DataTable t1 = new DataTable("Notifications");
            t1.Columns.Add("id");
            t1.Columns.Add("Offer");
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