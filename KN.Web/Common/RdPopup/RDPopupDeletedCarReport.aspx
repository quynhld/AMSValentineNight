<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupDeletedCarReport.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupDeletedCarReport" %>
<html>
    <head>
        <title>Deleted Car Report</title>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : 입주자번호
            // Datum1 : 섹션코드
            // Datum2 : 관리비/주차비/임대료
            // Datum3 : 거주년
            // Datum4 : 거주월
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv RentCd[<%=strDatum0%>] StartDt[<%=strDatum1%>] EndDt[<%=strDatum2%>] CarTyCd[<%=strDatum3%>] RoomNo[<%=strDatum4%>] CarNo[<%=strDatum5%>] ParkingCardNo[<%=strDatum6%>]');
        </script>
    </body>
</html>