<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="ManuallySettingCreated.aspx.cs" Inherits="KN.Web.Settlement.Balance.ManuallySettingCreated"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
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
            $("#<%=txtSearchDt.ClientID %>").monthpicker();
            $("#<%=txtSearchDtTo.ClientID %>").monthpicker();
        });
        function fnCheckValidate(strAlertText, strTanVal, strLongVal, strShortVal) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID %>");
            if (trim(txtSearchDt.value) == "") {
                alert(strAlertText);
                txtSearchDt.focus();
                return false;
            }
            var txtSearchDtTo = document.getElementById("<%=txtSearchDtTo.ClientID %>");
            if (trim(txtSearchDtTo.value) == "") {
                alert(strAlertText);
                txtSearchDtTo.focus();
                return false;
            }            
            return true;
        }        
    //-->
    </script>
    <div class="Tab-wp MrgB10">
        <ul class="TabM">
            <li class="title"><asp:LinkButton ID="lbtNotCreated" runat="server" Text="Not Created Debit Note" 
                    onclick="lbtNotCreated_Click"></asp:LinkButton></li>
            <li class="Over CursorNon">Created Debit Note</li>
            <li class="title"><asp:LinkButton ID="lnkPrintList" runat="server" Text="Printing List" 
                    onclick="lnkPrintList_Click"></asp:LinkButton></li>                        
        </ul>
    </div>
        <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10 ">
            <li><asp:Literal ID="ltItem" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlItems" runat="server"></asp:DropDownList></li>
            <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            <li>~</li>
            <li><asp:TextBox ID="txtSearchDtTo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDtTo.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            
            <li>
                <asp:RadioButtonList ID="rbIsPrinted" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="Y">Printed</asp:ListItem>
                    <asp:ListItem Value="N" Selected="True">Not Printed</asp:ListItem>
                </asp:RadioButtonList>
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
        <div style="overflow-x: scroll;overflow-y: scroll; width: 1200;">          
            <table class="TbCel-Type6-E" cellpadding="0">
                    <col width="35px" />                
                    <col width="60px" />
                    <col width="65px" />
                    <col width="70px" />
                    <col width="115px" />
                    <col width="85px" />
                    <col width="68px" />
                    <col width="75px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="160px" />
                    <col width="100px" />                    
                <tr>
                    <th class="Fr-line"><asp:CheckBox ID="chkAll" Style="text-align: center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged" /></th>                  
                    <th>Debit Type</th>
                    <th>Fee Type</th>
                    <th>Room No</th>
                    <th>Tenant Nm</th>
                    <th>Using Period</th>
                    <th>Fee</th>                    
                    <th>LeasingArea</th>
                    <th>Issuing Date</th>
                    <th>Contract Name</th>
                    <th>Address</th>
                    <th>Tax Code</th>
                </tr>
            </table>
            <div style="height: 400px; width: 1200px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter">
                            <col width="40px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="140px" />
                            <col width="100px" />
                            <col width="80px" />
                            <col width="100px" />
                            <col width="90px" />
                            <col width="100px" />
                            <col width="210px" />
                            <col width="90px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Bd-Lt TbTxtCenter">
                            <asp:CheckBox ID="chkboxList" runat="server" OnCheckedChanged="chkboxList_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPaymetDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfContractNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRentSeq" runat="server" Visible="false"></asp:TextBox>                                                               
                                <asp:TextBox ID="txtDebitCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfFloor" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRealMonthViAmtNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfUnPaidAmount" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfDongtoDollar" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfBundleSeqNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfStartUsingDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfEndUsingDt" runat="server" Visible="false"></asp:TextBox>
                                <asp:Literal ID="ltDebitType" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txthfFeeTypeCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:Literal ID="ltFeeType" runat="server"></asp:Literal>                                
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtLeft">
                                <asp:Literal ID="ltUsingPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtRight2">                                
                                <asp:Literal ID="ltMonthFee" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                                
                                 <asp:Literal ID="ltLeasingArea" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtLeft">
                                <asp:TextBox ID="txtIssuingDate" runat="server" Width="80"></asp:TextBox>
                                <asp:Literal ID="ltPrintStatus" runat="server" Visible="False"></asp:Literal>

                            </td>

                            <td class="Bd-Lt TbTxtCenter">                                
                                 <asp:Literal ID="ltConTract" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                                
                                 <asp:Literal ID="ltAddress" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                                
                                 <asp:Literal ID="ltTaxCode" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="20px" />                
                            <col width="60px" />
                            <col width="60px" />
                            <col width="60px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />               
                            <tr>
                                <td colspan="7" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
          </div>
        </ContentTemplate>
    </asp:UpdatePanel>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp " style="display: none">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnMergeBilling" runat="server" Text="Merge Billing" 
                                    onclick="lnkbtnMergeBilling_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkDividualBilling" runat="server" 
                                    Text="Individual Billing" onclick="lnkDividualBilling_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click" Text="Cancel"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>  
    <style type="text/css">
    .Tab-wp .TabM li.title {
        width: 180px;
    }
    .Tab-wp .TabM li.Over {
        width: 180px;
    }    
</style>      
</asp:Content>