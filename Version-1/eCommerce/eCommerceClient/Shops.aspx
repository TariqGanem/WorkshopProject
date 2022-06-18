<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="Shops.aspx.cs" Inherits="Client.Shops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="DataListShops" runat="server" OnItemCommand="DataListproducts_ItemCommand" BackColor="White" BorderStyle="Double" CellPadding="4"  RepeatDirection="Horizontal" RepeatColumns="3" BorderColor="#336666" BorderWidth="3px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="Horizontal">
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
                    <td><span style="font-size: 16px;">Id: <%#Eval("storeId") %></span></td>
                </tr>
                <tr>
                    <td><span style="font-size: 16px;">Name: <%#Eval("StoreName") %></span></td>
                </tr>
               <tr>
                    <td><span style="font-size: 16px;">Founder: <%#Eval("StoreFounder") %></span></td>
                </tr>
               <tr>
                    <td><span style="font-size: 16px;">Rate: <%#Eval("Rate") %></span></td>
                </tr>
                <tr>
                    <td><span style="font-size: 16px;">Number Of Rates: <%#Eval("NumbeOfRates") %></span></td>
                </tr>
                <tr>
                     <td class="auto-style8">
                        <asp:TextBox ID="TextBoxRate" CssClass="txt" placeholder="Rate Store" runat="server" Style="text-align: center" Height="10px" Width="80px"></asp:TextBox></td>
                    <td  class="auto-style19">
                        <asp:Button ID="btnRateStore" runat="server" CommandArgument='<%#Eval("storeId")%>' Text="Rate" CssClass="auto_class8" Height="30px" Width="50px" CommandName="RateStore"  />
                    </td>                
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                    <td><asp:Label ID="LabelRateError" runat="server" Text=":"></asp:Label></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
