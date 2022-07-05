using Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Client
{
    public partial class UserOffers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserHandler a = new UserHandler();
                DataListOffers.DataSource = a.getUserOffers(Session["userId"].ToString());
                DataListOffers.DataBind();
            }
        }

        protected void DataListOffers_ItemCommand1(object source, DataListCommandEventArgs e)
        {
            UserHandler a = new UserHandler();
            string[] cargs = e.CommandArgument.ToString().Split(',');
            if (e.CommandName == "AcceptCounterOffer")
            {
                string str1 = a.AnswerCounterOffer(Session["userId"].ToString(), cargs[1], true);
                if (str1.Substring(1,6) == "Error:")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert(" + str1 + ")", true);
                    return;
                }
            }
            if (e.CommandName == "DeclineCounterOffer")
            {
                string str2 = a.AnswerCounterOffer(Session["userId"].ToString(), cargs[1], false);
                if (str2.Substring(1,6) == "Error:")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert("+str2+")", true);
                    return;
                }
            }
        }

        protected void DataListproducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}