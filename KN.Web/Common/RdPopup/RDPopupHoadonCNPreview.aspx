<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupHoadonCNPreview.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupHoadonCNPreview" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>Hoadon Preview</title>
        <link rel="stylesheet" type="text/css" href="/Common/Css/keangnam.css"/>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/jquery-1.8.3.min.js"></script>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/jquery.windowmsg-1.0.js"></script>
        <script language="javascript" type="text/javascript" src="/Common/Javascript/Global.js"></script>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:600px; width:723px;">
        <form id="form1" runat="server"> 
            <table class="TypeA-shorter">
                <tr>
                    <td width="1px"><asp:ImageButton ID="imgbtnPrint" runat= "server" ImageUrl="~/Common/Images/Common/nav-rpBg2.gif" onclick="imgbtnPrint_Click"/></td>
                    <td width="70px"><img src="../Images/Btn/btn_blue_print.gif" alt="Print" onclick="rd_Print()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Common/disk.gif" alt="Save" onclick="rd_Save()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Icon/Magnifier.gif" alt="Find" onclick="rd_Find()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Common/plus.gif" alt="Zoom In" onclick="rd_ZoomIn()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Common/minus.gif" alt="Zoom Out" onclick="rd_ZoomOut()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Btn/prev2.gif" alt="First Page" onclick="rd_First()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Btn/prev.gif" alt="Previous Page" onclick="rd_Prev()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Btn/next.gif" alt="Next Page" onclick="rd_Next()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Btn/next2.gif" alt="Last Page" onclick="rd_Last()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Icon/exell.gif" alt="Export to Excel" onclick="rd_Excel()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Icon/word.gif" alt="Export to Word" onclick="rd_Word()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Icon/ppt.gif" alt="Export to Powerpoint" onclick="rd_PPT()" style="cursor:pointer;"/></td>
                    <td width="20px"><img src="../Images/Icon/pdf.gif" alt="Export to PDF" onclick="rd_PDF()" style="cursor:pointer;"/></td>
                </tr>
            </table>
        </form>
        <script language="javascript" type="text/javascript">
        <!--//
            function rd_Print()
            {    
                var button = document.getElementById("<%=imgbtnPrint.ClientID%>"); //document.getElementByID("Image_Print");
                button.click();
                Rdviewer.PrintDialog();
                window.opener.Func1('<%=DOC_NO%>');
                this.close();
            }

            function rd_Save()
            {
                Rdviewer.SaveAsDialog();
            }

            function rd_Find()
            {
                Rdviewer.FindDialog();
            }

            function rd_ZoomIn()
            {
                Rdviewer.ZoomIn();
            }

            function rd_ZoomOut()
            {
                Rdviewer.ZoomOut();
            }

            function rd_First()
            {
                Rdviewer.FirstPage();
            }

            function rd_Prev()
            {
                Rdviewer.PrevPage();
            }

            function rd_Next()
            {
                Rdviewer.NextPage();
            }

            function rd_Last()
            {
                Rdviewer.LastPage();
            }

            function rd_Excel()
            {
                Rdviewer.ViewExcel();
            }

            function rd_Word()
            {
                Rdviewer.ViewWord();
            }

            function rd_PPT()
            {
                Rdviewer.ViewPpt();
            }

            function rd_PDF()
            {
                Rdviewer.ViewPDF();
            }
        //-->
        </script>
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            Rdviewer.AutoAdjust = true;
            Rdviewer.ZoomRatio = 100;
            Rdviewer.HideToolbar();
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv TempDocNo[<%=DOC_NO%>]');
        </script>
        <asp:TextBox ID="txtHfDocNo" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfPayDt" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    </body>
</html>