<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ReleaseRequestBasicModify.aspx.cs" Inherits="KN.Web.Stock.Release.ReleaseRequestBasicModify" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckValidate(strText, strDateTxt)
    {
        var strProcessDt = document.getElementById("<%=txtProcessDt.ClientID%>");

        if (trim(strProcessDt.value) == "")
        {
            alert(strText);
            return false;
        }

        var strNowDate = document.getElementById("<%=hfToday.ClientID%>");
        var strRequestDate = document.getElementById("<%=hfProcessDt.ClientID%>");
        var intNowDate = Number(strNowDate.value);
        var intReqDate = Number(strRequestDate.value.replace(/\-/gi, ""));

        if (intNowDate > intReqDate)
        {
            alert(strDateTxt);
            return false;
        }

        return true;
    }
//-->
</script>
    <table class="TbCel-Type2-D">
     <colgroup>
            <col width="140px" />
            <col width="300px" />
            <col width="140px" />
            <col width="" />
        </colgroup>
        <tr>
            <th><asp:Literal ID="ltDpt" runat="server"></asp:Literal></th>
            <td><asp:Literal ID="ltInsDpt" runat="server"></asp:Literal></td>
            <th class="Bd-Lt"><asp:Literal ID="ltProcessMemNo" runat="server"></asp:Literal></th>
            <td><asp:Literal ID="ltInsProcessMemNm" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <th><asp:Literal ID="ltProcessDt" runat="server"></asp:Literal></th>
            <td>
	            <asp:TextBox ID="txtProcessDt" runat="server" ReadOnly="true" Width="80" MaxLength="10"></asp:TextBox>
	            <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtProcessDt.ClientID%>', '<%=hfProcessDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                <asp:HiddenField ID="hfProcessDt" runat="server"/>
            </td>
            <th class="Bd-Lt"><asp:Literal ID="ltApprovalYn" runat="server"></asp:Literal></th>
            <td><asp:Literal ID="ltInsApproval" runat="server"></asp:Literal></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0 PL10"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" Width="500" Height="45"></asp:TextBox></td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltFmsFee" runat="server"></asp:Literal></th>
	        <td colspan="3">
	            <asp:TextBox ID="txtFmsFee" runat="server" MaxLength="10" Width="100px" CssClass="input-St"></asp:TextBox>&nbsp;
	            <asp:Literal ID="ltFmsDong" runat="server"></asp:Literal>
	        </td>
        </tr>
        <tr>
	        <th><asp:Literal ID="ltFmsRemark" runat="server"></asp:Literal></th>
	        <td colspan="3" class="P0 PL10"><asp:TextBox ID="txtFmsRemark" runat="server" TextMode="MultiLine" Rows="3" Width="500" Height="45"></asp:TextBox></td>
        </tr>
    </table>
    <asp:ListView ID="lvRequestList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvRequestList_LayoutCreated" OnItemDataBound="lvRequestList_ItemDataBound">
        <LayoutTemplate>
            <table class="TbCel-Type6">
                <tr>
                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltHaveQty" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltProgressCd" runat="server"></asp:Literal></th>
                    <th class="Bd-Lt"><asp:Literal ID="ltStatusCd" runat="server"></asp:Literal></th>
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
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:TextBox ID="txtHfReleaseSeq" runat="server" Visible="false"></asp:TextBox>
    <div class="Btwps FloatR">
	    <div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span> <asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
	    <div class="Btn-Type3-wp">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					    <span> <asp:LinkButton ID="lnkbtnList" runat="server" onclick="lnkbtnList_Click"></asp:LinkButton> </span>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <asp:TextBox ID="txtHfProgressCd" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfToday" runat="server" />
</asp:Content>