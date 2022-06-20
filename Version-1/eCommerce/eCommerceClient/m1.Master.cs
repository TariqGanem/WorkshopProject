using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Client.Code;
using WebSocketSharp;

namespace Client
{
    public partial class m1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHistoryBtn.Visible = false;
            ShoppingHistory.Visible = false;
            ResetSystem.Visible = false;
            txtAdmin.Visible = false;
            btnRemoveAdmin.Visible = false;
            btnAddAdmin.Visible = false;
            LabelPasword.Visible = false;
            LabelUsername.Visible = false;
            Labelname.Visible = true;
            OpenShop.Visible = false;
            MyShops.Visible = false;
            Notifications.Visible = false;
            StoreHistory.Visible = false;
            ButtonCloseStore.Visible = false;
            ButtonBanUser.Visible = false;
            UserOfferBtn.Visible = false;
            InitFilePath.Visible = false;


            if (Session["isLogin"] != null && new UserHandler().isAdminUser(Session["userId"].ToString()))
            {

                OpenShop.Visible = true;
                Login_table.Visible = false;
                ButtonLogOut.Visible = true;
                MyShops.Visible = true;
                Notifications.Visible = true;
                btnAddAdmin.Visible = true;
                btnRemoveAdmin.Visible = true;
                txtAdmin.Visible = true;
                ResetSystem.Visible = true;
                UserHistoryBtn.Visible = true;
                ShoppingHistory.Visible = true;
                StoreHistory.Visible=true;
                ButtonCloseStore.Visible = true;
                ButtonBanUser.Visible = true;
                UserOfferBtn.Visible = true;
                InitFilePath.Visible = true;

            }
            else if(Session["isLogin"] != null)
            {
                OpenShop.Visible = true;
                Login_table.Visible = false;
                ButtonLogOut.Visible = true;
                MyShops.Visible = true;
                Notifications.Visible = true;
                ShoppingHistory.Visible = true;
                UserOfferBtn.Visible = true;

            }
            else if (Session["userId"] == null)
            {
                UserHandler h = new UserHandler();
                Session["userId"] = h.GuestLogin().Substring(1,32);
                Labelname.Text = "Hello Dear Guest";
                Labelname.Visible = true;
                UserOfferBtn.Visible = true;

            }
            else
            {
                Labelname.Text = "Hello Dear Guest";
                Labelname.Visible = true;
                UserOfferBtn.Visible = true;
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if ((txtusername.Text.Trim().Length == 0) || (txtpassword.Text.Trim().Length == 0))
            {
                if (txtusername.Text.Trim().Length == 0)
                {
                    if (txtpassword.Text.Trim().Length == 0) LabelPasword.Visible = true;
                    LabelUsername.Visible = true;
                }

                if (txtpassword.Text.Trim().Length == 0)
                {
                    if (txtusername.Text.Trim().Length == 0) LabelUsername.Visible = true;
                    LabelPasword.Visible = true;
                }
            }
            else
            {
                string msg = new UserHandler().Login(txtusername.Text, txtpassword.Text);
                //Console.WriteLine(msg);
                //Console.WriteLine(msg.Substring(0,6));
                if (!msg.Substring(1,6).Equals("Error:"))
                { 
                    msg = msg.Substring(1,32);
                    ButtonLogOut.Visible = true;
                    Login_table.Visible = false;
                    OpenShop.Visible = true;
                    Notifications.Visible = true;
                    MyShops.Visible = true;
                    Labelname.Visible = true;
                    ShoppingHistory.Visible = true;
                    UserOfferBtn.Visible = true;
                    Session["isLogin"] = "true";
                    Session["username"] = txtusername.Text;
                    Labelname.Text = "Hello " + txtusername.Text;
                    Session["userId"] = msg;
                    Session["admin"] = null;
                    Console.Out.WriteLine(Session["userId"].ToString());
                    if (new UserHandler().isAdminUser(Session["userId"].ToString()))
                    {
                        Session["admin"] = msg;
                        btnAddAdmin.Visible = true;
                        btnRemoveAdmin.Visible = true;
                        txtAdmin.Visible = true;
                        ResetSystem.Visible = true;
                        UserHistoryBtn.Visible = true;
                        StoreHistory.Visible = true;
                        ButtonCloseStore.Visible = true;
                        ButtonBanUser.Visible = true;
                        InitFilePath.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                        "alert(" + msg + ")", true);
                }
            }
        }

