<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MaterialMngView.aspx.cs" Inherits="KN.Web.Stock.Material.MaterialMngView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnGraphPopup(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd)
    {
        var strData = String(strRentCd) + String(strSvcZoneCd) + String(strGrpCd) + String(strMainCd) + String(strSubCd);
        <%=Page.GetPostBackEventReference(imgbtnCheck)%>
        window.open("<%=Master.PAGE_POPUP2%>?" + "<%=Master.PARAM_DATA4%>=" + strData, 'GoodsGraph', 'status=no, resizable=no, width=700, height=650, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');
        return false;
    }
//-->
</script>
<div style="overflow-y:scroll;height:500px;" class="iw840">
	<div class="Tb-Tp-tit"><asp:Literal ID="ltInventoryReport" runat="server"></asp:Literal></div>
	<asp:UpdatePanel ID="upDetail" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnGraph" EventName="Click" />
	        <asp:AsyncPostBackTrigger ControlID="imgbtnCheck" EventName="Click" />
	    </Triggers>
	    <ContentTemplate>
	        <table class="TbCel-Type2-F iw820">
		        <tr>
			        <th><asp:Literal ID="ltClassi" runat="server"></asp:Literal></th>
			        <td colspan="3"><asp:Literal ID="ltInsClassi" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
			        <td colspan="3">
			            <asp:Literal ID="ltInsItemNm" runat="server"></asp:Literal>
			            <asp:ImageButton ID="imgbtnGraph"    runat="server" ImageUrl="~/Common/Images/Icon/graph.gif" ImageAlign="AbsMiddle" />&nbsp;
			            ( <asp:Literal ID="ltGoodsCd" runat="server"></asp:Literal>&nbsp;<asp:Image ID="imgPos" runat="server" ImageUrl="~/Common/Images/Icon/new.gif" ImageAlign="AbsMiddle"/>&nbsp;)
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
			        <td colspan="3"><span class="Tr3T1"><asp:Literal ID="ltInsComp" runat="server"></asp:Literal></span></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltPrimeCost" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsPrimeCost" runat="server"></asp:Literal> <asp:Literal ID="ltPrimeDong" runat="server"></asp:Literal></td>
			        <th><asp:Literal ID="ltSellingPrice" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsSellingPrice" runat="server"></asp:Literal>  <asp:Literal ID="ltSellingDong" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			        <td colspan="3"><asp:Literal ID="ltInsQty" runat="server"></asp:Literal> <asp:Literal ID="ltScale" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltVATRatio" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsVATRatio" runat="server"></asp:Literal> %</td>
			        <th><asp:Literal ID="ltVATYn" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsVATYn" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltStatus" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsStatus" runat="server"></asp:Literal></td>
			        <th><asp:Literal ID="ltAutoApproval" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsAutoApproval" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th colspan="4"><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
		        </tr>
		        <tr>
			        <td colspan="4"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="5" Columns="90" ReadOnly="true"></asp:TextBox></td>
		        </tr>
	        </table>
	        <div class="Tb-Tp-tit"><asp:Literal ID="ltDetails" runat="server"></asp:Literal></div>
	        <table class="TbCel-Type5-D iw820">
		        <tr>
			        <th>&nbsp;</th>
			        <th class="Bd-Lt"><asp:Literal ID="ltReleaseReq" runat="server"></asp:Literal></th>
			        <th class="Bd-Lt"><asp:Literal ID="ltOrderReq" runat="server"></asp:Literal></th>
		        </tr>
		        <tr class="end">
			        <td class="TxtBold TbTxtCenter"><asp:Literal ID="ltQuantity" runat="server"></asp:Literal></td>
			        <td class="Bd-Lt TbTxtRight2"><asp:Literal ID="ltInsReleaseQt" runat="server"></asp:Literal></td>
			        <td class="Bd-Lt TbTxtRight2"><asp:Literal ID="ltInsOrderQt" runat="server"></asp:Literal></td>
		        </tr>
	        </table>
            <asp:ImageButton ID="imgbtnCheck" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnCheck_Click"/>
	    </ContentTemplate>
	</asp:UpdatePanel>
</div>	
	<div class="Btwps FloatR">
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnModify" runat="server" OnClick="lnkbtnModify_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
	</div>
	<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfSvcZoneCd" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfGrpCd" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfMainCd" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfSubCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>