<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthMngInfoView.aspx.cs" Inherits="KN.Web.Management.Manage.MonthMngInfoView" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
	<div class="TpAtit1">	    
        <span class="shf-sel FloatL">
            <b><asp:Literal ID="ltDeadLine" runat="server"></asp:Literal></b>
            <asp:Literal ID="ltYear" runat="server"></asp:Literal>
            <asp:Literal ID="ltMonth" runat="server"></asp:Literal>         
            <asp:TextBox ID="txtLimitDt" runat="server" ReadOnly="true" Width="90"></asp:TextBox>
            <img alt="Calendar" onclick="ContCalendar(this, '<%=txtLimitDt.ClientID%>', '<%=hfLimitDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/>
            <asp:Literal ID="ltLimitDt" runat="server" Visible="false"></asp:Literal>
            <asp:HiddenField ID="hfLimitDt" runat="server"/>
            <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>            
        </span>        
        <div class="FloatR2">
            (<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal>)
            <asp:TextBox ID="txtTopBaseRate" runat="server" Visible="false"></asp:TextBox>
        </div>
	</div>
    <table class="TypeA">
        <col width="10%"/>
        <col width="20%"/>
        <col width="25%"/>
        <col width="15%"/>
        <col width="15%"/>
        <col width="15%"/>
	    <thead>
            <tr>
                <th><asp:Literal ID="ltUseYn" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltMngFeeCd" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltMngFeeNm" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
                <th><asp:Literal ID="ltMngFee" runat="server"></asp:Literal></th>
            </tr>                       
	    </thead>
    </table>
    <asp:ListView ID="lvMonthMngInfoView" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMonthMngInfoView_ItemDataBound">
        <LayoutTemplate>
            <table class="TypeA">
                <col width="10%"/>
                <col width="20%"/>
                <col width="25%"/>
                <col width="15%"/>
                <col width="15%"/>
                <col width="15%"/>
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server"></tr>
                </tbody>                
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td align="center"><asp:CheckBox ID="chkInsUseYn" runat="server" OnCheckedChanged="chkInsUseYn_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                <td align="center"><asp:Literal ID="ltInsMngFeeCd" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltInsMngFeeNm" runat="server"></asp:Literal></td>
                <td align="center"><asp:TextBox ID="txtInsMngFeeNet" runat="server" MaxLength="21" Enabled="false" OnTextChanged="txtInsMngFeeNet_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                <td align="center"><asp:Literal ID="ltInsMngFeeVat" runat="server"></asp:Literal></td>
                <td align="center"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA">
                <tbody>
                    <tr>
                        <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>        
    </asp:ListView>
    <div class="Btwps FloatR2">
	    <div class="Btn-Type3-wp ">
		    <div class="Btn-Tp3-L">
			    <div class="Btn-Tp3-R">
				    <div class="Btn-Tp3-M">
					 <span><asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
				    </div>
			    </div>
		    </div>
	    </div>
		<div class="Btn-Type3-wp ">
			<div class="Btn-Tp3-L">
				<div class="Btn-Tp3-R">
					<div class="Btn-Tp3-M">
						<span> <asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
					</div>
				</div>
			</div>
		</div>
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
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMngMM" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    <asp:HiddenField ID="hfRentCd" runat="server"/>
    <asp:HiddenField ID="hfMngYear" runat="server"/>
    <asp:HiddenField ID="hfMngMM" runat="server"/>
    <asp:HiddenField ID="hfVatRation" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>