using Client.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class EditShop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // edit permissions + add/remove policies + storestaff
            if (!Page.IsPostBack)
            {
                Label1.Text = "Select what you want to view/edit";
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.SelectedItem.Text == "Select")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                DataListRemoveProduct.Visible = false;
                StoreStaff.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;

            }

            if (DropDownList1.SelectedItem.Text == "Add New Item") {
                
                table1.Visible = true;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Add New Manager")
            {
                table1.Visible = false;
                table2.Visible = true;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Add New Owner")
            {

                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = true;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Fire Manager")
            {

                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = true;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;




            }
            if (DropDownList1.SelectedItem.Text == "Fire Owner")
            {

                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = true;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;



            }
            if (DropDownList1.SelectedItem.Text == "Edit Product")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = true;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;


                UserHandler a = new UserHandler();
                DataListproducts.DataSource = a.GetAllProductByStoreIDToDisplay(Session["storeId"].ToString());
                DataListproducts.DataBind();
            }
            if (DropDownList1.SelectedItem.Text == "Add Permissions")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = true;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Remove Permissions")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = true;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;

            }
            if (DropDownList1.SelectedItem.Text == "Store Staff")
            {
                UserHandler a = new UserHandler();
                if (a.isStoreOwner(Session["userId"].ToString(),Session["storeId"].ToString()))
                {
                    table1.Visible = false;
                    table2.Visible = false;
                    table3.Visible = false;
                    table4.Visible = false;
                    table5.Visible = false;
                    table6.Visible = false;
                    table7.Visible = false;
                    DataListproducts.Visible = false;
                    StoreStaff.Visible = true;
                    DataListRemoveProduct.Visible = false;
                    DataListsShoppingHistory.Visible = false;
                    DataListOffers.Visible = false;
                    PurchasePolicies.Visible = false;
                    ButtonAddPurchasePolicyToMain.Visible = false;
                    MainDiscountBtn.Visible = false;
                    DataListDiscountPolicies.Visible = false;
                    OwnerRequests.Visible = false;

                    StoreStaff.DataSource = a.GetStoreStaff(Session["userId"].ToString(),Session["storeId"].ToString());
                    StoreStaff.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                        "alert('you don't have permission to view the store staff list')", true);
            }
            if (DropDownList1.SelectedItem.Text == "Remove Item")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = true;
                DataListsShoppingHistory.Visible = false;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;


                UserHandler a = new UserHandler();
                DataListRemoveProduct.DataSource = a.GetAllProductByStoreIDToDisplay(Session["storeId"].ToString());
                DataListRemoveProduct.DataBind();
            }
            if (DropDownList1.SelectedItem.Text == "Store History")
            {
                table1.Visible = false;
                table2.Visible = false;
                table3.Visible = false;
                table4.Visible = false;
                table5.Visible = false;
                table6.Visible = false;
                table7.Visible = false;
                DataListproducts.Visible = false;
                StoreStaff.Visible = false;
                DataListRemoveProduct.Visible = false;
                DataListsShoppingHistory.Visible = true;
                DataListOffers.Visible = false;
                PurchasePolicies.Visible = false;
                ButtonAddPurchasePolicyToMain.Visible = false;
                MainDiscountBtn.Visible = false;
                DataListDiscountPolicies.Visible = false;
                OwnerRequests.Visible = false;


                UserHandler a = new UserHandler();
                DataListsShoppingHistory.DataSource = a.GetStorePurchaseHistory(Session["userId"].ToString(),Session["storeId"].ToString());
                DataListsShoppingHistory.DataBind();
            }
            if (DropDownList1.SelectedItem.Text == "Store Offer Requests")
            {
                UserHandler a = new UserHandler();
                if (a.isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                {
                    table1.Visible = false;
                    table2.Visible = false;
                    table3.Visible = false;
                    table4.Visible = false;
                    table5.Visible = false;
                    table6.Visible = false;
                    table7.Visible = false;
                    DataListproducts.Visible = false;
                    StoreStaff.Visible = false;
                    DataListRemoveProduct.Visible = false;
                    DataListsShoppingHistory.Visible = false;
                    DataListOffers.Visible = true;
                    PurchasePolicies.Visible = false;
                    ButtonAddPurchasePolicyToMain.Visible = false;
                    MainDiscountBtn.Visible = false;
                    DataListDiscountPolicies.Visible = false;
                    OwnerRequests.Visible = false;

                    DataListOffers.DataSource = a.getStoreOffers(Session["storeId"].ToString());
                    DataListOffers.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                       "alert('you don't have permission to view the store staff list')", true);

            }

            if (DropDownList1.SelectedItem.Text == "Pruchase Policies")
            {
                UserHandler a = new UserHandler();
                if (a.isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                {
                    table1.Visible = false;
                    table2.Visible = false;
                    table3.Visible = false;
                    table4.Visible = false;
                    table5.Visible = false;
                    table6.Visible = false;
                    table7.Visible = false;
                    DataListproducts.Visible = false;
                    StoreStaff.Visible = false;
                    DataListRemoveProduct.Visible = false;
                    DataListsShoppingHistory.Visible = false;
                    DataListOffers.Visible = false;
                    PurchasePolicies.Visible = true;
                    ButtonAddPurchasePolicyToMain.Visible = true;
                    MainDiscountBtn.Visible = false;
                    DataListDiscountPolicies.Visible = false;
                    OwnerRequests.Visible = false;

                    PurchasePolicies.DataSource = a.getPruchasePolicies(Session["storeId"].ToString());
                    PurchasePolicies.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                       "alert('you don't have permission to view the store staff list')", true);

            }
            if (DropDownList1.SelectedItem.Text == "Discount Policies")
            {
                UserHandler a = new UserHandler();
                if (a.isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                {
                    table1.Visible = false;
                    table2.Visible = false;
                    table3.Visible = false;
                    table4.Visible = false;
                    table5.Visible = false;
                    table6.Visible = false;
                    table7.Visible = false;
                    DataListproducts.Visible = false;
                    StoreStaff.Visible = false;
                    DataListRemoveProduct.Visible = false;
                    DataListsShoppingHistory.Visible = false;
                    DataListOffers.Visible = false;
                    PurchasePolicies.Visible = false;
                    ButtonAddPurchasePolicyToMain.Visible = false;
                    MainDiscountBtn.Visible = true;
                    DataListDiscountPolicies.Visible = true;
                    OwnerRequests.Visible = false;

                    DataListDiscountPolicies.DataSource = a.getDiscountPolicies(Session["storeId"].ToString());
                    DataListDiscountPolicies.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                       "alert('you don't have permission to view the store staff list')", true);
            }
            if (DropDownList1.SelectedItem.Text == "Owner Requests")
            {
                UserHandler a = new UserHandler();
                if (a.isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                {
                    table1.Visible = false;
                    table2.Visible = false;
                    table3.Visible = false;
                    table4.Visible = false;
                    table5.Visible = false;
                    table6.Visible = false;
                    table7.Visible = false;
                    DataListproducts.Visible = false;
                    StoreStaff.Visible = false;
                    DataListRemoveProduct.Visible = false;
                    DataListsShoppingHistory.Visible = false;
                    DataListOffers.Visible = false;
                    PurchasePolicies.Visible = false;
                    ButtonAddPurchasePolicyToMain.Visible = false;
                    MainDiscountBtn.Visible = false;
                    DataListDiscountPolicies.Visible = false;
                    OwnerRequests.Visible = true;

                    OwnerRequests.DataSource = a.getOwnerRequests(Session["storeId"].ToString());
                    OwnerRequests.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                       "alert('you don't have permission to view the store staff list')", true);
            }

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            if(TextBoxproductName.Text.Trim().Length == 0 | TextBoxprice.Text.Trim().Length == 0 | TextBoxAmount.Text.Trim().Length == 0 | TextBoxcategories.Text.Trim().Length == 0)
            {
                Labelerror1.Visible = true;
                Labelerror1.Text = "Fill all the fields";
                Labelerror1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string s = a.AddProductToStore(Session["userId"].ToString(), Session["storeId"].ToString(), TextBoxproductName.Text.ToString(),
                int.Parse(TextBoxprice.Text.ToString()), int.Parse(TextBoxAmount.Text.ToString()), TextBoxcategories.Text.ToString());

            if (s.Substring(1, 6) == "Error:") 
            {

                Labelerror1.Visible = true;
                Labelerror1.Text = s;
                Labelerror1.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                table1.Visible = false;
                Labelerror1.Text = "The product has been added!";
                Labelerror1.ForeColor = System.Drawing.Color.Green;
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Select"));
            }

        }

        
        protected void Buttonaddmanager_Click(object sender, EventArgs e)
        {
            UserHandler u = new UserHandler();
            if(TextBoxAddManager.Text.Trim().Length == 0)
            {
                Labelerror2.Visible = true;
                Labelerror2.Text = "Field cant be Empty";
                Labelerror2.ForeColor = System.Drawing.Color.Red;
                return;
            }
            String userid = u.getUserIdByUsername(TextBoxAddManager.Text.ToString());
            if(userid.Substring(1,6).Equals("Error:"))
            {
                Labelerror2.Visible = true;
                Labelerror2.Text = userid;
                Labelerror2.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string s = u.AddStoreManager(Session["storeId"].ToString(), Session["userId"].ToString(), userid.Substring(1, 32));
            if (s.Substring(1,6) != "Error:")
            {
                table2.Visible = false;
                table1.Visible = false;
                Labelerror2.Text = "User has been added as a manager";
                Labelerror2.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Labelerror2.Visible = true;
                Labelerror2.Text = s;
            }
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Select"));
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserHandler u = new UserHandler();
            if (TextBoxAddOwner.Text.Trim().Length == 0)
            {
                Labelerror3.Visible = true;
                Labelerror3.Text = "Field cant be Empty";
                Labelerror3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            String userid = u.getUserIdByUsername(TextBoxAddOwner.Text.ToString());
            if (userid.Substring(1, 6).Equals("Error:"))
            {
                Labelerror3.Visible = true;
                Labelerror3.Text = userid;
                Labelerror3.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string str = u.SendOwnerApp(Session["storeId"].ToString(), Session["userId"].ToString(), userid.Substring(1, 32));
            if (str.Substring(1,6) != "Error:")
            {
                table3.Visible = false;
                table3.Visible = false;
                Labelerror3.Text = "Owner App Request Sent";
                Labelerror3.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Labelerror3.Visible = true;
                Labelerror3.Text = str;
            }
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Select"));

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserHandler u = new UserHandler();
            if (TextBoxManagerToRemove.Text.Trim().Length == 0)
            {
                Labelerror4.Visible = true;
                Labelerror4.Text = "Field cant be Empty";
                Labelerror4.ForeColor = System.Drawing.Color.Red;
                return;
            }
            String userid = u.getUserIdByUsername(TextBoxManagerToRemove.Text.ToString());
            if (userid.Substring(1, 6).Equals("Error:"))
            {
                Labelerror4.Visible = true;
                Labelerror4.Text = userid;
                Labelerror4.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string s = u.removeManager(Session["userId"].ToString(), Session["storeId"].ToString(), userid.Substring(1, 32));
            if (s.Substring(1,6) != "Error:")
            {
                table4.Visible = false;
                table4.Visible = false;
                Labelerror4.Text = "manager has been removed";
                Labelerror4.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Labelerror4.Visible = true;
                Labelerror4.Text = s;
            }
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Select"));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            UserHandler u = new UserHandler();
            if (TextBoxOwnerToRemove.Text.Trim().Length == 0)
            {
                Labelerror5.Visible = true;
                Labelerror5.Text = "Field cant be Empty";
                Labelerror5.ForeColor = System.Drawing.Color.Red;
                return;
            }
            String userid = u.getUserIdByUsername(TextBoxOwnerToRemove.Text.ToString());
            if (userid.Substring(1, 6).Equals("Error:"))
            {
                Labelerror5.Visible = true;
                Labelerror5.Text = userid;
                Labelerror5.ForeColor = System.Drawing.Color.Red;
                return;
            }
            string s = u.removeOwner(Session["userId"].ToString(), Session["storeId"].ToString(), userid.Substring(1, 32));
            if (s.Substring(1,6) != "Error:")
            {
                table5.Visible = false;
                table5.Visible = false;
                Labelerror5.Text = "owner has been removed";
                Labelerror5.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Labelerror5.Visible = true;
                Labelerror5.Text = s;
            }
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Select"));
        }

        protected void DataListproducts_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            UserHandler a = new UserHandler();
            if(e.CommandName.Equals("editNameProduct"))
            {
                TextBox nametxtbox = (TextBox)(e.Item.FindControl("txtBoxEditName"));
                if (nametxtbox.Text.Trim().Length == 0)
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Name Field is Empty";
                    return;
                }
                string res = a.editProductDetail(Session["userId"].ToString(), Session["storeId"].ToString(), e.CommandArgument.ToString(), "Name", nametxtbox.Text.ToString());
                if (res.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Name Updated";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = res;
                }
            }
            if(e.CommandName.Equals("editPriceProduct"))
            {
                TextBox nametxtbox = (TextBox)(e.Item.FindControl("TextBoxEditPrice"));
                if (nametxtbox.Text.Trim().Length == 0)
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Price Field is Empty";
                    return;
                }
                string str2 = a.editProductDetail(Session["userId"].ToString(), Session["storeId"].ToString(), e.CommandArgument.ToString(), "Price", nametxtbox.Text.ToString());
                if (str2.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Price Updated";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str2;
                }
            }
            if(e.CommandName.Equals("editCategoryProduct"))
            {
                TextBox nametxtbox = (TextBox)(e.Item.FindControl("TextBoxEditCategory"));
                if (nametxtbox.Text.Trim().Length == 0)
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Category Field is Empty";
                    return;
                }
                string str3 = a.editProductDetail(Session["userId"].ToString(), Session["storeId"].ToString(), e.CommandArgument.ToString(), "Category", nametxtbox.Text.ToString());
                if (str3.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Category Updated";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str3;
                }
            }
            if(e.CommandName.Equals("editQuantityProduct"))
            {
                TextBox nametxtbox = (TextBox)(e.Item.FindControl("TextBoxEditQuantity"));
                if (nametxtbox.Text.Trim().Length == 0)
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Quantity Field is Empty";
                    return;
                }
                string str4 = a.editProductDetail(Session["userId"].ToString(), Session["storeId"].ToString(), e.CommandArgument.ToString(), "Quantity", nametxtbox.Text.ToString());
                if (str4.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Quantity Updated";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelEditProductError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str4;
                }
            }
        }

        protected void ButtonAddPermission_Click(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            if (TextBoxUsernameAddPerm.Text.Trim().Length == 0 | TextBoxPermissionsToSet.Text.Trim().Length == 0 )
            {
                LabelErrorAddPerm.Visible = true;
                LabelErrorAddPerm.Text = "One of the field are empty";
                return;
            }
            String[] temp = TextBoxPermissionsToSet.Text.ToString().Split(',');
            bool goodFormat = true;
           foreach(String s in temp)
            {
                if (int.TryParse(s, out int num))
                {
                    if(!((num >= 0 && num <= 12) | num == 777))
                    {
                        goodFormat = false;
                        break;
                    }
                }
                else
                {
                    goodFormat = false;
                    break;
                }
            }
           if(!goodFormat)
            {
                LabelErrorAddPerm.Visible = true;
                LabelErrorAddPerm.Text = "Permissions Format is not good";
                return;
            }
            String userid = a.getUserIdByUsername(TextBoxUsernameAddPerm.Text.ToString());
            if(userid.Substring(1,6)=="Error:")
            {
                LabelErrorAddPerm.Visible = true;
                LabelErrorAddPerm.Text = "Username is not a registered User";
                return;
            }
            userid = userid.Substring(1, 32);
            string str = a.SetPermissions(Session["storeId"].ToString(), userid, Session["userId"].ToString(), TextBoxPermissionsToSet.Text.ToString());
           if (str.Substring(1,6) != "Error:")
            {
                LabelErrorAddPerm.Visible = true;
                LabelErrorAddPerm.Text = "Permissions Updated";
                return;
            }
           else
            {
                LabelErrorAddPerm.Visible = true;
                LabelErrorAddPerm.Text = str;
                return;
            }
        }

        protected void ButtonRemovePerm_Click(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            if (TextBoxRemoveUsernamePerm.Text.Trim().Length == 0 | TextBoxPermToRem.Text.Trim().Length == 0)
            {
                LabelErrorRemovePerm.Visible = true;
                LabelErrorRemovePerm.Text = "One of the field are empty";
                return;
            }
            String[] temp = TextBoxPermToRem.Text.ToString().Split(',');
            bool goodFormat = true;
            foreach (String s in temp)
            {
                if (int.TryParse(s, out int num))
                {
                    if (!((num >= 0 && num <= 11) | num == 777))
                    {
                        goodFormat = false;
                        break;
                    }
                }
                else
                {
                    goodFormat = false;
                    break;
                }
            }
            if (!goodFormat)
            {
                LabelErrorRemovePerm.Visible = true;
                LabelErrorRemovePerm.Text = "Permissions Format is not good";
                return;
            }
            String userid = a.getUserIdByUsername(TextBoxRemoveUsernamePerm.Text.ToString());
            if (userid.Substring(1, 6) == "Error:")
            {
                LabelErrorRemovePerm.Visible = true;
                LabelErrorRemovePerm.Text = "Username is not a registered User";
                return;
            }
            userid = userid.Substring(1, 32);
            string str2 = a.SetPermissions(Session["storeId"].ToString(), userid, Session["userId"].ToString(), TextBoxPermToRem.Text.ToString());
            if (str2.Substring(1,6) != "Error:")
            {
                LabelErrorRemovePerm.Visible = true;
                LabelErrorRemovePerm.Text = "Permissions Updated";
                return;
            }
            else
            {
                LabelErrorRemovePerm.Visible = true;
                LabelErrorRemovePerm.Text = str2;
                return;
            }

        }

        protected void DataListRemoveProduct_ItemCommand1(object sender, DataListCommandEventArgs e)
        {
            if(e.CommandName.Equals("Remove_Product"))
            {
                string productid = e.CommandArgument.ToString();
                new UserHandler().RemoveProductFromStore(Session["userId"].ToString(), Session["storeId"].ToString(), productid);
                Response.Redirect("~/EditShop.aspx");
            }
        }

        protected void DataListOffers_ItemCommand1(object sender, DataListCommandEventArgs e)
        {
            UserHandler a = new UserHandler();

            if(e.CommandName == "AcceptOffer")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                string str1 = a.SendOfferResponseToUser(Session["storeId"].ToString(), Session["userId"].ToString(), cargs[0].ToString(), cargs[1].ToString(), true, -1);
                if (str1.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Response Sent!";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str1;
                }
            }
            if (e.CommandName == "DeclineOffer")
            {
                string[] cargs = e.CommandArgument.ToString().Split(',');
                string str2 = a.SendOfferResponseToUser(Session["storeId"].ToString(), Session["userId"].ToString(), cargs[0].ToString(), cargs[1].ToString(), false, -1);
                if (str2.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Response Sent!";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str2;
                }
            }
            if (e.CommandName == "SendCounterOffer")
            {
                TextBox counterofferbox = (TextBox)(e.Item.FindControl("CounterOffer"));
                string counteroffer = counterofferbox.Text;
                if (counteroffer.Trim().Length == 0 || !int.TryParse(counteroffer,out int val ) || val < 0 )
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Counter Offer Field is not legal";
                    return;
                }

                string[] cargs = e.CommandArgument.ToString().Split(',');
                string str3 = a.SendOfferResponseToUser(Session["storeId"].ToString(), Session["userId"].ToString(), cargs[0].ToString(), cargs[1].ToString(), false, int.Parse(counteroffer));
                if (str3.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Response <Counter Offer> Sent!";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelOfferError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str3;
                }
            }

        }

        protected void DataListPurchasePolicies_ItemCommand1(object sender, DataListCommandEventArgs e)
        {
            if (!(new UserHandler().isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString())))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('you don't have permission to view the store staff list')", true);

            if(e.CommandName.Equals("AddPolicy"))
            {
                Session["PolicyId"] = e.CommandArgument.ToString();
                Response.Redirect("~/AddPruchasePolicy.aspx");
                return;
            }
            if(e.CommandName.Equals("RemovePolicy"))
            {
                string str = new UserHandler().RemovePurchasePolicy(Session["storeId"].ToString(), e.CommandArgument.ToString());
                if (str.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelPolicyError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Policy Removed";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelPolicyError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str;
                }
            }
        }

        protected void ButtonAddPurchasePolicyToMain_Click(object sender, EventArgs e)
        {
            if (new UserHandler().isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                Response.Redirect("~/AddPruchasePolicyMain.aspx");
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('you don't have permission to view the store staff list')", true);
        }

        protected void DataListDiscountPolicies_ItemCommand1(object sender, DataListCommandEventArgs e)
        {
            if (!(new UserHandler().isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString())))
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('you don't have permission to view the store staff list')", true);

            if (e.CommandName.Equals("AddDiscountPolicy"))
            {
                Session["DisId"] = e.CommandArgument.ToString();
                Response.Redirect("~/AddDiscountPolicy.aspx");
                return;
            }
            if (e.CommandName.Equals("RemoveDiscountPolicy"))
            {
                string str = new UserHandler().RemoveDiscountPolicy(Session["storeId"].ToString(), e.CommandArgument.ToString());
                if (str.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelDiscountError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Discount Removed";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("LabelDiscountError"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str;
                }
                return;
            }
            if (e.CommandName.Equals("AddDiscountCondition"))
            {
                Session["DisId"] = e.CommandArgument.ToString();
                Response.Redirect("~/AddDiscountCondition.aspx");
                return;
            }
        }

        protected void MainDiscountBtn_Click(object sender, EventArgs e)
        {
            if (new UserHandler().isStoreOwner(Session["userId"].ToString(), Session["storeId"].ToString()))
                Response.Redirect("~/AddDiscountPolicyToMain.aspx");
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert",
                    "alert('you don't have permission to view the store staff list')", true);

        }

        protected void OwnerRequests_ItemCommand1(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("AcceptOwner"))
            {
                string str = new UserHandler().SendOwnerRequestResponseToUser(Session["storeId"].ToString(), Session["userId"].ToString(), e.CommandArgument.ToString(), true);
                if (str.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("OwnerReqErrorLabel"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Request Accepted";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("OwnerReqErrorLabel"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str;
                }
            }
            if (e.CommandName.Equals("DeclineOwner"))
            {
                string str2 = new UserHandler().SendOwnerRequestResponseToUser(Session["storeId"].ToString(), Session["userId"].ToString(), e.CommandArgument.ToString(), false);
                if (str2.Substring(1,6) != "Error:")
                {
                    Label errorlabel = (Label)(e.Item.FindControl("OwnerReqErrorLabel"));
                    errorlabel.Visible = true;
                    errorlabel.Text = "Request Declined";
                }
                else
                {
                    Label errorlabel = (Label)(e.Item.FindControl("OwnerReqErrorLabel"));
                    errorlabel.Visible = true;
                    errorlabel.Text = str2;
                }
            }
        }


        protected void DataListRemoveProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DataListPurchasePolicies_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DataListDiscountPolicies_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DataListOwnerRequests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}