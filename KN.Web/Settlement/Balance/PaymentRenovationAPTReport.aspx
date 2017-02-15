<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="PaymentRenovationAPTReport.aspx.cs" Inherits="KN.Web.Management.Manage.PaymentRenovationAPTReport" %>
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
      var roomNo = $('#<%=txtRoomNo.ClientID %>').val();
        if (roomNo =="") {
            alert(strText);
            $('#<%=txtRoomNo.ClientID %>').focus();
            return false;
        }        
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
   
    
    $(document).ready(function () {
        callCalendar();
        onLoadinit();
        
    });

    function callCalendar() {
        $("#<%=txtSearchDt.ClientID %>").datepicker({  
        });  
        $("#<%=txtESearchDt.ClientID %>").datepicker({  
        });         
       
    }    



    function SaveSuccess() {
        alert('Save Successful !');         
    }   

    function resetTable() {
        $("div#<%=upSearch.ClientID %> tbody").find("tr").each(function() {
            //get all rows in table
            $(this).removeClass('rowSelected');
        });  
    }

    function rowHover(trow) {
        $(trow).addClass('rowHover');
    }
    function rowOut(trow) {
        $(trow).removeClass('rowHover');
    }

    function ReLoadData() {
        document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
    }

    function fnConfirmR(strText) {
        if(confirm(strText)) {
            return true;
        } else {
            return false;
        }

    }
     function fnOccupantList(feeTy,startDt, roomNo, tenantNm, endDt) {
            window.open("/Common/RdPopup/RDPopupRenovationCarCardReport.aspx?Datum0=" + feeTy + "&Datum1=" + startDt + "&Datum2=" + roomNo + "&Datum3=" + tenantNm + "&Datum4=" + endDt, "ARAgingReport", "status=no, resizable=yes, width=1020, height=820, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no", false);
            
            return false;
        }

    function reLoadData(refSeq) {            
        window.open("/Common/RdPopup/RDPopupReciptRenovationAPT.aspx?Datum0=" + refSeq + "&Datum1=2", "KeangNamReciept", "status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        ReLoadData();
        return false;        
    }
//-->
   
</script>
<style type="text/css">
    .rowSelected 
    {
        background-color: #E4EEF5
    }
    .rowHover 
    {
        background-color: #E4EEF5
    }    
</style>
<fieldset class="sh-field5 MrgB10" >
    <legend>검색</legend>
    <ul class="sf5-ag MrgL30">
        <li><b><asp:Literal ID="Literal5" runat="server" Text="Rent :"></asp:Literal></b></li>
        <li><asp:DropDownList ID="ddlFeeType" runat="server"></asp:DropDownList></li>   
        <li><b><asp:Literal ID="ltComPanyNameTitle" runat="server" Text="Customer Name :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtCompanyNm" CssClass="grBg bgType2" MaxLength="10" Width="390px" runat="server"></asp:TextBox></li>        
    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
         <li><b><asp:Literal ID="Literal1" runat="server" Text="Room :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtRoomNo" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>  
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Period :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>
        <li><b><asp:Literal ID="Literal12" runat="server" Text="~"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtESearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtESearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>                                
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
<table  class="TbCel-Type6" style="margin-bottom: 0px;">
                    <col width="10% "/>
                    <col width="30%"/>
                    <col width="20%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
    <thead>
        <tr>
            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server" Text="Room"></asp:Literal></th>
            <th><asp:Literal ID="ltTopName" runat="server" Text="Tenant Name"></asp:Literal></th>
            <th><asp:Literal ID="ltTopFeeTy" runat="server" Text="Fee"></asp:Literal></th>
            <th><asp:Literal ID="ltPayLimitDay" runat="server" Text="Receipt Date"></asp:Literal></th>
            <th><asp:Literal ID="ltReturnDt" runat="server" Text="Return Date"></asp:Literal></th>
            <th><asp:Literal ID="ltTotalPaid" runat="server" Text="Amount"></asp:Literal></th>
            <th><asp:Literal ID="ltPayNoPaid" runat="server" Text=""></asp:Literal></th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:210px;width:840px;border-bottom: solid 2px rgb(182, 182, 182)">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>       
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>             
        <asp:AsyncPostBackTrigger ControlID="ddlFeeType" EventName="SelectedIndexChanged"/>  
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvPaymentList_ItemCreated" OnItemDataBound="lvPaymentList_ItemDataBound"
                OnItemUpdating="lvPaymentDetails_ItemUpdating" OnItemDeleting="lvPaymentDetails_ItemDeleting">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="10% "/>
                    <col width="30%"/>
                    <col width="20%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="cursor:pointer;" onmouseover="rowHover(this)" onmouseout="rowOut(this)">
                    <td align="center">
                        <asp:Literal ID="ltRoom" runat="server"></asp:Literal>
                        <asp:TextBox ID="txthfSeq" runat="server" Visible="False"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsName" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltFeeTy" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltPayDay" runat="server"></asp:Literal>
                    </td>                                                                                            
                    <td align="center">
                        <asp:Literal ID="ltReturnDate" runat="server"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal ID="ltInsTotalAmt" runat="server"></asp:Literal>
                    </td>
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton AlternateText="Reprint Receive" CommandName="Update" ID="imgbtnPrint" ImageUrl="~/Common/Images/Icon/print.gif" runat="server" />                            
                            <asp:ImageButton AlternateText="Delete this row " CommandName="Delete" ID="imgbtnDelete" ImageUrl="~/Common/Images/Icon/Trash.gif" runat="server" />
                        </span>                      
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>        
        </asp:ListView>
        <asp:HiddenField ID="hfSelectedLine" runat="server" />
        <asp:TextBox ID="txtHfRefSeq" runat="server" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span><asp:LinkButton ID="lnkExportExcel" runat="server" 
                            OnClick="lnkbtnExcelReport_Click" Text="Export Excel"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>

        
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>      
          
</asp:Content>
