﻿<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="ShoppingHistoryAdmin.aspx.cs" Inherits="Client.Shops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="DataListShoppingBags" runat="server" BackColor="White" BorderStyle="Double" CellPadding="4"  RepeatDirection="Horizontal" RepeatColumns="3" BorderColor="#336666" BorderWidth="3px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal" OnSelectedIndexChanged="DataListproducts_SelectedIndexChanged">
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
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td><span style="font-size: 16px;">StoreId: <%#Eval("storeid") %></span></td>
                </tr>
                <tr>
                    <td><span style="font-size: 16px;">Name: <%#Eval("Name") %></span></td>
                </tr>
               <tr>
                    <td><span style="font-size: 16px;">Price: <%#Eval("Price") %></span></td>
                </tr>
                 <tr>
                    <td><span style="font-size: 16px;">Quantity: <%#Eval("Quantity") %></span></td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
