<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ParkingDebitDetail.aspx.cs" Inherits="KN.Web.Park.ParkingDebitDetail" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar(); 
        }
    }       
    <!--//
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            return false;
        }
    }

    function fnSelectCheckValidate(strText)
    {

        return true;
    }

    function fnRentChange()
    {


        return false;
    }

    function fnCardNoChange()
    {

        return false;
    }

    function fnCalendarChange()
    {
        return false;
    }

    function fnOpenCalendar(obj)
    {

        return false;
    }

    function fnCheckValidate(strTxt)
    {           
        return true;
    }

    function fnOccupantList() {  
            var strPeriod = document.getElementById("<%=hfPeriod.ClientID%>").value;
            var strUserSeq = document.getElementById("<%=hfUserSeq.ClientID%>").value;
            var strRefSeq = document.getElementById("<%=hfReftSeq.ClientID%>").value;

            window.open("/Common/RdPopup/RDPopupParkingDebitList.aspx?Datum0=" + strUserSeq + "&Datum1=" + strPeriod+ "&Datum2=" + strRefSeq, "PrintParkingList", "status=no, resizable=yes, width=1100, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
                        
        }
    
    $(document).ready(function () {
        callCalendar();      
    });

    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").monthpicker({  
        });
       
    }    
    function SaveSuccess() {
        alert('Save Successful !');         
    } 

     function fnLoadData() {
          <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
     }
//-->
</script>
<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Company Name :"></asp:Literal></b></li>
        <li><asp:Literal ID="ltComPanyName" runat="server"></asp:Literal></li>
        <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></li>  
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Month :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True" AutoPostBack = "true"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>                     
          <li>
	                <asp:Literal ID="ltPrintYN" runat="server" Text="Printed YN" Visible="false"></asp:Literal>
	                <asp:DropDownList ID="ddlPrintYN" runat="server" 
                        onselectedindexchanged="ddlPrintYN_SelectedIndexChanged" 
                        AutoPostBack="true" Visible="false"></asp:DropDownList>
	            </li> 
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
<table class="TypeA" width="840px">
    <col width="40"/>
    <col width="80"/>
    <col width="200"/>
    <col width="50"/>
    <col width="50"/>
    <col width="110"/>
    <col width="100"/>
    <col width="160"/>
    
    <thead>
        <tr>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopSeq" runat="server" Text="Seq"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopDebitDt" runat="server" Text="Period"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="lttopDes" runat="server" Text="Description"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopUnit" runat="server" Text="Unit"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopQuantity" runat="server" Text="Quantity"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopUnitPrice" runat="server" Text="Unit Price"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopAmout" runat="server" Text="Amount"></asp:Literal></th>
            <th class="TbTxtCenter"><asp:Literal ID="ltTopMark" runat="server" Text="Remark"></asp:Literal></th>
            <th class="TbTxtCenter" >&nbsp;</th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:400px;width:840px;">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnPrint" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
    <div>    
        <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound"
                OnItemUpdating="lvActMonthParkingCardList_ItemUpdating" OnItemDeleting="lvActMonthParkingCardList_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="40"/>
                    <col width="70"/>
                    <col width="200"/>
                    <col width="50"/>
                    <col width="50"/>
                    <col width="100"/>
                    <col width="100"/>
                    <col width="200"/>                   
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfSeq" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltDebitDt" runat="server"></asp:Literal>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltDes" runat="server" ></asp:Literal>                       
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltUnit" runat="server"></asp:Literal>                        
                    </td>
		            <td class="TbTxtCenter">
		                 <asp:Literal ID="ltQuantity" runat="server"></asp:Literal>
		            </td>
		            <td class="TbTxtCenter">
                         <asp:Literal ID="ltUnitPrice" runat="server"></asp:Literal>
		            </td>
		            <td class="TbTxtCenter">
		               <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
		            </td>
                    <td class="TbTxtLeft">
                        <asp:Literal ID="ltRemark" runat="server"></asp:Literal>
                    </td>
                   
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="8" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
      </div>
        
        <asp:HiddenField ID="hfUserSeq" runat="server" />
        <asp:HiddenField ID="hfSeq" runat="server" Value="0"/>
        <asp:HiddenField ID="hfPeriod" runat="server" Value="0"/>
        <asp:HiddenField ID="hfSendPeriod" runat="server" Value="0"/>
        <asp:HiddenField ID="hfComNo" runat="server" Value="0"/>
        <asp:HiddenField ID="hfMemNo" runat="server" Value="0"/>
        <asp:HiddenField ID="hfIp" runat="server" Value="0"/>
        <asp:HiddenField ID="hfRentCd" runat="server" Value="0"/>
        <asp:HiddenField ID="hfReftSeq" runat="server" Value="0"/>
        <asp:HiddenField ID="hfSelectedLine" runat="server" />
        <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<div class="Btwps FloatR2">
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp3-L">
                    <div class="Btn-Tp3-R">
	                    <div class="Btn-Tp3-M">
		                    <span><asp:LinkButton ID="lnkbtnPrint" runat="server" 
                                OnClick="lnkbtnPrint_Click" Text="Print"></asp:LinkButton></span>
	                    </div>
                    </div>
                </div>
            </div>
            <div class="Btn-Type3-wp ">
                <div class="Btn-Tp3-L">
                    <div class="Btn-Tp3-R">
                        <div class="Btn-Tp3-M">
                            <span>
                                <asp:LinkButton ID="lnkbtnCancel" runat="server" Text="Cancel" OnClick="lnkbtnCancel_Click" Visible="false"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>