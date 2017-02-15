<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemberMngList.aspx.cs" Inherits="KN.Web.Config.Authority.MemberMngList"  ValidateRequest="false"%>
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

function fnMemberDetailView(strAccessOk, strCompCd, strMemSeq)
{
    if (strAccessOk == "Y")
    {
        document.getElementById("<%=hfCompCd.ClientID%>").value = strCompCd;
        document.getElementById("<%=hfMemSeq.ClientID%>").value = strMemSeq;
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
        return false;
    }
    else
    {
        alert('<%=AlertText%>');
        return false;
    }    
}
</script>
<fieldset class="sh-field3 MrgB10">
	<legend></legend>
	<ul class="sf3-ag MrgL180 ">
		<li><asp:DropDownList ID="ddlAuthGrp" runat="server"></asp:DropDownList></li>
		<li><asp:DropDownList ID="ddlKeyCd" runat="server"></asp:DropDownList></li>
		<li><asp:TextBox ID="txtKeyWord" runat="server" class="sh-input iw150"></asp:TextBox></li>
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
<asp:Literal ID="ltSearchCon" runat="server"></asp:Literal>
<asp:ListView ID="lvMemberList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
    OnLayoutCreated="lvMemberList_LayoutCreated" OnItemDataBound="lvMemberList_ItemDataBound" OnItemCreated="lvMemberList_ItemCreated">
    <LayoutTemplate>
        <table cellspacing="0" class="TypeA">
            <col width="90px"/>
            <col width="100px"/>
            <col width="90px"/>
            <col width="90px"/>
            <col width="90px"/>
            <col width="100px"/>
            <col width="90px"/>
            <thead>
                <tr>
                    <th><asp:Literal ID="ltUserId" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltAuthGrp" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltTel" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltMobile" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltMail" runat="server"></asp:Literal></th>
	                <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                <tr>
            </thead>
            <tbody>
                <tr id="iphItemlPlaceholderId" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr style="background-color: #FFFFFF; cursor: pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'"
            onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:fnMemberDetailView('<%#Eval("ModifyYn")%>','<%#Eval("CompNo")%>','<%#Eval("MemNo")%>');">
            <td><asp:Literal ID="ltInsUserId" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsAuthGrp" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsTel" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsMobile" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsMail" runat="server"></asp:Literal></td>
            <td><asp:Literal ID="ltInsInsDt" runat="server"></asp:Literal></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table cellspacing="0" class="TypeA">
            <col width="90px"/>
            <col width="100px"/>
            <col width="90px"/>
            <col width="90px"/>
            <col width="90px"/>
            <col width="100px"/>
            <col width="90px"/>
            <thead>
                <tr>
                    <th><asp:Literal ID="ltUserId" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltAuthGrp" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltTel" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltMobile" runat="server"></asp:Literal></th>
	                <th><asp:Literal ID="ltMail" runat="server"></asp:Literal></th>
	                <th class="end"><asp:Literal ID="ltInsDt" runat="server"></asp:Literal></th>
                <tr>
            </thead>
             <tbody>
                <tr><td colspan="7" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
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
				<span><asp:LinkButton ID="lnkbtnWrite" runat="server" OnClick="lnkbtnWrite_Click"></asp:LinkButton></span>
			</div>
		</div>
	</div>
</div>
<asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
<asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailview_Click"/>
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
<asp:TextBox ID="txtHfBoardTy" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfBoardCd" runat="server" Visible="false"></asp:TextBox>
<asp:HiddenField ID="hfMemSeq" runat="server"/>
<asp:HiddenField ID="hfCompCd" runat="server" />
</asp:Content>