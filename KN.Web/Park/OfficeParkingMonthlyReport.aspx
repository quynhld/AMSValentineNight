<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OfficeParkingMonthlyReport.aspx.cs" Inherits="KN.Web.Park.OfficeParkingMonthlyReport" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    <!--//   
  

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

    function fnAccountList(room, tenant, period)
{
    // Datum0 : 시작일
    // Datum1 : 종료일
    // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
    // Datum3 : 임대(RentCd : 아파트, 상가)
    // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)

    var strRoomNo = document.getElementById("<%=txtInsRoomNo.ClientID%>").value;  
    
       
    window.open("/Common/RdPopup/RDPopupOfficeMonthlyParking.aspx?Datum0=" + room + "&Datum1=" + tenant + "&Datum2=" + period , "ParkingDebit", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
    return false;
}

    function fnDetailView(strUserSeq, strRentCd, strRefSeq, strPeriod)
    {
        document.getElementById("<%=txtUserSeq.ClientID%>").value = strUserSeq;
        document.getElementById("<%=hfRentCd.ClientID%>").value = strRentCd;
        document.getElementById("<%=hfRefSeq.ClientID%>").value = strRefSeq;
        document.getElementById("<%=hfPeriod.ClientID%>").value = strPeriod;
        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
    }  
    
    function fnCheckMonth(strAlertText, strTanVal, strLongVal, strShortVal) {
            var txtSearchDt = document.getElementById("<%=txtSearchDt.ClientID %>");
            if (trim(txtSearchDt.value) == "") {
                alert(strAlertText);
                txtSearchDt.focus();
                return false;
            }
            return true;
        }  
    
     function LoadCalender() {
        datePicker();
    }

    function datePicker() {
        $("#<%=txtSearchDt.ClientID %>").monthpicker();
           
    }

    $(document).ready(function () {
        LoadCalender();
    });   
    //-->
</script>
<fieldset class="sh-field5 MrgB10">
    <legend>검색</legend>
    <ul  class="sf5-ag MrgL30"> 
        <li><b><asp:Literal ID="Literal2" runat="server" Text="Month :"></asp:Literal></b></li>
        <li><asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server"></asp:TextBox></li>    
        <li><img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  /></li>    
             
        <li><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtInsRoomNo" runat="server" Width="80px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li> 

          <li>
	        <div class="Btn-Type4-wp">
		        <div class="Btn-Tp4-L">
			        <div class="Btn-Tp4-R">
				        <div class="Btn-Tp4-M">
					        <span><asp:LinkButton ID="lnkbtnPrint" runat="server" 
                                onclick="lnkbtnPrint_Click">Print</asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>		        
        </li>          

    </ul>
     <ul class="sf5-ag MrgL30 bgimgN">
       
         <li><asp:Literal ID="ltInstenantNm" runat="server" Text="Tenant Name"></asp:Literal></li>
        <li><asp:TextBox ID="txtTenanNm" runat="server" Width="400px" MaxLength="200" CssClass="sh-input"></asp:TextBox></li>      
          
             
        
     </ul>
</fieldset>
<table class="TypeA" width="840px">
    <col width="20"/>
    <col width="40"/>
    <col width="80"/>
    <col width="80"/>
    <col width="300"/>
    <col width ="120"/>

    <thead>
        <tr>
             <th align = "center">
            </th>
            <th align="center"><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltPeriod" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTenantName" runat="server" Text="Tenant Name"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltAmount" runat="server" Text="Amount"></asp:Literal></th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:480px;width:840px;">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>       
        <asp:AsyncPostBackTrigger ControlID="lnkbtnPrint" EventName="Click"/>        
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>        
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound"
                 OnItemDeleting="lvActMonthParkingCardList_ItemDeleting" 
            onitemupdating="lvActMonthParkingCardList_ItemUpdating">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width ="20"/>
                    <col width ="40"/>
                    <col width ="80"/>
                    <col width ="80"/>
                    <col width ="280"/>
                    <col width ="100"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                 <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("UserSeq")%>","<%#Eval("RentCd")%>","<%#Eval("Ref_Seq")%>","<%#Eval("Period")%>");'>
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Common/Images/Icon/Trash.gif" AlternateText="Delete this row "/>
                        </span>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtRefSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltPeriod" runat="server"></asp:Literal>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                    </td>
                     <td class="TbTxtCenter">
                        <asp:Literal ID="ltAmount" runat="server"></asp:Literal>
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
        <asp:HiddenField ID="txtUserSeq" runat="server" />     
        <asp:HiddenField ID="hfRentCd" runat="server" />  
        <asp:HiddenField ID="hfRefSeq" runat="server" />     
        <asp:HiddenField ID="hfPeriod" runat="server" />           
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
</asp:Content>