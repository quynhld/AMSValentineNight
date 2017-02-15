<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ManuallySetting.aspx.cs" Inherits="KN.Web.Settlement.Balance.ManuallySetting" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                initControl();
            }
        }         
        function fnMakingLine(strTxt)
        {
//            var strItems = document.getElementById("<%=ddlItems.ClientID%>");
//            var strYear = document.getElementById("<%=txtSearchDt.ClientID%>");

//            if (trim(strItems.value) == "") {
//                alert(strTxt);
//                document.getElementById("<%=ddlItems.ClientID%>").focus();
//                return false;
//            }
//            if (trim(strYear.value) == "") {
//                alert(strTxt);
//                document.getElementById("<%=txtSearchDt.ClientID%>").focus();
//                return false;
//            }            
//            ShowLoading("Loading data........");
        }
        
        $(document).ready(function () {
            initControl();
        });

        function initControl() {
            $("#<%=lnkMakeDebitNote.ClientID %>").bind("click", function () {
                ShowLoading("Making debit note........");
            });
            $("#<%=imgbtnLoadData.ClientID %>").bind("click", function () {
                $("#<%=lnkbtnSearch.ClientID %>").click();
            });
            $("#<%=txtSearchDt.ClientID %>").monthpicker();

            $("#<%=txtSSearchDt.ClientID %>").datepicker({
            });
            $("#<%=txtESearchDt.ClientID %>").datepicker({
            });          
        }
        
    //-->
        function fnCheckValidate(strAlertText, strTanVal, strLongVal, strShortVal) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID %>");
            if (trim(txtSearchDt.value) == "") {
                alert(strAlertText);
                txtSearchDt.focus();
                return false;
            }
            var ddlItems = document.getElementById("<%=ddlItems.ClientID %>");
            if (trim(ddlItems.value) == "") {
                alert(strAlertText);
                ddlItems.focus();
                return false;
            }
            ShowLoading("Loading data........");
            return true;
        }
    </script>
    <div class="Tab-wp MrgB10">
        <ul class="TabM">
            
            <li class="Over CursorNon">Not Created Debit Note</li>
            <li class="title" style="display: none"><asp:LinkButton ID="lnkCreatedNote" runat="server" Text="Created Debit Note" 
                    onclick="lnkCreatedNote_Click"></asp:LinkButton></li>
            <li class="title"><asp:LinkButton ID="lnkPrintList" runat="server" Text="Printing List" 
                    onclick="lnkPrintList_Click"></asp:LinkButton></li>
        </ul>
    </div>
    <fieldset class="sh-field5 MrgB10" >
        <legend>검색</legend>
       <ul class="sf5-ag MrgL30"> 
            <li><asp:Literal ID="ltCompany" runat="server" Text="Company Name :"></asp:Literal></li>
            <li><asp:TextBox ID="txtRoomNo" runat="server" MaxLength="200" Width="200"></asp:TextBox></li>
            <li><asp:Literal ID="ltItem" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlItems" runat="server"></asp:DropDownList></li>
            <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            <li><asp:Literal ID="ltMonth" runat="server"></asp:Literal></li>
            <li>
                <asp:CheckBox ID="chkNewTenants" runat="server" Text="New Tenants" Visible="False"/></li>            
        </ul>
         <ul class="sf5-ag MrgL30 bgimgN">
            <li><b><asp:Literal ID="Literal2" runat="server" Text="Issue Date From :"></asp:Literal></b></li>
            <li><asp:TextBox ID="txtSSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
            <li><b><asp:Literal ID="Literal12" runat="server" Text="~"></asp:Literal></b></li>
            <li><asp:TextBox ID="txtESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
            <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtESearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
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
             <asp:AsyncPostBackTrigger ControlID="lnkMakeDebitNote" EventName="Click" /> 
             <asp:AsyncPostBackTrigger ControlID="imgbtnLoadData" EventName="Click" />           
        </Triggers>
        <ContentTemplate>           
            <table class="TbCel-Type6-A" cellpadding="0">                
                    <col width="30px" />
                    <col width="60px" />
                    <col width="45px" />
                    <col width="200px" />
                    <col width="80px" />
                    <col width="70px" />
                    <col width="60px" />
                    <col width="60px" />
                    <col width="70px" />
                    <col width="80px" />
                <tr>                    
                    <th class="Fr-line"><asp:CheckBox ID="chkAll" Style="text-align: center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged" /></th>                  
                    <th>Fee Type</th>
                    <th>Room</th>
                    <th>Tenant Name</th>
                    <th>Using Period</th>
                    <th>Fee Amt</th>
                    <th>VAT Amt</th>
                    <th>Ex Rate</th>
                    <th>Total Amt</th>
                    <th>Issuing Date</th>
                </tr>
            </table>
            <div style="overflow-y: scroll; height: 400px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" OnItemCreated="lvPrintoutList_ItemCreated" OnItemDataBound="lvPrintoutList_ItemDataBound">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="823">
                            <col width="30px" />
                            <col width="60px" />
                            <col width="50px" />
                            <col width="180px" />
                            <col width="90px" />
                            <col width="70px" />
                            <col width="60px" />
                            <col width="60px" />
                            <col width="70px" />
                            <col width="65px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr onMouseOver="this.style.backgroundColor='#E4EEF5'" onMouseOut="this.style.backgroundColor='#ffffff'">
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkboxList" runat="server"></asp:CheckBox>
                            </td>                          
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txthfFeeTypeCode" runat="server" Visible="false"></asp:TextBox>
                                <asp:Literal ID="ltFeeType" runat="server"></asp:Literal>  
                                
                                <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfContractNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfRentSeq" runat="server" Visible="false"></asp:TextBox>                                                               
                                <asp:TextBox ID="txthfRentCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txthfVatRatio" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfContractType" runat="server" Visible="false"></asp:TextBox>   
                                <asp:TextBox ID="txthfStartUsingDt" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfEndUsingDt" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfPayCycleType" runat="server" Visible="false"></asp:TextBox>  
                                <asp:TextBox ID="txthfPayCycle" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfLeasingArea" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtDongToDollar" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfPayDate" runat="server" Visible="false"></asp:TextBox>  
                                <asp:TextBox ID="txthfFloor" runat="server" Visible="false"></asp:TextBox> 
                                <asp:TextBox ID="txthfUnitPrice" runat="server" Visible="false"></asp:TextBox>                                                             
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                              
                                <asp:Literal ID="ltUsingPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                              
                                <asp:Literal ID="ltFeeAmt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                              
                                <asp:Literal ID="ltVATAmt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                              
                                <asp:Literal ID="ltExRate" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                              
                                <asp:Literal ID="LtTotal" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">                               
                                <asp:Literal ID="ltIssuingDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            
                            <col width="80px" />
                            <col width="80px" />
                            <col width="120px" />
                            <col width="80px" />
                            <col width="80px" />
                            <tr>
                                <td colspan="6" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span><asp:LinkButton ID="lnkMakeDebitNote" runat="server" 
                            Text="Make Debit Note Data" onclick="lnkMakeDebitNote_Click"></asp:LinkButton></span>
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
 <asp:ImageButton ID="imgbtnLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnLoadData_Click" />
</asp:Content>
