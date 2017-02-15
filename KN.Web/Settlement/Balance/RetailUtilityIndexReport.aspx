<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="RetailUtilityIndexReport.aspx.cs" Inherits="KN.Web.Settlement.Balance.RetailUtilityIndexReport" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        function fnSelectCheckValidate(strText) {

            return true;
        }

        function fnCheckRoomNo(strAlertText, strTanVal, strLongVal, strShortVal) {
            return true;
        }



        function fnAccountList(rentCd) {
            // Datum0 : 시작일
            // Datum1 : 종료일
            // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
            // Datum3 : 임대(RentCd : 아파트, 상가)
            // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)

            var strStartDt = document.getElementById("<%=ddlSearchYear.ClientID%>").value;
         
            

            if (strStartDt != "") {
                strStartDt = strStartDt.replace(/\-/gi, "");            }


            window.open("/Common/RdPopup/RDPopupEstateRetailUtilityIndexReport.aspx?Datum0=" + rentCd + "&Datum1=" + strStartDt, "OfficeUtilityIndexReport", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
            return false;
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
        }

        function datePicker() {          
        
        }

        $(document).ready(function () {
            LoadCalender();
        }); 
       

    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
         <li><asp:DropDownList ID="ddlInsRentCd" runat="server" Visible="False"></asp:DropDownList></li> 
          
         <li><b><asp:Literal ID="Literal4" runat="server" Text="Year :"></asp:Literal></b>
            <asp:DropDownList ID="ddlSearchYear" Width="70px" runat="server">
                    </asp:DropDownList> 
         </li>       
                
          <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnIssuing_Click" Text="Search"></asp:LinkButton></span>
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
        </Triggers>
        <ContentTemplate>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="40px" />
                    <col width="60px" />
                    <col width="70px" />
                    <col width="80px" />
                    <col width="280px" />
                    <col width="120px" />
                    <col width="130px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack = "true"
                                oncheckedchanged="chkAll_CheckedChanged1" Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Tenant Name"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                        </th>
                        <th class="Ls-line">
                            <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 210px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" 
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onitemcommand="lvPrintoutList_ItemCommand">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">
                            <col width="40px" />                           
                            <col width="60px" />
                            <col width="70px" />
                            <col width="80px" />
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

                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="55" MaxLength="7" AutoPostBack="true" OnTextChanged="txtInvoiceNo_TextChanged" Visible = "false"></asp:TextBox>
                                <asp:TextBox ID="txtOldInvoiceNo" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsTaxCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInsNm" runat="server" Visible="false"></asp:TextBox>

                            </td>
                          
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAmtViNo" runat="server" Width="110" CssClass="TbTxtRight"></asp:TextBox></td>
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
                                <td colspan="8" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfUserSeq" runat="server" />
    <asp:HiddenField ID="hfPayDt" runat="server" />
     <asp:HiddenField ID="hfPayDtE" runat="server" />
    <asp:HiddenField ID="hfBillCd" runat="server"/>
    <asp:HiddenField ID="hfBillCdDt" runat="server"/>
    <asp:HiddenField ID="hfPeriod" runat="server"/>
    <asp:HiddenField ID="hfPeriodE" runat="server"/>
    <asp:HiddenField ID="hfRoomNo" runat="server"/>
    <asp:HiddenField ID="hfTenantNm" runat="server"/>
</asp:Content>