<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupReciptKNDetail.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupReciptKNDetail" %>
<html>
    <head>
        <title></title>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:700px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : 입주자번호
            // Datum1 : 섹션코드
            // Datum2 : 관리비/주차비/임대료
            // Datum3 : 거주년
            // Datum4 : 거주월
             alert('<%=strDatum0%>', '<%=strDatum1%>');
             Rdviewer.AutoAdjust = false;
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv UserSeq[<%=strDatum2%>] RentCd[] BillCd[] DataYear[] DataMonth[] PrintSeq[<%=strDatum0%>] PrintDetSeq[<%=strDatum1%>] MemNm[<%=Session["MemNm"].ToString()%>]');
        </script>
    </body>
</html>