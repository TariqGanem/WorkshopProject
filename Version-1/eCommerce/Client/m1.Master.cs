using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class m1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelPasword.Visible = false;
            LabelUsername.Visible = false;
            Labelname.Visible = true;
            OpenShop.Visible = false;
            MyShops.Visible = false;
            Notifications.Visible = false;
            InitSystem.Visible = false;

            if (Session["isLogin"] != null)
            {
                OpenShop.Visible = true;
                Login_table.Visible = false;
                ButtonLogOut.Visible = true;
                MyShops.Visible = true;
                Notifications.Visible = true;
                if (Session["admin"] != null)
                {
                    InitSystem.Visible = true;
                }
                else { InitSystem.Visible = false; }
            }
            else if (Session["username"] == null)
            {
                UserHandler h = new UserHandler();
                Session["username"] = h.GuestLogin().ToString();
                Labelname.Text = "Hello " + Session["username"].ToString();
                Labelname.Visible = true;

            }
            else
            {
                Labelname.Text = "Hello " + Session["username"].ToString();
                Labelname.Visible = true;
            }
        }
    }
}