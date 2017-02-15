<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MaterialMngModify.aspx.cs" Inherits="KN.Web.Stock.Material.MaterialMngModify" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    <!--//        
        function fnCompSearch(strText1, strData1, strText2, strData2)
        {
            <%=Page.GetPostBackEventReference(imgbtnComp)%>
            window.open("<%=Master.PAGE_POPUP1%>?" + strText1 + "=" + strData1 + "&" + strText2 + "=" + strData2, 'SearchCompany', 'status=no, resizable=no, width=570, height=280, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
            
            return false;
        }
    //-->
    </script>
	<asp:UpdatePanel ID="upGoodsInfo" runat="server" UpdateMode="Conditional">
	    <ContentTemplate>
	        <table class="TbCel-Type2-E">
		        <tr>
			        <th><asp:Literal ID="ltRentCd" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsRentCd" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSvcZoneCd" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsSvcZoneCd" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltGrpCd" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsGrpCd" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltMainCd" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsMainCd" runat="server"></asp:Literal></td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSubCd" runat="server"></asp:Literal></th>
			        <td><asp:Literal ID="ltInsSubCd" runat="server"></asp:Literal></td>
			        <asp:CheckBox ID="chkAutoApproval" runat="server"/>	
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtCompNm" runat="server" MaxLength="255" Width="210px" CssClass="input-St FloatL" ReadOnly="true"></asp:TextBox>
				        <div class="Btn-Type1-wp FloatL Mrg0 MrgL10">
					        <div class="Btn-Tp-L">
						        <div class="Btn-Tp-R">
							        <div class="Btn-Tp-M">
								        <span><asp:LinkButton ID="lnkbtnCdsearch" runat="server"></asp:LinkButton></span>								        
							        </div>
						        </div>
					        </div>
				        </div>
				        <asp:HiddenField ID="hfCompCd" runat="server"/>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtQty" runat="server" MaxLength="10" Width="100px" CssClass="input-St" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>&nbsp;+
			            <asp:TextBox ID="txtReadyQty" runat="server" MaxLength="10" Width="100px" CssClass="input-St" ReadOnly="true"></asp:TextBox>&nbsp;=
			            <asp:Literal ID="ltSumQty" runat="server"></asp:Literal>
			            <asp:DropDownList ID="ddlScale" runat="server"></asp:DropDownList>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltPrimeCost" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtPrimeCost" runat="server" MaxLength="20" Width="210px" CssClass="input-St"></asp:TextBox>
			            <asp:Literal ID="ltPrimeDong" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltSellingCost" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtSellingCost" runat="server" MaxLength="20" Width="210px" CssClass="input-St"></asp:TextBox>
			            <asp:Literal ID="ltSellingDong" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltVat" runat="server"></asp:Literal></th>
			        <td>
			            <asp:TextBox ID="txtVatRatio" runat="server" MaxLength="5" Width="100px" CssClass="input-St"></asp:TextBox> %
			            <asp:DropDownList ID="ddlVatYn" runat="server"></asp:DropDownList>&nbsp;
			            <asp:Literal ID="ltIncludedVat" runat="server"></asp:Literal>
			        </td>
		        </tr>
		        <tr>
			        <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
			        <td><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="5" Width="500" Height="100"></asp:TextBox></td>
		        </tr>
	        </table>
	    </ContentTemplate>
	</asp:UpdatePanel>
	<asp:ImageButton ID="imgbtnComp" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnComp_Click"/>
	<div class="Btwps FloatR">
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnCancel" runat="server" OnClick="lnkbtnCancel_Click"></asp:LinkButton></span>
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