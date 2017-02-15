<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupTitleFind.aspx.cs" Inherits="KN.Web.Common.Popup.PopupTitleFind"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Title Find</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--//
    function fnPopupConfirm(strText)
    {
        var strReturnBox = document.getElementById("<%=HfReturnBox.ClientID%>").value;

        opener.document.getElementById(strReturnBox).value = strText;
        self.close();
    }

    function fnClose()
    {
        self.close();
    }
//-->
</script>
<base target="_self"/>
</head>
<body>
<form id="frmPopup" runat="server">
<table cellspacing="0"class="TbCel-Type6-A iw570">
    <col width="100px"/>
    <col width="150px"/>
    <col width="150px"/>
    <col width="150px"/>
    <tr>
        <th><asp:Literal ID="ltExpressCd" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMenuEn" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMenuVi" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMenuKr" runat="server"></asp:Literal></th>            
    </tr>
    <tr>
        <td colspan="4" style="height:1px"></td>
    </tr>
</table>  
<div style="text-align:center;overflow-y:scroll;height:200px;width:570px;">
    <asp:ListView ID="lvTitleList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvTitleList_ItemDataBound" OnItemCreated="lvTitleList_ItemCreated">
        <LayoutTemplate>
            <table cellspacing="0" class="TbCel-Type6-A iw550">
                <col width="135px"/>
                <col width="135px"/>
                <col width="135px"/>
                <col width="135px"/>
                <tr id="iphItemPlaceHolderId" runat="server"></tr>
            </table>    
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnPopupConfirm("<%#Eval("ExpressCd")%>");'>
                <td><asp:Literal ID="ltDataExpressCd" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltDataMenuEn" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltDataMenuVi" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltDataMenuKr" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</div>
<div class="Btn-Type2-wp FloatR2">
    <div class="Btn-Tp2-L">
        <div class="Btn-Tp2-R">
            <div class="Btn-Tp2-M">
                <span>  <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClientClick="javascript:fnClose();"></asp:LinkButton></span>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HfReturnBox" runat="server"/>
</form>
</body>
</html>