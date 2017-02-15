<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupCompRoomNo.aspx.cs" Inherits="KN.Web.Common.Popup.PopupCompRoomNo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Company Find</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/jquery-1.8.3.min.js"></script>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--    //
    function fnPopupConfirm(strRoom, strUserSeq,strComNm,strRentCd)
    {
        var strReturnRoomNo = document.getElementById("<%=HfReturnRoomNoId.ClientID%>").value;
        var strReturnUserSeq = document.getElementById("<%=HfReturnUserSeqId.ClientID%>").value;
        var strReturnComNm = document.getElementById("<%=HfReturnCompNmID.ClientID%>").value;

        opener.document.getElementById(strReturnRoomNo).value = strRoom;
        opener.document.getElementById(strReturnUserSeq).value = strUserSeq;
        opener.document.getElementById(strReturnComNm).value = strComNm;
        opener.callBack(strComNm, strRentCd, strRoom, strUserSeq);
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
<asp:ScriptManager ID="smManager" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkbtnSearch" />
    </Triggers>
    <ContentTemplate>
        <fieldset class="sh-field2 MrgB10" style="width: 570px">
            <legend>검색</legend>
            <ul class="sf2-ag MrgL30">
                <li><asp:Literal ID="Literal1" runat="server" Text="Company Name:"></asp:Literal></li>
                <li><asp:TextBox ID="txtCompNm" runat="server" Width="180px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>                        
                <li><asp:Literal ID="ltInsRoomNo" runat="server" Text="Room No :"></asp:Literal></li>
                <li><asp:TextBox ID="txtInsRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
                <li><asp:Literal ID="ltInsPaidCd" runat="server"></asp:Literal></li>
                <li>
	                <div class="Btn-Type4-wp">
		                <div class="Btn-Tp4-L">
			                <div class="Btn-Tp4-R">
				                <div class="Btn-Tp4-M">
					                <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span>
				                </div>
			                </div>
		                </div>
	                </div>		        
                </li>
            </ul>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>      
<table cellspacing="0"class="TbCel-Type6-A iw570">
    <col width="20px"/>
    <col width="300px"/>
    <col width="90px"/>
    <tr>
        <th><asp:Literal ID="ltCompNo" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltBizIndusNm" runat="server"></asp:Literal></th>                 
    </tr>
    <tr>
        <td colspan="4" style="height:1px"></td>
    </tr>
</table>  
<div style="text-align:center;overflow-y:scroll;height:500px;width:570px;">
    <asp:ListView ID="lvCompList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvCompList_ItemDataBound"
     OnLayoutCreated="lvCompList_LayoutCreated" OnItemCreated="lvCompList_ItemCreated">
        <LayoutTemplate>
            <table cellspacing="0" class="TbCel-Type6-A iw550">
                <col width="20px"/>
                <col width="300px"/>
                <col width="70px"/>
                <tr id="iphItemPlaceHolderId" runat="server"></tr>
            </table>    
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnPopupConfirm("<%#Eval("RoomNo")%>", "<%#Eval("UserSeq")%>", "<%#Eval("UserNm")%>", "<%#Eval("RentCd")%>");'>
                <td><asp:Literal ID="ltDataCompNo" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltDataCompNm" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltDataBizIndusNm" runat="server"></asp:Literal></td>
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
<asp:HiddenField ID="HfReturnCompNmID" runat="server"/>
<asp:HiddenField ID="HfReturnRoomNoId" runat="server"/>
<asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>
<asp:HiddenField ID="hfRentCd" runat="server"/>
<asp:HiddenField ID="hfCompNmS" runat="server"/>

</form>
</body>
</html>