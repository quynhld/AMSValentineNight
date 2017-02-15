<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="PersonalMngView.aspx.cs" Inherits="KN.Web.Config.Authority.PersonalMngView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<div class="Tbtop-tit"><asp:Label ID="lblView" runat="server"></asp:Label></div>
<table cellspacing="0" class="TbCel-Type2-B">
    <col width="125px"/>
    <col width="200px"/>
    <col width="125px"/>
    <col width="200px"/>
    <tr>
        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
        <td colspan="3">
            <asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal>
            <asp:TextBox ID="txtHfCompNo" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
        <th class="lebd"><asp:Literal ID="ltAuthGrp" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblAuthGrp" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltUserID" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblUserID" runat="server"></asp:Label></td>
        <th class="lebd"><asp:Literal ID="ltPwd" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblPwd" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblTel" runat="server"></asp:Label></td>
        <th class="lebd"><asp:Literal ID="ltMobileNo" runat="server"></asp:Literal></th>
        <td><asp:Label ID="lblMobile" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltEmail" runat="server"></asp:Literal></th>
        <td colspan="3"><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <th rowspan="2" valign="top"><asp:Literal ID="ltAddr" runat="server"></asp:Literal></th>
        <td colspan="3" class="P10" style="height:25px"><asp:Label ID="lblAddr" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" class="P10" style="height:25px"><asp:Label ID="lblDetAddr" runat="server"></asp:Label></td>
    </tr>
</table>
<div class="Btwps FloatR">
	<div class="Btn-Type2-wp FloatL">
		<div class="Btn-Tp2-L">
			<div class="Btn-Tp2-R">
				<div class="Btn-Tp2-M">
					<span>   <asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfMemSeq" runat="server" Visible="false"></asp:TextBox>
</asp:Content>