        protected void btnRemoveAdmin_OnClick(object sender, EventArgs e)
        {
            if ((txtAdmin.Text.Trim().Length == 0))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                        "alert('You didn't input an admin username')", true);
                return;
            }
            String ret = new UserHandler().RemoveSystemAdmin(Session["userId"].ToString(),txtAdmin.Text);
            if(!ret.Substring(1,6).Equals("Error:"))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert(" + ret + ")", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('Admin Removed Successfully')", true);

        }

        protected void btnAddAdmin_OnClick(object sender, EventArgs e)
        {
            if ((txtAdmin.Text.Trim().Length == 0))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                        "alert('You didn't input an admin username')", true);
                return;
            }
            String ret = new UserHandler().AddSystemAdmin(Session["userId"].ToString(), txtAdmin.Text);
            if (!ret.Substring(1, 6).Equals("Error:"))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert(" + ret + ")", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('Admin Added Successfully')", true);

        }

        protected void ResetSystem_OnClick(object sender, EventArgs e)
        {
            bool res = false;
            if (InitFilePath.Text.Trim().Length == 0)
                res = new UserHandler().ResetSystem(Session["userId"].ToString(),"");
            else
                res = new UserHandler().ResetSystem(Session["userId"].ToString(), InitFilePath.Text);
            if (!res)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('System Ressetted , yet Could not be Initialized by given Init File Path')", true);
            else
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void ShoppingHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShoppingHistory.aspx");
        }

        protected void UserHistoryBtn_OnClick(object sender, EventArgs e)
        {
            if (txtAdmin.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('You didn't input an admin username')", true);
                return;
            }
            UserHandler u = new UserHandler();
            string userid = u.getUserIdByUsername(txtAdmin.Text);
            if (userid.Substring(1, 6) != "Error:")
            {
                Session["useradminhistoryid"] = userid.Substring(1,32);
                Response.Redirect("~/ShoppingHistoryAdmin.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('user is not registered')", true);
        }

        protected void StoryHistoryBtn_OnClick(object sender, EventArgs e)
        {
            if (txtAdmin.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('You didn't input an admin username')", true);
                return;
            }
            UserHandler u = new UserHandler();
            string storeid = u.getStoreIdByStoreName(txtAdmin.Text);
            if(!storeid.Substring(1,6).Equals("Error:"))
            {
                Session["storeIdAdmin"] = storeid.Substring(1,32);
                Response.Redirect("~/StoreShoppingHistoryAdmin.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('store does not exist')", true);
        }

        protected void ButtonCloseStore_OnClick(object sender, EventArgs e)
        {
            if (txtAdmin.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('You didn't input an admin username')", true);
                return;
            }
            UserHandler u = new UserHandler();
            string storeid = u.getStoreIdByStoreName( txtAdmin.Text);
            if (storeid.Substring(1, 6).Equals("Error:"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('store does not exist')", true);
                return;
            }
            string res = u.CloseStoreAdmin(storeid.Substring(1,32));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
            "alert(" + res + " )", true);
        }

        protected void ButtonBanUser_OnClick(object sender, EventArgs e)
        {
            if (txtAdmin.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('You didn't input an admin username')", true);
                return;
            }
            UserHandler u = new UserHandler();
            string userid = u.getUserIdByUsername(txtAdmin.Text);
            if(userid.Substring(1,6).Equals("Error:"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert('user does not exist')", true);
                return;
            }
            string res = u.BanUser(userid.Substring(1,32), Session["userId"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
            "alert(" + res + " )", true);
            /*
            if (res.Substring(1,6).Equals("Error:"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                "alert(" + res + " )", true);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
            "alert(" + res + " )", true);
            */
        }

        protected void ButtonLogOut_Click(object sender, EventArgs e)
        {
            new UserHandler().Logout(Session["userId"].ToString());
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Home.aspx");
        }

        protected void Offers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserOffers.aspx");

        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        protected void Allshops_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/Shops.aspx");
        }

        protected void OpenShop_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Open_shop.aspx");
        }

        protected void Allshops_Click1(object sender, EventArgs e) 
        {
            Response.Redirect("~/Shops.aspx");
        }

        protected void OpenShop_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Open_shop.aspx");
        }

        protected void ImageButtoncart_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }

        protected void MyShops_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyShops.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e) // search button
        {
            if (TextBox2.Text.Trim().Length != 0)
                Response.Redirect("~/Home.aspx?keyword=" + TextBox2.Text.ToString());
        }

        protected void Notifications_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Notifications.aspx");
        }

 
    }
}