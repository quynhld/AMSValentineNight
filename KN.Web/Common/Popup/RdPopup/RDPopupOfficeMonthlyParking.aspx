<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupOfficeMonthlyParking.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupOfficeMonthlyParking" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>일일정산표</title>
</head>
 <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Session["LangCd"] : 언어코드 (0003:한글, xxxx:영어, xxxx: 베트남어)
            // Datum0 : 시작일
            // Datum1 : 종료일
            // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
            // Datum3 : 임대(RentCd : 아파트, 상가)
            // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)
            // Datum5 : 출력자
            Rdviewer.AutoAdjust = false;
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rv RoomNo[<%=strDatum0 %>] TenantNm[<%=strDatum1 %>] Period[<%=strDatum2 %>] %>]');
        </script>
    </body>
</html>