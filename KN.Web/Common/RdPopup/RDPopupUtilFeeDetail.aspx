<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupUtilFeeDetail.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupUtilFeeDetail" %>
<html>
    <head>
        <title>ElectricityFee</title>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:700px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : Year
            // Datum1 : Month
            // Datum2 : UserSeq
            // Datum3 : UserNm
            // Datum4 : RoomNo
            // Datum5 : RentCd
            
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv Year[<%=strDatum0%>] Month[<%=strDatum1%>] UserSeq[<%=strDatum2%>] UserNm[<%=strDatum3%>] RoomNo[<%=strDatum4%>] RentCd[<%=strDatum5%>]');
        </script>
    </body>
</html>