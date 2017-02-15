<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master"
    AutoEventWireup="true" CodeBehind="KNHoaDonCancel.aspx.cs" Inherits="KN.Web.Management.Manage.KNHoaDonCancel"
    ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strTxt)
        {
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");

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

        function fnOccupantList(strSeq) {            
            window.open("/Common/RdPopup/RDPopupReprintHoaDon.aspx?Datum0=" + strSeq , "ReprintInvoice", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
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
	                <b><asp:Literal ID="ltInvoiceYN" runat="server" Text="Bill Type"></asp:Literal></b>
	                <asp:DropDownList ID="ddlInvoiceYN" runat="server" 
                        onselectedindexchanged="ddlInvoiceYN_SelectedIndexChanged" AutoPostBack ="true" ></asp:DropDownList>
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
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkPrint" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlInvoiceYN" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            
        <table class="TbCel-Type6-A" cellpadding="0" >               
                <colgroup>
                    <col width="40px" />
                    <col width="70px" />                    
                    <col width="80px" />
                    <col width="120px" />
                    <col width="200px" />                   
                    <col width="80px" />
                    <col width="90px" />
                    <col width="70px" />
                    <tr>
                       <th class="Fr-line">
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"
                                Style="text-align: left" />
                        </th>
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
                            <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal ID="ltIssDt" runat="server"></asp:Literal>
                        </th>
                         <th>
                            <asp:Literal ID="ltBillTy" runat="server"></asp:Literal>
                        </th>
                    </tr>
                </colgroup>
            </table>
            <div style="overflow-y: scroll; height: 400px; width: 840px;">
                <asp:ListView ID="lvPrintoutList" runat="server" 
                    ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvPrintoutList_LayoutCreated" 
                    OnItemCreated="lvPrintoutList_ItemCreated" 
                    OnItemDataBound="lvPrintoutList_ItemDataBound" 
                    onitemcommand="lvPrintoutList_ItemCommand" 
                    onselectedindexchanged="lvPrintoutList_SelectedIndexChanged" >
                    <LayoutTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820"> 
                            <col width="40px" />                              
                            <col width="80px" />                            
                            <col width="80px" />
                            <col width="120px" />
                            <col width="220px" />                           
                            <col width="80px" />  
                            <col width="80px" /> 
                            <col width="70px" />                         
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                         <tr onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#ffffff'">
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:CheckBox ID="chkboxList" runat="server">
                                </asp:CheckBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltInvoiceNoP" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfrefSerialNoP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfPrintDetSeqP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfUserSeqP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtHfInvoiceNoP" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtRef_Seq" runat="server" Visible="false"></asp:TextBox>
                            </td>                            
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsRoomNoP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFeeNameP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsUserNmP" runat="server"></asp:Literal></td>                            
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltInsAmtViNoP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltnsIssDtP" runat="server"></asp:Literal></td>
                            <td class="Bd-Lt TbTxtRight"><asp:Literal ID="ltnsBillTy" runat="server"></asp:Literal></td>
                        </tr>
                        </ItemTemplate>
                    <EmptyDataTemplate>
                        <table cellpadding="0" class="TypeA-shorter" width="820">   
                            <col width="30px" />                              
                            <col width="80px" />
                            <col width="80px" />
                            <col width="80px" />
                            <col width="120px" />
                            <col width="220px" />                           
                            <col width="80px" /> 
                            <col width="70px" />                            
                            <tr>
                            <td colspan="9" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <asp:HiddenField ID="txthfrefSerialNo" runat="server" />
            <asp:HiddenField ID="txtsendTempDocNo" runat="server" />            

           <div class="Btwps31 FloatR2">
                <div class="Btn-Type31-wp ">
                    <div class="Btn-Tp31-L">
                        <div class="Btn-Tp31-R">
                            <div class="Btn-Tp31-M">
                                <span>
                                    <asp:LinkButton ID="lnkPrint" runat="server" Text="Report Cancel" 
                                    onclick="lnkPrint_Click" ></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type31-wp ">
                    <div class="Btn-Tp31-L">
                        <div class="Btn-Tp31-R">
                            <div class="Btn-Tp31-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" 
                                    onclick="lnkbtnCancel_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnDetailview_Click" />
    <asp:ImageButton ID="imgUpdateInvoice" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgUpdateInvoice_Click" />
    <asp:HiddenField ID="txthfPrintBundleNo" runat="server" />
    <asp:HiddenField ID="hfRentCd" runat="server" />
    <asp:HiddenField ID="hfRoomNo" runat="server" />
    <asp:HiddenField ID="HfReturnUserSeqId" runat="server"/> 
        </ContentTemplate>
    </asp:UpdatePanel>

     
    
</asp:Content>
