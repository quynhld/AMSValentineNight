<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupKNReceiptList.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupKNReceiptList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
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
            // Datum5 : 프린트순번
            // Datum6 : 프린트세부순번
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            //Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/MngPostRecipt.mrd', '/rv StatusCd[0001] UserSeq[<%=strDatum0%>] RentCd[<%=strDatum1%>] BillCd[<%=strDatum2%>] DataYear[<%=strDatum3%>] DataMonth[<%=strDatum4%>]');
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv UserSeq[<%=strDatum0%>] RentCd[<%=strDatum1%>] BillCd[<%=strDatum2%>] DataYear[<%=strDatum3%>] DataMonth[<%=strDatum4%>] PrintSeq[<%=strDatum5%>] PrintDetSeq[<%=strDatum6%>] MemNm[<%=Session["MemNm"].ToString()%>]');
        </script>
    </body>
</html>