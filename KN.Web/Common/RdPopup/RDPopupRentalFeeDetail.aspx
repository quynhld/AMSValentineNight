<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDPopupRentalFeeDetail.aspx.cs" Inherits="KN.Web.Common.RdPopup.RDPopupRentalFeeDetail" %>
<html>
    <head>
        <title></title>
    </head>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="height:700px; width:700px;">
        <script language="javascript" src="embeded.js" type="text/javascript"></script>
        <script language="javascript" id="clientEventHandlersJS" type="text/javascript">
            // Datum0 : UserSeq
            // Datum1 : RentCd
            // Datum2 : FeeTy
            // Datum3 : RentalYear
            // Datum4 : RentalMM
            Rdviewer.AutoAdjust = false
            Rdviewer.ZoomRatio = 100;
            Rdviewer.FileOpen('<%=NOW_DOMAIN%>/Common/Mrd/<%=strMRDFile%>', '/rv UserSeq[] RentCd[<%=strDatum1%>] FeeTy[<%=strDatum2%>] RentalYear[<%=strDatum3%>] RentalMM[<%=strDatum4%>] MemNm[<%=Session["MemNm"].ToString()%>]');
        </script>
    </body>
</html>
