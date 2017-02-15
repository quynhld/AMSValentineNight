<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ParkingFeeRetail.aspx.cs" Inherits="KN.Web.Park.ParkingFeeRetail" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    <!--//
    function fnCheckType()
    {
        if (event.keyCode == 13)
        {
            document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
            return false;
        }
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

    function fnAccountList(rentCd, month)
{
    // Datum0 : 시작일
    // Datum1 : 종료일
    // Datum2 : 지불항목(FeeTy : 관리비,주차비..)
    // Datum3 : 임대(RentCd : 아파트, 상가)
    // Datum4 : 지불방법(PaymentCd : 카드,현금,계좌이체)

    var strRoomNo = document.getElementById("<%=txtInsRoomNo.ClientID%>").value;  
    
       
    window.open("/Common/RdPopup/RDPopupMonthlyParkingDebit.aspx?Datum0=" + rentCd + "&Datum1=" + month + "&Datum2=" + strRoomNo , "ParkingDebit", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no",false);
    return false;
}

    function fnDetailView(strUserSeq, strRentCd, strRefSeq, strPeriod)
    {
        document.getElementById("<%=txtUserSeq.ClientID%>").value = strUserSeq;
        document.getElementById("<%=hfRentCd.ClientID%>").value = strRentCd;
       
        <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
    }      
           

   
    //-->
</script>
<fieldset class="sh-field2 MrgB10">
    <legend>검색</legend>
    <ul class="sf2-ag MrgL30">        
        <li><asp:DropDownList ID="ddlInsRentCd" runat="server" AutoPostBack="True"
                onselectedindexchanged="ddlInsRentCd_SelectedIndexChanged"></asp:DropDownList></li>
        <li><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtInsRoomNo" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
        <li><asp:Literal ID="ltInstenantNm" runat="server" Text="Tenant Name"></asp:Literal></li>
        <li><asp:TextBox ID="txtTenanNm" runat="server" Width="320px" MaxLength="200" CssClass="sh-input"></asp:TextBox></li>
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
    <col width="300"/>
    <col width="80"/>
    <thead>
        <tr>
            <th class="Fr-line" align="center"><asp:Literal ID="ltTopSeq" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltTenantName" runat="server" Text="Tenant Name"></asp:Literal></th>
            <th align="center"><asp:Literal ID="ltLabelType" runat="server" Text="Type"></asp:Literal></th>
        </tr>
    </thead>
</table>
<div style="overflow-y:scroll;overflow-x:hidden;height:480px;width:840px;">
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailView" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="ddlInsRentCd" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvActMonthParkingCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemCreated="lvActMonthParkingCardList_ItemCreated" OnItemDataBound="lvActMonthParkingCardList_ItemDataBound"
                 OnItemDeleting="lvActMonthParkingCardList_ItemDeleting" 
            onitemupdating="lvActMonthParkingCardList_ItemUpdating">
            <LayoutTemplate>
                <table class="TypeA">
                    <col width="40"/>
                    <col width="80"/>
                    <col width="300"/>
                    <col width="80"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
               </table>
            </LayoutTemplate>
            <ItemTemplate>
                 <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:return fnDetailView('<%#Eval("UserSeq")%>')">
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltTenantNm" runat="server"></asp:Literal>
                    </td>
                     <td class="TbTxtCenter">
                        <asp:Literal ID="ltType" runat="server"></asp:Literal>
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
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
</asp:Content>