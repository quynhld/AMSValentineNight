<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ExchangeList.aspx.cs" Inherits="KN.Web.Config.Exchange.ExchangeList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<%@ Register TagPrefix="cc1" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>
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
    
    function fnDetailView(intExchangeSeq)
    {
        document.getElementById("<%=hfExchangeSeq.ClientID%>").value = intExchangeSeq;
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
    }
//-->
</script>
<div style="overflow-y:scroll;height:500px;" class="iw840">    
	<div class="Gra-wp">
		<div class="FloatL">
			<table class="TbCel-Type1 iw390">
				<col width="30%"/>
				<col width="70%"/>
				<tr>
					<th><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
					<td><asp:Literal ID="ltInsDate" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<th><asp:Literal ID="ltBaseRate" runat="server"></asp:Literal></th>
					<td><asp:Literal ID="ltInsBaseRate" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<th><asp:Literal ID="ltFluctAmt" runat="server"></asp:Literal></th>
					<td><asp:Literal ID="ltInsFluctAmt" runat="server"></asp:Literal></td>
				</tr>
				<tr>
					<th><asp:Literal ID="ltFluctRatio" runat="server"></asp:Literal></th>
					<td><asp:Literal ID="ltInsFluctRatio" runat="server"></asp:Literal></td>
				</tr>
			</table>
		</div>
		<div class="Grap-s FloatL MrgL30"><cc1:ZedGraphWeb ID="zgwGraph" runat="server"></cc1:ZedGraphWeb></div>
	</div>
	<div>
        <asp:ListView ID="lvExchageList" runat="server" ItemPlaceholderID="iphItemPlaceHolderId" OnItemDataBound="lvExchageList_ItemDataBound" 
            OnLayoutCreated="lvExchageList_LayoutCreated" OnItemCreated="lvExchageList_ItemCreated">
            <LayoutTemplate>
		        <table class="TbCel-Type3-D MrgT30 Clear iw820">
			        <tr>
				        <th rowspan="2"><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
				        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltBaseRate" runat="server"></asp:Literal></th>
				        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltFluctAmt" runat="server"></asp:Literal></th>
				        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltFluctRatio" runat="server"></asp:Literal></th>
				        <th colspan="2" class="Bd-Lt"><asp:Literal ID="ltCash" runat="server"></asp:Literal></th>
				        <th colspan="2" class="Bd-Lt"><asp:Literal ID="ltWireTrans" runat="server"></asp:Literal></th>
			        </tr>
			        <tr>
				        <th class="Bd-Lt"><asp:Literal ID="ltBuying" runat="server"></asp:Literal></th>
				        <th class="Bd-Lt"><asp:Literal ID="ltSelling" runat="server"></asp:Literal></th>
				        <th class="Bd-Lt"><asp:Literal ID="ltSending" runat="server"></asp:Literal></th>
				        <th class="Bd-Lt"><asp:Literal ID="ltReceiving" runat="server"></asp:Literal></th>
			        </tr>
                    <tr id="iphItemPlaceHolderId" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("AppliedDt")%>");'>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsDate" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsBaseRate" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFluctAmt" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsFluctRatio" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsBuying" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsSelling" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsSending" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsReceiving" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type3-D MrgT30 iw820">
                    <tr>                
                        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltDate" runat="server"></asp:Literal></th>
                        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltBaseRate" runat="server"></asp:Literal></th>
                        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltFluctAmt" runat="server"></asp:Literal></th>
                        <th rowspan="2" class="Bd-Lt"><asp:Literal ID="ltFluctRatio" runat="server"></asp:Literal></th>
                        <th colspan="2" class="Bd-Lt"><asp:Literal ID="ltCash" runat="server"></asp:Literal></th>
                        <th colspan="2" class="Bd-Lt"><asp:Literal ID="ltWireTrans" runat="server"></asp:Literal></th>
                    </tr>
                    <tr>
                        <th class="Bd-Lt"><asp:Literal ID="ltBuying" runat="server"></asp:Literal></th>
                        <th class="Bd-Lt"><asp:Literal ID="ltSelling" runat="server"></asp:Literal></th>
                        <th class="Bd-Lt"><asp:Literal ID="ltSending" runat="server"></asp:Literal></th>
                        <th class="Bd-Lt"><asp:Literal ID="ltReceiving" runat="server"></asp:Literal></th>
                    </tr>
                    <tr>
                        <td colspan="8"><asp:Literal ID="ltNFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
		</div>
	   <div class="Clear">
		  <span id="spanPageNavi" runat="server" style="width:100%"></span>
	    </div>
    </div>	    
	<div class="Btwps FloatR">
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnWrite" runat="server" onclick="lnkbtnWrite_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
	</div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfExchangeSeq" runat="server"/>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>