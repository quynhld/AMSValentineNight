<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="LateFeeList.aspx.cs" Inherits="KN.Web.Management.LateFee.LateFeeList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

<script language="javascript" type="text/javascript">
function fnMovePage(intPageNo) 
{
    if (intPageNo == null) 
    {
        intPageNo = 1;
    }
    
    document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
    <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
}

function fnLateFeeList()
{
    var strData1 = document.getElementById("<%=hfRentCd.ClientID%>").value;
    var strData2 = document.getElementById("<%=hfFeeTy.ClientID%>").value;
    
    window.open("/Common/RdPopup/RDPopupLateFeeInfo.aspx?Datum0=" + strData1 + "&Datum1=" + strData2, "LateFeeList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
    return false;
}

function fnDetailView(strRentalYear, strRentalMM, strUserSeq)
{
    document.getElementById("<%=hfRentalYear.ClientID%>").value = strRentalYear;
    document.getElementById("<%=hfRentalMM.ClientID%>").value = strRentalMM;
    document.getElementById("<%=hfUserSeq.ClientID%>").value = strUserSeq;

    <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
}

function fnCheckValidate(strAlert)
{
    var strNm = document.getElementById("<%=txtNm.ClientID%>");
    var strSearchFloor = document.getElementById("<%=txtSearchFloor.ClientID%>");
    var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
    var strYear = document.getElementById("<%=ddlYear.ClientID%>");
    var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");
        
    if (trim(strNm.value) == "" && trim(strSearchFloor.value) == "" && trim(strSearchRoom.value) == "" && trim(strYear.value) == "" && trim(strMonth.value) == "")
    {       
        alert(strAlert);
        return false;
    }
    
    return true;
}
</script>
<fieldset class="sh-field2 MrgB10">
    <legend>검색</legend>
    <ul class="sf2-ag MrgL160">
        <li><asp:Literal ID="ltName" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>
        <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
        <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
        <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
        <li>
            <div class="C235-st FloatL">
                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
	        
            </div>
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
<asp:ListView ID="lvLateFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvLateFeeList_ItemCreated"
        OnLayoutCreated="lvLateFeeList_LayoutCreated" OnItemDataBound="lvLateFeeList_ItemDataBound">
    <LayoutTemplate>
        <table class="TypeA">
            <col width="20%"/>
            <col width="15%"/>
            <col width="10%"/>
            <col width="15%"/>
            <col width="15%"/>
            <col width="15%"/>
            <col width="15%"/>
            <thead>
                <tr>
		            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
		            <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
		            <th><asp:Literal ID="ltYearMM" runat="server"></asp:Literal></th>
		            <th><asp:Literal ID="ltPayment" runat="server"></asp:Literal></th>
		            <th><asp:Literal ID="ltLateFee" runat="server"></asp:Literal></th>
		            <th class="Ls-line"><asp:Literal ID="ltLateDt" runat="server"></asp:Literal></th>
	            </tr>
            </thead>
            <tbody>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </tbody>                
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("RentalYear")%>","<%#Eval("RentalMM")%>","<%#Eval("UserSeq") %>");'>
		    <td class="TbTxtCenter"><asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal></td>
		    <td class="TbTxtCenter"><asp:Literal ID="ltNameList" runat="server"></asp:Literal></td>
		    <td class="TbTxtCenter"><asp:Literal ID="ltYearMMList" runat="server"></asp:Literal></td>
		    <td class="TbTxtCenter"><asp:Literal ID="ltPaymentList" runat="server"></asp:Literal></td>
		    <td class="TbTxtCenter"><asp:Literal ID="ltLateFeeList" runat="server"></asp:Literal></td>
		    <td class="TbTxtCenter">
		        <asp:Literal ID="ltLateDtList" runat="server"></asp:Literal>
		        <asp:TextBox ID="txtHfContractNoList" runat="server" Visible="false"></asp:TextBox>
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
    <div><span id="spanPageNavi" runat="server" style="width:100%"></span></div>
    <div class="Btn-Type3-wp FloatR">
	    <div class="Btn-Tp3-L">
		    <div class="Btn-Tp3-R">
			    <div class="Btn-Tp3-M">
				    <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" onclick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
			    </div>
		    </div>
	    </div>
    </div>
    <div class="Btn-Type3-wp FloatR">
        <div class="Btn-Tp3-L">
	        <div class="Btn-Tp3-R">
		        <div class="Btn-Tp3-M">
			        <span><asp:LinkButton ID="lnkbtnReport" runat="server" OnClientClick="javascript:return fnLateFeeList();"></asp:LinkButton></span>
		        </div>
	        </div>
        </div>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfRentalYear" runat="server"/>
    <asp:HiddenField ID="hfRentalMM" runat="server"/>
    <asp:HiddenField ID="hfUserSeq" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server"/>
    <asp:HiddenField ID="hfFeeTy" runat="server"/>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
</asp:Content>