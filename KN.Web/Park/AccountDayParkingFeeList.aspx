<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AccountDayParkingFeeList.aspx.cs" Inherits="KN.Web.Park.AccountDayParkingFeeList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {                
                LoadCalender();
            }
        }

function fnMonthlyParkingList()
{
    var strData1 = document.getElementById("<%=hfSearchDt.ClientID%>").value;
    var strData2 = document.getElementById("<%=hfSearchDt.ClientID%>").value;

    window.open('/Common/RdPopup/RDPopupMonthParkingList.aspx?Datum0=' + strData1 + '&Datum1=' + strData2 + '&Datum2=<%=Session["LangCd"].ToString()%>', "MonthlyParkingList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
    return false;
}

function LoadCalender() {
            datePicker();

            var options = {
                rowsPerPage: 13
            };
            $('#tblListDebit').tablePagination(options); 
        }

function datePicker() {
    $("#<%=txtSearchDt.ClientID %>").datepicker();
    

}

function fnMovePage(intPageNo)
{
    if (intPageNo == null) 
    {
        intPageNo = 1;
    }
    
    document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
    <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
}

  $(document).ready(function () {
            LoadCalender();
        });

</script>
<fieldset class="sh-field2 MrgB10">
    <legend>검색</legend>
    <ul class="sf2-ag MrgL30">       
        <li><asp:Literal ID="ltParkingDay" runat="server"></asp:Literal></li>
        <li> 
            <asp:TextBox ID="txtSearchDt" CssClass="grBg bgType2" MaxLength="10" Width="70px" runat="server" Visible="True"></asp:TextBox>    
		    <img align="absmiddle" alt="Calendar" onclick="CallCalendar('#<%=txtSearchDt.ClientID %>')" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;align: absmiddle;"  />
		    <asp:HiddenField ID="hfSearchDt" runat="server"/>
        </li>                
        <li><asp:DropDownList ID="ddlSeq" runat="server"></asp:DropDownList></li>
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
<asp:UpdatePanel ID="upResult1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnReport" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnAccounts" EventName="Click" />            
        </Triggers>
        <ContentTemplate>
<asp:ListView ID="lvActDayParkingFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvActDayParkingFeeList_ItemCreated" 
    OnLayoutCreated="lvActDayParkingFeeList_LayoutCreated" OnItemDataBound="lvActDayParkingFeeList_ItemDataBound">
    <LayoutTemplate>
        <table class="TypeA" id="tblListDebit">
            <col width="60"/>
            <col width="100"/>
            <col width="260"/>
            <col width="50"/>
            <col width="70"/>
            <col width="70"/>
            <col width="70"/>
            <thead>
                <tr>
                    <th class="Fr-line"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCarNo" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltUseTime" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCarTy" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
                    <th class="Ls-line"><asp:Literal ID="ltFee" runat="server"></asp:Literal></th>
                </tr>
            </thead>
            <tbody>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </tbody>                    
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td class="TbTxtCenter"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltCarNoList" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltUseTimeList" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltCarTyList" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFeeNET" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFeeVAT" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter">
                <asp:Literal ID="ltFeeList" runat="server"></asp:Literal>
                <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfAccountYn" runat="server" Visible="false"></asp:TextBox>	
                <asp:TextBox ID="txtHfDongToDollarList" runat="server" Visible="false"></asp:TextBox>	
                <asp:TextBox ID="txtHfStartTime" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfEndTime" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtHfVatRatio" runat="server" Visible="false"></asp:TextBox>
            </td>		         
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table class="TypeA">
            <col width="60"/>
            <col width="100"/>
            <col width="260"/>
            <col width="50"/>
            <col width="70"/>
            <col width="70"/>
            <col width="70"/>
            <thead>
                <tr>
                    <th class="Fr-line"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCarNo" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltUseTime" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCarTy" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
                    <th class="Ls-line"><asp:Literal ID="ltFee" runat="server"></asp:Literal></th>
                </tr>
            </thead>
            <tbody>
            <tr>
                <td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
            </tr>
            </tbody>
        </table>
    </EmptyDataTemplate>        
</asp:ListView>
<table class="TypeA">
    <colgroup>
        <col width="60"/>
        <col width="100"/>
        <col width="260"/>
        <col width="50"/>
        <col width="70"/>
        <col width="70"/>
        <col width="70"/>
        <thead>
            <tr>
                <th class="Fr-line">
                    &nbsp;</th>
                <th>
                    &nbsp;</th>
                <th>
                    <asp:Literal ID="ltTotal" runat="server"></asp:Literal>
                    &nbsp;(VND)</th>
                <th>
                    &nbsp;</th>
                <th>
                    <asp:Literal ID="ltTotalNET" runat="server"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="ltTotlaVAT" runat="server"></asp:Literal>
                </th>
                <th class="Ls-line">
                    <asp:Literal ID="ltTotalFee" runat="server"></asp:Literal>
                </th>
            </tr>
        </thead>
    </colgroup>
</table>
<div class="Clear">
    <span id="spanPageNavi" runat="server" style="width:100%"></span>
</div>
<div class="Btwps FloatR">
    <div class="Btn-Type3-wp FloatL">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				    <span><asp:LinkButton ID="lnkbtnAccounts" runat="server" OnClientClick="javascript:return fnMonthlyParkingList();" OnClick="lnkbtnAccounts_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
</div>
<div class="Btwps FloatR">
    <div class="Btn-Type3-wp FloatL">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				    <span><asp:LinkButton ID="lnkbtnReport" runat="server" 
                        OnClientClick="javascript:return fnMonthlyParkingList();" 
                        ></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
</div>
<asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
<asp:HiddenField ID="hfAlertText" runat="server"/>
    </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>