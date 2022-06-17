<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="EditShop.aspx.cs" Inherits="Client.EditShop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            width: 165px;
        }
        .auto-style14 {
            width: 272px;
        }
        .auto-style15 {
            width: 698px;
        }
        .auto-style16 {
            width: 430px;
        }
        .auto-style17 {
            width: 246px;
        }
        .auto-style18 {
            width: 243px;
        }
        .auto-style19 {
            width: 245px;
        }
        .auto-style20 {
            width: 246px;
            height: 27px;
        }
        .auto-style21 {
            height: 27px;
        }
        .auto-style22 {
            width: 430px;
            height: 27px;
        }
        .addstylee {
            background-color: limegreen;
        }
        .removestylee{
             background-color:red ;
        }
        .btnsuccess {
            background: #1F60F0; 
            color: #eee;

        }
        .btnsuccesshover {
            background: #111; 
            color: #eee; 

        }
        .pgalogin {
background: none repeat scroll 0 0 #2EA2CC;
border-color: #0074A2;
box-shadow: 0 1px 0 rgba(120, 200, 230, 0.5) inset, 0 1px 0 rgba(0, 0, 0, 0.15);
color: #FFFFFF;
text-decoration: none;
text-align: center;
vertical-align: middle;
border-radius: 3px;
padding: 4px;
height: 27px;
font-size: 14px;
margin-bottom: 16px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <table class="auto-style5">
        <tr>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                      <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Edit Product" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Add New Item" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Add New Manager" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Add New Owner" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Fire Manager" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Fire Owner" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Add PurchasePolicy" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Remove PurchasePolicy" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Add Discount" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="Remove Discount" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Add Permissions" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="Remove Permissions" Value="12"></asp:ListItem>
                                                 <asp:ListItem Text="Store Staff" Value="13"></asp:ListItem>



                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style15">
                <table id="table1"  runat="server"  class="auto-style5">
                    <tr>
                        <td class="auto-style13">
                <asp:Label ID="Label3" runat="server" Text="Add item"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="LabelproductName" runat="server" Text="productName : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TextBoxproductName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Labelprice" runat="server" Text="price : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TextBoxprice" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="Labelcategories" runat="server" Text="categories : "></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TextBoxcategories" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">Quantity : </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style14">
                            <asp:Button ID="ButtonAdd" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAdd_Click" Height="39px" Width="90px" />
                        </td>
                        <td><asp:label ID="Labelerror1" runat="server"></asp:label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="auto-style5" id="table2"  runat="server">
        <tr>
            <td class="auto-style17">
                Add New Manager</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">
                <asp:Label ID="Labelmanagername" runat="server" Text="Choose UserName : "></asp:Label>
            </td>
            <td class="auto-style8">
              <asp:TextBox ID="TextBoxAddManager" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">&nbsp;</td>
            <td class="auto-style16">&nbsp</td>
        </tr>
        <tr>
            <td class="auto-style17">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">&nbsp;</td>
            <td>
                <asp:Button ID="Buttonaddmanager" CssClass="addstylee" runat="server" Text="Add" OnClick="Buttonaddmanager_Click" Height="37px" Width="68px" />
            </td>
            <td class="auto-style16"><asp:label ID="Labelerror2" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style20"></td>
            <td class="auto-style21"></td>
            <td class="auto-style22"></td>
        </tr>
        <tr>
            <td class="auto-style17">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>
    <td>
                        </td>
    <table class="auto-style5" id="table3"  runat="server">
        <tr>
            <td class="auto-style18">
                <asp:Label ID="Label2" runat="server" Text="Add New Owner"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">Choose UserName : </td>
             <td class="auto-style8">
              <asp:TextBox ID="TextBoxAddOwner" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>
                <asp:Button ID="Button1" CssClass="addstylee" runat="server" Text="Add" OnClick="Button1_Click" Height="38px" Width="72px" />
            </td>
            <td class="auto-style16"><asp:label ID="Labelerror3" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>

    <table class="auto-style5" id="table4"  runat="server">
        <tr>
            <td class="auto-style18">Fire Manager</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">Choose UserName : </td>
            <td class="auto-style8">
              <asp:TextBox ID="TextBoxManagerToRemove" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>
                <asp:Button ID="Button2" CssClass="removestylee" runat="server" Text="Remove" OnClick="Button2_Click" />
            </td>
            <td class="auto-style16"><asp:label ID="Labelerror4" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>

    <table class="auto-style5" id="table5"  runat="server">
        <tr>
            <td class="auto-style19">Fire Owner</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">Choose UserName : </td>
         <td class="auto-style8">
              <asp:TextBox ID="TextBoxOwnerToRemove" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td>
                <asp:Button ID="Button3" CssClass="removestylee" runat="server" Text="Remove" OnClick="Button3_Click" />
            </td>
            <td class="auto-style16"><asp:label ID="Labelerror5" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>
        <table class="auto-style5" id="table6"  runat="server">
        <tr>
            <td class="auto-style18">
                <asp:Label ID="LabelAddPerm" runat="server" Text="Add Permissions"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">Choose UserName : </td>
             <td class="auto-style8">
              <asp:TextBox ID="TextBoxUsernameAddPerm" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">
                <asp:TextBox ID="TextBoxPermissionsToSet" CssClass="txt" placeholder="Enter Permissions" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            </td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>
                <asp:Button ID="ButtonAddPermission" CssClass="addstylee" runat="server" Text="Add" OnClick="ButtonAddPermission_Click" Height="38px" Width="72px" />
            </td>
            <td class="auto-style16"><asp:label ID="LabelErrorAddPerm" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>
    <table class="auto-style5" id="table7"  runat="server">
        <tr>
            <td class="auto-style18">
                <asp:Label ID="Label4" runat="server" Text="Remove Permissions"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">Choose UserName : </td>
             <td class="auto-style8">
              <asp:TextBox ID="TextBoxRemoveUsernamePerm" CssClass="txt" placeholder="Enter Username" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            <td class="auto-style16">
                <asp:TextBox ID="TextBoxPermToRem" CssClass="txt" placeholder="Enter Permissions" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
            </td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>
                <asp:Button ID="ButtonRemovePerm" CssClass="removestylee" runat="server" Text="Remove" OnClick="ButtonRemovePerm_Click" Height="38px" Width="72px" />
            </td>
            <td class="auto-style16"><asp:label ID="LabelErrorRemovePerm" runat="server"></asp:label></td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
    </table>
    <asp:DataList ID="DataListproducts" runat="server" OnItemCommand="DataListproducts_ItemCommand" BackColor="White" BorderStyle="Double" CellPadding="4"  RepeatDirection="Horizontal" RepeatColumns="3" BorderColor="#336666" BorderWidth="3px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal" OnSelectedIndexChanged="DataListproducts_SelectedIndexChanged" Height="588px">
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <SelectedItemStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        <ItemStyle BackColor="White" ForeColor="#333333" />
        <ItemTemplate>
                       
                                <table align="left" style="width: 250px; background-color: #f5f5f5; border: 1px solid #CCC;">

                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                                    <span style="font-weight: 700; font-size: 14px;">Id: <%#Eval("productId") %> </span></td>

                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-weight: 700; font-size: 20px;">Name: <%#Eval("Name") %> </span></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtBoxEditName" CssClass="txt" placeholder="Enter New Name" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
                                        <td style="auto-style8" class="auto-style19">
                                            <asp:Button ID="btnEditName" runat="server" CommandArgument='<%#Eval("productId")%>' Text="edit" CssClass="auto_class8" Height="30px" Width="50px" OnClick="btnEditName_OnClick" CommandName="editNameProduct"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">Price: <%#Eval("price") %></span></td>
                                       <td class="auto-style8">
                                            <asp:TextBox ID="TextBoxEditPrice" CssClass="txt" placeholder="Enter New Price" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
                                        <td style="auto-style8" class="auto-style19">
                                            <asp:Button ID="btnEditPrice" runat="server" CommandArgument='<%#Eval("productId")%>' Text="edit" CssClass="auto_class8" Height="30px" Width="50px" OnClick="btnEditPrice_OnClick" CommandName="editPriceProduct"  />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <span style="font-size: 16px;">Catagory: <%#Eval("catagory") %></span></td>
                                         <td class="auto-style8">
                                            <asp:TextBox ID="TextBoxEditCategory" CssClass="txt" placeholder="Enter New Category" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
                                        <td style="auto-style8" class="auto-style19">
                                            <asp:Button ID="btnEditCategory" runat="server" CommandArgument='<%#Eval("productId")%>' Text="edit" CssClass="auto_class8" Height="30px" Width="50px" OnClick="btnEditCategory_OnClick" CommandName="editCategoryProduct"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 16px;">Quantity: <%#Eval("quantity") %></span></td>
                                            <td class="auto-style8">
                                            <asp:TextBox ID="TextBoxEditQuantity" CssClass="txt" placeholder="Enter New Category" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
                                        <td style="auto-style8" class="auto-style19">
                                            <asp:Button ID="btnEditQuantity" runat="server" CommandArgument='<%#Eval("productId")%>' Text="edit" CssClass="auto_class8" Height="30px" Width="50px" OnClick="btnEditCategory_OnClick" CommandName="editQuantityProduct"  />
                                        </td>
                                    </tr>
                                    <tr>
                                            <td style="height: 40px;"></td>
                                            <td><asp:Label ID="LabelEditProductError" runat="server" Text="Error:"></asp:Label></td>


                                    </tr>
                                    <tr>
                                    </tr>
                                </table>
                        
                        </ItemTemplate>
    </asp:DataList>

    <asp:DataList ID="StoreStaff" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <table align="center" style="width: 100%; border-bottom: 1px solid #CCC">
                                                    <tr>
                                                        <td style="text-align: center; width: 60px;">
                                                        <td style="width: 10px;"></td>
                                                        <td style="width: 302px">
                                                            <table align="left" style="width: 300px">
                                                                <tr>
                                                                    <td style="text-align: left;"><span style="font-size: 14px; font-weight: 700;">ID :<%#Eval("id")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left;"><span style="font-size: 14px;">MSG : <%#Eval("Username") %></span></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: right;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>

    </asp:Content>
