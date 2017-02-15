<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="AccountMonthParkingFeeList.aspx.cs" Inherits="KN.Web.Park.AccountMonthParkingFeeList"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--        //
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
               // document.getElementById("<%=txtStartDt.ClientID%>").value = document.getElementById("<%=hfStartDt.ClientID%>").value;
                LoadCalender();                
                
            }
        }

        function fnCheckType() {
            if (event.keyCode == 13) {
                document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
                return false;
            }
        }

        function fnSearchCheckValidate(strText) {
            var strYear = document.getElementById("<%=ddlSearchYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlSearchMonth.ClientID%>");
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strCarNo = document.getElementById("<%=txtSearchCarNo.ClientID%>");
            var strCardNo = document.getElementById("<%=txtSearchCardNo.ClientID%>");

            if (trim(strYear.value) == "" && trim(strMonth.value) == "" && trim(strSearchRoom.value) == "" && trim(strCarNo.value) == "" && trim(strCardNo.value) == "") {
                alert(strText);
                return false;
            }

            return true;
        }

        function fnSearchCheckValidateNew(strText) {
            var strYear = document.getElementById("<%=ddlSearchYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlSearchMonth.ClientID%>");
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strCarNo = document.getElementById("<%=txtSearchCarNo.ClientID%>");
            var strCardNo = document.getElementById("<%=txtSearchCardNo.ClientID%>");

            if (trim(strSearchRoom.value) == "" ) {
                alert(strText);
                document.getElementById("<%=txtSearchRoom.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function fnOccupantList(strSeq) {
            var strPayDt = document.getElementById("<%=hfpayDate.ClientID%>").value; 
            window.open("/Common/RdPopup/RDPopupParkingReciptDebit.aspx?Datum0=" + strSeq + "&Datum1=" + strPayDt, "PrintRecipt", "status=no, resizable=yes, width=750, height=860, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
            return false;
        }

        function fnReConfirm(strText) {
            if (confirm(strText)) {
                document.getElementById("<%=imgbtnDelMonthInfo.ClientID%>").click();
            }
            else {
                document.getElementById("<%=imgbtnCancel.ClientID%>").click();
            }
        }

        function fnMonthlyParkingList() {
            var strData1 = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
            var strData2 = document.getElementById("<%=ddlSearchMonth.ClientID%>").value;
            var strData3 = document.getElementById("<%=ddlRentCd.ClientID%>").value;
            var strData4 = document.getElementById("<%=ddlCarTy.ClientID%>").value;

            window.open("/Common/RdPopup/RDPopupAcntMMParkingFeeList.aspx?Datum0=" + strData1 + "&Datum1=" + strData2 + "&Datum2=" + strData3 + "&Datum3=" + strData4, "MonthlyParkingList", "status=no, resizable=no, width=740, height=700, left=100, top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

            return false;
        }

        function fnRoomChange() {
            document.getElementById("<%=imgbtnRoomChange.ClientID%>").click();
        }

        function fnCalendarChange() {
            document.getElementById("<%=imgbtnCardNoChange.ClientID%>").click();
            return false;
        }

        function fnOpenCalendar(obj) {
            document.getElementById('<%=txtStartDt.ClientID%>').focus();
            Calendar(obj, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true);

            return false;
        }

        function LoadCalender() {
           
            datePicker();        
        }

        function datePicker() {
            $("#<%=txtPayDt.ClientID %>").datepicker();           
            $("#<%=txtStartDt.ClientID %>").datepicker({
                altField: "#<%=hfStartDt.ClientID %>"
            });

            $('#<%=hfStartDt.ClientID %>').live('change', function () { fnCalendarChange(); });

        }


        function fnRegistCheckValidate(strTxt) {
            var strRoomNo = document.getElementById("<%=txtRegRoomNo.ClientID%>");
            var strCarNo = document.getElementById("<%=ddlCarNo.ClientID%>");
            var strParkingFee = document.getElementById("<%=txtParkingFee.ClientID%>");
            var strCardFee = document.getElementById("<%=txtCardFee.ClientID%>");
            var strPaymentCd = document.getElementById("<%=ddlPaymentCd.ClientID%>");
            var strTransfer = document.getElementById("<%=ddlTransfer.ClientID%>");
            var strGateList = document.getElementById("<%=hfGateList.ClientID%>");

            if (strRoomNo.value == "") {
                document.getElementById("<%=txtRegRoomNo.ClientID%>").focus();
                alert(strTxt);

                return false;
            }

            if (strCarNo.value == "") {
                document.getElementById("<%=ddlCarNo.ClientID%>").focus();
                alert(strTxt);

                return false;
            }

            if (strGateList.value == "" || strGateList.value == "0000") {
                strGateList.focus();
                alert(strTxt);

                return false;
            }

            if (strParkingFee.value == "") {
                document.getElementById("<%=txtParkingFee.ClientID%>").focus();
                alert(strTxt);

                return false;
            }

            if (strCardFee.value == "") {
                document.getElementById("<%=txtCardFee.ClientID%>").focus();
                alert(strTxt);

                return false;
            }

            if (strPaymentCd.value == "0000") {
                document.getElementById("<%=ddlPaymentCd.ClientID%>").focus();
                alert(strTxt);

                return false;
            }
            else if (strPaymentCd.value == "0003") {
                if (strTransfer.value == "") {
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
    
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30 ">
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlRentCd" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCarTy" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSearchYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSearchMonth" runat="server">
                    </asp:DropDownList>
                </div>
            </li>
            <li>
                <div class="Btn-Type1-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnReport" runat="server" OnClientClick="javascript:return fnMonthlyParkingList();"></asp:LinkButton></span>
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
                                    <asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>            
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li>
                <asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltSearchCarNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCarNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <asp:Literal ID="ltSearchCardNo" runat="server"></asp:Literal></li>
            <li>
                <asp:TextBox ID="txtSearchCardNo" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"
                    OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>

             <li>
                <div class="TpAtit1">
                    <div class="FloatR">
                        (<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal
                            ID="ltRealBaseRate" runat="server"></asp:Literal>)<asp:TextBox ID="hfRealBaseRate"
                                runat="server" Visible="false"></asp:TextBox></div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnPrint" EventName="Click" />  
            <asp:AsyncPostBackTrigger ControlID="imgbtnDelMonthInfo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRoomChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCardNoChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />               
            <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />       
        </Triggers>
        <ContentTemplate>
    <table class="TypeA">
        <col width="22" />
        <col width="60" />
        <col width="80" />
        <col width="180" />
        <col width="110" />
        <col width="105" />
        <col width="100" />
        <col width="100" />
        <col width="105" />
        <thead>
            <tr>
                <th >
                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" nCheckedChanged="chkAll_CheckedChanged"
                    Style="text-align: left" oncheckedchanged="chkAll_CheckedChanged" />
            </th>
                <th class="Fr-line" align="center">
                    <asp:Literal ID="ltTopYearMM" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopFloorRoom" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopCarNo" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopCarTyCd" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopPaymentCd" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopFee" runat="server"></asp:Literal>
                </th>
                <th align="center">
                    <asp:Literal ID="ltTopPayDt" runat="server"></asp:Literal>
                </th>
                <th class="Ls-line" align="center">
                    &nbsp;
                </th>
            </tr>
        </thead>
    </table>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 138px; width: 840px;">     
                <asp:ListView ID="lvActMonthParkingFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnItemCreated="lvActMonthParkingFeeList_ItemCreated" OnItemDataBound="lvActMonthParkingFeeList_ItemDataBound"
                    OnItemDeleting="lvActMonthParkingFeeList_ItemDeleting">
                    <LayoutTemplate>
                        <table class="TypeA">
                            <col width="20" />
                            <col width="60" />
                            <col width="80" />
                            <col width="180" />
                            <col width="100" />
                            <col width="100" />
                            <col width="100" />
                            <col width="100" />
                            <col width="100" />
                            <tbody>
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                             <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkboxList" runat="server">
                                </asp:CheckBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltYearMM" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfParkingYear" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfParkingMonth" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfFloor" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltCarNo" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfCardNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltCarTyCd" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfCarTyCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltFee" runat="server"></asp:Literal>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:Literal ID="ltPayDt" runat="server"></asp:Literal>
                            </td>
                            <td class="TbTxtCenter">
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" />
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPayDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfYear" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfMM" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA">
                            <tbody>
                                <tr>
                                    <td colspan="8" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ImageButton ID="imgbtnDelMonthInfo" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                    OnClick="imgbtnDelMonthInfo_Click" />
                <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                    OnClick="imgbtnCancel_Click" />
                <asp:TextBox ID="txtHfDelDebitCreditCd" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelPaymentDt" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelUserSeq" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelUserDetSeq" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelRoomNo" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelCardNo" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelRentCd" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfDelCarTyCd" runat="server" Visible="false"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upRegist" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCarNo" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDuringMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdobtnParkingDays" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRoomChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCardNoChange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDelMonthInfo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtCardFee" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtParkingFee" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkAll" EventName="CheckedChanged" />  
            <asp:AsyncPostBackTrigger ControlID="lnkbtnPrint" EventName="Click" />  

        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1 iw840">
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegRent" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltRentCd" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRegRoomNo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtRegRoomNo" runat="server" CssClass="bgType2" 
                            onchange="javascript:return fnRoomChange();" MaxLength="20" Width="100"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRegParkingCarNo" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlCarNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCarNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltGateTy" runat="server"></asp:Literal>
                    </th>
                    <td colspan="2">
                        <asp:CheckBoxList ID="chkGateList" runat="server" BorderWidth="0" CellPadding="0"
                            CellSpacing="0" RepeatLayout="Flow" OnSelectedIndexChanged="chkGateList_SelectedIndexChanged">
                        </asp:CheckBoxList>
                        <asp:HiddenField ID="hfGateList" runat="server" />
                    </td>
                    <td>
                        <th align="center">
                            <asp:Literal ID="ltPayDt" runat="server" Text="Payment Date"></asp:Literal>
                        </th>
                        <td>
                            <asp:TextBox ID="txtPayDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                                Visible="True" ontextchanged="txtPayDt_TextChanged"></asp:TextBox>
                            <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPayDt.ClientID%>')"
                                src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                        </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegCarTy" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:Literal ID="ltCarTy" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfCarTy" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRegParkingCardNo" runat="server"></asp:Literal>
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="ltParkingCardNo" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfParkingCardNo" runat="server" Visible="false"></asp:TextBox>
                        <asp:HiddenField ID="hfUserSeq" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfFloorNo" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hfParkingTagNo" runat="server"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegStartDt" runat="server"></asp:Literal>
                    </th>
                    <td colspan="5">
                        <asp:TextBox ID="txtStartDt" runat="server" Width="70px" ></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  />

                        
                        <asp:HiddenField ID="hfStartDt" runat="server" />
                        <asp:DropDownList ID="ddlDuringMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDuringMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RadioButtonList ID="rdobtnParkingDays" runat="server" AutoPostBack="true" RepeatLayout="Flow"
                            BorderWidth="0" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rdobtnParkingDays_SelectedIndexChanged">
                        </asp:RadioButtonList>
                        <asp:TextBox ID="txtEndDt" runat="server" Width="70px"></asp:TextBox>
                        <asp:HiddenField ID="hfEndDt" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegCardFee" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCardFee" runat="server" CssClass="bgType2" MaxLength="18" Width="100"
                            AutoPostBack="true" OnTextChanged="txtCardFee_TextChanged"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRegParkingFee" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtParkingFee" runat="server" CssClass="bgType2" MaxLength="20"
                            Width="100" AutoPostBack="true" OnTextChanged="txtParkingFee_TextChanged"></asp:TextBox>
                        <asp:TextBox ID="txtHfMonthlyFee" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="ltRegTotalFee" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtTotalFee" runat="server" CssClass="bgType2" MaxLength="20" Width="100"
                            ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtHfTotalFee" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltRegPaymentCd" runat="server"></asp:Literal>
                    </th>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlPaymentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentCd_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlTransfer" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="imgbtnRoomChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnRoomChange_Click" />
            <asp:ImageButton ID="imgbtnCardNoChange" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnCardNoChange_Click" />
             <asp:HiddenField ID="hfpayDate" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Btwps FloatR">
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
			        <span><asp:LinkButton ID="lnkbtnPrint" runat="server" onclick="lnkbtnPrint_Click1">Print Receipt</asp:LinkButton></span>
		        </div>
	        </div>
        </div>
    </div>    
</div>
    
</asp:Content>
