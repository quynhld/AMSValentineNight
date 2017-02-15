<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngPaymentView.aspx.cs" Inherits="KN.Web.Management.Manage.MngPaymentView"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="vbscript" type="text/vbscript" src="/Common/VBscript/fnShowReport.vbs"></script>
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckValidate(strAlert, strAlertReceit)
    {
        var strInsPayDay = document.getElementById("<%=hfInsPayDay.ClientID%>");
        var strInsPayment = document.getElementById("<%=txtInsPayment.ClientID%>");
        var strReceit = document.getElementById("<%=chkReceitCd.ClientID%>");

        if (strReceit.checked)
        {
            alert(strAlertReceit);
        }

        if (!strReceit.checked)
        {
            if (trim(strInsPayDay.value) == "") 
            {
                alert(strAlert);
                strInsPayDay.focus();
                return false;
            }
            
            if (trim(strInsPayment.value) == "")
            {
                alert(strAlert);
                strInsPayment.focus();
                return false;
            }
        }

        return true;
    }

    function fnCheckAccoutValidate(strAlertCancel, strData1, strData2, strData3, strData4, strData5)
    {
        var strReturn;
        var strReceit = document.getElementById("<%=chkReceitCd.ClientID%>");

        if (strReceit.checked)
        {
            document.getElementById("<%=imgbtnComplete.ClientID%>").click();

            strReturn = window.showModalDialog("<%=Master.PAGE_POPUP1%>?UserSeq=" + strData1 + "&RentCd=" + strData2 + "&FeeTy=" + strData3 + "&RentalYear=" + strData4 + "&RentalMM=" + strData5, "window", "dialogWidth:500px;dialogHeight:280px;scroll=no;menubar=no;toolbar=no;location=no;status=no;resizable=no;");

            if (strReturn == "FINISHED")
            {
                fnCompleteList();
                return false;
            }
            else
            {
                strReceit.checked = false;
                return false;
            }
        }
        else
        {
            fnCancelList();
            alert(strAlertCancel);
            return false;
        }
    }

    function fnDataDelete()
    {
        document.getElementById("<%=imgbtnDelete.ClientID%>").click();
    }

    function fnPrintOutBill()
    {
        var strUserSeq = document.getElementById("<%=hfUserSeq.ClientID%>").value;
        var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
        var strFeeTy = document.getElementById("<%=hfFeeTy.ClientID%>").value;
        var strYear = document.getElementById("<%=hfRentalYear.ClientID%>").value;
        var strMonth = document.getElementById("<%=hfRentalMM.ClientID%>").value;

        // Datum0 : 입주자번호
        // Datum1 : 섹션코드
        // Datum2 : 관리비/주차비/임대료
        // Datum3 : 거주년
        // Datum4 : 거주월
        if (trim(strFeeTy) == "0001")
        {
            window.open("/Common/RdPopup/RDPopupMngFeeDetail.aspx?Datum0=" + strUserSeq + "&Datum1=" + strRentCd + "&Datum2=" + strFeeTy + "&Datum3=" + strYear + "&Datum4=" + strMonth, "ManageFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }
        else if (trim(strFeeTy) == "0002")
        {
            window.open("/Common/RdPopup/RDPopupRentalFeeDetail.aspx?Datum0=" + strUserSeq + "&Datum1=" + strRentCd + "&Datum2=" + strFeeTy + "&Datum3=" + strYear + "&Datum4=" + strMonth, "RentalFee", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        }
    }

    function fnCompleteList()
    {
        document.getElementById("<%=imgbtnAccount.ClientID%>").click();
    }

    function fnCancelList()
    {
        document.getElementById("<%=imgbtnCancelAcc.ClientID%>").click();
    }
//-->
</script>
<div class="Btn-Type1-wp">
    <div class="Btn-Tp-L">
        <div class="Btn-Tp-R">
            <div class="Btn-Tp-M">
                <span><asp:LinkButton ID="lnkbtnBillPrint" runat="server"></asp:LinkButton></span>
            </div>
        </div>
    </div>
</div>
<div class="C235-wp">
    <div class="Tb-Tp-tit FloatL Mrg0" style=""><asp:Literal ID="ltPaymentTitle" runat="server"></asp:Literal></div>
    <div class="FloatL PL15">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)</div>
</div>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnAccount" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDelete" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnCancelAcc" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <table class="TbCel-Type6">
            <thead>
                <tr>
                    <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltPayLimitDay" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltTotalPay" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltTotalPaid" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltPayNoPaid" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltLateYn" runat="server" Visible="false"></asp:Literal></th>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Literal ID="ltInsFloorRoom" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfFloor" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td align="center"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
                    <td align="center">
                        <asp:TextBox ID="txtInsPayLimitDay" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                        <img alt="Calendar" onclick="Calendar(this, '<%=txtInsPayLimitDay.ClientID%>', '<%=hfInsPayLimitDay.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
                        <asp:HiddenField ID="hfInsPayLimitDay" runat="server"/>
                        <asp:TextBox ID="txtHfOriginLimitDt" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsTotalPay" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfTotalPay" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td align="center"><asp:Literal ID="ltInsPaidAmt" runat="server"></asp:Literal></td>
                    <td align="center"><asp:CheckBox ID="chkReceitCd" runat="server" class="bd0"></asp:CheckBox></td>
                    <td align="center"><asp:CheckBox ID="chkInsLateYn" runat="server" class="bd0" Visible="false"></asp:CheckBox></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltPayDay" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltPay" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></th>
                    <th class="Ls-line" colspan="4"></th>
                </tr>
                <tr>
                    <td>
                        <span runat="server" id="tdInsPayDay">
                            <asp:TextBox ID="txtInsPayDay" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                            <img alt="Calendar" onclick="Calendar(this, '<%=txtInsPayDay.ClientID%>', '<%=hfInsPayDay.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
                            <asp:HiddenField ID="hfInsPayDay" runat="server"/>
                            <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                        </span>
                    </td>
                    <td align="center"><asp:TextBox ID="txtInsPayment" runat="server" CssClass="bgType2" Width="70" MaxLength="21" ></asp:TextBox></td>
                    <td align="center" colspan="3">
                        <asp:DropDownList ID="ddlPaymentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentCd_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="ddlTransfer" runat="server"></asp:DropDownList>                    
                    </td>
                    <td colspan="2"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
                </tr>
            </thead>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="Tb-Tp-tit"><asp:Literal ID="ltPayDetail" runat="server"></asp:Literal></div>
<asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chkReceitCd" EventName="CheckedChanged"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDelete" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnCancelAcc" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvMngPaymentView" runat="server" GroupPlaceholderID="groupPlaceHolderID" GroupItemCount="2" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMngPaymentView_ItemCreated"
            OnLayoutCreated="lvMngPaymentView_LayoutCreated" OnItemDataBound="lvMngPaymentView_ItemDataBound" OnItemDeleting="lvMngPaymentView_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="25%"/>
                    <col width="25%"/>
                    <col width="25%"/>
                    <col width="25%"/>
                    <thead>
                        <tr>
		                    <th class="Fr-line"><asp:Literal ID="ltPay" runat="server"></asp:Literal></th>
		                    <th class="Ls-line"><asp:Literal ID="ltPayDay" runat="server"></asp:Literal></th>
		                    <th class="Fr-line"><asp:Literal ID="ltPay1" runat="server"></asp:Literal></th>
		                    <th class="Ls-line"><asp:Literal ID="ltPayDay1" runat="server"></asp:Literal></th>
	                    </tr>
                    </thead>
                    <tbody>
                        <tr id="groupPlaceHolderID" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr><td id="iphItemPlaceHolderID" runat="server"></td></tr>
            </GroupTemplate>
            <ItemTemplate>
                    <td align="center">
                        <asp:TextBox ID="txtPayList" runat="server" CssClass="bgType2" ReadOnly="true"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" Visible="false"/>
                        <asp:TextBox ID="txtHfItemUserSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemRentCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemFeeTy" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemRentalYear" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemRentalMM" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemPaySeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfItemInsDate" runat="server" Visible="false"></asp:TextBox>
                    </td>
		            <td align="center">
		                <asp:Literal ID="ltPayDayList" runat="server"></asp:Literal>
		                <asp:TextBox ID="txtHfItemPayDt" runat="server" Visible="false"></asp:TextBox>
		                <asp:TextBox ID="txtHfItemPayAmt" runat="server" Visible="false"></asp:TextBox>
		            </td>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type6">
                    <tbody>
                    <tr>
                        <td colspan="2" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="Tb-Tp-tit"><asp:Literal ID="ltItem" runat="server"></asp:Literal></div>
	        <asp:ListView ID="lvMngPaymentViewDetail" runat="server" GroupPlaceholderID="groupPlaceHolderID" GroupItemCount="3" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMngPaymentViewDetail_ItemCreated"
            OnLayoutCreated="lvMngPaymentViewDetail_LayoutCreated" OnItemDataBound="lvMngPaymentViewDetail_ItemDataBound">
            <LayoutTemplate>
                <table class="TbCel-Type6">
                    <tbody>
                        <tr id="groupPlaceHolderID" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr><td id="iphItemPlaceHolderID" runat="server"></td></tr>
            </GroupTemplate>
            <ItemTemplate>
                    <td align="center"><asp:Literal ID="ltMenuList" runat="server"></asp:Literal></td>
		            <td align="center"><asp:Literal ID="ltPaymentList" runat="server"></asp:Literal></td>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                        <tr><td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="Tb-Tp-tit"><asp:Literal ID="ltMemo" runat="server"></asp:Literal></div>
        <table class="TbCel-Type6">
            <tbody>
                <tr><td align="left"><asp:Literal ID="ltMemoText" runat="server"></asp:Literal></td></tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hfCurrentPage" runat="server"/>
        <asp:HiddenField ID="hfAlertText" runat="server"/>
        <asp:HiddenField ID="hfRentalYear" runat="server"/>
        <asp:HiddenField ID="hfRentalMM" runat="server"/>
        <asp:HiddenField ID="hfUserSeq" runat="server"/>
        <asp:HiddenField ID="hfRentCd" runat="server"/>
        <asp:HiddenField ID="hfFeeTy" runat="server"/>
        <asp:TextBox ID="txtHfNowLine" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfTotalPayment" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfVatRatio" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRealBaseRate" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDelete_Click"/>
        <asp:ImageButton ID="imgbtnCancelAcc" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnCancelAcc_Click"/>
        <asp:ImageButton ID="imgbtnAccount" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnAccount_Click"/>
        <asp:ImageButton ID="imgbtnComplete" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnComplete_Click"/>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="Btwps FloatR">
	<div class="Btn-Type3-wp ">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				     <span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
</div>
</asp:Content>