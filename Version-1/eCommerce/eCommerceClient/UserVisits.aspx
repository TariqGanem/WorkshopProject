<%@ Page Title="" Language="C#" MasterPageFile="~/m1.Master" AutoEventWireup="true" CodeBehind="UserVisits.aspx.cs" Inherits="Client.UserVisits" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="Data_cart" runat="server" Width="100%">
                                            <ItemTemplate>
                                                <table align="center" style="width: 100%; border-bottom: 1px solid #CCC">
                                                    <tr>
                                                        <td style="text-align: center; width: 60px;">
                                                        <td style="width: 10px;"></td>
                                                        <td style="width: 302px">
                                                            <table align="left" style="width: 300px">
                                                                <tr>
                                                                    <td style="text-align: left;"><span style="font-size: 14px; font-weight: 700;">Date :<%#Eval("Date")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width:250px"><span style="font-size: 14px;">#Registered Users : <%#Eval("Regs")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width:250px"><span style="font-size: 14px;">#Guest Users : <%#Eval("Guests")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width:250px"><span style="font-size: 14px;">#Managers : <%#Eval("Managers")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width:250px"><span style="font-size: 14px;">#Owners : <%#Eval("Owners")%></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; width:250px"><span style="font-size: 14px;">#Admins : <%#Eval("Admins")%></span></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: right;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
    
    <asp:Chart ID="Chart1" runat="server" BackColor="LightGray"  >
         <ChartAreas>
             <asp:ChartArea Name="ChartArea1" BackColor="Orange"  >
             </asp:ChartArea>
         </ChartAreas>
         <Legends>
             <asp:Legend BackColor="Gray" Name="Legend1" LegendStyle="Row" Alignment="Center" Docking="Bottom" >               
             </asp:Legend>            
        </Legends>
     </asp:Chart>
</asp:Content>
