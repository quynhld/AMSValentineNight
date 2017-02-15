﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ManuallySettingPrintList_Test.aspx.cs" Inherits="KN.Web.Settlement.Balance.ManuallySettingPrintList_Test"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                LoadCalender();
            }
        }
        function LoadCalender() {
            datePicker();
        }

        function datePicker() {
            $("#<%=txtStartDt.ClientID %>").datepicker();
            $("#<%=txtEndDt.ClientID %>").datepicker();
            $("#<%=txtReqDt.ClientID %>").datepicker();
			$("#<%=lnkPrint.ClientID %>").bind("click", function () {
			    ShowLoading("Making data........");
			});   
			$("#<%=lnkbtnSearch.ClientID %>").bind("click", function () {
			    ShowLoading("Loading data ......");			    
			}); 
            
			$("#<%=imgbtnLoadData.ClientID %>").bind("click", function () {
			    ShowLoading("Loading data ......");			    
			});    
            
			$("#<%=lnkbtnCancel.ClientID %>").bind("click", function () {
			    ShowLoading("Canceling data ......");			    
			});  
        }

        function fnOccupantList(strSeq) {
            CloseLoading();
            var strReqDt = document.getElementById("<%=hfreqDate.ClientID%>").value;
            var strPayType = document.getElementById("<%=ddlUV.ClientID%>").value;
            var rentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
            window.open("/Common/RdPopup/RDPopupDebitList1.aspx?Datum0=" + strSeq + "&Datum1=" + strReqDt + "&Datum2=" + strPayType+ "&Datum3=" + rentCd, "PrintInvoice", "status=no, resizable=yes, width=750, height=860, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            return false;
        }

        $(document).ready(function () {
             LoadCalender();
        });

        $(function () {
            $.windowMsg("childMsg1", function (message) {
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = message;                
                     
                <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
                return false;
            });
        });  
    function fnDetailView(refSeq)
    {
        $('#<%=hfRefSeq.ClientID %>').val(refSeq);
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;        
        return false;
    }

    function fnLoadData() {
        var strItems = document.getElementById("<%=ddlPrintYN.ClientID%>");
        if (strItems.value=="N") {
            var button = document.getElementById("<%=lnkbtnSearch.ClientID%>");
            button.click();
        }
    }
    //-->
    </script>
    <div class="Tab-wp MrgB10">
        <ul class="TabM">
            <li class="title">
                <asp:LinkButton ID="lbtNotCreated" runat="server" Text="Not Created Debit Note" OnClick="lbtNotCreated_Click"></asp:LinkButton></li>
            <li class="title" style="display: none">
                <asp:LinkButton ID="lnkCreatedNote" runat="server" Text="Created Debit Note" OnClick="lnkCreatedNote_Click"></asp:LinkButton></li>
            <li class="Over CursorNon" style="width: 180px">Printing List</li>
        </ul>
    </div>
    <fieldset class="sh-field61">
        <legend>검색</legend>
        <ul class="sf61-ag MrgL10 ">
            <li>
                <asp:Literal ID="ltRoomNo" runat="server" Text="Room No"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="8" Width="100"></asp:TextBox>
            </li>
            <li>
                <asp:Literal ID="ltTenant" runat="server" Text="Tenant Name"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtTenant" runat="server"> </asp:TextBox>
            </li>
            <li>
                <asp:Literal ID="ltPrintedYN" runat="server" Text="Printed YN"></asp:Literal>
            </li>
            <li>
                <asp:DropDownList ID="ddlPrintYN" runat="server">
                </asp:DropDownList>
            </li>
        </ul>
    </fieldset>
    <fieldset class="sh-field62">
        <legend>검색</legend>
        <ul class="sf62-ag MrgL10 ">
            <li>
                <asp:Literal ID="ltItem" runat="server" Text="Fee Type"></asp:Literal>
            </li>
            <li>
                <asp:DropDownList ID="ddlItems" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Literal ID="ltPeriod" runat="server" Text="Period"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtStartDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server" Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
            </li>
            <li><span>~</span></li>
            <li>
                <asp:TextBox ID="txtEndDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtEndDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
            </li>
            <li>
                <li>
                    <div class="Btn-Type4-wp">
                        <div class="Btn-Tp4-L">
                            <div class="Btn-Tp4-R">
                                <div class="Btn-Tp4-M">
                                    <span>
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span></div>
                            </div>
                        </div>
                    </div>
                </li>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkPrint" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnLoadData" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPrintYN" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="15px" />
                    <col width="70px" />
                    <col width="50px" />
                    <col width="170px" />
                    <col width="120px" />
                    <col width="60px" />
                    <col width="50px" />
                    <col width="50px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"
                                Style="text-align: left" />
                        </th>
                        <th>
                            Fee Type
                        </th>
                        <th>
                            Room
                        </th>
                        <th>
                            Company Name
                        </th>
                        <th>
                            Period
                        </th>
                        <th>
                            Total Amt
                        </th>
                        <th>
                            Discount
                        </th>
                        <th>
                            Invoice
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 400px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated"
                    OnItemDataBound="lvPrintoutList_ItemDataBound" OnSelectedIndexChanged="lvPrintoutList_SelectedIndexChanged">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="823">
                            <col width="2%" />
                            <col width="10%" />
                            <col width="5%" />
                            <col width="25%" />
                            <col width="15%" />
                            <col width="5%" />
                            <col width="5%" />
                            <col width="5%" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#ffffff'">
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkboxList" runat="server">
                                </asp:CheckBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txthfFeeTypeCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfContractNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfBundleSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtDebitCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRefSerialNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtPrintBundleNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtRef_Seq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtLink" runat="server" Visible="false"></asp:TextBox>
                                <asp:Literal ID="ltFeeType" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter" onclick='javascript:fnDetailView("<%#Eval("REF_SEQ")%>")' style="cursor:pointer">
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight2">
                                <asp:Literal ID="ltTotalAmount" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight2">
                                <asp:Literal ID="ltDiscount" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight2">
                                <asp:Literal ID="ltInvoice" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="200px" />
                            <col width="80px" />
                            <col width="80px" />
                            <tr>
                                <td colspan="7" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div class="Btwps31 FloatR2">
                <div class="Btn-Type31-wp ">
                    <div class="Btn-Tp31-L">
                        <div class="Btn-Tp31-R">
                            <div class="Btn-Tp31-M">
                                <span>
                                    <asp:LinkButton ID="lnkPrint" runat="server" Text="Print" OnClick="lnkPrint_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type31-wp ">
                    <div class="Btn-Tp31-L">
                        <div class="Btn-Tp31-R">
                            <div class="Btn-Tp31-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click" Text="Cancel"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Btwps FloatR2">
                <span></span>
                <li>
                    <asp:Literal ID="ltPaymentType" runat="server" Text="Payment Type"></asp:Literal>
                </li>
                <li>
                    <asp:DropDownList ID="ddlUV" runat="server" Width="120px" Height="20px">
                    </asp:DropDownList>
                </li>
            </div>
            <div class="Btwps FloatR2">
                <span></span>
                <li>
                    <asp:Literal ID="ltReqDt" runat="server" Text="Request Date"></asp:Literal>
                </li>
                <li>
                    <asp:TextBox ID="txtReqDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                        Visible="True" OnTextChanged="txtReqDt_TextChanged"></asp:TextBox>
                    <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtReqDt.ClientID%>')"
                        src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                </li>
                <span></span>
            </div>
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="hfRentCd" runat="server" />
            <asp:HiddenField ID="hfRefSeq" runat="server" />
            <asp:HiddenField ID="hfLink" runat="server" />
            <asp:HiddenField ID="txthfPrintBundleNo" runat="server" />
            <asp:HiddenField ID="hfStartDt" runat="server" />
            <asp:HiddenField ID="hfEndDt" runat="server" />
            <asp:HiddenField ID="hfsendParam" runat="server" />
            <asp:HiddenField ID="hfreqDate" runat="server" />
            <asp:ImageButton ID="imgbtnLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnLoadData_Click" />
            <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnDetailview_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <style type="text/css">
        .Tab-wp .TabM li.title
        {
            width: 180px;
        }
    </style>
</asp:Content>