<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="KN.Web.Config.SiteMap.SiteMap" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">        
    <asp:ListView ID="lvSiteMapList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
        OnLayoutCreated="lvSiteMapList_LayoutCreated" OnItemDataBound="lvSiteMapList_ItemDataBound" OnItemCreated="lvSiteMapList_ItemCreated">
        <LayoutTemplate>
            <div class="stmap">            
                <table cellspacing="0">               
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <asp:Literal ID="ltContent" runat="server"></asp:Literal>              
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>            
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>