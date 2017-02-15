<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonInscreaseForAPT.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonInscreaseForAPT" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        function fnSelectCheckValidate(strText) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID%>");

            if (trim(txtSearchDt.value) == "") {
                alert(strText);
                txtSearchDt.focus();
                return false;
            }
            
            var txtInvoiceNo = document.getElementById("<%=txtInvoiceNo.ClientID%>");

            if (trim(txtInvoiceNo.value) == "") {
                alert(strText);
                txtInvoiceNo.focus();
                return false;
            }
            ShowLoading();
            return true;
        }

        function fnOccupantList(refPrintNo, amount) {        
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = refPrintNo;

            window.open("/Common/RdPopup/RDPopupHoadonCNIncrease.aspx?Datum0=" + refPrintNo+"&Datum1=" + amount,"Reciept","status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            
            return false;
        }       

        function LoadCalender() {
            datePicker();
        }

        function datePicker() {
            $("#<%=txtSearchDt.ClientID %>").datepicker();
           
        }

        $(document).ready(function () {
            LoadCalender();
        });

        function fnIssuingCheck(strText) {
           var txtAmount = document.getElementById("<%=txtAmount.ClientID%>");

            if (trim(txtAmount.value) == "") {
                alert(strText);
                txtAmount.focus();
                return false;
            }
            return true;
        }

        function Func1() 
        {
           <%=Page.GetPostBackEventReference(imgUpdateInvoice)%>;
        } 
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>

     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Issue Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Invoice No :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtInvoiceNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>              
                               
        <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
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
            <asp:AsyncPostBackTrigger ControlID="imgbtnLoadData" EventName="Click" />
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
                            <asp:CheckBox ID="chkAll" runat="server" Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltInvoiceNo" runat="server" Text="Invoice"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltSerialNo" runat="server" Text="Serial No"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Description"></asp:Literal>
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
            <div style="overflow-y: scroll; height: 410px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" 
                    OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    >
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
                            </td>
                          
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltSerialNo" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInsBillNm" runat="server"></asp:Literal>
                            </td>
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal>
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
                        <span><asp:LinkButton ID="lnkbtnIssuing" runat="server" OnClick="lnkbtnIssuing_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL" style="margin-top: 3px">
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp2-L" style="background: none">
                    <asp:Literal ID="ltReqDt" runat="server" Text="Increase Amount :"></asp:Literal>
                    <span id="issueCalendar">
                        <asp:TextBox ID="txtAmount" CssClass="grBg bgType2" MaxLength="20" Width="80px"
                            runat="server"></asp:TextBox>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserSeq" runat="server" />
    <asp:HiddenField ID="hfPayDt" runat="server" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfsendParam" runat="server"/>
    <asp:HiddenField ID="hfBillCd" runat="server"/>
    <asp:ImageButton ID="imgbtnLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnLoadData_Click" />
     <asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgUpdateInvoice_Click"/>
</asp:Content>