﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupMonthParkingList.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupMonthParkingList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
    <script language="javascript" src="embeded.js" type="text/javascript"></script>
    <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
        // Datum0 : StartDt
        // Datum1 : EndDt
        // Datum2 : LangCd : 언어코드 (0003:한글, 0002:영어, 0001: 베트남어)
        Rdviewer.AutoAdjust = false
        Rdviewer.ZoomRatio = 100;
        Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rv StartDt[<%=strDatum0%>] EndDt[<%=strDatum1%>] LangCd[<%=strDatum2%>] MemNm[<%=Session["MemNm"].ToString()%>]');
    </script>
</body>
</html>