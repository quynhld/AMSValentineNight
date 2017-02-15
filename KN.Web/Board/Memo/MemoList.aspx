<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MemoList.aspx.cs" Inherits="KN.Web.Board.Memo.MemoList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="contList" ContentPlaceHolderID="cphContent" runat="server">
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
</script>
<fieldset class="sh-field1">
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
<asp:UpdatePanel ID="upLogList" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnDelete" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvMemoList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId"
            OnLayoutCreated="lvMemoList_LayoutCreated" OnItemDataBound="lvMemoList_ItemDataBound" OnItemCreated="lvMemoList_ItemCreated">
            <LayoutTemplate>
                <table cellspacing="0" class="TypeA">
                    <col width="77px"/>
                    <col width="175px"/>
                    <col/>
                    <col width="155px"/>            
                    <thead>
                        <tr>
                            <th><asp:CheckBox ID="chkAll" style="text-align:center" AutoPostBack="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged"/></th>
                            <th><asp:Literal ID="ltSender" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
                            <th class="end"><asp:Literal ID="ltReceiveDate" runat="server"></asp:Literal></th>
                        <tr>
                    </thead>
                    <tbody>
                        <tr id="iphItemlPlaceholderId" runat="server"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="cbkMemoSeq" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxList_CheckedChanged"></asp:CheckBox>
                        <asp:HiddenField ID="hfMemoSeq" runat="server" />
                        <asp:TextBox ID="txtHfMemoSeq" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtHfFilePath" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Literal ID="ltInsSender" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtHfInsSender" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td><asp:Literal ID="ltInsTitle" runat="server"></asp:Literal></td>
                    <td align="center"><asp:Literal ID="ltInsReceiveDate" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" class="TypeA MrgT10">
                    <col width="77px"/>            
                    <col width="110px"/>
                    <col/>
                    <col width="155px"/>
                    <thead>
                        <tr>
                            <th>&nbsp;</th>
                            <th><asp:Literal ID="ltSender" runat="server"></asp:Literal></th>
                            <th><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
                            <th class="end"><asp:Literal ID="ltReceiveDate" runat="server"></asp:Literal></th>
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
        <div class="Btwps FloatR">
            <div class="Btn-Type3-wp">
	            <div class="Btn-Tp3-L">
		            <div class="Btn-Tp3-R">
			            <div class="Btn-Tp3-M">
				            <span><asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
			            </div>
		            </div>
	            </div>
            </div>
        </div>
        <div class="Clear">
            <span id="spanPageNavi" runat="server" style="width:100%"></span>
        </div>
        <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
        <asp:HiddenField ID="hfCurrentPage" runat="server"/>
        <asp:HiddenField ID="hfMemoSeq" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>