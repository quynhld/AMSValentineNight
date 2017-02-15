<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardTestList.aspx.cs" Inherits="KN.Web.Board.Board.BoardTestList" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
&nbsp;<telerik:RadDatePicker ID="RadDatePicker1" Runat="server">
    </telerik:RadDatePicker>
</asp:Content>
