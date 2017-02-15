<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CashReportByExcel.aspx.cs" Inherits="KN.Web.Settlement.Balance.CashReportByExcel" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender(); 
             $('#cashReportList').tablePagination();
        }
    }        
        <!--//
        $(document).ready(function () {
            LoadCalender();
            $('#cashReportList').tablePagination();
        });

        function LoadCalender() {
            var datetype = $("#rbDateType input:radio:checked").val();
           // alert(datetype);
            if (datetype == "D") {
                datePicker();
            } else if (datetype == "M") {
                monthPicker();
            } else if (datetype == "Y") {
                yearPicker();
            }            
        }

        function datePicker() {
            $("#<%=txtSearchDt.ClientID %>").datepicker();
        }

        function monthPicker() {
            $("#<%=txtSearchDt.ClientID %>").monthpicker();
        }

        function yearPicker() {
             $("#<%=txtSearchDt.ClientID %>").monthpicker();
        }
        //-->
        function fnCheckValidate(strAlertText, strTanVal, strLongVal, strShortVal) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID %>");
            if (trim(txtSearchDt.value) == "") {
                alert(strAlertText);
                txtSearchDt.focus();
                return false;
            }
            return true;
        }
        
        function fnCashReport()
        {
            var datetype = $("#rbDateType input:radio:checked").val();
            var dateval = $("#<%=txtSearchDt.ClientID %>").val().trim().replace('-','');
            var txtSearchYear = dateval.substring(0,4);
            var txtSearchMonth = dateval.substring(4,6);
            var txtSearchFeeType = $('#<%=ddlItems.ClientID %> option:selected').val();
            var rentCode =  $('#<%=txtHfRentCd.ClientID %>').val();
            var txtSearchDay = "";
            if (datetype=="D") {
                 txtSearchDay = dateval.replace('-','').substring(6,8);
            }
            window.open("/Common/RdPopup/RDCashReport.aspx?SearchYear=" + txtSearchYear + "&SearchMonth=" + txtSearchMonth + "&SearchDay=" + txtSearchDay+ "&RentCd=" + rentCode+ "&FeeTy=" + txtSearchFeeType+ "&DateType=" + datetype, "ChestnutReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");            
            return false;
        }
    </script>
    <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10 ">
            <li><asp:Literal ID="ltItem" runat="server" Text="Date"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            <li><asp:Literal ID="Literal1" runat="server" Text="Fee Type"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlItems" runat="server"></asp:DropDownList></li>            
            <li>
                <div id="rbDateType">
                <asp:RadioButtonList ID="rdbDate" runat="server" AutoPostBack="True" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="Y">Yearly</asp:ListItem>
                    <asp:ListItem Value="M" Selected="True">Monthly</asp:ListItem>
                    <asp:ListItem Value="D">Dailly</asp:ListItem>
                </asp:RadioButtonList>
                </div>
            </li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
        </Triggers>
        <ContentTemplate>           
            <table class="TbCel-Type6-A" cellpadding="0" style="overflow-y: scroll;">                
                    <col width="10%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="9%" />
                    <col width="9%" />
                    <col width="9%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="11%" />
                    <col width="10%" />
                <tr>                    
                    <th>Room No</th>
                    <th>Rental Y/M</th>                    
                    <th>Receivable</th>
                    <th>Cash</th>
                    <th>Card</th>
                    <th>Transfer</th>
                    <th>TotalPay</th>
                    <th>Blance</th>
                    <th>Pay Date</th>
                    <th>Fee Type</th>
                </tr>
            </table>
            <div style="overflow-y: scroll; height: 400px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820" id="cashReportList">
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <col width="10%" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltRentalYN" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                               
                                <asp:Literal ID="ltReceivable" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                               
                                <asp:Literal ID="ltCash" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                               
                                <asp:Literal ID="ltCard" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                               
                                <asp:Literal ID="ltTransfer" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                              
                                <asp:Literal ID="ltMonthFee" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight">                               
                                <asp:Literal ID="ltBlance" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                               
                                <asp:Literal ID="ltPaydate" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                               
                                <asp:Literal ID="ltFeeType" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820" >
                            <col width="100%" />
                            <tr>
                                <td colspan="6" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="Type-viewB-PN">
        <col width="30%"/>
        <col width="60%"/>
        <tr>
            <th class="Fr-line"><asp:Literal ID="ltAddonFile" runat="server"></asp:Literal></th>
	        <td>
	            <span class="Ls-line">
	                <asp:FileUpload ID="fuExcelUpload" runat="server"/>
	                <asp:Literal ID="ltSampleFile" runat="server"></asp:Literal>
	                <asp:HyperLink ID="hlExcel" ImageUrl="~/Common/Images/Icon/exell.gif" runat="server" NavigateUrl="#"></asp:HyperLink>
	            </span>
	        </td>
        </tr>
    </table>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp3-L">
	                <div class="Btn-Tp3-R">
		                <div class="Btn-Tp3-M">
			                <span><asp:LinkButton ID="lnkbtnFileUpload" runat="server" OnClick="lnkbtnFileUpload_Click" Text="Upload Excel"></asp:LinkButton></span>
		                </div>
	                </div>
                </div>
            </div>
            <div class="Btn-Type3-wp ">            
                <div class="Btn-Tp2-L">
                    <div class="Btn-Tp2-R">
                        <div class="Btn-Tp2-M">
                            <span>
                            <asp:LinkButton ID="lnkMakeCashReport" runat="server" 
                                Text="Print Cash Report" onclick="lnkMakeCashReport_Click"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Btn-Type3-wp ">            
                <div class="Btn-Tp2-L">
                    <div class="Btn-Tp2-R">
                        <div class="Btn-Tp2-M">
                            <span>
                            <asp:LinkButton ID="lnkExportExcel" runat="server" 
                                Text="Export Excel" onclick="lnkExportExcel_Click"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>    
</asp:Content>
