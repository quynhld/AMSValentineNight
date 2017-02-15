<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemoView.aspx.cs" Inherits="KN.Web.Board.Memo.MemoView"  ValidateRequest="false"%>
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
    <col width="250px"/>
    <col width="150px"/>
    <col width="250px"/>
    <tbody>
        <tr>
	        <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
	        <td colspan="3"><asp:Literal ID="ltInsTitle" runat="server"></asp:Literal></td>
        </tr>
        <tr>    
	        <th><asp:Literal ID="ltNm" runat="server"></asp:Literal></th>
	        <td>
	            <asp:Literal ID="ltInsNm" runat="server"></asp:Literal>
	            <asp:TextBox ID="txtHfInsMemNo" runat="server" Visible="false"></asp:TextBox>
	            <asp:TextBox ID="txtHfInsCompNo" runat="server" Visible="false"></asp:TextBox>	            
	        </td>
	        <th class="lebd"><asp:Literal ID="ltReceiveDate" runat="server"></asp:Literal></th>
	        <td><asp:Literal ID="ltInsReceiveDate" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0"><asp:Literal ID="ltInsContent" runat="server"></asp:Literal></td>
        </tr>
        <tr runat="server" id="trFileAddon">
	        <th><asp:Literal ID="ltFileAddon" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0"><asp:Literal ID="ltInsFileAddon" runat="server"></asp:Literal></td>
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
		<div class="Btn-Tp2-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span> <asp:LinkButton ID="lnkbtnReply" runat="server" onclick="lnkbtnReply_Click"></asp:LinkButton></span>
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