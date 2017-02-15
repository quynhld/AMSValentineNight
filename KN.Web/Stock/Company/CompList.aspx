<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CompList.aspx.cs" Inherits="KN.Web.Stock.Company.CompList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
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

function fnDetailView(strCompNo)
{
    document.getElementById("<%=hfCompNo.ClientID%>").value = strCompNo;
    <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
}
</script>
<fieldset class="sh-field1 MrgB10 ">
	<legend></legend>
	<ul class="sf1-ag MrgL180">
		<li>
			<asp:DropDownList ID="ddlKeyCd" runat="server"></asp:DropDownList>
		</li>
		<li>
			<asp:TextBox ID="txtKeyWord" runat="server" CssClass="sh-input"></asp:TextBox>
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
<asp:ListView ID="lvCompList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnLayoutCreated="lvCompList_LayoutCreated" 
    OnItemDataBound="lvCompList_ItemDataBound"  OnItemCreated="lvCompList_ItemCreated">
    <LayoutTemplate>
        <table cellspacing="0" class="TypeA">
            <col width="77px"/>
            <col width="109px"/>
            <col/>
            <col width="110px"/>
            <thead>
                <tr>
	                <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltBizIndus" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
	                <th class="end"><asp:Literal ID="ltModDt" runat="server"></asp:Literal></th>
                <tr>
            </thead>
            <tbody>
                <tr id="iphItemlPlaceholderId" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("CompNo")%>");'>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsBizIndus" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal></td>
            <td class="TbTxtCenter"><asp:Literal ID="ltInsModDt" runat="server"></asp:Literal></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table cellspacing="0" class="TypeA">
            <col width="77px"/>
            <col width="109px"/>
            <col/>
            <col width="110px"/>
            <thead>
                <tr>
	                <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltBizIndus" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
	                <th class="end"><asp:Literal ID="ltModDt" runat="server"></asp:Literal></th>
                <tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </tbody>
        </table>
    </EmptyDataTemplate>
</asp:ListView>
<div class="Clear">
	<span id="spanPageNavi" runat="server" style="width:100%"></span>
</div>
<div class="Btn-Type2-wp FloatR">
	<div class="Btn-Tp2-L">
		<div class="Btn-Tp2-R">
			<div class="Btn-Tp2-M">
				<span><asp:LinkButton ID="lnkbtnWrite" runat="server" onclick="lnkbtnWrite_Click"></asp:LinkButton></span>
			</div>
		</div>
	</div>
</div>

<asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
<asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
<asp:HiddenField ID="hfCompNo" runat="server"/>
</asp:Content>