using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Shops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserHandler a = new UserHandler();
                DataListShops.DataSource = a.getAllStores();
                DataListShops.DataBind();
            }
        }

        protected void DataListproducts_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            UserHandler a = new UserHandler();
            if (e.CommandName.Equals("RateStore"))
            {
                TextBox nametxtbox = (TextBox)(e.Item.FindControl("TextBoxRate"));
                if (nametxtbox.Text.Trim().Length == 0)
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelRateError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Rate Field is Empty";
                    return;
                }
                string str = a.AddStoreRating(Session["userId"].ToString(), e.CommandArgument.ToString(), int.Parse(nametxtbox.Text.ToString()));
                if (str.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelRateError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Rate Updated";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelRateError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str;
                }
            }
        }
    }
}