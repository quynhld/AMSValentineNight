<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupGoodsGraph.aspx.cs" Inherits="KN.Web.Common.Popup.PopupGoodsGraph"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<%@ Register TagPrefix="cc1" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>Goods Graph</title>
    <link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
    <script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
    </head>
    <body>
    <form id="frmGoodsGraph" runat="server">
        <asp:ScriptManager ID="scManager" runat="server"></asp:ScriptManager>
        <asp:Literal ID="ltEntireInventory" runat="server"></asp:Literal>
        <div><cc1:ZedGraphWeb ID="zgwEntireGoodsGraph" runat="server" Width="600" Height="300"></cc1:ZedGraphWeb></div>
        <asp:Literal ID="ltInventory" runat="server"></asp:Literal>
        <div><cc1:ZedGraphWeb ID="ZedEachGoodsGraph" runat="server" Width="600" Height="300"></cc1:ZedGraphWeb></div>
        <asp:TextBox ID="txtParams" runat="server" Visible="false"></asp:TextBox>
    </form>
    </body>
</html>