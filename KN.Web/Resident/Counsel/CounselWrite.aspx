<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CounselWrite.aspx.cs" Inherits="KN.Web.Resident.Counsel.CounselWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckValidate(strAlertText, strEtcVal)
    {
        var strIndusCd = document.getElementById("<%=ddlIndus.ClientID%>");
        var strIndusEtcNm = document.getElementById("<%=txtEtcIndus.ClientID%>");
        var strCountryCd = document.getElementById("<%=ddlCountry.ClientID%>");
        var strCountryEtcNm = document.getElementById("<%=txtEtcCountry.ClientID%>");
        var strCompNm = document.getElementById("<%=txtCompNm.ClientID%>");
        var strUseAreaCd = document.getElementById("<%=ddlUseArea.ClientID%>");
        var strUseAreaEtcNm = document.getElementById("<%=txtEtcUseArea.ClientID%>");
        var strCurrRentalCd = document.getElementById("<%=ddlCurrRental.ClientID%>");
        var strCurrRentalEtcNm = document.getElementById("<%=txtEtcCurrRental.ClientID%>");
        var strLeaseAreaInNeedCd = document.getElementById("<%=ddlLeaseAreaInNeed.ClientID%>");
        var strLeaseAreaInNeedEtcNm = document.getElementById("<%=txtEtcLeaseAreaInNeed.ClientID%>");
        
        var strCompAddr = document.getElementById("<%=txtCompAddr.ClientID%>");
        var strCompDetAddr = document.getElementById("<%=txtCompDetAddr.ClientID%>");
        var strGrade = document.getElementById("<%=txtGrade.ClientID%>");
        var strChargerNm = document.getElementById("<%=txtChargerNm.ClientID%>");
        var strTelNo = document.getElementById("<%=txtTelNo.ClientID%>");
        var strTelFrontNo = document.getElementById("<%=txtTelFrontNo.ClientID%>");
        var strTelMidNo = document.getElementById("<%=txtTelMidNo.ClientID%>");
        var strTelRearNo = document.getElementById("<%=txtTelRearNo.ClientID%>");
        var strFaxNo = document.getElementById("<%=txtFaxNo.ClientID%>");
        var strFaxFrontNo = document.getElementById("<%=txtFaxFrontNo.ClientID%>");
        var strFaxMidNo = document.getElementById("<%=txtFaxMidNo.ClientID%>");
        var strFaxRearNo = document.getElementById("<%=txtFaxRearNo.ClientID%>");
        var strEmailID = document.getElementById("<%=txtEmailID.ClientID%>");
        var strEmailServer = document.getElementById("<%=txtEmailServer.ClientID%>");      
        var strReasonTrans = document.getElementById("<%=txtReasonTrans.ClientID%>");
        var strBudgetTrans = document.getElementById("<%=txtBudgetTrans.ClientID%>");
        var strFavorateFloor = document.getElementById("<%=txtFavorateFloor.ClientID%>");
        var strFavorateDir = document.getElementById("<%=txtFavorateDir.ClientID%>");
        var strCompBudgert = document.getElementById("<%=txtCompBudgert.ClientID%>");
        var strNeedParkingNo = document.getElementById("<%=txtNeedParkingNo.ClientID%>");
        var strRemark = document.getElementById("<%=txtRemark.ClientID%>");
   
        if (strIndusCd.value == strEtcVal)
        {
            if (trim(strIndusEtcNm.value) == "")
            {
                alert(strAlertText);
                strIndusEtcNm.focus();
                return false;
            }
        }

        if (strCountryCd.value == strEtcVal)
        {
            if (trim(strCountryEtcNm.value) == "")
            {
                alert(strAlertText);
                strCountryEtcNm.focus();
                return false;
            }
        }

        if (trim(strCompNm.value) == "")
        {
            alert(strAlertText);
            strCompNm.focus();
            return false;
        }

        if (strUseAreaCd.value == strEtcVal)
        {
            if (trim(strUseAreaEtcNm.value) == "")
            {
                alert(strAlertText);
                strUseAreaEtcNm.focus();
                return false;
            }
        }

        if (strCurrRentalCd.value == strEtcVal)
        {
            if (trim(strCurrRentalEtcNm.value) == "")
            {
                alert(strAlertText);
                strCurrRentalEtcNm.focus();
                return false;
            }
        }

        if (strLeaseAreaInNeedCd.value == strEtcVal)
        {
            if (trim(strLeaseAreaInNeedEtcNm.value) == "")
            {
                alert(strAlertText);
                strLeaseAreaInNeedEtcNm.focus();
                return false;
            }
        }


        if (trim(strCompAddr.value) == "") 
        {
            alert(strAlertText);
            strCompAddr.focus();
            return false;
        }

        if (trim(strCompDetAddr.value) == "") 
        {
            alert(strAlertText);
            strCompDetAddr.focus();
            return false;
        }

        if (trim(strGrade.value) == "") 
        {
            alert(strAlertText);
            strGrade.focus();
            return false;
        }

        if (trim(strChargerNm.value) == "") 
        {
            alert(strAlertText);
            strChargerNm.focus();
            return false;
        }

        if (trim(strTelNo.value) == "") 
        {
            alert(strAlertText);
            strTelNo.focus();
            return false;
        }

        if (trim(strTelFrontNo.value) == "") 
        {
            alert(strAlertText);
            strTelFrontNo.focus();
            return false;
        }

        if (trim(strTelMidNo.value) == "") 
        {
            alert(strAlertText);
            strTelMidNo.focus();
            return false;
        }

        if (trim(strTelRearNo.value) == "") 
        {
            alert(strAlertText);
            strTelRearNo.focus();
            return false;
        }

        if (trim(strFaxNo.value) == "") 
        {
            alert(strAlertText);
            strFaxNo.focus();
            return false;
        }

        if (trim(strFaxFrontNo.value) == "") 
        {
            alert(strAlertText);
            strFaxFrontNo.focus();
            return false;
        }

        if (trim(strFaxMidNo.value) == "") 
        {
            alert(strAlertText);
            strFaxMidNo.focus();
            return false;
        }

        if (trim(strFaxRearNo.value) == "") 
        {
            alert(strAlertText);
            strFaxRearNo.focus();
            return false;
        }

        if (trim(strEmailID.value) == "") 
        {
            alert(strAlertText);
            strEmailID.focus();
            return false;
        }

        if (trim(strEmailServer.value) == "") 
        {
            alert(strAlertText);
            strEmailServer.focus();
            return false;
        }

        if (trim(strReasonTrans.value) == "") 
        {
            alert(strAlertText);
            strReasonTrans.focus();
            return false;
        }

        if (trim(strBudgetTrans.value) == "") 
        {
            alert(strAlertText);
            strBudgetTrans.focus();
            return false;
        }

        if (trim(strFavorateFloor.value) == "") 
        {
            alert(strAlertText);
            strFavorateFloor.focus();
            return false;
        }

        if (trim(strFavorateDir.value) == "") 
        {
            alert(strAlertText);
            strFavorateDir.focus();
            return false;
        }

        if (trim(strCompBudgert.value) == "") 
        {
            alert(strAlertText);
            strCompBudgert.focus();
            return false;
        }

        if (trim(strNeedParkingNo.value) == "") 
        {
            alert(strAlertText);
            strNeedParkingNo.focus();
            return false;
        }

        if (trim(strRemark.value) == "") 
        {
            alert(strAlertText);
            strRemark.focus();
            return false;
        }

        return true;
    }
