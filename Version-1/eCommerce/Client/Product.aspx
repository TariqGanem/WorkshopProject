<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Client.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            width: 955px;
        }
        .auto-style14 {
            width: 184px;
        }
        .auto-style15 {
            width: 653px;
        }
        .auto-style16 {
            margin-left: 3px;
        }
        .auto-style23 {
            width: 1px;
        }
        .auto-style24 {
            width: 7px;
        }
        .label-class {
            display: flex;
            align-items: center;
        }
        .td {
            vertical-align: middle;
            text-align: center;
        }
        .auto-style25 {
            width: 184px;
            height: 27px;
            margin-left: 0;
        }
        .auto-style26 {
            width: 137px;
            height: 27px;
        }
        .auto-style27 {
            width: 812px;
            height: 27px;
        }
        .auto-style28 {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style5">
        <tr>
            <td class="auto-style14">
                            &nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="LabelproductName" runat="server" Text="Name : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="LabelproductName0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelbarcode" runat="server" Text="Id : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelbarcode0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="LabelstoreId" runat="server" Text="storeId : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="LabelstoreId0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelcategories" runat="server" Text="Category : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelcategories0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            <asp:Label ID="Labelprice" runat="server" Text="Price : "></asp:Label>
                        </td>
            <td class="auto-style15">
                            <asp:Label ID="Labelprice0" runat="server"></asp:Label>
                        </td>
            <td class="auto-style13">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">
                            &nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">
                            <asp:Label ID="Labelprice1" runat="server" Width="90px" Text="Qty :"></asp:Label>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="img/-.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px" OnClick="ImageButton1_Click" />
                            <asp:Label ID="Label1"  runat="server" CssClass="td" Height="30px" Text="0" Width="54px"></asp:Label>
                        <asp:ImageButton ID="ImageButton2" ImageUrl="img/plus.PNG" runat="server" CssClass="auto-style16" Height="30px" Width="30px" OnClick="ImageButton2_Click" />
                            <table class="auto-style5">
                                <tr>
                                    <td class="auto-style24">
                                        &nbsp;</td>
                                    <td class="auto-style24" >
                                        &nbsp;</td>
                                    <td class="auto-style23">
                                        &nbsp;</td>
                                </tr>
                            </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14">&nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td class="auto-style13">&nbsp;</td>
            <td>
  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ><img src="img/add_to_cart.PNG" style="width: auto; height: auto;" /></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
