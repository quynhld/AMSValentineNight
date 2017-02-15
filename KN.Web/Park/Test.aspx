<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="KN.Web.Park.Test" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div class="tbse3">
        <telerik:RadDatePicker ID="RadDatePicker1" Runat="server" Culture="en-US" 
            HiddenInputTitleAttibute="Visually hidden input created for functionality purposes." 
            WrapperTableSummary="Table holding date picker control for selection of dates.">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DateInput DisplayDateFormat="yyyy/MM/dd" DateFormat="yyyy/MM/dd" LabelWidth="40%"></DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        </telerik:RadDatePicker>
    </div>
</asp:Content>
