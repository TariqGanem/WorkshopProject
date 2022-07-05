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
    public partial class AddDiscountPolicy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // edit permissions + add/remove policies + storestaff
            if (!Page.IsPostBack)
            {
                Label1.Text = "Select which policy you want to add";
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text == "Select")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;
            }

            if (DropDownList1.SelectedItem.Text == "Visible Discount") {

                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = true;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Discreet Discount")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = true;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Conditional Discount")
            {

                ConditionalDiscount.Visible = true;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Addition Discount")
            {

                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = true;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;


            }
            if (DropDownList1.SelectedItem.Text == "And Discount")
            {

                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = true;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Max Discount")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = true;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Min Discount")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = true;
                OrDiscount.Visible = false;
                XorDiscount.Visible = false;
            }
            if (DropDownList1.SelectedItem.Text == "Or Discount")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = true;
                XorDiscount.Visible = false;
            }
            if (DropDownList1.SelectedItem.Text == "Xor Discount")
            {
                ConditionalDiscount.Visible = false;
                VisibleDiscount.Visible = false;
                DiscreetDiscount.Visible = false;
                AdditionDiscount.Visible = false;
                AndDiscount.Visible = false;
                MaxDiscount.Visible = false;
                MinDiscount.Visible = false;
                OrDiscount.Visible = false;
                XorDiscount.Visible = true;
            }
        }

        protected void ButtonAConditional_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyConditionalDiscount(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                ConditionalLabel.Visible = true;
                ConditionalLabel.Text = "Discount Added";
                return;
            }
            else
            {
                ConditionalLabel.Visible = true;
                ConditionalLabel.Text = str;
                return;
            }
        }

        protected void ButtonAnd_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountAnd(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                AndLabel.Visible = true;
                AndLabel.Text = "Discount Added";
                return;
            }
            else
            {
                AndLabel.Visible = true;
                AndLabel.Text = str;
                return;
            }
        }

        protected void ButtonAddition_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountAddition(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                additionlabel.Visible = true;
                additionlabel.Text = "Discount Added";
                return;
            }
            else
            {
                additionlabel.Visible = true;
                additionlabel.Text = str;
                return;
            }
        }

        protected void ButtonMax_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountMax(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                MaxLabel.Visible = true;
                MaxLabel.Text = "Discount Added";
                return;
            }
            else
            {
                MaxLabel.Visible = true;
                MaxLabel.Text = str;
                return;
            }
        }

        protected void ButtonMin_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountMin(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                MinLabel.Visible = true;
                MinLabel.Text = "Discount Added";
                return;
            }
            else
            {
                MinLabel.Visible = true;
                MinLabel.Text = str;
                return;
            }
        }

        protected void ButtonOr_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountOr(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                OrLabel.Visible = true;
                OrLabel.Text = "Discount Added";
                return;
            }
            else
            {
                OrLabel.Visible = true;
                OrLabel.Text = str;
                return;
            }
        }

        protected void ButtonXor_Click(object sender, EventArgs e)
        {
            string str = new UserHandler().AddDiscountPolicyDiscountXor(Session["storeId"].ToString(), Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                XorLabel.Visible = true;
                XorLabel.Text = "Discount Added";
                return;
            }
            else
            {
                XorLabel.Visible = true;
                XorLabel.Text = str;
                return;
            }
        }

        protected void ButtonDiscreet_Click(object sender, EventArgs e)
        {
            if(DisCodeBox.Text.Trim().Length == 0)
            {
                DiscreetLabel.Visible = true;
                DiscreetLabel.Text = "Code Field is Empty";
                return;
            }
            string str = new UserHandler().AddDiscountPolicyDiscreetDiscount(Session["storeId"].ToString(), DisCodeBox.Text, Session["DisId"].ToString());
            if (str.Substring(1,6) != "Error:")
            {
                DiscreetLabel.Visible = true;
                DiscreetLabel.Text = "Discount Added";
                return;
            }
            else
            {
                DiscreetLabel.Visible = true;
                DiscreetLabel.Text = str;
                return;
            }
        }

        protected void ButtonVisibleDis_Click(object sender, EventArgs e)
        {
            string str = "";
            if (ExpirationDateBox.Text.Trim().Length == 0 | PercentageBox.Text.Trim().Length == 0 | TargetTypeBox.Text.Trim().Length == 0 )
            {
                VisibleLabel.Visible = true;
                VisibleLabel.Text = "One Or More Required Fields are empty";
                return;
            }
            if(!DateTime.TryParse(ExpirationDateBox.Text, out _))
            {
                VisibleLabel.Visible = true;
                VisibleLabel.Text = "Wrong Expiration Date Format [yyyy-mm-dd]";
                return;
            }
            if(!int.TryParse(PercentageBox.Text,out _))
            {
                VisibleLabel.Visible = true;
                VisibleLabel.Text = "Percentage field is not a number";
                return;
            }
            string type = TargetTypeBox.Text.Trim();
            if (type != "Shop" & type != "Products" & type != "Categories" )
            {
                VisibleLabel.Visible = true;
                VisibleLabel.Text = "Illegal Target Type";
                return;
            }
            if (type == "Products" | type == "Categories")
            {
                if(TargetParamsBox.Text.Trim().Length == 0)
                {
                    VisibleLabel.Visible = true;
                    VisibleLabel.Text = "Target Parameter field can't be empty for given target type";
                    return;
                }
            }
            if (type == "Shop")
            {
                str = new UserHandler().AddDiscountPolicyVisibleDiscount(Session["storeId"].ToString(), ExpirationDateBox.Text, PercentageBox.Text, type + "|", Session["DisId"].ToString());
                if (str.Substring(1,6) != "Error:")
                {
                    VisibleLabel.Visible = true;
                    VisibleLabel.Text = "Discount Added";
                    return;
                }
                else
                {
                    DiscreetLabel.Visible = true;
                    DiscreetLabel.Text = str;
                    return;
                }
            }
            else if(type == "Products")
            {
                str = new UserHandler().AddDiscountPolicyVisibleDiscount(Session["storeId"].ToString(), ExpirationDateBox.Text, PercentageBox.Text, type + "|ProductIds:" + TargetParamsBox.Text, Session["DisId"].ToString());
                if (str.Substring(1,6) != "Error:")
                {
                    VisibleLabel.Visible = true;
                    VisibleLabel.Text = "Discount Added";
                    return;
                }
                else
                {
                    DiscreetLabel.Visible = true;
                    DiscreetLabel.Text = str;
                    return;
                }
            }
            else if (type == "Categories")
            {
                str = new UserHandler().AddDiscountPolicyVisibleDiscount(Session["storeId"].ToString(), ExpirationDateBox.Text, PercentageBox.Text, type + "|Categories:" + TargetParamsBox.Text, Session["DisId"].ToString());
                if (str.Substring(1,6) != "Error:")
                {
                    VisibleLabel.Visible = true;
                    VisibleLabel.Text = "Discount Added";
                    return;
                }
                else
                {
                    DiscreetLabel.Visible = true;
                    DiscreetLabel.Text = str;
                    return;
                }
            }
        }
    }
}