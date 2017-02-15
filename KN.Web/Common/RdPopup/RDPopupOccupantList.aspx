<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupOccupantList.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupOccupantList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:1000px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : 임대(RentCd : 아파트, 상가)
            // Datum1 : 방넘버(RoomNo)
            // Datum2 : 이름(UserNm)
            // Datum3 : 시작일(ContDt)
            // Datum4 : 종료일(ResaleDt)
            // Session["MemNm"] : 출력자
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile %>', '/rp [<%=strDatum0%>] [<%=strDatum1%>] [<%=strDatum2%>] [<%=strDatum3%>] [<%=strDatum4%>] [<%=strDatum5%>] [<%=Session["MemNm"].ToString()%>]');
        </script>
    </body>
</html>