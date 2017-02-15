<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupRentContractList.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupRentContractList"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
    <table>
        <tr>
            <td width="20px"><img src="../Images/Btn/btn_blue_print.gif" alt="Print" onclick="rd_Print()"/> </td>
            <td width="20px"><img src="../Images/Common/disk.gif" alt="Save" onclick="rd_Save()" /> </td>
            <td width="20px"><img src="../Images/Icon/Magnifier.gif" alt="Find" onclick="rd_Find()" /> </td>
            <td width="20px"><img src="../Images/Common/plus.gif" alt="Zoom In" onclick="rd_ZoomIn()" /> </td>
            <td width="20px"><img src="../Images/Common/minus.gif" alt="Zoom Out" onclick="rd_ZoomOut()" /> </td>
            <td width="20px"><img src="../Images/Btn/prev2.gif" alt="First Page" onclick="rd_First()" /> </td>
            <td width="20px"><img src="../Images/Btn/prev.gif" alt="Previous Page" onclick="rd_Prev()" /> </td>
            <td width="20px"><img src="../Images/Btn/next.gif" alt="Next Page" onclick="rd_Next()" /> </td>
            <td width="20px"><img src="../Images/Btn/next2.gif" alt="Last Page" onclick="rd_Last()" /> </td>
            <td width="20px"><img src="../Images/Icon/exell.gif" alt="Export to Excel" onclick="rd_Excel()" /> </td>
            <td width="20px"><img src="../Images/Icon/word.gif" alt="Export to Word" onclick="rd_Word()" /> </td>
            <td width="20px"><img src="../Images/Icon/ppt.gif" alt="Export to Powerpoint" onclick="rd_PPT()" /> </td>
            <td width="20px"><img src="../Images/Icon/pdf.gif" alt="Export to PDF" onclick="rd_PDF()" /> </td>
        </tr>
    </table>
        <script language="javascript">
            function rd_Print()
            {
                Rdviewer.PrintDialog();
                
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
        </script>
    <script language="javascript" src="embeded.js" type="text/javascript"></script>
    <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : 검색종류
            // Datum1 : 검색어
            // Datum2 : 계약여부
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.HideToolbar();
        
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rp [<%=strDatum0%>] [<%=strDatum1%>] [<%=strDatum2%>] [<%=strDatum3%>] [<%=Session["MemNm"].ToString()%>]');
    </script>
</body>
</html>