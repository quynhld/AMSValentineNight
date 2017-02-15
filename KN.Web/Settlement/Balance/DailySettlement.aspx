<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="DailySettlement.aspx.cs" Inherits="KN.Web.Settlement.Balance.DailySettlement" ValidateRequest="false"%>
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
    <asp:ListView ID="lvAccountsList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvAccountsList_ItemCreated"
        OnLayoutCreated="lvAccountsList_LayoutCreated" OnItemDataBound="lvAccountsList_ItemDataBound">
        <LayoutTemplate>
            <table class="TypeA">                
                <col width="15%"/>
                <col width="25%"/>
                <col width="20%"/>
                <col width="20%"/>
                <col width="20%"/>
                <thead>
                    <tr>
			            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltPayAmt" runat="server"></asp:Literal></th>
			            <th class="Ls-line"><asp:Literal ID="ltPayDt" runat="server"></asp:Literal></th>
		            </tr>
                </thead>
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                </tbody>                
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter"><asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal></td>
			    <td class="TbTxtCenter"><asp:Literal ID="ltNameList" runat="server"></asp:Literal></td>
			    <td class="TbTxtCenter">
			        <asp:Literal ID="ltItemList" runat="server"></asp:Literal>
			        <asp:TextBox ID="txtHfItemCd" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfVatRatio" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfRentCd1" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
			        <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
			    </td>			
			    <td class="TbTxtCenter"><asp:Literal ID="ltPayAmtList" runat="server"></asp:Literal></td>
			    <td class="TbTxtCenter"><asp:Literal ID="ltPayDtList" runat="server"></asp:Literal></td>
			    <asp:HiddenField ID="hfDongToDollarList" runat="server"/>
			    <asp:HiddenField ID="hfPaymentCdList" runat="server"/>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA">
                <col width="15%"/>
                <col width="25%"/>
                <col width="20%"/>
                <col width="20%"/>
                <col width="20%"/>
                <thead>
                    <tr>
			            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltPayAmt" runat="server"></asp:Literal></th>
			            <th class="Ls-line"><asp:Literal ID="ltPayDt" runat="server"></asp:Literal></a></th>
		            </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>        
    </asp:ListView>
    <div class="Btwps FloatR">
	    <div class="Btn-Type2-wp FloatL">
		    <div class="Btn-Tp2-L">
			    <div class="Btn-Tp2-R">
				    <div class="Btn-Tp2-M">
					    <span><asp:LinkButton ID="lnkbtnAccounts" runat="server" onclick="lnkbtnAccounts_Click"></asp:LinkButton></span>
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
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>