<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ReleaseRequestView.aspx.cs" Inherits="KN.Web.Stock.Release.ReleaseRequestView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
   <div class="iw840 ih500 OverYs ">
    <table class="TbCel-Type2-D iw820">
        <colgroup>
            <col width="140px" />
            <col width="300px" />
            <col width="140px" />
            <col width="" />
        </colgroup>
        <tr>
            <th><asp:Literal ID="ltDpt" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltInsDpt" runat="server"></asp:Literal>
                <asp:TextBox ID="txtHfDptCd" runat="server" Visible="false"></asp:TextBox>
            </td>
            <th class="Bd-Lt"><asp:Literal ID="ltProcessMemNo" runat="server"></asp:Literal></th>
            <td>
                <asp:Literal ID="ltInsProcessMemNm" runat="server"></asp:Literal>
                <asp:TextBox ID="txtHfProcessMemNo" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th><asp:Literal ID="ltProcessDt" runat="server"></asp:Literal></th>
            <td><asp:Literal ID="ltInsProcessDt" runat="server"></asp:Literal></td>
            <th class="Bd-Lt"><asp:Literal ID="ltApprovalYn" runat="server"></asp:Literal></th>
            <td><asp:Literal ID="ltInsApproval" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0 PL10"><asp:Literal ID="ltInsRemark" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltFmsFee" runat="server"></asp:Literal></th>
	        <td colspan="3">
	            <asp:Literal ID="ltInsFmsFee" runat="server"></asp:Literal>
	            <asp:Literal ID="ltDong" runat="server"></asp:Literal>
	        </td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltFmsRemark" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0 PL10"><asp:Literal ID="ltInsFmsRemark" runat="server"></asp:Literal></td>
        </tr>
    </table>
    <asp:ListView ID="lvRequestList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
        OnLayoutCreated="lvRequestList_LayoutCreated" OnItemDataBound="lvRequestList_ItemDataBound" OnItemUpdating="lvRequestList_ItemUpdating">
        <LayoutTemplate>
            <table class="TbCel-Type6 iw820">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltHaveQty" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltProgressCd" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltStatusCd" runat="server"></asp:Literal></th>
                    <th></th>
                </tr>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
                <td class="TbTxtCenter">
                    <asp:Literal ID="ltInsItem" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfSvcZoneCd" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfClassiGrpCd" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfClassiMainCd" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfClassCd" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="Bd-Lt TbTxtCenter">
                    <asp:Literal ID="ltInsHaveQty" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfRealQty" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsQty" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsProgressCd" runat="server"></asp:Literal></td>
                <td class="Bd-Lt TbTxtCenter">
                    <asp:Literal ID="ltInsStatusCd" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtInsStatusCd" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
		            <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL" runat="server" id="divRelease">
						<div class="Btn-Tp-L">
							<div class="Btn-Tp-R">
								<div class="Btn-Tp-M">
									<span><asp:LinkButton ID="lnkbtnRelease" CommandName="Update"  runat="server"></asp:LinkButton></span>
								</div>
							</div>
						</div>
					</div>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:ListView ID="lvChargerList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvChargerList_LayoutCreated" OnItemDataBound="lvChargerList_ItemDataBound"
        OnItemUpdating="lvChargerList_ItemUpdating" OnItemDeleting="lvChargerList_ItemDeleting">
        <LayoutTemplate>
            <table class="TbCel-Type6 iw820">
                <col width="50" />
                <col width="200" />
                <col width="200" />
                <col />
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltCharger" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltStatus" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                </tr>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter" runat="server" id="tdSeq">
                    <asp:Literal ID="ltChargeSeq" runat="server"></asp:Literal>
                </td>
                <td class="TbTxtCenter">
                    <asp:Literal ID="ltChargerNm" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfReleaseDetSeq" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfChargerSeq" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfChargeMemNo" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="Bd-Lt TbTxtCenter">
                    <asp:Literal ID="ltConclusion" runat="server"></asp:Literal>
		            <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL" runat="server" id="divUpdate">
						<div class="Btn-Tp-L">
							<div class="Btn-Tp-R">
								<div class="Btn-Tp-M">
									<span><asp:LinkButton ID="lnkbtnApproval" CommandName="Update"  runat="server"></asp:LinkButton></span>
								</div>
							</div>
						</div>
					</div>
                    <div class="Btn-Type1-wp Mrg0 MrgL10 FloatL" runat="server" id="divReject">
						<div class="Btn-Tp-L">
							<div class="Btn-Tp-R">
								<div class="Btn-Tp-M">
									<span><asp:LinkButton ID="lnkbtnRejected" CommandName="Delete" runat="server"></asp:LinkButton></span>
								</div>
							</div>
						</div>
					</div>
                </td>
                <td class="Bd-Lt TbTxtCenter">
                    <asp:TextBox ID="txtRemark" runat="server" Width="360px"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    </div>
    <asp:TextBox ID="txtHfReleaseSeq" runat="server" Visible="false"></asp:TextBox>
    <div class="Btwps FloatR">
	    <div class="Btn-Type3-wp" runat="server" id="divModi">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp" runat="server" id="divUpdate">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnUpdate" runat="server" onclick="lnkbtnUpdate_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp" runat="server" id="divCancel">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp" runat="server" id="divDel">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnDelete" runat="server" onclick="lnkbtnDelete_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp" runat="server" id="divList">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span><asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <asp:TextBox ID="txtHfProgressCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRequestCnt" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfChargerCnt" runat="server" Visible="false"></asp:TextBox>
</asp:Content>