//-->
</script>
<asp:UpdatePanel ID="upPanel" UpdateMode="Conditional" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlIndus" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCompTy" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlUseArea" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCurrRental" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCurrFare" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlStaffNo" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCommecingYear" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlContPeriod" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlCar" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlMotorBike" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlLeaseAreaInNeed" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlExpectedRentals" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlExpectedPeriod" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlFinalDecision" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlInternalConst" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlMovingLocate" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="TbCel-Type1 iw820">
            <colgroup>
                <col width="140px"/>
                <col width="185px"/>
                <col width="140px"/>
                <col width="185px"/>
                <tbody>
                    <tr>
                        <th rowspan="2"><asp:Literal ID="ltTitleCompAddr" runat="server"></asp:Literal></th>
                        <td colspan="3"><asp:TextBox ID="txtCompAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:TextBox ID="txtCompDetAddr" runat="server" MaxLength="255" Width="480" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleIndus" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlIndus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIndus_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcIndus" runat="server" Width="200" MaxLength="100" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>    
                        <th><asp:Literal ID="ltTitleCountry" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcCountry" runat="server" Width="200" MaxLength="100" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCompNm" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtCompNm" runat="server" Width="160" MaxLength="255" CssClass="bgType2"></asp:TextBox></td>
                        <th class="lebd"><asp:Literal ID="ltTitleGrade" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtGrade" runat="server" Width="20" MaxLength="1" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>    
                        <th><asp:Literal ID="ltTitleCompTy" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlCompTy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompTy_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcCompTy" runat="server" Width="240" MaxLength="100" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleChargerNm" runat="server"></asp:Literal></th>
                        <td style="width: 148px"><asp:TextBox ID="txtChargerNm" runat="server" Width="140" MaxLength="200" CssClass="bgType2"></asp:TextBox></td>
                        <th class="lebd"><asp:Literal ID="ltTitleTelNo" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtTelNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>)<asp:TextBox ID="txtTelFrontNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>-<asp:TextBox ID="txtTelMidNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>-<asp:TextBox ID="txtTelRearNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleFaxNo" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtFaxNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>)<asp:TextBox ID="txtFaxFrontNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>-<asp:TextBox ID="txtFaxMidNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox>-<asp:TextBox ID="txtFaxRearNo" runat="server" Width="30" MaxLength="4" CssClass="bgType2"></asp:TextBox></td>
                        <th class="lebd"><asp:Literal ID="ltTitleEmail" runat="server"></asp:Literal></th>
                        <td>
                            <asp:TextBox ID="txtEmailID" runat="server" Width="60" MaxLength="50" CssClass="bgType2"></asp:TextBox>&nbsp;@
                            <asp:TextBox ID="txtEmailServer" runat="server" Width="60" MaxLength="50" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </colgroup>
        </table>
	    <div class="Tb-Tp-tit"><asp:Literal ID="ltTitleCounsultTitle1" runat="server"></asp:Literal></div>
        <table cellspacing="0" class="TbCel-Type1 iw820">
            <colgroup>
                <col width="140px"/>
                <col width="185px"/>
                <col width="140px"/>
                <col width="185px"/>
                <tbody>
                    <tr>
                        <th><asp:Literal ID="ltTitleUseArea" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlUseArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUseArea_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcUseArea" runat="server" Width="50" MaxLength="8" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleSMeter1" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCurrRental" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlCurrRental" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrRental_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcCurrRental" runat="server" Width="60" MaxLength="9" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDollar1" runat="server"></asp:Literal>
                        </td>
                        <th colspan="lebd"><asp:Literal ID="ltTitleCurrMng" runat="server"></asp:Literal></th>
                        <td>
                            <asp:TextBox ID="txtCurrMng" runat="server" Width="60" MaxLength="9" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDollar5" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCurrFare" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlCurrFare" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrFare_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcCurrFare" runat="server" Width="80" MaxLength="9" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleStaffNo" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlStaffNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcStaffNo" runat="server" Width="40" MaxLength="5" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCommecingYear" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlCommecingYear" runat="server"></asp:DropDownList>
                        </td>
                        <th class="lebd"><asp:Literal ID="ltTitleContPeriod" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlContPeriod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlContPeriod_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcContPeriod" runat="server" Width="20" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleYears1" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th class="brNone cen" colspan="4"><asp:Literal ID="ltTitleCurrParkingNo" runat="server"></asp:Literal></th>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCar" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlCar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCar_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcCar" runat="server" Width="20" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                        <th class="lebd"><asp:Literal ID="ltTitleMotorBike" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlMotorBike" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMotorBike_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcMotorBike" runat="server" Width="20" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleReasonTrans" runat="server"></asp:Literal></th>
                        <td colspan="3"><asp:TextBox ID="txtReasonTrans" runat="server" Columns="55" Rows="2" MaxLength="500" TextMode="MultiLine" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleBudgetTrans" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:TextBox ID="txtBudgetTrans" runat="server" Width="80" MaxLength="9" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDollar2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </colgroup>
        </table>
    	<div class="Tb-Tp-tit"><asp:Literal ID="ltTitleCounsultTitle2" runat="server"></asp:Literal></div>
        <table cellspacing="0" class="TbCel-Type1 iw820">
            <colgroup>
                <col width="148px"/>
                <col width="178px"/>
                <col width="148px"/>
                <col width="178px"/>
                <tbody>
                    <tr>
                        <th><asp:Literal ID="ltTitleLeaseAreaInNeed" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlLeaseAreaInNeed" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaseAreaInNeed_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcLeaseAreaInNeed" runat="server" Width="80" MaxLength="9" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleSMeter2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleExpectedRentals" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlExpectedRentals" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExpectedRentals_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcExpectedRentals" runat="server" Width="80" MaxLength="9" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDollar3" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleFavorateFloor" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtFavorateFloor" runat="server" Width="80" MaxLength="9" CssClass="bgType2"></asp:TextBox></td>
                        <th class="lebd"><asp:Literal ID="ltTitleFavorateDir" runat="server"></asp:Literal></th>
                        <td><asp:TextBox ID="txtFavorateDir" runat="server" Width="30" MaxLength="3" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleCompBudget" runat="server"></asp:Literal></th>
                        <td>
                            <asp:TextBox ID="txtCompBudgert" runat="server" Width="80" MaxLength="9" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDollar4" runat="server"></asp:Literal>
                        </td>
                        <th class="lebd"><asp:Literal ID="ltTitleExpectedPeriod" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlExpectedPeriod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExpectedPeriod_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcExpectedPeriod" runat="server" Width="20" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleYears3" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitlePossibleLeasePeriod" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                            <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleFinalDecision" runat="server"></asp:Literal></th>
                        <td>
                            <asp:DropDownList ID="ddlFinalDecision" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFinalDecision_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcFinalDecision" runat="server" Width="20" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleMonth2" runat="server"></asp:Literal>
                        </td>
                        <th class="lebd"><asp:Literal ID="ltTitleNeedParkingNo" runat="server"></asp:Literal></th>
                        <td>
                            <asp:TextBox ID="txtNeedParkingNo" runat="server" Width="20" MaxLength="2" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleInternalConst" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlInternalConst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInternalConst_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcInternalConst" runat="server" Width="30" MaxLength="3" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                            <asp:Literal ID="ltTitleDays" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleMovingLocate" runat="server"></asp:Literal></th>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlMovingLocate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMovingLocate_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;<asp:TextBox ID="txtEtcMovingLocate" runat="server" Width="240" MaxLength="200" ReadOnly="true" Enabled="false" CssClass="bgType2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th><asp:Literal ID="ltTitleRemark" runat="server"></asp:Literal></th>
                        <td colspan="3"><asp:TextBox ID="txtRemark" runat="server" Columns="55" Rows="3" TextMode="MultiLine" CssClass="bgType2"></asp:TextBox></td>
                    </tr>
                </tbody>
            </colgroup>
        </table>
        <div class="Btwps FloatR">
	        <div class="Btn-Type2-wp FloatL">
		        <div class="Btn-Tp2-L">
			        <div class="Btn-Tp2-R">
				        <div class="Btn-Tp2-M">
					        <span>  <asp:LinkButton ID="lnkbtnWrite" runat="server" onclick="lnkbtnWrite_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>

	        <div class="Btn-Type3-wp FloatL">
		        <div class="Btn-Tp3-L">
			        <div class="Btn-Tp3-R">
				        <div class="Btn-Tp3-M">
					        <span>    <asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:TextBox ID="txtHfCounselCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>