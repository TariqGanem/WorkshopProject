using Client.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class AddPurchasePolicy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // edit permissions + add/remove policies + storestaff
            if (!Page.IsPostBack)
            {
                Label1.Text = "Select which policy you want to add";
                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text == "Select")
            {
                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;
            }

            if (DropDownList1.SelectedItem.Text == "And Policy") {

                AndPolicy.Visible = true;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Or Policy")
            {
                AndPolicy.Visible = false;
                OrPolicy.Visible = true;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Conditional Policy")
            {

                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = true;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Max Product Policy")
            {

                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = true;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;




            }
            if (DropDownList1.SelectedItem.Text == "Min Product Policy")
            {

                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = true;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Min Age Policy")
            {
                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false ;
                MinAgePolicy.Visible = true;
                RestrictedHoursPolicy.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Restricted Hours Policy")
            {
                AndPolicy.Visible = false;
                OrPolicy.Visible = false;
                ConditionalPolicy.Visible = false;
                MaxProductPolicy.Visible = false;
                MinProudctPolicy.Visible = false;
                MinAgePolicy.Visible = false;
                RestrictedHoursPolicy.Visible = true;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if(new UserHandler().AddPurchasePolicyAndPolicy(Session["storeId"].ToString(),Session["PolicyId"].ToString()))
            {
                AndLabel.Visible = true;
                AndLabel.Text = "Policy Added";
                return;
            }
            else
            {
                AndLabel.Visible = true;
                AndLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonOr_Click(object sender, EventArgs e)
        {
            if (new UserHandler().AddPurchasePolicyOrPolicy(Session["storeId"].ToString(), Session["PolicyId"].ToString()))
            {
                OrLabel.Visible = true;
                OrLabel.Text = "Policy Added";
                return;
            }
            else
            {
                OrLabel.Visible = true;
                OrLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonCond_Click(object sender, EventArgs e)
        {
            if (new UserHandler().AddPurchasePolicyConditionalPolicy(Session["storeId"].ToString(), Session["PolicyId"].ToString()))
            {
                ConditionalLabel.Visible = true;
                ConditionalLabel.Text = "Policy Added";
                return;
            }
            else
            {
                ConditionalLabel.Visible = true;
                ConditionalLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonMaxPro_Click(object sender, EventArgs e)
        {
            if(ProductIdBox.Text.Trim().Length == 0 | MaxBox.Text.Trim().Length == 0)
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "One or more of the fields are empty";
                return;
            }
            if(!int.TryParse(MaxBox.Text,out _))
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "Max Field is not a number";
                return;
            }
            if (new UserHandler().AddPurchasePolicyMaxProductPolicy(Session["storeId"].ToString(), ProductIdBox.Text , MaxBox.Text , Session["PolicyId"].ToString()))
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "Policy Added";
                return;
            }
            else
            {
                MaxProductLabel.Visible = true;
                MaxProductLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonMinPro_Click(object sender, EventArgs e)
        {
            if (ProductIdBoxMin.Text.Trim().Length == 0 | MinBox.Text.Trim().Length == 0)
            {
                MinProductLabel.Visible = true;
                MinProductLabel.Text = "One or more of the fields are empty";
                return;
            }
            if (!int.TryParse(MinBox.Text, out _))
            {
                MinProductLabel.Visible = true;
                MinProductLabel.Text = "Min Field is not a number";
                return;
            }
            if (new UserHandler().AddPurchasePolicyMinProductPolicy(Session["storeId"].ToString(), ProductIdBoxMin.Text, MinBox.Text, Session["PolicyId"].ToString()))
            {
                MinProductLabel.Visible = true;
                MinProductLabel.Text = "Policy Added";
                return;
            }
            else
            {
                MinProductLabel.Visible = true;
                MinProductLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonMinAge_Click(object sender, EventArgs e)
        {
            if (AgeBox.Text.Trim().Length == 0 )
            {
                MinAgeLabel.Visible = true;
                MinAgeLabel.Text = "Age field is empty";
                return;
            }
            if (!int.TryParse(AgeBox.Text, out _))
            {
                MinAgeLabel.Visible = true;
                MinAgeLabel.Text = "Age Field is not a number";
                return;
            }
            if (new UserHandler().AddPurchasePolicyMinAgePolicy(Session["storeId"].ToString(), AgeBox.Text, Session["PolicyId"].ToString()))
            {
                MinAgeLabel.Visible = true;
                MinAgeLabel.Text = "Policy Added";
                return;
            }
            else
            {
                MinAgeLabel.Visible = true;
                MinAgeLabel.Text = "Policy Failed To Add";
                return;
            }
        }

        protected void ButtonRestrictedHours_Click(object sender, EventArgs e)
        {
            if (StartRestrictBox.Text.Trim().Length == 0 | EndRestrictBox.Text.Trim().Length == 0 | ProductIdBoxRes.Text.Trim().Length == 0)
            {
                RestrictedHoursLabel.Visible = true;
                RestrictedHoursLabel.Text = "One or more of the fields are empty";
                return;
            }
            if(!DateTime.TryParse(StartRestrictBox.Text, out _) | !DateTime.TryParse(EndRestrictBox.Text, out _))
            {
                RestrictedHoursLabel.Visible = true;
                RestrictedHoursLabel.Text = "StartRestrict or EndRestrict are not dates";
                return;
            }
            if (new UserHandler().AddPurchasePolicyRestrictedHoursPolicy(Session["storeId"].ToString(),StartRestrictBox.Text , EndRestrictBox.Text, ProductIdBoxRes.Text, Session["PolicyId"].ToString()))
            {
                RestrictedHoursLabel.Visible = true;
                RestrictedHoursLabel.Text = "Policy Added";
                return;
            }
            else
            {
                RestrictedHoursLabel.Visible = true;
                RestrictedHoursLabel.Text = "Policy Failed To Add";
                return;
            }
        }
    }
}