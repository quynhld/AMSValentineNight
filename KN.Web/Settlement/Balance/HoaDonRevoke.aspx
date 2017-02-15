<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="HoaDonRevoke.aspx.cs" Inherits="KN.Web.Management.Manage.HoaDonRevoke"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strTxt)
        {
            return true;
        }

         function LoadCalender() {
            datePicker();
        
        $('#<%=rbReason.ClientID %>').change(function() {
         var moneyCd = $('#<%=rbReason.ClientID %> input:radio:checked').val();
            if (moneyCd=="1") {
                $('div#lstNewInvoice').hide("slow");
            } else {
               $('div#lstNewInvoice').show("slow");
            }
        });             

        }

          function datePicker() {
                $("#<%=txtSearchDt.ClientID %>").datepicker();
        }

        function fnIssuingCheck(strTxt)
        {
            if (confirm(strTxt))
            {
                return true;            
            }
            else
            {
                return false;
            }
        }
        function fnDetailView(refSeq)
        {
            document.getElementById("<%=txthfrefSerialNo.ClientID%>").value = refSeq;
           
            return false;
        }

        function fnOccupantList(strSeq) {            
            window.open("/Common/RdPopup/RDPopupReprintHoaDon.aspx?Datum0=" + strSeq , "Reprint Invoice", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            document.getElementById("<%=txtsendTempDocNo.ClientID%>").value = strSeq;
            
        }
          $(document).ready(function () {
             LoadCalender();
              $('div#lstNewInvoice').hide();
        });

          function fnLoadData(oldInvoice, refPrint) {
//              document.getElementById("<%=hfOldInvoiceNo.ClientID%>").value = oldInvoice;
//              document.getElementById("<%=hfsendParam.ClientID%>").value = refPrint;
            document.getElementById("<%=imgUpdateInvoice.ClientID%>").click();
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        }          
    //-->
    </script>
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li>
                <asp:Literal ID="ltInvoice" runat="server" Text="Invoice No"></asp:Literal>
            </li>

            <li>
                <asp:TextBox ID="txtInvoice" runat="server" Width="100px" MaxLength="8" CssClass="sh-input"></asp:TextBox>
            </li>

            <li>
                <b><asp:Literal ID="ltSerial" runat="server" Text="Serial"></asp:Literal></b>
            </li>

            <li>
                <asp:DropDownList ID="ddlSerial" runat="server">
                </asp:DropDownList>
            </li>
        <li>
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
            <li>
                <asp:Literal ID="Literal1" runat="server" Text="Reason of revoking invoices: "></asp:Literal>
            </li>
            <li>
                <asp:RadioButtonList ID="rbReason" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">Wrong Infomation</asp:ListItem>
                    <asp:ListItem Value="2">Wrong number</asp:ListItem>
                </asp:RadioButtonList>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnReplace" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div style="height: 180px; width: 820px;">
                <table class="TbCel-Type6-A" cellpadding="0">
                    <colgroup>
                        <col width="70px" />
                        <col width="80px" />
                        <col width="300px" />
                        <col width="80px" />
                        <tr>
                            <th>
                                <asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltFeeName" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltTotal" runat="server" Text="Total"></asp:Literal>
                            </th>
                        </tr>
                    </colgroup>
                </table>
                <div style="overflow-y: scroll; height: 170px; width: 840px;">
                    <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                        OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated"
                        OnItemDataBound="lvPrintoutList_ItemDataBound" OnItemCommand="lvPrintoutList_ItemCommand"
                        OnSelectedIndexChanged="lvPrintoutList_SelectedIndexChanged" OnItemUpdating="lvPrintoutList_ItemUpdating">
                        <LayoutTemplate>
                            <table cellpadding="0" class="TypeA-shorter" width="820">
                                <col width="80px" />
                                <col width="80px" />
                                <col width="260px" />
                                <col width="90px" />
                                <tr id="iphItemPlaceHolderID" runat="server">
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #FFFFFF; cursor: pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'"
                                onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInvoiceNoP" runat="server"></asp:Literal>
                                    <asp:TextBox ID="txtPSeq" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtSeq" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsRoomNoP" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltInsDescription" runat="server"></asp:Literal>
                                </td>
                                <td class="Bd-Lt TbTxtCenter">
                                    <asp:Literal ID="ltTotal" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table cellpadding="0" class="TypeA-shorter" width="820">
                                <col width="80px" />
                                <col width="80px" />
                                <col width="80px" />
                                <col width="200px" />
                                <col width="220px" />
                                <tr>
                                    <td colspan="9" style="text-align: center">
                                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <asp:HiddenField ID="txthfrefSerialNo" runat="server" />
                <asp:HiddenField ID="txthfrefSeq" runat="server" />
                <asp:HiddenField ID="txtsendTempDocNo" runat="server" />
                <asp:HiddenField ID="hfOldInvoiceNo" runat="server" />
                <asp:HiddenField ID="hfsendParam" runat="server"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="lstNewInvoice">
    <fieldset class="sh-field5 MrgB10">
        <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li><b>
                <asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
            <li>
                <asp:DropDownList ID="ddlItemCd" runat="server">
                </asp:DropDownList>
            </li>
            <li><b>
                <asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Tenant Name :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px"
                    runat="server"></asp:TextBox></li>
        </ul>
        <ul class="sf5-ag MrgL30 bgimgN">
            <li><b>
                <asp:Literal ID="Literal2" runat="server" Text="Room :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"
                    Visible="True"></asp:TextBox></li>
            <li><b>
                <asp:Literal ID="Literal3" runat="server" Text="Payment Date :"></asp:Literal></b></li>
            <li>
                <asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px"
                    runat="server"></asp:TextBox></li>
            <li>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer; align: absmiddle;" /></li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnSearchNew" runat="server" OnClick="lnkbtnSearchNew_Click" Text="Search"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearchNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <p style="text-align: right">
                <b>
                    <asp:Literal ID="ltMaxNo" runat="server" Text="Max Invoice"></asp:Literal></b>&nbsp;:&nbsp;<asp:Literal
                        ID="ltInsMaxNo" runat="server" ></asp:Literal></p>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="40px" />
                    <col width="60px" />
                    <col width="70px" />
                    <col width="280px" />
                    <col width="120px" />
                    <col width="130px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged1"
                                Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server" Text="Period"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="Literal4" runat="server" Text="Room"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Tenant Name"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltAmount" runat="server" Text="Total"></asp:Literal>
                        </th>
                        <th class="Ls-line">
                            <asp:Literal ID="ltPaymentDt" runat="server"  Text="Pay Day"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 110px; width: 840px;">
                <asp:ListView ID="lvPrintoutListNew" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                    OnLayoutCreated="lvPrintoutListNew_LayoutCreated" OnItemCreated="lvPrintoutListNew_ItemCreated"
                    OnItemDataBound="lvPrintoutListNew_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="40px" />
                            <col width="60px" />
                            <col width="70px" />
                            <col width="240px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter" runat="server" id="tdChk">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                                <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfRefPrintNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtOldInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsTaxCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsNm" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtLeft">
                                <asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtInsAmtViNo" runat="server" Width="110" CssClass="TbTxtRight"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtInsRegDt" runat="server" MaxLength="10" Width="70px" ReadOnly="true"></asp:TextBox>
                                <asp:Literal ID="ltCalendarImg" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hfInsRegDt" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="30px" />
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
                            <col width="280px" />
                            <col width="120px" />
                            <col width="110px" />
                            <tr>
                                <td colspan="8" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type3-wp ">
            <div class="Btn-Tp3-L">
                <div class="Btn-Tp3-R">
                    <div class="Btn-Tp3-M">
                        <span>
                            <asp:LinkButton ID="lnkbtnReplace" runat="server" OnClick="lnkbtnReplace_Click">Cancel Invoice</asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailview_Click" />
    <asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgUpdateInvoice_Click" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfRoomNo" runat="server" />
     
</asp:Content>
