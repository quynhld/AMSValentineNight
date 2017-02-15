<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupTmpExchangeRate.aspx.cs" Inherits="KN.Web.Common.Popup.PopupTmpExchangeRate" ValidateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--//
    function fnApplyChange(strText)
    {
        if (IsNumeric(strText))
        {
            if (event.keyCode == 13) 
            {
                <%=Page.GetPostBackEventReference(imgbtnProcess)%>;
                return false; 
            }
        }
    }
    
    function fnCheckValidate(strText)
    {
        var strExchageRate = document.getElementById("<%=txtPostExchageRate.ClientID%>").value;
        var strRetunBox1 = document.getElementById("<%=hfReturnBox1.ClientID%>").value;
        var strRetunBox2 = document.getElementById("<%=hfReturnBox2.ClientID%>").value;
        
        if (trim(strExchageRate) != "")
        {
            if (trim(strRetunBox1) != "")
            {
                opener.document.getElementById("<%=hfReturnBox1.Value%>").value = strExchageRate;
            }
            
            if (trim(strRetunBox2) != "")
            {
                opener.document.getElementById("<%=hfReturnBox2.Value%>").value = strExchageRate;
            }
            
            opener.fnResetCurrency();
            
            self.close();
        }
        else
        {
            alert(strText);
            return false;
        }
    }
    
    function fnWindowsClose()
    {
        self.close();
    }
//-->
</script>
</head>
<body>
<form id="frmPopup" runat="server">
<div style="text-align:center;">
    <table cellspacing="0" class="TbCel-Type-Pup4 iw320">
        <tr>
            <th></th>
            <th><asp:Literal ID="ltTitlePreTitle" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTitlePostTitle" runat="server"></asp:Literal></th>
        </tr>
        <tr>
            <td>1 dollar : </td>
            <td><asp:Literal ID="ltTitlePreExchangeRate" runat="server"></asp:Literal></td>
            <td>
                <asp:TextBox ID="txtPostExchageRate" runat="server" MaxLength="12" Width="100"></asp:TextBox>
                <asp:Literal ID="ltTitleUnit" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">
				<div class="Btwps FloatR2">
					<div class="Btn-Type3-wp ">
						<div class="Btn-Tp3-L">
							<div class="Btn-Tp3-R">
								<div class="Btn-Tp3-M">
									<span><asp:LinkButton ID="lnkbtnChange" runat="server" onclick="lnkbtnChange_Click"></asp:LinkButton></span>
								</div>
							</div>
						</div>
					</div>
					<div class="Btn-Type3-wp ">
						<div class="Btn-Tp3-L">
							<div class="Btn-Tp3-R">
								<div class="Btn-Tp3-M">
									<span><asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton></span>
								</div>
							</div>
						</div>
					</div>
				</div>
            </td>
        </tr>
    </table>
    <asp:ImageButton runat="server" ID="imgbtnProcess" ImageUrl="~/Common/Images/Common/blank.gif" alt="" ImageAlign="AbsMiddle" OnClick="imgbtnProcess_Click"/>
    <asp:HiddenField ID="hfReturnBox1" runat="server"/>
    <asp:HiddenField ID="hfReturnBox2" runat="server"/>
</div>
</form>
</body>
</html>