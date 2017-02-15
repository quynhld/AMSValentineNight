<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupAcntMMParkingFeeList.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupAcntMMParkingFeeList" %>
<html>
<head>
    <title></title>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
    <script language="javascript" src="embeded.js" type="text/javascript"></script>
    <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
        // Session["LangCd"] : 언어코드 (0003:한글, 0002:영어, 0001: 베트남어)
        // Datum0 : Year
        // Datum1 : Month
        // Datum2 : FloorNo
        // Datum3 : RoomNo
        // Datum4 : ParkingCarNo
        // Datum5 : ParkingCardNo
        // Datum6 : ReceitCd
        Rdviewer.AutoAdjust = false
        Rdviewer.ZoomRatio = 100;
        Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rv PrintYear[<%=strDatum0%>] PrintMonth[<%=strDatum1%>] PrintCarTyCd[<%=strDatum3%>] RentCd[<%=strDatum2%>] MemNm[<%=Session["MemNm"].ToString()%>]');
    </script>
</body>
</html>