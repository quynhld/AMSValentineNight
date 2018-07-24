<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthParkingCardMerge.aspx.cs" Inherits="KN.Web.Park.MonthParkingCardMerge" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<div class="TpAtit1">
</div>
    <div>
        <asp:ListView ID="lstMergeCard" runat="server"  ItemPlaceholderID="iphItemPlaceHolderID">
            <LayoutTemplate>
                <table id="tblMergeCard" runat="server">
                    <tr class="TypeA">
                        <td >ID</td>
                        <td >FloorNo</td>
                        <td >RoomNo</td>
                        <td >Vehicle</td>
                        <td >Status</td>
                        <td ><a href="#" runat="server">Edit</a> </td>
                        
                    </tr>
                    <tr id="iphItemPlaceHolderID"  runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                    <tr >
                        <td >ID</td>
                        <td >FloorNo</td>
                        <td >RoomNo</td>
                        <td >Vehicle</td>
                        <td >Status</td>
                        <td ><a href="#" >Merge</a> </td>
                    </tr>
            </ItemTemplate>           
        </asp:ListView>
    </div>
&nbsp;
</asp:Content>
