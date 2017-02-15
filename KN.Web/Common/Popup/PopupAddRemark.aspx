<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupAddRemark.aspx.cs" Inherits="KN.Web.Common.Popup.PopupAddRemark" ValidateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Add Remark</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--//
function fnValidateCheck(strText)
{
    var strRemark = document.getElementById("<%=txtRemark.ClientID%>");

    if (trim(strRemark.value) == "")
    {
        alert(strText);
        strRemark.focus();
        return false;
    }
    else
    {
        return true;
    }
}
//-->
</script>
<base target="_self"/>
</head>
<body>
    <form id="frmPopup" runat="server">
    <div>
        <table cellspacing="0" class="TbCel-Type-Pup4 MrgT20">
            <col width="20%"/>
            <col width="80%"/>
            <tbody>
                <tr>
                    <th valign="top"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:TextBox ID="txtRemark" runat="server" Columns="55" Rows="13" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </tbody>
        </table>
    </div>
	<div class="Btwps FloatR2">
		<div class="Btn-Type2-wp FloatL">
			<div class="Btn-Tp2-L">
				<div class="Btn-Tp2-R">
					<div class="Btn-Tp2-M">
						<span> <asp:LinkButton ID="lnkbtnAdd" runat="server" onclick="lnkbtnAdd_Click"></asp:LinkButton></span>
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

    <asp:TextBox ID="txtHfCounselCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfCounselSeq" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>