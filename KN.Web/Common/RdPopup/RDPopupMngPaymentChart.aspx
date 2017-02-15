<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupMngPaymentChart.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupMngPaymentChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
    <script language="javascript" src="embeded.js" type="text/javascript"></script>
    <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
        // Session["LangCd"] : 언어코드 (0003:한글, xxxx:영어, xxxx: 베트남어)
        // Datum0 : RentCd
        // Datum1 : FeeTy
        // Datum2 : FloorNo
        // Datum3 : RoomNo
        // Datum4 : TenantNm
        // Datum5 : ReceitCd
        // Datum6 : LateFeeYn
        // Datum7 : RentalYear
        // Datum8 : RentalMM
        // Session["MemNm"] : 출력자
        Rdviewer.AutoAdjust = false
        Rdviewer.ZoomRatio = 100;
        Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rv RentalCd[<%=strDatum0%>] FeeTy[<%=strDatum1%>] RentalYear[<%=strDatum2%>] RentalMM[<%=strDatum3%>]');
    </script>
</body>
</html>