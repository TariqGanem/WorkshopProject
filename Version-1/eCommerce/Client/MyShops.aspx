<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="MyShops.aspx.cs" Inherits="Client.MyShops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style13 {
            width: 144%;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:DataList ID="Data_shop" OnItemCommand="Data_shop_Command" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <table align="center" style="width: 100%; border-bottom: 1px solid #CCC">
                                                    <tr>
                                                        <td>
                                                            <td><span style="font-size: 22px;">Name: <%#Eval("storeName") %></span></td>
                                                            <td><span style="font-size: 12px;">ID: <%#Eval("storeId") %></span></td>
                                                        </td>
                                                        <td style="width: 200px;">
                                                        <td style="width: 312px">
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Button ID="btnedit" CommandArgument='<%#Eval("storeName")+","+Eval("storeId")%>' CommandName="editshop" runat="server" Text="editshop" Width="101px" Height="40px" />
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Button ID="ButtonClose" CommandArgument='<%#Eval("storeName")%>' CommandName="Close" runat="server" Text="Close Store" Width="101px" Height="40px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>

</asp:Content>
