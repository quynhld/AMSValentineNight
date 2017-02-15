<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="SendMemoView.aspx.cs" Inherits="KN.Web.Board.Memo.SendMemoView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="contView" ContentPlaceHolderID="cphContent" runat="server">
<script type="text/javascript"> 
<!--//
    function fnNoDelete(strText) 
    {
        alert(strText);

        return false;
    }
//-->
</script>
<div class="Tbtop-tit">
	<asp:Literal ID="ltView" runat="server"></asp:Literal>
</div>
<table cellspacing="0" class="TbCel-Type1">
    <col width="150px"/>
    <col width=""/>
    <col width="150px"/>
    <col width=""/>
    <tbody>
        <tr>
	        <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsTitle" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltNm" runat="server"></asp:Literal></th>
	        <td>
	            <asp:Literal ID="ltInsNm" runat="server"></asp:Literal>
	            <asp:TextBox ID="txtHfReceiveMemNo" runat="server" Visible="false"></asp:TextBox>
	            <asp:TextBox ID="txtHfReceiveCompNo" runat="server" Visible="false"></asp:TextBox>
	        </td>
	        <th class="lebd"><asp:Literal ID="ltSendDate" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltInsSendDate" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsContent" runat="server"></asp:Literal></td>
        </tr>
        <tr runat="server" id="trFileAddon">
	        <th><asp:Literal ID="ltFileAddon" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsFileAddon" runat="server"></asp:Literal></td>
        </tr>
    </tbody>
</table>
<div class="Btwps FloatR2"">
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span>  <asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span> <asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfMemoSeq" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFilePath" runat="server" Visible="false"></asp:TextBox>
</asp:Content>