<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="CancelList.aspx.cs" Inherits="KN.Web.Settlement.Balance.CancelList" ValidateRequest="false"%>
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
</script>
<asp:ListView ID="lvCancelList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDeleting="lvCancelList_ItemDeleting" 
    OnLayoutCreated="lvCancelList_LayoutCreated" OnItemDataBound="lvCancelList_ItemDataBound" OnItemCreated="lvCancelList_ItemCreated">
    <LayoutTemplate>
        <table class="TypeA MrgT10">
	        <thead>
		        <tr>
			        <th class="Fr-line"><span><asp:Literal ID="ltSeq" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltRentNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltUserNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltPaymentNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltAmt" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></span></th>
			        <th class="Ls-line"></th>
		        </tr>
	        </thead>
	        <tbody>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </tbody> 
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr> 
            <td class="TbTxtCenter">
                <span><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></span>
            </td>
            <td class="TbTxtCenter">
                <span><asp:Literal ID="ltIntRentNm" runat="server"></asp:Literal></span>
                <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            </td>
			<td class="TbTxtCenter">
			    <span><asp:Literal ID="ltInsItemNm" runat="server"></asp:Literal></span>
			    <asp:TextBox ID="txtHfItemCd" runat="server" Visible="false"></asp:TextBox>
			    <asp:TextBox ID="txtHfItemSeq" runat="server" Visible="false"></asp:TextBox>
			</td>
			<td class="TbTxtCenter">
			    <span><asp:Literal ID="ltInsUserNm" runat="server"></asp:Literal></span>
			    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
			    <asp:TextBox ID="txtHfFloor" runat="server" Visible="false"></asp:TextBox>
			    <asp:TextBox ID="txtHfRoom" runat="server" Visible="false"></asp:TextBox>
			    <asp:TextBox ID="txtHfSvcYear" runat="server" Visible="false"></asp:TextBox>
			    <asp:TextBox ID="txtHfSvcMM" runat="server" Visible="false"></asp:TextBox>
			</td>
			<td class="TbTxtCenter">
			    <span><asp:Literal ID="ltInsPaymentNm" runat="server"></asp:Literal></span>
			    <asp:TextBox ID="txtHfPaymentCd" runat="server" Visible="false"></asp:TextBox>
			</td>
			<td class="TbTxtCenter"><span><asp:Literal ID="ltInsAmt" runat="server"></asp:Literal></span></td>
			<td class="TbTxtCenter">
			    <span>
			        <asp:Literal ID="ltInsPaymentDt" runat="server"></asp:Literal>
			        <asp:TextBox ID="txtHfPaymentDt" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfPaymentSeq" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfPaymentDetSeq" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfStatusCd" runat="server" Visible="false"></asp:TextBox>
			    </span>
			</td>
			<td class="TbTxtCenter"><span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span></td>			
        </tr>        
    </ItemTemplate>
    <EmptyDataTemplate>
        <table class="TypeA MrgT10">
	        <thead>
		        <tr>
			        <th class="Fr-line"><span><asp:Literal ID="ltSeq" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltRentNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltUserNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltPaymentNm" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltAmt" runat="server"></asp:Literal></span></th>
			        <th><span><asp:Literal ID="ltPaymentDt" runat="server"></asp:Literal></span></th>
			        <th class="Ls-line"></th>
		        </tr>
	        </thead>
	        <tbody>
	            <tr>
	                <td colspan="8" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
	            </tr>
	        </tbody>
        </table>
    </EmptyDataTemplate>
</asp:ListView>
<div class="Clear">
    <span id="spanPageNavi" runat="server" style="width:100%"></span>
</div>
<asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
<asp:HiddenField ID="hfCurrentPage" runat="server"/>
<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:HiddenField ID="hfRentSeq" runat="server"/>
</asp:Content>