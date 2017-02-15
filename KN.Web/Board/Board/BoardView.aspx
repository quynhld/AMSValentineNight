<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardView.aspx.cs" Inherits="KN.Web.Board.Board.BoardView" ValidateRequest="false"%>
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
	        <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltInsNm" runat="server"></asp:Literal></td>
	        <th class="lebd"><asp:Literal ID="ltAccessIP" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltInsAccessIP" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltRegistDate" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltInsRegDate" runat="server"></asp:Literal></td>
	        <th class="lebd"><asp:Literal ID="ltModifyDate" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltinsModDate" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsContent" runat="server"></asp:Literal></td>
        </tr>
        <tr runat="server" id="trFileAddon1">
	        <th><asp:Literal ID="ltFileAddon1" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsFileAddon1" runat="server"></asp:Literal></td>
        </tr>
        <tr runat="server" id="trFileAddon2">
	        <th><asp:Literal ID="ltFileAddon2" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsFileAddon2" runat="server"></asp:Literal></td>
        </tr>
        <tr runat="server" id="trFileAddon3">
	        <th><asp:Literal ID="ltFileAddon3" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsFileAddon3" runat="server"></asp:Literal></td>
        </tr>
    </tbody>
</table>
<div class="Btwps FloatR">
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnReply" runat="server" onclick="lnkbtnReply_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfBoardTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardSeq" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFilePath1" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFilePath2" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFilePath3" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFileCnt" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtReplyYn" runat="server" Visible="false"></asp:TextBox>
</asp:Content>