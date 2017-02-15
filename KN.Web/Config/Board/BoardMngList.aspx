<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardMngList.aspx.cs" Inherits="KN.Web.Config.Board.BoardMngList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="contList" ContentPlaceHolderID="cphContent" runat="server">
    <asp:ListView ID="lvBoarMngdList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
        OnLayoutCreated="lvBoarMngdList_LayoutCreated" OnItemDataBound="lvBoarMngdList_ItemDataBound" OnItemCreated="lvBoarMngdList_ItemCreated"
        OnItemUpdating="lvBoarMngdList_ItemUpdating">
        <LayoutTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="77px"/>
                <col/>
                <col width="110px"/>
                <col width="110px"/>
                <col width="100px"/>
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltFileCnt" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltReplyYn" runat="server"></asp:Literal></th>
	                    <th class="end">&nbsp;</th>
                    <tr>
                </thead>
                <tbody>
                    <tr id="iphItemlPlaceholderId" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:Literal ID="ltSeqList" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfMenuSeqList" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td><asp:Literal ID="ltTitleList" runat="server"></asp:Literal></td>
                <td align="center"><asp:DropDownList ID="ddlFileCntList" runat="server"></asp:DropDownList></td>               
                <td align="center"><asp:CheckBox ID="chkReplyYnList" runat="server" /></td>
                <td align="center"><span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="77px"/>
                <col/>
                <col width="110px"/>
                <col width="110px"/>
                <col width="100px"/>
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltFileCnt" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltReplyYn" runat="server"></asp:Literal></th>
	                    <th class="end">&nbsp;</th>
                    <tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>    
</asp:Content>