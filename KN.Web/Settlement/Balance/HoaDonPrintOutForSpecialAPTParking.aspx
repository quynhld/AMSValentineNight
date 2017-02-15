<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="HoaDonPrintOutForSpecialAPTParking.aspx.cs" Inherits="KN.Web.Settlement.Balance.HoaDonPrintOutForSpecialAPTParking" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
     <script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            LoadCalender();                        
        }
    }       
    <!--//

        function fnSelectCheckValidate(strText) {
            var strPaymentDt = document.getElementById("<%=txtSearchDt.ClientID%>");
             var strRoomNo = document.getElementById("<%=txtRoomNo.ClientID%>");

            if (trim(strPaymentDt.value) == "" && trim(strRoomNo.value) == "") {
                alert(strText);
                document.getElementById("<%=txtSearchDt.ClientID%>").focus();
                return false;
            }
            else if(trim(strPaymentDt.value) != "" && trim(strRoomNo.value) == ""){
             alert("Input Room");
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

          function LoadPopupMerge(printNo) {  
               document.getElementById("<%=hfsendParam1.ClientID%>").value = printNo;  

                window.open("/Common/RdPopup/RDPopupMergeInvoiceAPTParkingFee.aspx?Datum0=" + printNo + "&Datum1=" + strPrintDate, "ParkingFee", "status=yes, resizable=yes, width=840, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
          }
        function fnOccupantList(refPrintNo,strBilCd, strPayDt, strPrintDt, strShowYn) {        
            
            document.getElementById("<%=hfsendParam.ClientID%>").value = refPrintNo;
            document.getElementById("<%=hfBillCd.ClientID%>").value = strBilCd;
             document.getElementById("<%=hfprintDate.ClientID%>").value = strPrintDt;  
             if(trim(strShowYn) == "N")
             {     
                window.open("/Common/RdPopup/RDPopupHoaDonParkingSpecialAPTPreview.aspx?Datum0=" + refPrintNo + "&Datum1=" + strBilCd + "&Datum2=" + strPayDt + "&Datum3=" + strPrintDt,"Reciept","status=no, resizable=no, width=740, height=800, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            }
            else
            {
                 window.open("/Common/RdPopup/RDPopupParkingReprintHoaDon.aspx?Datum0=" + refPrintNo , "ReprintInvoice", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            }
            return false;
        }

        function fnIssuingCheck(strTxt)
        {
            if (confirm(strTxt))
            {
                return true;
            }
            elesf
            {
                return false;
            }
        }

        function LoadCalender() {
            datePicker();
        }

        function datePicker() {
            $("#<%=txtSearchDt.ClientID %>").monthpicker();
            $("#<%=txtPrintDt.ClientID %>").datepicker();
           
        }

        $(document).ready(function () {
            LoadCalender();
        }); 

        function Func1(printSeq) 
        {
            //alert(printSeq);
           <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
        } 


    
    //-->
    </script>

<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlItemCd" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlItemCd_SelectedIndexChanged"></asp:DropDownList></li>                
        <li><b>
            <asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
        <li>
            <asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="200" Width="160px"
                runat="server"></asp:TextBox>
        </li>  

        <li>
	                <asp:Literal ID="ltInvoice" runat="server" Text="Invoice No"></asp:Literal>	               
	    </li>
	     <li><asp:TextBox ID="txtInvoice" runat="server" Width="80px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
        <li>
            <asp:Literal ID="ltShow" runat="server" Text="Hide"></asp:Literal></li>
        <li>         
        <li>
            <asp:RadioButtonList ID="rbIsShow" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                <asp:ListItem Value="Y">Yes</asp:ListItem>
                
            </asp:RadioButtonList>
        </li>         
        
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Pay Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="60px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
         <li>
	                <asp:Literal ID="ltInvoiceYN" runat="server" Text="Printed YN"></asp:Literal>
	                <asp:DropDownList ID="ddlInvoiceYN" runat="server" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlInvoiceYN_SelectedIndexChanged"></asp:DropDownList>
	     </li> 

        <li><b><asp:Literal ID="Literal3" runat="server" Text="Print Date :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtPrintDt" CssClass="grBg bgType2" MaxLength="10" 
                Width="70px" runat="server" ontextchanged="txtPrintDt_TextChanged" AutoPostBack="true"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtPrintDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>               
                               
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
            <asp:AsyncPostBackTrigger ControlID="ddlInvoiceYN" EventName="SelectedIndexChanged" /> 
            <asp:AsyncPostBackTrigger ControlID="txtPrintDt" EventName="TextChanged" />             
            
        </Triggers>
        <ContentTemplate>
            <p style="text-align:right"><b><asp:Literal ID="ltMaxNo" runat="server"></asp:Literal></b>&nbsp;:&nbsp;<asp:Literal ID="ltInsMaxNo" runat="server"></asp:Literal></p>
            <table class="TbCel-Type6-A" cellpadding="0">
                <colgroup>
                    <col width="40px" />
                    <col width="60px" />                                      
                    <col width="280px" />
                    <col width="160px" /> 
                    <col width="100px" />
                    <col width="120px" />
                    <tr>
                        <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack = "true"
                                oncheckedchanged="chkAll_CheckedChanged1" Style="text-align: center" />
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                        </th>                      
                        
                        <th>
                            <asp:Literal ID="ltDescription" runat="server" Text="Tenant Name"></asp:Literal>
                        </th>
                         <th>
                            <asp:Literal ID="ltCmpNm" runat="server" Text = "Company Name"></asp:Literal>
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
                            <col width="240px" />
                            <col width="200px" /> 
                            <col width="80px" />
                            <col width="80px" />
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
                                <asp:TextBox ID="txtHfSeq" runat="server" Visible="false"></asp:TextBox>

                            </td>
                          
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></td>   
                            <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsDescription" runat="server" Width="270"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtCmpNm" runat="server" Width="200" ></asp:TextBox>
                                <asp:Literal ID="ltInsBillNm" runat="server" Visible="false"></asp:Literal>
                                <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
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
                            <col width="280px" />
                            <col width="160px" />     
                            <col width="100px" />
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
    <asp:HiddenField ID="hfsendParam1" runat="server"/>
    <asp:HiddenField ID="hfBillCd" runat="server"/>
    <asp:HiddenField ID="hfprintDate" runat="server" />
     <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
     
</asp:Content>