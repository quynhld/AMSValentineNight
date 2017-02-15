<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="HoadonPrintOut.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonPrintOut"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
        function fnCheckValidate(strTxt) {
            //            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            //            var strUserTaxCd = document.getElementById("<%=txtUserTaxCd.ClientID%>").value;

            //            if (trim(strSearchRoom.value) == "" && trim(strUserTaxCd) == "" && trim(strRssNo) == "")
            //            {
            //                strSearchRoom.focus();
            //                alert(strTxt);

            //                return false;
            //            }

            return true;
        }

        function fnChestNutPreview(strUserSeq, strPrintSeq, strPrintDetSeq) {
            window.open("/Common/RdPopup/RDPopupReciptDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "KeangNamReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

            return false;
        }

        function fnKeangNamPreview(strUserSeq, strPrintSeq, strPrintDetSeq) {
            window.open("/Common/RdPopup/RDPopupReciptKNDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "ChestnutReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

            return false;
        }

        function fnIssuingCheck(strTxt) {
            if (confirm(strTxt)) {
                return true;
            }
            else {
                return false;
            }
        }

        function fnLoadData() {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        }

        $(document).ready(function () {
            $("#<%=ltPaymentDt.ClientID %>").monthpicker();
        });

        function LoadCalender() {
            datePicker();
            $("#<%=chkSetIssueDt.ClientID %>").change(function () {
                if (this.checked) {
                    $("#issueCalendar").show();
                    $("#<%=txtIssueDt.ClientID %>").val('');
                } else {
                    $("#issueCalendar").hide();
                }
            });

            $("#<%=lnkbtnSearch.ClientID %>").bind("click", function () {
                ShowLoading("Loading data........");
            });
            $("#<%=imgbtnLoadData.ClientID %>").bind("click", function () {
                ShowLoading("Loading data........");
            });            
            $("#<%=lnkbtnIssuing.ClientID %>").bind("click", function () {
                ShowLoading("Making invoice........");
            });
        }

        function datePicker() {
            $("#<%=txtStartDt.ClientID %>").datepicker();
            $("#<%=txtEndDt.ClientID %>").datepicker();
            $("#<%=txtIssueDt.ClientID %>").datepicker();
        }

        $(document).ready(function () {
            LoadCalender();
        });
    //-->
    </script>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></li> 
            <li>
                <asp:DropDownList ID="ddlItemCd" runat="server">
                </asp:DropDownList>
            </li>                       
            <li>
              <b>  <asp:Literal ID="ltStartDtIssue" runat="server" Text="Issue Date From :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtStartDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server" Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
            </li>
            <li>
                 <b><asp:Literal ID="ltEndDt" runat="server"></asp:Literal> </b></li>
            <li>
                <asp:TextBox ID="txtEndDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtEndDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
            </li>
        </ul>
    </fieldset>
    <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">

            <li>
               <b> <asp:Literal ID="ltCompanyNm" runat="server" Text="Company Name :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtCompanyNm" runat="server" Width="190px"></asp:TextBox></li>
            <li>
                <b><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8"></asp:TextBox></li>
            <li>
               <b> <asp:Literal ID="Literal1" runat="server" Text="Tax Code :"></asp:Literal></b></li>
            <li>                
                <asp:TextBox ID="txtUserTaxCd" runat="server" ></asp:TextBox>
            </li>

            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnIssuing" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnLoadData" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnLoadData" EventName="Click" />
            <asp:PostBackTrigger ControlID="lnkbtnExcel" />
        </Triggers>
        <ContentTemplate>
            <p style="text-align: right">
                <b>
                    <asp:Literal ID="ltMaxNo" runat="server"></asp:Literal>
                </b>&nbsp;:&nbsp;<asp:Literal ID="ltInsMaxNo" runat="server"></asp:Literal>
            </p>
            <div style="overflow-x: scroll; overflow-y: scroll; width: 840px; height: 360px">
                <table class="TbCel-Type6-E" cellpadding="0">
                    <colgroup>
                        <col width="1%" />
                        <col width="5%" />
                        <col width="12%" />
                        <col width="3%" />
                        <col width="5%" />
                        <col width="11%" />
                        <col width="5%" />
                        <tr>
                            <th class="Fr-line">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" 
                                    OnCheckedChanged="chkAll_CheckedChanged" Style="text-align: center" />
                            </th>
                            <th class="Ls-line">
                                <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltCompNo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </colgroup>
                </table>
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated"
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onselectedindexchanged="lvPrintoutList_SelectedIndexChanged">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TbCel-Type6-E">
                            <col width="2%" />
                            <col width="7%" />
                            <col width="20%" />
                            <col width="7%" />
                            <col width="7%" />
                            <col width="10%" />
                            <col width="5%" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter" runat="server" id="tdChk">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                                <asp:TextBox ID="txtHfPrintSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRefSerialNo" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtHfContractType" runat="server" Visible="False"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltIssuingDt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltlnsCompNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtLeft">
                                <asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltViAmount" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TbCel-Type6-E" width="820">
                            <col width="5%" />
                            <col width="20%" />
                            <col width="7%" />
                            <col width="5%" />
                            <col width="20%" />
                            <col width="10%" />
                            <col width="9%" />
                            <tr>
                                <td colspan="7" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp2-L">
                    <div class="Btn-Tp2-R">
                        <div class="Btn-Tp2-M">
                            <span>
                                <asp:LinkButton ID="lnkbtnIssuing" runat="server" OnClick="lnkbtnIssuing_Click"></asp:LinkButton>
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
                                <asp:LinkButton ID="lnkbtnExcel" runat="server" Text="Export Excel" OnClick="lnkbtnExcel_Click"
                                    Width="82px"></asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL" style="margin-top: 15px">
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp2-L" style="background: none">
                    <asp:Literal ID="ltReqDt" runat="server" Text="Set Special Issue Date"></asp:Literal>
                    <asp:CheckBox ID="chkSetIssueDt" Style="text-align: center" runat="server" ToolTip="Set Special Issue Date" />
                    <span id="issueCalendar" style="display: none">
                        <asp:TextBox ID="txtIssueDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                            runat="server" Visible="True"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtIssueDt.ClientID%>')"
                            src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" />
                    </span>
                </div>
            </div>
        </div>
    </div>
    <div style="margin-top: 3px; float: left">
        <b>(*):</b>Trong trường hợp muốn thay đổi ngày issue trên hóa đơn thì check vào check box (Set Special Issue Date).Sau đó chọn ngày issue mới , tất cả các hóa đơn trong list được chọn sẽ được thay đổi ngày issue mới
    </div>
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:ImageButton ID="imgbtnLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnLoadData_Click" />
</asp:Content>
