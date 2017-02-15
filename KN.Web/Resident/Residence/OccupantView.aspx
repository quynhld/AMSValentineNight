<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OccupantView.aspx.cs" Inherits="KN.Web.Resident.Residence.OccupantView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <div style="overflow-y:scroll;height:490px;width:840px;">
        <div class="Tb-Tp-tit"><asp:Literal ID="ltTenantInfo" runat="server"></asp:Literal></div>
        <table class="TbCel-Type2 iw820">
		    <col width="15%"/>
		    <col width="35%"/>
		    <col width="15%"/>
		    <col width="35%"/>
		    <tr>
			    <th><asp:Literal ID="ltFloor" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsFloor" runat="server"></asp:Literal></td>
			    <th class="Bd-Lt"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
			</tr>
			<tr>
			    <th><asp:Literal ID="ltTenantNm" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsTenantNm" runat="server"></asp:Literal></td>
			    <th class="Bd-Lt"><asp:Literal ID="ltTenantTelNo" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsTenantTelNo" runat="server"></asp:Literal></td>
		    </tr>
	    </table>
	    <div class="Tb-Tp-tit"><asp:Literal ID="ltRentalinfo" runat="server"></asp:Literal></div>
	    <table class="TbCel-Type2 iw820">
		    <col width="15%"/>
		    <col width="35%"/>
		    <col width="15%"/>
		    <col width="35%"/>
			<tr>
			    <th><asp:Literal ID="ltNm" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsNm" runat="server"></asp:Literal></td>
			    <th class="Bd-Lt"><asp:Literal ID="ltBirthDt" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsBirthDt" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltTelNo" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsTelNo" runat="server"></asp:Literal></td>
			    <th class="Bd-Lt"><asp:Literal ID="ltMobileNo" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsMobileNo" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltGender" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsGender" runat="server"></asp:Literal></td>
			    <th><asp:Literal ID="ltOccupationDt" runat="server"></asp:Literal></th>
			    <td><asp:Literal ID="ltInsOccupationDt" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th><asp:Literal ID="ltTaxCd" runat="server"></asp:Literal></th>
			    <td colspan="3"><asp:Literal ID="ltInsTaxCd" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <th rowspan="2" style="height:52px" class="Bd-bt"><asp:Literal ID="ltTaxAddr" runat="server"></asp:Literal> (For Tax)</th>
			    <td colspan="3" style="height:26px"><asp:Literal ID="ltInsTaxAddr" runat="server"></asp:Literal></td>
		    </tr>
		    <tr>
			    <td colspan="3" style="height:26px" class="Bd-bt"><asp:Literal ID="ltInsTaxDetAddr" runat="server"></asp:Literal></td>
		    </tr>
		    <tr id="KsystemCode" runat="server">
			    <th class="Bd-bt"><asp:Literal ID="Literal1" runat="server" Text="Ksystem Code"></asp:Literal></th>
			    <td class="Bd-bt"><asp:Literal ID="ltKsystemCode" runat="server"></asp:Literal></td>
			    <td class="Bd-bt" colspan="2"><asp:Literal ID="Literal4" runat="server" Text="This code is very important for mapping betweent AMS and K-system(Please ask Ms.Van to input this code!)"></asp:Literal></td>
		    </tr>
	    </table>
	    <div class="Tb-Tp-tit"><asp:Literal ID="ltMngAddon" runat="server"></asp:Literal></div>
        <asp:ListView ID="lvMngAddon" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
            OnItemDataBound="lvMngAddon_ItemDataBound" OnLayoutCreated="lvMngAddon_LayoutCreated" OnItemCreated="lvMngAddon_ItemCreated">
            <LayoutTemplate>
	            <table class="TbCel-Type3 iw820">
		            <col width="35%"/>
		            <col width="35%"/>
		            <col width="30%"/>
		            <tr>
			            <th><asp:Literal ID="ltAddonUser" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltAddonRelation" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltAddonoSex" runat="server"></asp:Literal></th>
		            </tr>
		            <tr runat="server" id="iphItemPlaceHolderID"></tr>
	            </table>
            </LayoutTemplate>
            <ItemTemplate>
	            <tr>
		            <td class="TbTxtCenter"><asp:Literal ID="ltInsAddonUser" runat="server"></asp:Literal></td>
		            <td class="Bd-Lt TbTxtCenter">
		                <asp:DropDownList ID="ddlAddonUser" runat="server"></asp:DropDownList>
		            </td>
		            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsAddonoSex" runat="server"></asp:Literal></td>
	            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type3 iw820">
		            <col width="35%"/>
		            <col width="35%"/>
		            <col width="30%"/>
		            <tr>
			            <th><asp:Literal ID="ltAddonUser" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltAddonRelation" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltAddonoSex" runat="server"></asp:Literal></th>
		            </tr>
		            <tr>
		                <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
		            </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
	    <div class="Tb-Tp-tit"><asp:Literal ID="ltMngCard" runat="server"></asp:Literal></div>
        <asp:ListView ID="lvMngCardList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMngCardList_ItemDataBound" 
            OnItemCreated="lvMngCardList_ItemCreated" OnLayoutCreated="lvMngCardList_LayoutCreated">
            <LayoutTemplate>
                <table class="TbCel-Type4 iw820">
                    <col width="25%"/>
                    <col width="25%"/>
                    <col width="50%"/>
		            <tr>
			            <th><asp:Literal ID="ltCardUser" runat="server"></asp:Literal></th>
			            <th class="Bd-Lt"><asp:Literal ID="ltRelation" runat="server"></asp:Literal></th>
			            <th class="Bd-Lt"><asp:Literal ID="ltMngCardNo" runat="server"></asp:Literal></th>
		            </tr>
		            <tr runat="server" id="iphItemPlaceHolderID"></tr>
	            </table>
            </LayoutTemplate>
            <ItemTemplate>
	            <tr>
		            <td class="TbTxtCenter"><asp:Literal ID="ltInsCardUser" runat="server"></asp:Literal></td>
		            <td class="Bd-Lt TbTxtCenter"><asp:DropDownList ID="ddlInsRelation" runat="server"></asp:DropDownList></td>
		            <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsMngCardNo" runat="server"></asp:Literal></td>
	            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type4 iw820">
		            <col width="25%"/>
		            <col width="25%"/>
		            <col width="50%"/>
		            <tr>
			            <th><asp:Literal ID="ltCardUser" runat="server"></asp:Literal></th>
			            <th class="Bd-Lt"><asp:Literal ID="ltRelation" runat="server"></asp:Literal></th>
			            <th class="Bd-Lt"><asp:Literal ID="ltMngCardNo" runat="server"></asp:Literal></th>
		            </tr>
		            <tr>
		                <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
		            </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
	</div>
	<div class="Btwps FloatR">
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
		<div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span> <asp:LinkButton ID="lnkbtnDel" runat="server" onclick="lnkbtnDel_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
        </div>
	</div>
	<asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
	<asp:TextBox ID="txtHfRentSeq" runat="server" Visible="false"></asp:TextBox>
</asp:Content>