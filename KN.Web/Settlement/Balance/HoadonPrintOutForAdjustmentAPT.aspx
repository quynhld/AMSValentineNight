<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoadonPrintOutForAdjustmentAPT.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoadonPrintOutForAdjustmentAPT" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--        //

        function fnSelectCheckValidate(strText) {
            var strRoomNo = document.getElementById("<%=txtRoomNo.ClientID%>");
            var strInvoiceNo =  document.getElementById("<%=txtInvoiceNo.ClientID %>");

            if (trim(strRoomNo.value) == "") {
                alert(strText);
                document.getElementById("<%=txtRoomNo.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function fnCheckRoomNo(strAlertText, strTanVal, strLongVal, strShortVal) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID %>");
            if (trim(txtRoomNo.value) == "") {
                alert(strAlertText);
                txtRoomNo.focus();
                return false;
            }
            return true;
        }


        function fnOccupantList(refPrintNo,strBilCd, strPayDt,strinvoiceNo) {        
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = refPrintNo;
            document.getElementById("<%=hfBillCd.ClientID%>").value = strBilCd;
            document.getElementById("<%=hfsendOldInvoiceNo.ClientID%>").value = strinvoiceNo;

            window.open("/Common/RdPopup/RDPopupHoadonCNAdjPreview.aspx?Datum0=" + refPrintNo + "&Datum1=" + strBilCd + "&Datum2=" + strPayDt+ "&Datum3=" + strinvoiceNo,"Reciept","status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            
            return false;
        }

       

        function fnChestNutPreview(strUserSeq, strPrintSeq, strPrintDetSeq)
        {
            window.open("/Common/RdPopup/RDPopupReciptDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "KeangNamReciept", "status=no, resizable=no, width=840, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
            return false;
        }

        function fnKeangNamPreview(strUserSeq, strPrintSeq, strPrintDetSeq)
        {
            window.open("/Common/RdPopup/RDPopupReciptKNDetail.aspx?Datum2=" + strUserSeq + "&Datum0=" + strPrintSeq + "&Datum1=" + strPrintDetSeq, "ChestnutReciept", "status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            
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
            $("#<%=txtSearchDt.ClientID %>").datepicker();
           
        }

        $(document).ready(function () {
            LoadCalender();
        }); 

        function Func1() 
        {
            //alert(printSeq);
           <%=Page.GetPostBackEventReference(imgUpdateInvoice)%>;
        } 
       

    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlItemCd" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="chkAll_CheckedChanged1"></asp:DropDownList></li>   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Tenant Name :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px" runat="server"></asp:TextBox></li>        
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Payment Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li><b><asp:Literal ID="Literal3" runat="server" Text="Invoice No :"></asp:Literal></b></li>
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
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
            
        </Triggers>
        <ContentTemplate>
            <p style="text-align:right"><b><asp:Literal ID="ltMaxNo" runat="server"></asp:Literal></b>&nbsp;:&nbsp;<asp:Literal ID="ltInsMaxNo" runat="server"></asp:Literal></p>
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
            <div style="overflow-y: scroll; height: 410px; width: 840px;">
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
    <asp:HiddenField ID="hfUserSeq" runat="server" />
    <asp:HiddenField ID="hfPayDt" runat="server" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfsendParam" runat="server"/>
    <asp:HiddenField ID="hfsendOldInvoiceNo" runat="server"/>
    <asp:HiddenField ID="hfBillCd" runat="server"/>
     <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
     <asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgUpdateInvoice_Click"/>
</asp:Content>