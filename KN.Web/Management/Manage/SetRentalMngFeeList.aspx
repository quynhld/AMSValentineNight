<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="SetRentalMngFeeList.aspx.cs" Inherits="KN.Web.Management.Manage.SetRentalMngFeeList" ValidateRequest="false"%>
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
            $('#<%=txtCompanyNm.ClientID %>').keydown(function(e) {          
               var code = e.keyCode || e.which;
               if (code == '9') {             
                   $('#<%=imgbtnSearchCompNm.ClientID %>').click();
               
               return false;
               }
                return true;
            });    
              

			$("#<%=lnkbtnSearch.ClientID %>").bind("click", function () {
			    ShowLoading("Loading data ......");			    
			});  
        }

        function fnChangePopup(strCompNmId, strRoomNoId,strUserSeqId,strCompNmS,strRentCd)
        {
            strCompNmS = $('#<%=txtCompanyNm.ClientID %>').val();
            strRentCd = document.getElementById("<%=ddlRentCd.ClientID%>").value;
            window.open("/Common/Popup/PopupCompFindList.aspx?CompID=" + strCompNmId + "&RoomID=" + strRoomNoId+ "&UserSeqId=" + strUserSeqId+ "&CompNmS=" + strCompNmS+"&RentCdS=" +strRentCd , 'SearchTitle', 'status=no, resizable=no, width=575, height=655, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');        
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
        function fnDetailView(refSerialNo)
        {
            document.getElementById("<%=txthfrefSerialNo.ClientID%>").value = refSerialNo;
            <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
            return false;
        }

        function fnOccupantList(strSeq,strRoomNo) {            
            window.open("/Common/RdPopup/RDPopupReprintHoaDon.aspx?Datum0=" + strSeq + "&Datum1=" + strRoomNo, "ReprintInvoice", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
            document.getElementById("<%=txtsendTempDocNo.ClientID%>").value = strSeq;            
        }
          $(document).ready(function () {
             LoadCalender();
        });
          
        function callBack(compNm,rentCd,roomNo,userSeq) {
            if (rentCd == '0007' ||rentCd == '0008' ) {
                rentCd = '9900';    
            }
            if (rentCd == '0003') {
                rentCd = '0002';    
            }                
            document.getElementById("<%=ddlRentCd.ClientID%>").value = rentCd;
        }
    //-->
    </script>
    
    <fieldset class="sh-field5 MrgB10">
            <legend>검색</legend>
        <ul class="sf5-ag MrgL30">
            <li><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></li>
            <li><asp:DropDownList ID="ddlItemCd" runat="server"></asp:DropDownList></li>
            <li><b><asp:Literal ID="ltSearchRoom" runat="server" Text="Room :" ></asp:Literal></b></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Name :"></asp:Literal></b></li>
            <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="250" Width="220px" runat="server"></asp:TextBox></li>
            <asp:ImageButton ID="lnkLoadData" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="lnkLoadData_Click"/> 
        <li><asp:ImageButton ID="imgbtnSearchCompNm" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/></li> 
         
        </ul>

        <ul class="sf5-ag MrgL30 bgimgN">
             <li>
	                <b><asp:Literal ID="ltInvoiceYN" runat="server" Text="Printed"></asp:Literal></b>
	                <asp:DropDownList ID="ddlInvoiceYN" runat="server" ></asp:DropDownList>
	            </li>              
	            <li>
	                <b><asp:Literal ID="ltInvoice" runat="server" Text="Invoice No"></asp:Literal></b>               
	            </li>
	           <li><asp:TextBox ID="txtInvoice" runat="server" Width="100px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            
	            <li>
                <b><asp:Literal ID="ltPeriod" runat="server" Text = "Period"></asp:Literal></b>
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
       
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlInvoiceYN" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkLoadData" EventName="Click" />            
        </Triggers>
        <ContentTemplate>     
    <div style="height: 180px; width: 820px;" >
        <table class="TbCel-Type6-A" cellpadding="0" >               
                <colgroup>
                    <col width="70px" />                    
                    <col width="80px" />
                    <col width="120px" />
                    <col width="200px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="80px" />
                    <col width="90px" />
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
                            <asp:Literal ID="ltUserNm" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltVat" runat="server" Text="VAT"></asp:Literal>
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
                    onselectedindexchanged="lvPrintoutList_SelectedIndexChanged" EnableViewState="False">
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">                           
                            <col width="80px" />
                            
                            <col width="80px" />
                            <col width="120px" />
                            <col width="220px" />
                            <col width="90px" />
                            <col width="80px" />
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
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNoP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFeeNameP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsUserNmP" runat="server"></asp:Literal></td>
                             <td class="Bd-Lt TbTxtCenter"> <asp:Literal ID="ltInsUPriceP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltVATAmt" runat="server"></asp:Literal></td>
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
     </ContentTemplate>
    </asp:UpdatePanel>       
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="lnkbtnReprint" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="lnkLoadData" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlInvoiceYN" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnUpdate" EventName="Click" />          
        </Triggers>
        <ContentTemplate>
    <div style="height: 260px; width: 840px; margin-top:40px">
        <asp:ListView ID="lvRentalMngList" runat="server" 
            ItemPlaceholderID="iphItemPlaceHolderID" 
            OnLayoutCreated="lvRentalMngList_LayoutCreated" 
            OnItemCreated="lvRentalMngList_ItemCreated" 
            OnItemUpdating="lvRentalMngList_ItemUpdating" 
            OnItemDataBound="lvRentalMngList_ItemDataBound" 
            onselectedindexchanged="lvRentalMngList_SelectedIndexChanged">
            <LayoutTemplate>
            <table cellspacing="0"  class="TbCel-Type1B iw840" width = "840">
                    <col width="90px" />
                    <col width="110px" />
                    <col width="90px" />
                    <col width="110px" />
                    <col width="90px" />
                    <col width="90px" />                    
                    <tr id="iphItemPlaceHolderID" runat="server">
                    </tr>
            </table>                
            </LayoutTemplate>
            <ItemTemplate>
                 <tr>
                   <th align="center"><asp:Literal ID="ltlnInvoice" runat="server"></asp:Literal></th>
                    <td class="Bd-Lt TbTxtCenter">                       
                        <asp:Literal ID="ltInvoiceNo" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfrefSerialNo" runat="server" Visible="false"></asp:TextBox>
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
                            <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
                    </td>                                                 
                </tr>

                <tr>                    
                    <th align="center"><asp:Literal ID="ltlnsCpNm" runat="server" ></asp:Literal></th>
                    <td class="Bd-Lt TbTxtLeft" colspan="3" >
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
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtStartDt" runat="server" Width="80px"></asp:TextBox></td>
                       <th align="center"><asp:Literal ID="ltlnEDate" runat="server" ></asp:Literal></th>        
                       <td class="Bd-Lt TbTxtLeft"><asp:TextBox ID="txtEndDt" runat="server" Width="80px"></asp:TextBox></td>    
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellpadding="0" class="TbCel-Type1B iw840" width="840">
                    <col width="90px" />
                    <col width="110px" />
                    <col width="90px" />
                    <col width="110px" />
                    <col width="90px" />
                    <col width="90px" />
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
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span>
                            <asp:LinkButton ID="lnkbtnCancel" runat="server" 
                                onclick="lnkbtnCancel_Click" Width="46px">Cancel</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>
      <div class="Btwps FloatR">
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span><asp:LinkButton ID="lnkExportExcel" runat="server" 
                                OnClick="lnkbtnExcel_Click" >Export Excel</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>
      <div class="Btwps FloatR">
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span><asp:LinkButton ID="lnkbtnUpdate" runat="server" 
                                OnClick="lnkbtnUpdate_Click" >Update</asp:LinkButton></span>
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
				            <span><asp:LinkButton ID="lnkbtnRegist" runat="server" 
                                onclick="lnkbtnRegist_Click">Register</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>

         <div class="Btwps FloatR">
            <div class="Btn-Type3-wp ">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span>
                            <asp:LinkButton ID="lnkbtnReprint" runat="server" onclick="lnkbtnReprint_Click" 
                                >RePrint</asp:LinkButton></span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>               
      <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
      <asp:HiddenField ID="hfRentCd" runat="server" />
      <asp:HiddenField ID="hfInvoiceNo" runat="server"/>
      <asp:HiddenField ID="hfRoomNo" runat="server"/>
    <asp:HiddenField ID="HfReturnUserSeqId" runat="server"/>
</asp:Content>