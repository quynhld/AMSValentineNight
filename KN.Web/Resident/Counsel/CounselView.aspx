<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CounselView.aspx.cs" Inherits="KN.Web.Resident.Counsel.CounselView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnConfirmAndPopup(strText, strData1, strData2)
    {
        if (confirm(strText))
        {
            <%=Page.GetPostBackEventReference(imgbtnAddRemark)%>
            window.open("<%=Master.PAGE_POPUP1%>?<%=Master.PARAM_DATA1%>=" + strData1 + "&<%=Master.PARAM_DATA2%>=" + strData2, 'AddRemark', 'status=no, resizable=no, width=550, height=300, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
            return false;
        }
        else
        {
            return false;
        }
    }
    
    function fnPrint() 
    {
        window.print();
    }
//-->
</script>
 <div class="Btn-Type1-wp ">
    <div class="Btn-Tp-L">
        <div class="Btn-Tp-R">
            <div class="Btn-Tp-M">
                <span><a href="#" onclick="javascript:fnPrint();"><asp:Literal ID="ltPrint" runat="server"></asp:Literal></a></span>
            </div>
        </div>
    </div>
</div>

    <div style="overflow-y:scroll;height:500px;">
        <table cellspacing="0" class="TbCel-Type1 iw820">
            <col width="148px"/>
            <col width="178px"/>
            <col width="148px"/>
            <col width="178px"/>
            <tbody>
                <tr>
                    <th><asp:Literal ID="ltTitleCompNm" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th rowspan="2"><asp:Literal ID="ltTitleCompAddr" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltCompAddr" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td colspan="3"><asp:Literal ID="ltCompDetAddr" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleIndus" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltIndustryNm" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleCountry" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCountryNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleGrade" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCompGrade" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleCompTy" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCompTyNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleChargerNm" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltChargerNm" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleTelNo" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleFaxNo" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltFaxNo" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleEmail" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltEmail" runat="server"></asp:Literal></td>
                </tr>
            </tbody>
        </table>
        <div class="Tb-Tp-tit"><asp:Literal ID="ltTitleCounsultTitle1" runat="server"></asp:Literal></div>
        <table cellspacing="0" class="TbCel-Type1 iw820">
            <col width="148px"/>
            <col width="178px"/>
            <col width="148px"/>
            <col width="178px"/>
            <tbody>
                <tr>
                    <th><asp:Literal ID="ltTitleUseArea" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltUsingAreaNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleSMeter1" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleCurrRental" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCurrRentalNm" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleCurrMng" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCurrMngFee" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleCurrFare" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCurrServiceFareNm" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleStaffNo" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltStaffNoNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleCommecingYear" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltContCommeceYear" runat="server"></asp:Literal> <asp:Literal ID="ltTitleYears1" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleContPeriod" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltContPeriodNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleYears2" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th colspan="4" class="brNone cen"><asp:Literal ID="ltTitleCurrParkingNo" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleCar" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCarNoNm" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleMotorBike" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltMotoBikeNoNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleReasonTrans" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltTransferPlanReason" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleBudgetTrans" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltTransferCostBuget" runat="server"></asp:Literal></td>
                </tr>
            </tbody>
        </table>
        <div class="Tb-Tp-tit"><asp:Literal ID="ltTitleCounsultTitle2" runat="server"></asp:Literal></div>
        <table cellspacing="0" class="TbCel-Type1 mn iw820">
            <col width="148px"/>
            <col width="178px"/>
            <col width="148px"/>
            <col width="178px"/>
            <tbody>
                <tr>
                    <th><asp:Literal ID="ltTitleLeaseAreaInNeed" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltLeaseAreaNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleSMeter2" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleExpectedRentals" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltExpectedRentalNm" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleFavorateFloor" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltFavorateFloor" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleFavorateDir" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltFavorateDir" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleCompBudget" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltCompBudget" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleExpectedPeriod" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltExpectedlPeriodNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleYears3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitlePossibleLeasePeriod" runat="server"></asp:Literal></th>
                    <td colspan="3"><asp:Literal ID="ltLeaseYear" runat="server"></asp:Literal>.<asp:Literal ID="ltLeaseMonth" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleFinalDecision" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltDecisionNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleMonth1" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleNeedParkingNo" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltNeedParkNo" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltTitleInternalConst" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltInternalConstNm" runat="server"></asp:Literal> <asp:Literal ID="ltTitleDays1" runat="server"></asp:Literal></td>
                    <th class="lebd"><asp:Literal ID="ltTitleMovingLocate" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltMovingLocationNm" runat="server"></asp:Literal></td>
                </tr>
            </tbody>
        </table>
        <asp:ListView ID="lvRemark" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
            OnItemDataBound="lvRemark_ItemDataBound" OnItemCreated="lvRemark_ItemCreated">
            <LayoutTemplate>
                <table cellspacing="0" class="TbCel-Type1 mn iw820">
                    <col width="148px"/>
                    <col width="178px"/>
                    <col width="148px"/>
                    <col width="178px"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
               <tr>
                    <th>
                        <asp:Literal ID="ltTitleRemark" runat="server"></asp:Literal>
                        <asp:Literal ID="ltInsDate" runat="server"></asp:Literal>
                    </th>
                    <td colspan="3"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" class="TbCel-Type1 mn iw820">
                    <col width="148px"/>
                    <col width="178px"/>
                    <col width="148px"/>
                    <col width="178px"/>
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="ltTitleRemark" runat="server"></asp:Literal>
                            </th>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
  <div class="Btwps FloatR2">
	<div class="Btn-Type2-wp FloatL">
		<div class="Btn-Tp2-L">
			<div class="Btn-Tp2-R">
				<div class="Btn-Tp2-M">
					<span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>

	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span>  <asp:LinkButton ID="lnkbtnAdd" runat="server"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>
	
	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span> <asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>

	<div class="Btn-Type3-wp FloatL">
		<div class="Btn-Tp3-L">
			<div class="Btn-Tp3-R">
				<div class="Btn-Tp3-M">
					<span><asp:LinkButton ID="lnkbtList" runat="server" onclick="lnkbtList_Click"></asp:LinkButton></span>
				</div>
			</div>
		</div>
	</div>

</div>

    <asp:TextBox ID="txtHfCounselCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfCounselSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:ImageButton ID="imgbtnAddRemark" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnAddRemark_Click"/>
</asp:Content>