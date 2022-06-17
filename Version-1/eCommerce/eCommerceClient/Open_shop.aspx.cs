using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class Open_shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Labelerror.Text = "Error";
            Labelerror.Visible = false;
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            if (TextBoxShopname.Text.Trim().Length == 0)
            {
                Labelerror.Text = "Error , Missing Store Name";
                Labelerror.Visible = true;
                return;
            }
            UserHandler s = new UserHandler();
            string userId = Session["userId"].ToString();
            string open = s.OpenShop(userId, TextBoxShopname.Text);
            if (open.Substring(1,6).Equals("Error:")) {
                Labelerror.Text = open;
                Labelerror.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('failed to open store!!!')", true);
            }
            Response.Redirect("~/Home.aspx");
        }
    }
}