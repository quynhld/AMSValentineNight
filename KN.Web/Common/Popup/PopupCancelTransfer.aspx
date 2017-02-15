<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupCancelTransfer.aspx.cs" Inherits="KN.Web.Common.Popup.PopupCancelTransfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Contract Delete</title>
        <link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/jquery-1.8.3.min.js"></script>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/jquery.windowmsg-1.0.js"></script>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>

<base target="_self"/>
</head>
<body>
   <script language="javascript" type="text/javascript">
        <!--       //
       function rd_Close() {
           alert("Cancel Transfer Statement Successfully !");

       }
       function rd_Close1() {
           this.close();
       }


       function CancelSuccess() {
           $.initWindowMsg();
           $.triggerParentEvent("childMsg1", 'Cancel Transfer Statement Successfully !');
           this.close();
       }

        
    //-->
       </script>
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript"></script>
    <form id="frmPopup" runat="server">
        <div style="width:500px;">
            <table class="TbCel-Type5-C iw500 Mrg0">
                <col width="50px"/>
                <col width="250px"/>
                <col width="200px" />
                <tr><th colspan="2"><asp:Literal ID="ltCancelTitle" runat="server"></asp:Literal></th></tr>
                <tr>
                    <th><asp:Literal ID="ltReasonType" runat="server"></asp:Literal></th>
                    <td><asp:DropDownList ID="ddlReason" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltContents" runat="server"></asp:Literal></th>
                    <td><asp:TextBox ID="txtCancellReason" runat="server" Columns="50" Rows="10" 
                            TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </table>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnOK" runat="server" 
                                    onclick="lnkbtnOK_Click" >OK</asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M"><span><asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                    onclick="lnkbtnCancel_Click" >Cancel</asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>      
            <asp:HiddenField ID="hfRentCd" runat="server" />
            <asp:HiddenField ID="hfRefInvoiceNo" runat="server" />
            <asp:HiddenField ID="hfRoomNo" runat="server" />
            <asp:HiddenField ID="hfUserSeq" runat="server" />
            <asp:HiddenField ID="hfMemNo" runat="server" />
            <asp:HiddenField ID="hfIP" runat="server" />
            <asp:HiddenField ID="hfRefSeq" runat="server" />
            <asp:HiddenField ID="hfListType" runat="server" />
            <asp:HiddenField ID="hfSlipNo" runat="server" />
    </form>
</body>
</html>