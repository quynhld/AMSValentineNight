<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BoardReply.aspx.cs" Inherits="KN.Web.Board.Board.BoardReply"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnValidateData(strAlertTitle, strAlertContext)
    {
        var strTitle = document.getElementById("<%=txtTitle.ClientID%>");

        if (trim(strTitle.value) == "")
        {
            strTitle.focus();
            alert(strAlertTitle);
            return false;
        }

        var strContent = document.getElementById("<%=txtContext.ClientID%>");

        if (trim(strContent.value) == "")
        {
            strContent.focus();
            alert(strAlertContext);
            return false;
        }

        return true;
    }
</script>
<div class="Tb-Tp-tit">&bull;Board / Download</div>
<div class="Tbtop-tit">
	<asp:Literal ID="ltReply" runat="server"></asp:Literal>
</div>
<table cellspacing="0" class="TbCel-Type1">
    <col width="147px"/>
    <col width="503px"/>
    <tbody>
        <tr>
            <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
            <td><asp:TextBox ID="txtTitle" runat="server" Width="450px" MaxLength="255">[Re] </asp:TextBox></td>
        </tr>
        <tr>
            <th valign="top"><asp:Literal ID="ltContent" runat="server"></asp:Literal></th>
            <td><asp:TextBox runat="server" ID="txtContext" Rows="5" Columns="60" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <th><asp:Literal ID="ltFileAddon1" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon1" runat="server" Width="450px"/>
            </td>
        </tr>
        <tr>
            <th><asp:Literal ID="ltFileAddon2" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon2" runat="server" Width="450px"/>
            </td>
        </tr>
        <tr>
            <th><asp:Literal ID="ltFileAddon3" runat="server"></asp:Literal></th>
            <td>
                <asp:FileUpload ID="fileAddon3" runat="server" Width="450px"/>
            </td>
        </tr>
    </tbody>
</table>
  <div class="Btwps FloatR2">
	<div class="Btn-Type2-wp FloatL">
		<div class="Btn-Tp2-L">
			<div class="Btn-Tp2-R">
				<div class="Btn-Tp2-M">
					<span>  <asp:LinkButton ID="lnkbtnReset" runat="server" onclick="lnkbtnReset_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span> <asp:LinkButton ID="lnkbtnRegist" runat="server" onclick="lnkbtnRegist_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtnList" runat="server"></asp:LinkButton> </span>
				</div>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardSeq" runat="server" Visible="false"></asp:TextBox>
</asp:Content>