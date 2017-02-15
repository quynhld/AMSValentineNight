<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupLogView.aspx.cs" Inherits="KN.Web.Common.Popup.PopupLogView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Log View</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <table cellspacing="0" class="TbCel-Type-Pup4">
            <col width="15%"/>
            <col width="35%"/>
            <col width="15%"/>
            <col width="35%"/>
            <tbody>
                <tr>
	                <th colspan="4"><asp:Literal ID="lblInsLogSeq" runat="server"></asp:Literal> (<asp:Literal ID="lblInsErrTy" runat="server"></asp:Literal>)</th>
                </tr>
                <tr>
	                <th><asp:Literal ID="lblDate" runat="server"></asp:Literal></th>
	                <td><asp:Literal ID="lblInsDate" runat="server"></asp:Literal></td>
	                <th class="lebd"><asp:Literal ID="lblMachId" runat="server"></asp:Literal></th>
	                <td><asp:Literal ID="lblInsMachId" runat="server"></asp:Literal></td>
                </tr>
                <tr>
	                <th><asp:Literal ID="lblUrl" runat="server"></asp:Literal></th>
	                <td colspan="3"><asp:Literal ID="lblInsUrl" runat="server"></asp:Literal></td>
                </tr>
                <tr>
	                <th><asp:Literal ID="lblErrPos" runat="server"></asp:Literal></th>
	                <td colspan="3"><div style="height:200px;overflow-x:hidden;overflow-y:scroll;"><asp:Literal ID="lblInsErrPos" runat="server"></asp:Literal></div></td>
                </tr>
                <tr>
	                <th><asp:Literal ID="lblErrTxt" runat="server"></asp:Literal></th>
	                <td colspan="3"><asp:Literal ID="lblInsErrTxt" runat="server"></asp:Literal></td>
                </tr>
            </tbody>
        </table>
	<div class="Btwps FloatR2">
		<div class="Btn-Type2-wp FloatL">
			<div class="Btn-Tp2-L">
				<div class="Btn-Tp2-R">
					<div class="Btn-Tp2-M">
						<span>  <asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
					</div>
				</div>
			</div>
		</div>

		<div class="Btn-Type3-wp FloatL">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span> <asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton> </span>
					</div>
				</div>
			</div>
		</div>
	</div>
        <asp:TextBox ID="txtHfLogSeq" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfErrTy" runat="server" Visible="false"></asp:TextBox> 
    </div>
    </form>
</body>
</html>