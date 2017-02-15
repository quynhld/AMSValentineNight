<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="TransferStatement.aspx.cs" Inherits="KN.Web.Settlement.Balance.TransferStatement"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--    //
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
    }

    function fnDetailView()
    {  
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
    }
     $.initWindowMsg();        
        

       

       function fnOccupantList() {
            var childWin; 
            var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
            var strInvoiceNo = document.getElementById("<%=hfRefInvoiceNo.ClientID%>").value;
            var strRoomNo = document.getElementById("<%=hfRoomNo.ClientID%>").value;
            var strUserSeq = document.getElementById("<%=hfUserSeq.ClientID%>").value;
            var strMemNo = document.getElementById("<%=hfMemNo.ClientID%>").value;
            var strIP = document.getElementById("<%=hfIP.ClientID%>").value;
            var strRefSeq = document.getElementById("<%=hfRefSeq.ClientID%>").value;
            var strListType = document.getElementById("<%=hfListType.ClientID%>").value;
            var strSlipNo = document.getElementById("<%=hfSlipNo.ClientID%>").value;

             $.initWindowMsg();            
             childWin = window.open("/Common/Popup/PopupCancelTransfer.aspx?Datum0=" + strRentCd + "&Datum1=" + strInvoiceNo + "&Datum2=" + strRoomNo + "&Datum3=" + strUserSeq + "&Datum4=" + strMemNo + "&Datum5=" + strIP + "&Datum6=" + strRefSeq + "&Datum7=" + strListType + "&Datum8=" + strSlipNo, "Cancel Transfer", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
                         
        }

          $(function() {
            $.windowMsg("childMsg1", function(message) {
                alert(message);                
                var button = document.getElementById("<%=lnkbtnSearch.ClientID%>"); //document.getElementByID("Image_Print");
                button.click();
            });          
        });       
 
    function fnMakingLine(strTxt) {
        var strItems = document.getElementById("<%=ddlItems.ClientID%>");
        var strYear = document.getElementById("#txtSearchDt");
        var strMonth = document.getElementById("#txtSearchDt");

        if (trim(strRoomNo.value) != "") {
            if (trim(strItems.value) != "") {
                if (trim(strYear.value) != "") {
                    if (trim(strMonth.value) != "") {
                        return true;
                    }
                    else {
                        alert(strTxt);
                        strMonth.focus();

                        return false;
                    }
                }
                else {
                    alert(strTxt);
                    strYear.focus();

                    return false;
                }
            }
            else {
                alert(strTxt);
                strItems.focus();

                return false;
            }
        }
        else {
            alert(strTxt);
            strRoomNo.focus();

            return false;
        }
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
                <asp:Literal ID="ltIP" runat="server" Text="Invoice/Payment"></asp:Literal>
                <asp:DropDownList ID="ddlItems" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Literal ID="ltRevenueType" runat="server" Text="Revenue Type"></asp:Literal>
                <asp:DropDownList ID="ddlPayType" runat="server">
                </asp:DropDownList>
            </li>
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li>
                <asp:Literal ID="ltStatementDate" runat="server" Text="Statement Date"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtStartDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server" Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
            </li>
            <li>
                <asp:Literal ID="ltSNS" runat="server" Text="Send/Not Send"></asp:Literal>
                <asp:DropDownList ID="ddlSNS" runat="server" OnSelectedIndexChanged="ddlSNS_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" Text="Search" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upList" runat="server" >
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnTran" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnExcel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lknbtnCancel" EventName="Click" />
        </Triggers>
        <ContentTemplate>
                <table class="TbCel-Type4-A" cellpadding="0">
                    <colgroup>
                        <col width="20px" />
                        <col width="70px" />
                        <col width="225px" />
                        <col width="75px" />
                        <col width="70px" />
                        <col width="60px" />
                        <col width="65px" />
                        <col width="60px" />
                        <col width="60px" />
                        <tr>
                            <th class="Fr-line">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Style="text-align: left"
                                    OnCheckedChanged="chkAll_CheckedChanged" />
                            </th>
                            <th>
                                Invoice
                            </th>
                            <th>
                                Tenant
                            </th>
                            <th>
                                Type
                            </th>
                            <th>
                                Total
                            </th>
                            <th>
                                Room
                            </th>
                            <th>
                                Slip No
                            </th>
                            <th>
                                Tr.Emp
                            </th>
                            <th>
                                Tr.Date
                            </th>
                        </tr>
                    </colgroup>
                </table>
                <div style="height: 434px;overflow-y: scroll;width: 840px;overflow-x: visible" >
                    <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                        OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound"
                        OnSelectedIndexChanged="lvPrintoutList_SelectedIndexChanged">
                        <LayoutTemplate>
                            <table cellpadding="0" class="TypeA">
                                <col width="25px" />
                                <col width="70px" />
                                <col width="255px" />
                                <col width="100px" />
                                <col width="80px" />
                                <col width="60px" />
                                <col width="75px" />
                                <col width="80px" />
                                <col width="70px" />
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:CheckBox ID="chkboxList" runat="server" AutoPostBack="false"></asp:CheckBox>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsInvoiceNo" runat="server"></asp:Literal>
                                    <asp:TextBox ID="txtPaymentCode" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtRefSeq" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtBillType" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtExchangeRate" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtUserSeq" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtListType" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsTenant" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsType" runat="server"></asp:Literal>
                                    <asp:TextBox ID="txtFeeType" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td class="Bd-Lt TbTxtRight">
                                    <asp:Literal ID="ltInsTotal" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsSlipNo" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltlnsTrEmp" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsTrDate" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table cellpadding="0" class="TypeA">
                                <col width="25px" />
                                <col width="100px" />
                                <col width="235px" />
                                <col width="115px" />
                                <col width="100px" />
                                <col width="60px" />
                                <col width="75px" />
                                <col width="80px" />
                                <col width="70px" />
                                <tr>
                                    <td colspan="8" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnDetailview_Click" />
            <asp:HiddenField ID="hfRentCd" runat="server" />
            <asp:HiddenField ID="hfRefInvoiceNo" runat="server" />
            <asp:HiddenField ID="hfRoomNo" runat="server" />
            <asp:HiddenField ID="hfUserSeq" runat="server" />
            <asp:HiddenField ID="hfMemNo" runat="server" />
            <asp:HiddenField ID="hfIP" runat="server" />
            <asp:HiddenField ID="hfRefSeq" runat="server" />
            <asp:HiddenField ID="hfListType" runat="server" />
            <asp:HiddenField ID="hfSlipNo" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btwps FloatR">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span>
                            <asp:LinkButton ID="lnkbtnTran" runat="server" Text="Transfer" OnClick="lnkbtnTran_Click"></asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span>
                            <asp:LinkButton ID="lknbtnCancel" runat="server" Text="Cancel" OnClick="lknbtnCancel_Click"></asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span>
                            <asp:LinkButton ID="lnkbtnExcel" runat="server" Text="Export Excel"></asp:LinkButton>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
