<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupEnergyDetail.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupEnergyDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>Energy Detail</title>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:730px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv UserSeq[<%=USER_SEQ%>] RentCd[<%=RENT_CD%>] RoomNo[<%=ROOM_NO%>] YearMM[<%=YEAR_MM%>]');
        </script>
    </body>
</html>