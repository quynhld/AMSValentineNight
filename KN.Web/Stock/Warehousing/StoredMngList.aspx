<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="StoredMngList.aspx.cs" Inherits="KN.Web.Stock.Warehousing.StoredMngList" ValidateRequest="false"%>
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
        
        function fnDetailView(strWarehouseSeq, strWarehouseDetSeq)
        {
            document.getElementById("<%=hfWarehouseSeq.ClientID%>").value = strWarehouseSeq;
            document.getElementById("<%=hfWarehouseDetSeq.ClientID%>").value = strWarehouseDetSeq;
            
            <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
            return false;
        }
    //-->
    </script>
    
    <asp:ListView ID="lvStoredOrderList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
        OnLayoutCreated="lvStoredOrderList_LayoutCreated" OnItemDataBound="lvStoredOrderList_ItemDataBound" OnItemCreated="lvStoredOrderList_ItemCreated">
        <LayoutTemplate>
            <table class="TbCel-Type6-C">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltNoAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltReceitDt" runat="server"></asp:Literal></th>
                </tr>
                <tr runat="server" id="iphItemplaceHolderId"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("WarehouseSeq")%>", "<%#Eval("WarehouseDetSeq")%>");'>
                <td><asp:Literal ID="ltSeqList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltCompList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltDeptList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltItemList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltTotalAmountList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltAmountList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltNoAmountList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltTotalPriceList" runat="server"></asp:Literal></td>
                <td class="Bd-Lt"><asp:Literal ID="ltReceitDtList" runat="server"></asp:Literal></td>
            </tr>        
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TbCel-Type6-C">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltDept" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltNoAmount" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltReceitDt" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <td colspan="9" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    
    <div class="Clear">
            <span id="spanPageNavi" runat="server" style="width:100%;display:block;" class="Clear"></span>
    </div>
    
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
	<asp:HiddenField ID="hfCurrentPage" runat="server"/>
	<asp:HiddenField ID="hfWarehouseSeq" runat="server"/>
	<asp:HiddenField ID="hfWarehouseDetSeq" runat="server"/>
    								
</asp:Content>
