<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="IncomeParkingList.aspx.cs" Inherits="KN.Web.Settlement.Balance.IncomeParkingList"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
<!--//
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                LoadCalender(); 
            }
        }

       function LoadCalender() {
            datePicker();
           
            var options = {
                rowsPerPage: 13
            };
            $('#tblListDebit').tablePagination(options); 
        }

        
        function datePicker() {
            $("#<%=txtStartDt.ClientID %>").datepicker();
            $("#<%=txtEndDt.ClientID %>").datepicker();
        }

function fnMovePage(intPageNo) 
{
    if (intPageNo == null) 
    {
        intPageNo = 1;
    }   
    
    document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
    <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
}

function fnAccountList(strTotalSend,rentCd)
{
    // Datum0 : 시작일
    // Datum1 : 종료일
    // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
    // Datum3 : 임대(RentCd : 아파트, 상가)
    // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)

    var strStartDt = document.getElementById("<%=txtStartDt.ClientID%>").value;
    var strEndDt = document.getElementById("<%=txtEndDt.ClientID%>").value; 
      
    var strPayment = document.getElementById("<%=ddlPayment.ClientID%>").value;
   
    
    if (strStartDt != "")
    {
        strStartDt = strStartDt.replace(/\-/gi,"");
    }
    
    if (strEndDt != "")
    {
        strEndDt = strEndDt.replace(/\-/gi,"");
    }
    
    window.open("/Common/RdPopup/RDPopupAptDailyParkingList.aspx?Datum0=" + strStartDt + "&Datum1=" + strEndDt + "&Datum2=" + strPayment + "&Datum3=" + strTotalSend + "&Datum4=" + rentCd, "IncomeParkingFeeList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
    return false;
}

 $(document).ready(function() {
     LoadCalender();
 });
//-->
    </script>
    <asp:UpdatePanel ID="upSearch1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport" />
            <asp:PostBackTrigger ControlID="lnkbtnReceiptList" />
        </Triggers>
        <ContentTemplate>           
            <fieldset class="sh-field2">
                <ul class="sf2-ag MrgL10 ">                  
                    <li>
                        <asp:Literal ID="ltTerm" runat="server"></asp:Literal></li>
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
                        <li><asp:DropDownList ID="ddlInsRentCd" runat="server"></asp:DropDownList></li>
                      <li>                        
                        <asp:DropDownList ID="ddlCarTy" runat="server">
                        </asp:DropDownList>
                    </li>
                    
                    <li>
                        <asp:Literal ID="ltPayMethod" runat="server"></asp:Literal></li>
                    <li>
                        <asp:DropDownList ID="ddlPayment" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="TpAtit1" style="display: none">
        <div class="FloatR">
            <asp:Literal ID="ltInsVND" runat="server" Text="( UNIT : VND )"></asp:Literal></div>
    </div>
    <asp:UpdatePanel ID="upResult1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnExcelReport" />
            <asp:PostBackTrigger ControlID="lnkbtnReceiptList" />
        </Triggers>
        <ContentTemplate>
            <asp:TextBox ID="txtIP" runat="server" Visible="false"></asp:TextBox>
            <asp:ListView ID="lvAccountsList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnLayoutCreated="lvAccountsList_LayoutCreated" OnItemDataBound="lvAccountsList_ItemDataBound"
                OnItemCreated="lvAccountsList_ItemCreated">
                <LayoutTemplate>
                    <table class="TypeA MrgT10" id="tblListDebit">
                        <col width="85" />
                        <col width="80" />
                        <col width="170" />
                        <col width="80" />
                        <col width="85" />
                        <col width="105" />
                        <col width="105" />
                        <col width="105" />
                        <thead>
                            <tr>
                                <th class="Fr-line">
                                    <span>
                                        <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></span>
                                </th>
                                <th >
                                    <span>
                                        <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltCarNo" runat="server"></asp:Literal></span>
                                </th>
                                 <th>
                                    <span>
                                        <asp:Literal ID="ltCarType" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltFeeNET" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltFeeVAT" runat="server"></asp:Literal></span>
                                </th>
                                <th class="Ls-line">
                                    <span>
                                        <asp:Literal ID="ltTotalAmt" runat="server"></asp:Literal></span>
                                </th>
                            </tr>
                        </thead>
                         <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr style="background-color: #FFFFFF; cursor: pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'"
                        onmouseout="this.style.backgroundColor='#FFFFFF'">
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltInsPaymentDt" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></span>                               
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltCarNo" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltCarType" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltInsPaymentNm" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltInsFeeNET" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltInsFeeVAT" runat="server"></asp:Literal></span>
                        </td>
                        <td class="TbTxtCenter">
                            <span>
                                <asp:Literal ID="ltInsFeeAmt" runat="server"></asp:Literal></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA MrgT10">
                        <col width="85" />
                        <col width="80" />
                        <col width="125" />
                        <col width="160" />
                        <col width="85" />
                        <col width="95" />
                        <col width="95" />
                        <col width="95" />
                        <thead>
                            <tr>
                                <th class="Fr-line">
                                    <span>
                                        <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></span>
                                </th>
                                <th >
                                    <span>
                                        <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltCarNo" runat="server"></asp:Literal></span>
                                </th>
                                 <th>
                                    <span>
                                        <asp:Literal ID="ltCarType" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltFeeNET" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <span>
                                        <asp:Literal ID="ltFeeVAT" runat="server"></asp:Literal></span>
                                </th>
                                <th class="Ls-line">
                                    <span>
                                        <asp:Literal ID="ltTotalAmt" runat="server"></asp:Literal></span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
             <table class="TypeA MrgT10">
                <tr style="background-color: #E4EEF5">
                    <td colspan="5" class="TbTxtCenter" width="500">
                        <b>Total Apt</b>
                    </td>
                    <td width="105" align="center">
                       <asp:Literal ID="ltAptFeeNETAll" runat="server"></asp:Literal>
                    </td>
                    <td width="105" align="center">
                       <asp:Literal ID="ltAptFeeVATAll" runat="server"></asp:Literal>
                    </td>
                    <td width="105" align="center">
                        <asp:Literal ID="ltAptTotalAmtAll" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <table class="TypeA MrgT10">
                <tr style="background-color: #E4EEF5">
                    <td colspan="5" class="TbTxtCenter" width="500">
                        <b>Total</b>
                    </td>
                    <td width="105" align="center">
                       <asp:Literal ID="ltFeeNETAll" runat="server"></asp:Literal>
                    </td>
                    <td width="105" align="center">
                       <asp:Literal ID="ltFeeVATAll" runat="server"></asp:Literal>
                    </td>
                    <td width="105" align="center">
                        <asp:Literal ID="ltTotalAmtAll" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="Btn-Type2-wp FloatL">
                <div class="Btn-Tp2-L">
                    <div class="Btn-Tp2-R">
                        <div class="Btn-Tp2-M">
                            <span>
                                <asp:LinkButton ID="lnkbtnPrint" runat="server" 
                                onclick="lnkbtnPrint_Click1"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Btwps FloatR">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Btwps FloatR" id="divReceipt" runat="server">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnReceiptList" runat="server" OnClick="lnkbtnReceiptList_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
                OnClick="imgbtnPageMove_Click" />
            <asp:HiddenField ID="hfCurrentPage" runat="server" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfTotalSend" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="hfRentCd" runat="server" />
            <asp:HiddenField ID="hfRentSeq" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
