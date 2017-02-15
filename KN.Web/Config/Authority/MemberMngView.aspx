<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemberMngView.aspx.cs" Inherits="KN.Web.Config.Authority.MemberMngView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<div class="Tbtop-tit"><asp:Literal ID="ltInsView" runat="server"></asp:Literal></div>
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
        <td><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
        <th class="lebd"><asp:Literal ID="ltAuthGrp" runat="server"></asp:Literal></th>
        <td><asp:Literal ID="ltInsAuthGrp" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltUserID" runat="server"></asp:Literal></th>
        <td><asp:Literal ID="ltInsUserID" runat="server"></asp:Literal></td>
        <th class="lebd"><asp:Literal ID="ltPwd" runat="server"></asp:Literal></th>
        <td><asp:Literal ID="ltInsPwd" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></th>
        <td><asp:Literal ID="ltInsTel" runat="server"></asp:Literal></td>
        <th class="lebd"><asp:Literal ID="ltMobileNo" runat="server"></asp:Literal></th>
        <td><asp:Literal ID="ltInsMobile" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th><asp:Literal ID="ltEmail" runat="server"></asp:Literal></th>
        <td colspan="3"><asp:Literal ID="ltInsEmail" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <th rowspan="2" valign="top"><asp:Literal ID="ltAddr" runat="server"></asp:Literal></th>
        <td colspan="3" class="P10" style="height:25px"><asp:Literal ID="ltInsAddr" runat="server"></asp:Literal></td>
    </tr>
    <tr>
        <td colspan="3" class="P10" style="height:25px"><asp:Literal ID="ltInsDetAddr" runat="server"></asp:Literal></td>
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
	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfMemSeq" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfCompCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>