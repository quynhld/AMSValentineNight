<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupMngComplete.aspx.cs" Inherits="KN.Web.Common.Popup.PopupMngComplete" ValidateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Management & Rental Fee</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript">
<!--//
    function fnPopupConfirm(strText1)
    {
        var strReason = document.getElementById("<%=txtCompleteReason.ClientID %>");

        if (trim(strReason.value) == "")
        {
            alert(strText1);
            strReason.focus();
            
            return false;
        }

        return true;
    }
//-->
</script>
<base target="_self"/>
</head>
<body>
    <form id="frmPopup" runat="server">
        <div style="width:500px;height:270px">
            <table class="TbCel-Type5-C iw500 Mrg0">
                <col width="150px"/>
                <col width="350px"/>
                <tr><th colspan="2"><asp:Literal ID="ltReason" runat="server"></asp:Literal></th></tr>
                <tr>
                    <th><asp:Literal ID="ltContents" runat="server"></asp:Literal></th>
                    <td><asp:TextBox ID="txtCompleteReason" runat="server" Columns="50" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
