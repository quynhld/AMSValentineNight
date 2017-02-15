<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="TransferPayment.aspx.cs" Inherits="KN.Web.Settlement.Balance.TransferPayment" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //
    <!--    //
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender();
        }
    }        

        function fnSelectCheckValidate(strText) {
            ShowLoading("Loading data....");
            return true;
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

        function LoadCalender() {
            datePicker();
            $("#tblListPayment tbody tr").tooltip();      
            
            $('#<%=chkAll.ClientID %>').change(function () {
                var $this = $(this);
                var checkboxes = $('table#tblListPayment').find(':checkbox');
                if ($this.is(':checked')) {
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });
            
			$("#<%=lnkbtnIssuing.ClientID %>").bind("click", function () {
			    ShowLoading("Transfering ......");
			});   
        }

        function datePicker() {
            $("#<%=txtSearchDt.ClientID %>").datepicker();
            $("#<%=txtESearchDt.ClientID %>").datepicker();
           
        }

        $(document).ready(function () {
            LoadCalender();
        }); 
    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Fee :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlItemCd" runat="server" ></asp:DropDownList></li>   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Tenant Name :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px" runat="server"></asp:TextBox></li>        
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Payment Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li><b><asp:Literal ID="Literal3" runat="server" Text="~"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtESearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li>
            <asp:Literal ID="Literal6" runat="server" Text="Transfer"></asp:Literal></li>
        <li>
            <asp:RadioButtonList ID="rbIsTransfer" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
            </asp:RadioButtonList>
        </li>              
                               
        <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span>
				        </div>
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
            
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                        <col width="20px" />
                        <col width="60px" />
                        <col width="50px" />
                        <col width="60px" />
                        <col width="80px" />
                        <col width="60px" />
                        <col width="60px" />
                        <col width="60px" />
                        <col width="60px" />
                        <col width="70px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="Literal8" runat="server" Text="Paid Date"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltRoomNo" runat="server" Text="Room No"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPeriod" runat="server" Text="Period"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltFeeTy" runat="server" Text="Fee Type"></asp:Literal>
                        </th>
                        
                        <th>
                            <asp:Literal ID="Literal4" runat="server" Text="Payment"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltAmount" runat="server" Text="Total"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDebitCd" runat="server" Text="Debit Acc"></asp:Literal>
                        </th>
                        <th >
                            <asp:Literal ID="ltCreditAcc" runat="server" Text="Credit Acc"></asp:Literal>
                        </th>
                        <th >
                            <asp:Literal ID="ltSlipNumber" runat="server" Text="Slip No"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 410px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" 
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onitemcommand="lvPrintoutList_ItemCommand">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820" id="tblListPayment">
                                <col width="20px" />
                                <col width="60px" />
                                <col width="50px" />
                                <col width="60px" />
                                <col width="80px" />
                                <col width="60px" />
                                <col width="60px" />
                                <col width="60px" />
                                <col width="60px" />
                                <col width="50px" />
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr title="<%#Eval("UserNm")%>">
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
                                 <asp:TextBox ID="txthfBillTy" runat="server" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID="txthfFeeTyDt" runat="server" Visible="false"></asp:TextBox>
                                   <asp:TextBox ID="txthfTenantNm" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPaidDate" runat="server"></asp:Literal>
                            </td>                                                                                  
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltTotalAmt" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltDebitAcc" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltCreditAcc" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltSlipNo" runat="server"></asp:Literal>
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
                                <td colspan="8" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
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
                        <span><asp:LinkButton ID="lnkbtnIssuing" runat="server" OnClick="lnkbtnIssuing_Click" Text="Transfer"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserSeq" runat="server" />
    <asp:HiddenField ID="hfPayDt" runat="server" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfsendParam" runat="server"/>
    <asp:HiddenField ID="hfBillCd" runat="server"/>
</asp:Content>