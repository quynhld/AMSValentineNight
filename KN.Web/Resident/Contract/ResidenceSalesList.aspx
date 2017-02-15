<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ResidenceSalesList.aspx.cs" Inherits="KN.Web.Resident.Contract.ResidenceSalesList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
function fnMovePage(intPageNo) 
{
    if (intPageNo == null) 
    {
        intPageNo = 1;
    }
    
    document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
    <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
}

function fnContractList()
{
    var strRentCd = document.getElementById("<%=hfRentCd.ClientID%>").value;
    var strSearchTy = document.getElementById("<%=ddlKeyCd.ClientID%>").value;
    var strKeyWord = document.getElementById("<%=txtKeyWord.ClientID%>").value;
    var strConcYn = document.getElementById("<%=ddlConclusionYn.ClientID%>").value;
    
    window.open("/Common/RdPopup/RDPopupSalesContractList.aspx?Datum0=" + strRentCd + "&Datum1=" + strSearchTy + "&Datum2=" + strKeyWord + "&Datum3=" + strConcYn, "ContractList", "status=no, resizable=no, width=1000, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
    return false;
}

function fnDetailView(strViewPage, strParams1, strParams2, strRentCd, intRentSeq)
{
    document.getElementById("<%=hfPageUrl.ClientID%>").value = strViewPage;
    document.getElementById("<%=hfParam1.ClientID%>").value = strParams1;
    document.getElementById("<%=hfParam2.ClientID%>").value = strParams2;
    document.getElementById("<%=hfRentCd.ClientID%>").value = strRentCd;
    document.getElementById("<%=hfRentSeq.ClientID%>").value = intRentSeq;
    <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
}

function fnCheckValidate(strAlert)
{
    var strKeyWord = document.getElementById("<%=txtKeyWord.ClientID%>");
    
//    if (trim(strKeyWord.value) == "")
//    {
//        alert(strAlert);
//        strKeyWord.focus();
//        return false;
//    }        
}

function fnCheckType()
{
    if (event.keyCode == 13)
    {
        document.getElementById("<%=lnkbtnSearch.ClientID%>").click();
        return false;
    }
}
//-->
</script>
<fieldset class="sh-field3 ">
	<legend>검색</legend>
	<ul class="sf3-ag MrgL60">
		<li><asp:DropDownList ID="ddlKeyCd" runat="server" OnKeyPress="javascript:return fnCheckType();"></asp:DropDownList></li>
		<li><asp:TextBox ID="txtKeyWord" runat="server" CssClass="sh-input iw150" OnKeyPress="javascript:return fnCheckType();"></asp:TextBox></li>
		<li><asp:DropDownList ID="ddlConclusionYn" runat="server" OnKeyPress="javascript:return fnCheckType();"></asp:DropDownList></li>
        <li><asp:Literal ID="ltNo" runat="server" Text="Lessor Type"></asp:Literal></li>
        <li><asp:DropDownList ID="ddlTenantTy" runat="server">
            <asp:ListItem Value="">All Lessor</asp:ListItem>
            <asp:ListItem Value="N">Lessor</asp:ListItem>
            <asp:ListItem Value="Y">SubLessor</asp:ListItem>
            </asp:DropDownList></li>
		<li>
			<div class="Btn-Type4-wp">
				<div class="Btn-Tp4-L">
					<div class="Btn-Tp4-R">
						<div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
					</div>
				</div>
			</div>
		</li>
	</ul>
</fieldset>
<asp:UpdatePanel ID="upBasicInfo" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        <asp:PostBackTrigger ControlID="lnkbtnExcelReport"/>
    </Triggers>
    <ContentTemplate>
    <asp:ListView ID="lvRentList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" 
        OnLayoutCreated="lvRentList_LayoutCreated" OnItemDataBound="lvRentList_ItemDataBound" OnItemCreated="lvRentList_ItemCreated">
        <LayoutTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="60px">
                <col width="80px">
                <col width="240px">
                <col width="119px">
                <col width="70px">
                <col width="80px">
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltNo" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltFloor" runat="server"></asp:Literal>/<asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTenant" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltContPeriod" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltConcYn" runat="server"></asp:Literal></th>
	                    <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="iphItemlPlaceholderId"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%=Master.PAGE_VIEW%>","<%=Master.PARAM_DATA1%>","<%=Master.PARAM_DATA2%>","<%#Eval("RentCd")%>","<%#Eval("RentSeq")%>");'>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsNo" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFloor" runat="server"></asp:Literal> / <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsTenant" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsContPeriod" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsConcYn" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsInsDt" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TypeA MrgT10">
                <col width="60px">
                <col width="80px">
                <col width="240px">
                <col width="119px">
                <col width="70px">
                <col width="80px">
                <thead>
                    <tr>
	                    <th><asp:Literal ID="ltNo" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltFloor" runat="server"></asp:Literal>/<asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltTenant" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltContPeriod" runat="server"></asp:Literal></th>
	                    <th><asp:Literal ID="ltConcYn" runat="server"></asp:Literal></th>
	                    <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                    <tr>
                </thead>
                <tbody>
                    <tr><td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="Clear">
        <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>
    <div class="Btwps FloatR2">
        <div class="Btn-Type2-wp FloatL">
	        <div class="Btn-Tp2-L">
		        <div class="Btn-Tp2-R">
			        <div class="Btn-Tp2-M">
				        <span><asp:LinkButton ID="lnkbtnWrite" runat="server"></asp:LinkButton></span>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="Btn-Type3-wp FloatL">
	        <div class="Btn-Tp3-L">
		        <div class="Btn-Tp3-R">
			        <div class="Btn-Tp3-M">
				        <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClick="lnkbtnExcelReport_Click"></asp:LinkButton></span>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="Btn-Type3-wp FloatR">
            <div class="Btn-Tp3-L">
	            <div class="Btn-Tp3-R">
		            <div class="Btn-Tp3-M">
			            <span><asp:LinkButton ID="lnkbtnReport" runat="server" OnClientClick="javascript:return fnContractList();"></asp:LinkButton></span>
		            </div>
	            </div>
            </div>
        </div>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfPageUrl" runat="server"/>
    <asp:HiddenField ID="hfParam1" runat="server"/>
    <asp:HiddenField ID="hfParam2" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server"/>
    <asp:HiddenField ID="hfRentSeq" runat="server"/>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>