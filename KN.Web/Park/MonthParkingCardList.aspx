<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthParkingCardList.aspx.cs" Inherits="KN.Web.Park.MonthParkingCardList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--    //

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender();
            
        }
    }

    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            return false;
        }
    }

    function fnSelectCheckValidate(strText)
    {
        var strRoomNo = document.getElementById("<%=txtInsRoomNo.ClientID%>");
        var strCarNo = document.getElementById("<%=txtInsCarNo.ClientID%>");
        var strCardNo = document.getElementById("<%=txtInsCardNo.ClientID%>");

        if (trim(strRoomNo.value) == "" && trim(strCarNo.value) == "" && trim(strCardNo.value) == "")
        {
            alert(strText);
            return false;
        }

        return true;
    }

    function fnAccountList(rentcd, room, carno, cardno, langcd) {
        // Datum0 : 시작일
        // Datum1 : 종료일
        // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
        // Datum3 : 임대(RentCd : 아파트, 상가)
        // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)

        var strRoomNo = document.getElementById("<%=txtInsRoomNo.ClientID%>").value;


        window.open("/Common/RdPopup/RDPopupAptParkingList.aspx?Datum0=" + rentcd + "&Datum1=" + room + "&Datum2=" + carno + "&Datum3=" + cardno + "&Datum4=" + langcd, "ParkingAptDebit", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
        return false;
    }


    function fnRentChange()
    {
        var strRentCd = document.getElementById("<%=ddlRegRentCd.ClientID%>");
        var strRoomNo = document.getElementById("<%=txtRegRoomNo.ClientID%>");

        if (trim(strRentCd.value) != "0000" && trim(strRoomNo.value) != "")
        {
            document.getElementById("<%=imgbtnRentChange.ClientID%>").click();
            document.getElementById("<%=txtCarNo.ClientID%>").focus();
        }
        else if (trim(strRentCd.value) == "0000" && trim(strRoomNo.value) != "")
        {
            document.getElementById("<%=ddlRegRentCd.ClientID%>").focus();
        }
        else if (trim(strRentCd.value) != "0000" && trim(strRoomNo.value) == "")
        {
            document.getElementById("<%=txtRegRoomNo.ClientID%>").focus();
        }

        return false;
    }

    function fnCardNoChange()
    {
        var strCarTy = document.getElementById("<%=ddlCarTy.ClientID%>");
        var strCardNo = document.getElementById("<%=txtParkingCardNo.ClientID%>");

        if (trim(strCarTy.value) != "0000" && trim(strCardNo.value) != "")
        {
            document.getElementById("<%=imgbtnCardNoChange.ClientID%>").click();
            document.getElementById("<%=ddlDuringMonth.ClientID%>").focus();
        }
        else
        {
            document.getElementById("<%=imgbtnCardNoChange.ClientID%>").click();
        }

        return false;
    }

    function fnCalendarChange()
    {
        document.getElementById("<%=imgbtnCardNoChange.ClientID%>").click();
        return false;
    }

    function fnOpenCalendar(obj)
    {
        document.getElementById('<%=txtStartDt.ClientID%>').focus();
        //CallCalendar(obj, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true);

        return false;
    }

    function LoadCalender() {
        datePicker();
        document.getElementById("<%=txtStartDt.ClientID%>").value = document.getElementById("<%=hfStartDt.ClientID%>").value;
    }

    function datePicker() {
        $("#<%=txtPayDt.ClientID %>").datepicker();

        $("#<%=txtStartDt.ClientID %>").datepicker({
            altField: "#<%=hfStartDt.ClientID %>"
        });
        
        
    }

    function fnCheckValidate(strTxt)
    {
        var strRentCd = document.getElementById("<%=ddlRegRentCd.ClientID%>");
        var strRoomNo = document.getElementById("<%=txtRegRoomNo.ClientID%>");
        var strCarNo = document.getElementById("<%=txtCarNo.ClientID%>");
        var strCarTy = document.getElementById("<%=ddlCarTy.ClientID%>");
        var strCardNo = document.getElementById("<%=txtParkingCardNo.ClientID%>");
        var strParkingFee = document.getElementById("<%=txtParkingFee.ClientID%>");
        var strCardFee = document.getElementById("<%=txtCardFee.ClientID%>");
        var strPaymentCd = document.getElementById("<%=ddlPaymentCd.ClientID%>");
        var strTransfer = document.getElementById("<%=ddlTransfer.ClientID%>");
        var strGateList = document.getElementById("<%=hfGateList.ClientID%>");

        if (strRentCd.value == "0000")
        {
            document.getElementById("<%=ddlRegRentCd.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strRoomNo.value == "")
        {
            document.getElementById("<%=txtRegRoomNo.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strCarNo.value == "")
        {
            document.getElementById("<%=txtCarNo.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strCarTy.value == "0000")
        {
            document.getElementById("<%=ddlCarTy.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strGateList.value == "" || strGateList.value == "0000")
        {
            strGateList.focus();
            alert(strTxt);

            return false;
        }

        if (strCardNo.value == "")
        {
            document.getElementById("<%=txtParkingCardNo.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strParkingFee.value == "")
        {
            document.getElementById("<%=txtParkingFee.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strCardFee.value == "")
        {
            document.getElementById("<%=txtCardFee.ClientID%>").focus();
            alert(strTxt);

            return false;
        }

        if (strPaymentCd.value == "0000")
        {
            document.getElementById("<%=ddlPaymentCd.ClientID%>").focus();
            alert(strTxt);

            return false;
        }
        else if (strPaymentCd.value == "0003")
        {
            if (strTransfer.value == "")
            {
                document.getElementById("<%=ddlTransfer.ClientID%>").focus();
                alert(strTxt);
                
                return false;
            }
        }

        return true;
    }

   
    $(document).ready(function () {
        LoadCalender();
    });

    //-->
</script>
<div class="TpAtit1">
    <div class="FloatR">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate" runat="server" Visible="false"></asp:TextBox></div>
</div>
<fieldset class="sh-field2 MrgB10">
    <legend>검색</legend>
    <ul class="sf2-ag MrgL30">        
        <li><asp:DropDownList ID="ddlInsRentCd" runat="server"></asp:DropDownList></li>
        <li><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtInsRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
        <li><asp:Literal ID="ltInsCardNo" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtInsCardNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
        <li><asp:Literal ID="ltInsCarNo" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtInsCarNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
        <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>		        
        </li>
        <li>
             <div class="Btn-Type1-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnReport" runat="server" Text = "Report" 
                                    onclick="lnkbtnReport_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
    </ul>
</fieldset>
<table class="TypeA" width="840px">
    <col width="40"/>
    <col width="80"/>
    <col width="150"/>
    <col width="100"/>
    <col width="140"/>
    <col width="140"/>
    <col width="100"/>
    <col width="90"/>
    <thead>
        <tr>
            <th class="Fr-line" align="center"><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTopName" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTopCarNo" runat="server"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopCardNo" runat="server"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopCardFee" runat="server"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopCarTy" runat="server"></asp:Literal></th>
            <th class="Ls-line" align="center">&nbsp;</th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:200px;width:840px;">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnReport" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>  
        <asp:AsyncPostBackTrigger ControlID="imgbtnCardNoChange" EventName="Click" />  
        <asp:AsyncPostBackTrigger ControlID="txtParkingFee" EventName="TextChanged"/>

    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound"
                OnItemUpdating="lvActMonthParkingCardList_ItemUpdating" 
            OnItemDeleting="lvActMonthParkingCardList_ItemDeleting" 
            onselectedindexchanged="lvActMonthParkingCardList_SelectedIndexChanged">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="40"/>
                    <col width="80"/>
                    <col width="150"/>
                    <col width="100"/>
                    <col width="130"/>
                    <col width="130"/>
                    <col width="100"/>
                    <col width="90"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="TbTxtCenter" rowspan="2"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter"><asp:Literal ID="ltName" runat="server"></asp:Literal></td>
		            <td class="TbTxtCenter"><asp:TextBox ID="txtCarNo" runat="server" MaxLength="13" Width="90"></asp:TextBox></td>
		            <td class="TbTxtCenter">
		                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="16" Width="90"></asp:TextBox>
		                <asp:TextBox ID="txtHfTagNo" runat="server" Visible="false"></asp:TextBox>
		            </td>
		            <td class="TbTxtCenter">
		                <asp:TextBox ID="txtCardFee" runat="server" MaxLength="16" Width="90"></asp:TextBox>
		            </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltCarTyNm" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfCarTyCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfUserDetSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfParkingYYYYMM" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfPayDt" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter" rowspan="2">
                        <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/>
                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:CheckBoxList ID="chkGateCd" runat="server" AutoPostBack="true" BorderWidth="0" CellPadding="0" CellSpacing="0" RepeatLayout="Flow" OnSelectedIndexChanged="chkGateCd_SelectedIndexChanged"></asp:CheckBoxList>
	                    <asp:HiddenField ID="hfGateCd" runat="server" />
	                    <asp:TextBox ID="txtHfGateCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <asp:HiddenField ID="hfSelectedLine" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnReport" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRentChange" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnCardNoChange" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="ddlDuringMonth" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="ddlPaymentCd" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtCardFee" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtParkingFee" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="txtStartDt" EventName="TextChanged"/>
        <asp:AsyncPostBackTrigger ControlID="rdobtnParkingDays" EventName="SelectedIndexChanged" />     
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0"  class="TbCel-Type1B iw840">
	        <tr>
	            <th align="center"><asp:Literal ID="ltRegRent" runat="server"></asp:Literal></th>
                <td><asp:DropDownList ID="ddlRegRentCd" runat="server" onchange="javascript:return fnRentChange();"></asp:DropDownList></td>
	            <th align="center"><asp:Literal ID="ltRegRoomNo" runat="server"></asp:Literal></th>
                <td><asp:TextBox ID="txtRegRoomNo" runat="server" CssClass="bgType2" onblur="javascript:return fnRentChange();" onchange="javascript:return fnRentChange();" MaxLength="20" Width="100"></asp:TextBox></td>
	            <th align="center"><asp:Literal ID="ltRegParkingCarNo" runat="server"></asp:Literal></th>
                <td>
                    <asp:TextBox ID="txtCarNo" runat="server" CssClass="bgType2" MaxLength="20" Width="100"></asp:TextBox>
                    <asp:HiddenField ID="hfCarNo" runat="server"></asp:HiddenField>
                </td>
	        </tr>
            <tr>
                <th align="center"><asp:Literal ID="ltRegCarTy" runat="server"></asp:Literal></th>
                <td><asp:DropDownList ID="ddlCarTy" runat="server" onchange="javascript:return fnCardNoChange();"></asp:DropDownList></td>
	            <th align="center"><asp:Literal ID="ltRegParkingCardNo" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:TextBox ID="txtParkingCardNo" runat="server" CssClass="bgType2" onblur="javascript:return fnCardNoChange();" onchange="javascript:return fnCardNoChange();" MaxLength="10" Width="60"></asp:TextBox>
                    <asp:HiddenField ID="hfUserSeq" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfFloorNo" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfParkingTagNo" runat="server"></asp:HiddenField>
                </td>
	        </tr>
	        <tr>
	            <th align="center"><asp:Literal ID="ltGateTy" runat="server"></asp:Literal></th>
	            <td colspan="2">
	                <asp:CheckBoxList ID="chkGateList" runat="server" AutoPostBack="true" BorderWidth="0" CellPadding="0" CellSpacing="0" RepeatLayout="Flow" OnSelectedIndexChanged="chkGateList_SelectedIndexChanged"></asp:CheckBoxList>
	                <asp:HiddenField ID="hfGateList" runat="server" />
	            </td>
                 <td>
                 <th align="center"><asp:Literal ID="ltPayDt" runat="server" Text="Payment Date"></asp:Literal></th>
                    <td >
		                 <asp:TextBox ID="txtPayDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" 
                             runat="server" Visible="True"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDt.ClientID%>')"
                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"   />
                </td>
	        </tr>
            <tr>
                <th align="center"><asp:Literal ID="ltRegStartDt" runat="server"></asp:Literal></th>
                <td colspan="3">
		            <asp:TextBox ID="txtStartDt" runat="server" Width="70px" 
                        ontextchanged="txtStartDt_TextChanged" AutoPostBack = "true" ></asp:TextBox>
                    <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  />		           

		            <asp:HiddenField ID="hfStartDt" runat="server"/>
		            <asp:DropDownList ID="ddlDuringMonth" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="rdobtnParkingDays_SelectedIndexChanged"></asp:DropDownList>
		            <asp:RadioButtonList ID="rdobtnParkingDays" runat="server" AutoPostBack="true" RepeatLayout="Flow" BorderWidth="0" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rdobtnParkingDays_SelectedIndexChanged"></asp:RadioButtonList>
		            <asp:TextBox ID="txtEndDt" runat="server" Width="70px"></asp:TextBox>
		            <asp:HiddenField ID="hfEndDt" runat="server"/>
                </td>
                 <th align="center">
                        <asp:Literal ID="ltlFree" runat="server" Text="Free"></asp:Literal>
                    </th>
                 <td>
                        <asp:RadioButtonList ID="rbMoneyFree" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" 
                            onselectedindexchanged="rbMoneyFree_SelectedIndexChanged">
                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
	            <th align="center"><asp:Literal ID="ltRegCardFee" runat="server"></asp:Literal></th>
                <td><asp:TextBox ID="txtCardFee" runat="server" CssClass="bgType2" MaxLength="18" Width="100" AutoPostBack="true" OnTextChanged="txtCardFee_TextChanged"></asp:TextBox></td>
	            <th align="center"><asp:Literal ID="ltRegParkingFee" runat="server"></asp:Literal></th>
                <td>
                    <asp:TextBox ID="txtParkingFee" runat="server" CssClass="bgType2" MaxLength="20" Width="100" AutoPostBack="true" OnTextChanged="txtParkingFee_TextChanged"></asp:TextBox>
                    <asp:TextBox ID="txtHfMonthlyFee" runat="server" Visible="false"></asp:TextBox>
                </td>
                <th align="center"><asp:Literal ID="ltRegTotalFee" runat="server"></asp:Literal></th>
                <td>
                    <asp:TextBox ID="txtTotalFee" runat="server" CssClass="bgType2" MaxLength="20" Width="100" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtHfTotalFee" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
           	    <th align="center"><asp:Literal ID="ltRegPaymentCd" runat="server"></asp:Literal></th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlPaymentCd" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaymentCd_SelectedIndexChanged" Height="20px"></asp:DropDownList>
                    <asp:DropDownList ID="ddlTransfer" runat="server"></asp:DropDownList>
                </td>
                <td colspan="2">
                    <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
	                            </div>
                            </div>
                        </div>
                    </div>
                                        <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
	                            <div class="Btn-Tp3-M">
		                            <span><asp:LinkButton ID="lnkBtnImportExcel" runat="server" Text="Import Excel" 
                                        onclick="lnkBtnImportExcel_Click" ></asp:LinkButton></span>
	                            </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="imgbtnRentChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnRentChange_Click"/>
        <asp:ImageButton ID="imgbtnCardNoChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnCardNoChange_Click"/>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
