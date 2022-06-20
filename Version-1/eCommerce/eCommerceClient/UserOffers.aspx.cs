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
                if(!a.AnswerCounterOffer(Session["userId"].ToString(),cargs[1],true))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('Something went wrong - Offer still under acceptance process in the store')", true);
                    return;
                }
            }
            if (e.CommandName == "DeclineCounterOffer")
            {
                if (!a.AnswerCounterOffer(Session["userId"].ToString(), cargs[1], false))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('Something went wrong - Offer still under acceptance process in the store')", true);
                    return;
                }
            }
        }

        protected void DataListproducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}