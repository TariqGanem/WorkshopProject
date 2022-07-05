using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class MyShops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserHandler a = new UserHandler();

                Data_shopIown.DataSource = a.getStoresIOwn(Session["userId"].ToString());
                Data_shopIown.DataBind();
                Data_shopImanage.DataSource = a.getStoresIManage(Session["userId"].ToString());
                Data_shopImanage.DataBind();
            }
        }
        protected void Data_shopIown_Command(object source, DataListCommandEventArgs e)
        {
            
            if (e.CommandName == "editshop")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["storeId"] = cargs[1];
                Response.Redirect("~/EditShop.aspx");
            }
            if (e.CommandName == "Close")
            {
                Session["storeId"] = e.CommandArgument;
                UserHandler sh = new UserHandler();
                string res  = sh.CloseShop(Session["storeId"].ToString(), Session["userId"].ToString());
                Response.Redirect("~/MyShops.aspx");
                if(res.Substring(1,6) == "Error:")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + res + ")", true);
                }
            }
            if (e.CommandName == "reopen")
            {
                Session["storeId"] = e.CommandArgument;
                UserHandler sh = new UserHandler();
                string res = sh.reOpenStore(Session["storeId"].ToString(), Session["userId"].ToString());
                Response.Redirect("~/MyShops.aspx");
                if (res.Substring(1,6) == "Error:")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + res + ")", true);
                }
            }
        }

        protected void Data_shopImanage_Command(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "editshop")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                Session["storeId"] = cargs[1];
                Response.Redirect("~/EditShop.aspx");
            }
        }
    }
}