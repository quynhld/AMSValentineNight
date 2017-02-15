<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="LateFeeInfoView.aspx.cs" Inherits="KN.Web.Management.LateFee.LateFeeInfoView"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckValidate(strAlert, strAlertReceit) 
    {
        var strInsPayDay = document.getElementById("<%=hfInsPayDay.ClientID%>");
        var strInsPayment = document.getElementById("<%=txtInsPay.ClientID%>");
        var strReceit = document.getElementById("<%= chkReceitCd.ClientID %>");

        if (strReceit.checked)
        {
            alert(strAlertReceit);
        }

        else 
        {
            if (trim(strInsPayDay.value) == "" || trim(strInsPayment.value) == "") 
            {
                alert(strAlert);
                return false;
            }
        }

        return true;
    }
//-->
</script>
	<div class="C235-wp">
        <div class="Tb-Tp-tit FloatL Mrg0" style=""><asp:Literal ID="ltPaymentTitle" runat="server"></asp:Literal></div>
        <div class="FloatL PL15">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)</div>
    </div>
    <table class="TbCel-Type6">
        <thead>
            <tr>
	            <th class="Fr-line"><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
	            <th colspan="2"><asp:Literal ID="ltName" runat="server"></asp:Literal></th>
	            <th><asp:Literal ID="ltReceitCd" runat="server"></asp:Literal></th>
	            <th><asp:Literal ID="ltLateDt" runat="server"></asp:Literal></th>
	            <th class="Ls-line"><asp:Literal ID="ltLateFeeRatio" runat="server"></asp:Literal></th>
            </tr>
            <tr>
	            <td align="center"><asp:Literal ID="ltInsFloorRoom" runat="server"></asp:Literal></td>
	            <td align="center" colspan="2"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
	            <td align="center"><asp:CheckBox ID="chkReceitCd" runat="server" class="bd0"></asp:CheckBox></td>
	            <td align="center"><asp:Literal ID="ltInsLateDt" runat="server"></asp:Literal></td>
	            <td align="center"><asp:Literal ID="ltInsLateFeeRatio" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <th class="Fr-line"><asp:Literal ID="ltLateFee" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltTotalPay" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltLatePayment" runat="server"></asp:Literal></th>
	            <th><asp:Literal ID="ltPaymentCd" runat="server"></asp:Literal></th>
	            <th><asp:Literal ID="ltPayDay" runat="server"></asp:Literal></th>
	            <th class="Ls-line"><asp:Literal ID="ltPay" runat="server"></asp:Literal></th>
            </tr>
            <tr>
                <td align="center"><asp:Literal ID="ltInsLateFee" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltInsTotalPay" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltInsLatePayment" runat="server"></asp:Literal></td>
	            <td align="center"><asp:DropDownList ID="ddlPaymentCd" runat="server"></asp:DropDownList></td>
	            <td>
                    <asp:TextBox ID="txtInsPayDay" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                    <img alt="Calendar" onclick="Calendar(this, '<%=txtInsPayDay.ClientID%>', '<%=hfInsPayDay.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
                    <asp:HiddenField ID="hfInsPayDay" runat="server"/>
                    <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                </td>
	            <td align="center"><asp:TextBox ID="txtInsPay" runat="server" CssClass="bgType2" Width="70" MaxLength="21" ></asp:TextBox><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
            </tr>
        </thead>
    </table>
    <asp:ListView ID="lvMngLateFeeInfoView" runat="server" GroupPlaceholderID="groupPlaceHolderID" GroupItemCount="2" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMngLateFeeInfoView_ItemCreated"
        OnLayoutCreated="lvMngLateFeeInfoView_LayoutCreated" OnItemDataBound="lvMngLateFeeInfoView_ItemDataBound" OnItemUpdating="lvMngLateFeeInfoView_ItemUpdating">
        <LayoutTemplate>
            <table class="TbCel-Type6">
                <col width="25%"/>
                <col width="25%"/>
                <col width="25%"/>
                <col width="25%"/>
                <thead>
                    <tr>
			            <th><asp:Literal ID="ltPay" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltPayDay" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltPay1" runat="server"></asp:Literal></th>
			            <th><asp:Literal ID="ltPayDay1" runat="server"></asp:Literal></th>
		            </tr>
                </thead>
                <tbody>
                    <tr id="groupPlaceHolderID" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <GroupTemplate>
            <tr>
                <td id="iphItemPlaceHolderID" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
                <td align="right">
                    <asp:TextBox ID="txtPayList" runat="server" CssClass="bgType2" ReadOnly="true"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" Visible="false"/>
                    <asp:TextBox ID="txtHfInsDate" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfPaySeq" runat="server" Visible="false"></asp:TextBox>
                 </td>
			    <td align="center"><asp:Literal ID="ltPayDayList" runat="server"></asp:Literal></td>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA">
                <tbody>
                <tr>
                    <td colspan="2" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
	<div class="bottom-fix">
		<div class="Btwps FloatR2">
			<div class="Btn-Type3-wp ">
				<div class="Btn-Tp3-L">
					<div class="Btn-Tp3-R">
						<div class="Btn-Tp3-M">
							<span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton> </span>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfRentalYear" runat="server"/>
    <asp:HiddenField ID="hfRentalMM" runat="server"/>
    <asp:HiddenField ID="hfUserSeq" runat="server"/>
    <asp:HiddenField ID="hfTotalPayment" runat="server"></asp:HiddenField>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRentalMM" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfUserSeq" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtRealBaseRate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfVatRatio" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "") 
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>