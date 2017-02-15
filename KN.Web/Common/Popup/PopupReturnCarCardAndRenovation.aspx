<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupReturnCarCardAndRenovation.aspx.cs" Inherits="KN.Web.Common.Popup.PopupReturnCarCardAndRenovation" ValidateRequest="false"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Return Car Card & Renovation</title>
<link rel="Stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
<script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
<script language="javascript" type="text/javascript" src="/Common/Javascript/jquery-1.8.3.min.js"></script>
<script language="javascript" type="text/javascript" src="/Common/Javascript/jquery-ui.js"></script>
<link rel="stylesheet" href="/Common/Css/jquery-ui.css" />
<script type="text/javascript" src="/Common/Javascript/jquery.ui.monthpicker.js"></script>
<script language="javascript" type="text/javascript">
<!--    //     
    function fnPopupConfirm(strText1, strText2)
    {
        var ddlPaymentTy = document.getElementById("<%=ddlPaymentTy.ClientID%>");
        var txtPayDay = document.getElementById("<%=txtPayDay.ClientID %>");

        if (ddlPaymentTy.value == "0000")
        {
            alert(strText2);
            ddlPaymentTy.focus();
            return false;
        }

        if (trim(txtPayDay.value) == "")
        {
            alert(strText2);
            txtPayDay.focus();
            return false;
        }

        return true;
    }
    $(document).ready(function () {
        callCalendar();
    });
    function callCalendar() {
        $("#<%=txtPayDay.ClientID %>").datepicker({
        });
        $("#<%=txtPayDay.ClientID %>").datepicker("setDate", new Date());
    }

    function closePopup(refSeq) {
        window.opener.reLoadData(refSeq);
        this.close();
    }
//-->
</script>
<base target="_self"/>
</head>
<body>
    <form id="frmPopup" runat="server">
        <div style="width:500px;">
            <table class="TbCel-Type5-C iw500 Mrg0">
                <col width="70px"/>
                <col width="250px"/>
                <col width="200px" />
                <tr><th colspan="2"><asp:Literal ID="ltReturnTitle" runat="server" Text="Return Car Card & Renovation"></asp:Literal></th></tr>
                <tr>
                    <th><asp:Literal ID="ltReturnMethod" runat="server" Text="Return Method"></asp:Literal></th>
                    <td><asp:DropDownList ID="ddlPaymentTy" runat="server"></asp:DropDownList></td>                    
                </tr>
                <tr>
                    <th><asp:Literal ID="ltReturnDate" runat="server" Text="Return Date"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtPayDay" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDay.ClientID%>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" />
                    </td>
                </tr>
            </table>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click" Text="Return"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click" Text="Cancel"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>