<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="GoodsOrderBasicModify.aspx.cs" Inherits="KN.Web.Stock.Order.GoodsOrderBasicModify" ValidateRequest="false"%>
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
    <div class="FloatR2">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)</div>
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
		            <span class="Cal">
			            <asp:TextBox ID="txtProcessDt" runat="server" ReadOnly="true" Width="80" MaxLength="10"></asp:TextBox>
			            <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtProcessDt.ClientID%>', '<%=hfProcessDt.ClientID%>', true);" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
                        <asp:HiddenField ID="hfProcessDt" runat="server"/>
		            </span>
	            </td>
	            <th class="Bd-Lt"><asp:Literal ID="ltApproval" runat="server"></asp:Literal></th>
	            <td><asp:Literal ID="ltInsApproval" runat="server"></asp:Literal></td>
	        </tr>
	        <tr>
	            <th><asp:Literal ID="ltRemark" runat="server"></asp:Literal></th>
                <td class="P0 PL10" colspan="3"><asp:TextBox ID="txtRemark" runat="server" Height="45" Rows="3" TextMode="MultiLine" Width="500"></asp:TextBox></td>
            </tr>
        </table>
        <asp:TextBox ID="txtHfOrderSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:UpdatePanel ID="upRequestList" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvRequestList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
                OnLayoutCreated="lvRequestList_LayoutCreated" OnItemDataBound="lvRequestList_ItemDataBound">
                <LayoutTemplate>
                    <table class="TbCel-Type6 iw820">
                        <tr>
                            <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltItem" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt"><asp:Literal ID="ltCompNm" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt"><asp:Literal ID="ltQty" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt"><asp:Literal ID="ltSellingPrice" runat="server"></asp:Literal></th>
	                        <th class="Bd-Lt"><asp:Literal ID="ltTotPrice" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltProgressCd" runat="server"></asp:Literal></th>
                            <th class="Bd-Lt"><asp:Literal ID="ltStatusCd" runat="server"></asp:Literal></th>
                        </tr>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter"><asp:Literal ID="ltInsItem" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt"><asp:Literal ID="ltInsCompNm" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsQty" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt"><asp:Literal ID="ltInsSellingPrice" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt"><asp:Literal ID="ltInsTotPrice" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsProgressCd" runat="server"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter"><asp:Literal ID="ltInsStatusCd" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
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
        <div class="Btn-Type3-wp" runat="server" id="divCancel">
	        <div class="Btn-Tp3-L">
		        <div class="Btn-Tp3-R">
			        <div class="Btn-Tp3-M">
				        <span><asp:LinkButton ID="lnkbtnCancel" runat="server" onclick="lnkbtnCancel_Click"></asp:LinkButton> </span>
			        </div>
		        </div>
	        </div>
        </div>
    </div>
    <asp:HiddenField ID="hfToday" runat="server" />
</asp:Content>