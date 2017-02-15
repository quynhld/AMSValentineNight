<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="SetRentalMngFeeListWrite.aspx.cs" Inherits="KN.Web.Management.Manage.SetRentalMngFeeListWrite" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strTxt)
        {
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");

//            if (trim(strSearchRoom.value) == "")
//            {
//                strSearchRoom.focus();
//                alert(strTxt);

//                return false;
//            }

            return true;
        }

         function LoadCalender() {
            datePicker();
        }

          function datePicker() {
            $("#<%=txtStartDt.ClientID %>").datepicker();
            $("#<%=txtEndDt.ClientID %>").datepicker();           
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
        function fnDetailView(refSerialNo)
        {
            document.getElementById("<%=txthfrefSerialNo.ClientID%>").value = refSerialNo;
            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
            return false;
        }

        function fnOccupantList(strSeq) {            
            window.open("/Common/RdPopup/RDPopupReprintHoaDon.aspx?Datum0=" + strSeq , "Reprint Invoice", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
            document.getElementById("<%=txtsendTempDocNo.ClientID%>").value = strSeq;
            
        }
          $(document).ready(function () {
             LoadCalender();
        });
    //-->
    </script>
    
    <fieldset class="sh-field5 MrgB10">
            <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li><asp:DropDownList ID="ddlItemCd" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
           <%-- <li><asp:Literal ID="ltSearchYear" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltSearchMonth" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>--%>
             <li>
	                <asp:Literal ID="ltInvoiceYN" runat="server" Text="Invoice YN"></asp:Literal>
	                <asp:DropDownList ID="ddlInvoiceYN" runat="server" 
                        onselectedindexchanged="ddlInvoiceYN_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
	            </li>  
            <li>
                <div class="Btn-Type4-wp">
                     <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>

        <ul class="sf5-ag MrgL30 bgimgN">
	            <li>
	                <asp:Literal ID="ltInvoice" runat="server" Text="Invoice No"></asp:Literal>	               
	            </li>
	           <li><asp:TextBox ID="txtInvoice" runat="server" Width="100px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            
	            <li>
                <asp:Literal ID="ltPeriod" runat="server" Text = "Period"></asp:Literal>
           </li>
           <li>
                <asp:TextBox ID="txtStartDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"   />
            </li>
            <li><span>~</span></li>
                <li>
                <asp:TextBox ID="txtEndDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtEndDt.ClientID%>')"
                    src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"   />
            </li>        

	             
            </ul>
       
    </fieldset>
<%--    <table class="TbCel-Type6-A" cellpadding="0">
        <col width="50px" />
        <col width="80px" />
        <col width="80px" />
        <col width="80px" />
        <col width="290px" />
        <col width="80px" />
        <col width="130px" />
        <col width="50px" />
        <tr>
            <th class="Fr-line"><asp:CheckBox ID="chkAll" Style="text-align: center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged" /></th>
            <th><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltPaymentTy" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltDescription" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
            <th class="Ls-line"><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></th>
        </tr>
    </table>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" />
            
        </Triggers>
        <ContentTemplate>
    <div>
    
    <div style="height: 180px; width: 820px;" >
        <table class="TbCel-Type6-A" cellpadding="0" >               
                <colgroup>
                    <col width="70px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="120px" />
                    <col width="200px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="90px" />
                    <tr>
                        <th>
                            <asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltDate" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltFeeName" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltUserNm" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltIssDt" runat="server"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 170px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onitemcommand="lvPrintoutList_ItemCommand" 
                    onselectedindexchanged="lvPrintoutList_SelectedIndexChanged" >
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">                           
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="120px" />
                            <col width="220px" />
                            <col width="90px" />
                            <col width="80px" />  
                            <col width="80px" />                          
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'"  onclick="javascript:return fnDetailView('<%#Eval("REF_SERIAL_NO")%>')">                          
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInvoiceNoP" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfrefSerialNoP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeqP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeqP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNoP" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsYearP" runat="server"></asp:Literal>&nbsp;/&nbsp;<asp:Literal ID="ltInsMonthP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNoP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFeeNameP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsUserNmP" runat="server"></asp:Literal></td>
                             <td class="Bd-Lt TbTxtCenter"> <asp:Literal ID="ltInsUPriceP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsAmtViNoP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltnsIssDtP" runat="server"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">                          
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="120px" />
                            <col width="220px" />
                            <col width="90px" />
                            <col width="80px" /> 
                            <col width="80px" />                            
                            <tr>
                            <td colspan="9" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <asp:HiddenField ID="txthfrefSerialNo" runat="server" />
            <asp:HiddenField ID="txtsendTempDocNo" runat="server" />
    </div>    
    <div style="height: 260px; width: 840px; margin-top:40px">
       <table cellspacing="0"  class="TbCel-Type1 iw840">
        <tr>
                   <th align="center"><asp:Literal ID="ltlnInvoice" runat="server"></asp:Literal></th>
                    <td class="Bd-Lt TbTxtCenter">                       
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfInvoiceNo" runat="server" Visible="false"></asp:TextBox> 
                        <asp:TextBox ID="txtHfSerialNo" runat="server" Visible="false"></asp:TextBox> 
                    </td>
                    <th align="center"><asp:Literal ID="ltlnDt" runat="server"></asp:Literal></th>
                    <td class="Bd-Lt TbTxtCenter">
                        <asp:Literal ID="ltInsYear" runat="server" Visible="false"></asp:Literal><asp:Literal ID="ltInsMonth" runat="server" Visible="false"></asp:Literal>
                        <asp:DropDownList ID="ddllvYear" runat="server" onselectedindexchanged="ddllvYear_SelectedIndexChanged" AutoPostBack="true" Width="60"></asp:DropDownList> 
                        &nbsp;/&nbsp;                     
                        <asp:DropDownList ID="ddllvMonth" runat="server" onselectedindexchanged="ddllvMonth_SelectedIndexChanged" AutoPostBack="true" Width="40"></asp:DropDownList>         
                        <asp:TextBox ID="txtHfYear" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfMonth" runat="server" Visible="false"></asp:TextBox>
                    </td>
                     <th align="center"><asp:Literal ID="ltlnRoom" runat="server"></asp:Literal></th>                     
                    <td class="Bd-Lt TbTxtLeft">
                            <%--<asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>--%>
                            <asp:TextBox ID="txtRoomNo" runat="server" ></asp:TextBox>
                            <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
                    </td>                                                 
                </tr>

                <tr>                    
                    <th align="center"><asp:Literal ID="ltlnsCpNm" runat="server" ></asp:Literal></th>
                    <td class="Bd-Lt TbTxtLeft" colspan="4" >
                        <asp:TextBox ID="txtInsUserNm" runat="server"  Width="500px"></asp:TextBox>
                        <asp:TextBox ID="txtInsUserCd" runat="server" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtHfUserSeq1" runat="server" Visible="false"></asp:TextBox>
                    </td> 
                    <th align="center"><asp:Literal ID="ltnnInvoiceChoice" runat="server" Visible ="false"></asp:Literal>  </th>  
                    <td>
                        <asp:DropDownList ID="ddlInvoiceChoice" runat="server" onselectedindexchanged="ddlInvoiceChoice_SelectedIndexChanged" AutoPostBack="true" Width="100" Visible="false"></asp:DropDownList> 
                    </td>                                       
                </tr>

                <tr>
                    <th align="center" rowspan ="2"><asp:Literal ID="ltInsAddr" runat="server"></asp:Literal></th>
                    <td class="Bd-Lt TbTxtLeft" colspan="5"><asp:TextBox ID="txtInsAddress" runat="server" Width="600px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="Bd-Lt TbTxtLeft" colspan="5"><asp:TextBox ID="txtInsDetAddress" runat="server" Width="600px"></asp:TextBox></td>
                </tr>  
                            
                  <tr>
                     <th align="center"><asp:Literal ID="ltlnsDescription" runat="server"></asp:Literal></th>
                    <td class="Bd-Lt TbTxtLeft" colspan="5"><asp:TextBox ID="txtltnsDescription" runat="server" Width="500px"></asp:TextBox></td>
                </tr>

                <tr>
                     <th align="center"><asp:Literal ID="ltnsQty1" runat="server" ></asp:Literal></th>
                     <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtQty1" runat="server" Width="40px"></asp:TextBox></td>                    
                      <th align="center"><asp:Literal ID="ltnsVat1" runat="server" ></asp:Literal></th>
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtVat1" runat="server" Width="80px"></asp:TextBox></td> 
                         <th align="center">                      
                        <asp:Literal ID="ltnsExchangeRate" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfBillCd" runat="server" Visible="false"></asp:TextBox>
                       </th>
                    <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsExchageRate" runat="server" Width="80"></asp:TextBox></td>                    
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltlnsBillCode" runat="server" ></asp:Literal>
                        <asp:Literal ID="ltInsPaymentDt" runat="server" Visible="false"></asp:Literal>
                        <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
                    </th>                       
                    <td class="Bd-Lt TbTxtLeft">
                           <%-- <asp:Literal ID="ltlnsBillCd" runat="server"></asp:Literal>--%>
                            <asp:DropDownList ID="ddlBillCd" runat="server" Width="130"></asp:DropDownList> 
                    </td>                    
                     <th align="center"><asp:Literal ID="ltlnTaxCode" runat="server" ></asp:Literal></th> 
                     <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtUserTaxCd" runat="server" Width="100px"></asp:TextBox></td>                       
                      <th align="center"><asp:Literal ID="ltnsTotal" runat="server" ></asp:Literal></th>
                    <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtInsAmtViNo" runat="server" Width="120"></asp:TextBox></td>    
                                                
                </tr>
                  <tr>                     
                                       
                       <th align="center"><asp:Literal ID="ltlnIsDate" runat="server" ></asp:Literal></th>
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtIssDt" runat="server" Width="80px"></asp:TextBox></td>   
                       <th align="center"><asp:Literal ID="ltlnSDate" runat="server" ></asp:Literal></th> 
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="TextBox2" runat="server" Width="80px"></asp:TextBox></td>
                       <th align="center"><asp:Literal ID="ltlnEDate" runat="server" ></asp:Literal></th>        
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="TextBox3" runat="server" Width="80px"></asp:TextBox></td>    
                </tr> 
            </table> 

            <div class="Btwps FloatR">
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span><asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                onclick="lnkbtnCancel_Click">Cancel</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div> 

         <div class="Btwps FloatR" >
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span>
                            <asp:LinkButton ID="lnkbtnSave" runat="server" 
                                onclick="lnkbtnRegist_Click">Save</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>
    </div>
      

        
        
<%--    <fieldset class="sh-field2">
        <legend>등록</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltContItemCd" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlContItemCd" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltContYearMM" runat="server"></asp:Literal></li>
            <li>
                <asp:DropDownList ID="ddlContYear" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlContMM" runat="server"></asp:DropDownList>
            </li>
            <li><asp:Literal ID="ltContRoomNo" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtContRoomNo" runat="server" MaxLength="8" Width="80px"></asp:TextBox></li>
        </ul>
    </fieldset>
    <fieldset class="sh-field2">
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltContExchageRate" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtContExchageRate" runat="server" MaxLength="10" Width="80px"></asp:TextBox></li>
            <li><asp:Literal ID="ltContAmt" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtContAmt" runat="server" Width="80px"></asp:TextBox></li>
            <li><asp:Literal ID="ltUnitDong" runat="server"></asp:Literal></li>
        </ul>
    </fieldset>
    <fieldset class="sh-field2">
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltContContext" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtContContext" runat="server" Width="600px"></asp:TextBox></li>
        </ul>
    </fieldset>

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
    </div>--%>
      <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfRentCd" runat="server" />
     <asp:HiddenField ID="hfInvoiceNo" runat="server"/>
    <asp:HiddenField ID="hfRoomNo" runat="server"/>
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>