﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthParkingExcelImport.aspx.cs" Inherits="KN.Web.Park.MonthParkingExcelImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
        <td>Chọn file Excel 
            <asp:FileUpload ID="excelFileLoad" runat="server" />
            <asp:Button ID="btnReadFile" runat="server" onclick="btnReadFile_Click" 
                Text="Read data" Width="140px" />
            <asp:Button ID="btnImport" runat="server" onclick="btnImport_Click" 
                Text="Import Data" Width="140px" />
        </td>
      </tr>  
  <tr>
        <td>Chọn tháng
            <asp:DropDownList ID="ddlMonth" runat="server" Height="18px" Width="54px">
                <asp:ListItem Value="01">1</asp:ListItem>
                <asp:ListItem Value="02">2</asp:ListItem>
                <asp:ListItem Value="03">3</asp:ListItem>
                <asp:ListItem Value="04">4</asp:ListItem>
                <asp:ListItem Value="05">5</asp:ListItem>
                <asp:ListItem Value="06">6</asp:ListItem>
                <asp:ListItem Value="07">7</asp:ListItem>
                <asp:ListItem Value="08">8</asp:ListItem>
                <asp:ListItem Value="09">9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
        </td>
       
    </tr>
    <tr>
        <td colspan=2>
            <asp:DataList runat="server" ID ="dtlRecortUploaded" Enabled ="false" Visible="false">
                <HeaderTemplate>
                <table>
                    <tr>
                        <td>STT</td>
                        <td>Licence Plate</td>
                        <td>Car Card</td>
                        <td>Room Number</td>
                        <td>Price</td>
                        <td>Payment method</td>
                        <td>Status</td>
                        <td>Description</td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                <tr style="border-color:Black">
                        <td><asp:Label ID="lblNo" runat="server" /></td>
                        <td><asp:Label ID="LblLicencePlate" runat="server" /></td>
                        <td><asp:Label ID="lblCarCard" runat="server"/></td>
                        <td><asp:Label ID="lblRoomNo" runat="server"/></td>
                        <td><asp:Label ID="lblPrice" runat="server" /></td>
                        <td><asp:Label ID="lblPaymentMethod" runat="server" /></td>
                        <td><asp:Label ID="lblStatus" runat="server" /></td>
                        <td><asp:Label ID="lblDescripton" runat="server" /></td>
                        </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grdExcelContent" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
        </td>
    </tr>
    <//table>
    </div>
    </form>
</body>
</html>
