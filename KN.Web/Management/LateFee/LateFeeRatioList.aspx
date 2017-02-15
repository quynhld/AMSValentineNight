<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="LateFeeRatioList.aspx.cs" Inherits="KN.Web.Management.LateFee.LateFeeRatioList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script type="text/javascript">
    function fnCheckValidate(strText)
    {
        var intLateStartDate = document.getElementById("<%=txtLateStartDate.ClientID%>");
        var intLateEndDate = document.getElementById("<%=txtLateEndDate.ClientID%>");
        var strLateFeeRatio = document.getElementById("<%=txtLateFeeRatio.ClientID%>");
        var strInsDt = document.getElementById("<%=txtInsDt.ClientID%>");

        if (trim(intLateStartDate.value) == "")
        {
            intLateStartDate.focus();
            alert(strText);
            return false;
        }

        if (trim(intLateEndDate.value) == "")
        {
            intLateEndDate.focus();
            alert(strText);
            return false;
        }

        if (trim(strLateFeeRatio.value) == "")
        {
            strLateFeeRatio.focus();
            alert(strText);
            return false;
        }

        if (trim(strInsDt.value) == "")
        {
            alert(strText);
            return false;
        }

        return true;
    }
</script>
<table class="TbCel-Type6-A">
	<col width="30%"/>
	<col width="30%"/>
	<col width="30%"/>
	<col width="10%"/>
	<tr>
		<th><asp:Literal ID="ltLateDay" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltLateRatio" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></th>
        <th>&nbsp;</th>
	</tr>
</table>
<asp:ListView ID="lvLateFeeRationList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
    OnItemDataBound="lvLateFeeRationList_ItemDataBound" OnItemCreated="lvLateFeeRationList_ItemCreated" OnItemDeleting="lvLateFeeRationList_ItemDeleting" OnItemUpdating="lvLateFeeRationList_ItemUpdating">
    <LayoutTemplate>
        <table class="TbCel-Type4-A">
            <col width="30%"/>
            <col width="30%"/>
            <col width="30%"/>
            <col width="10%"/>
            <tbody>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td align="center" class="P0"><asp:TextBox ID="txtLateStartDate" runat="server" CssClass="bgType2" Width="30" MaxLength="3"></asp:TextBox>
            ~ <asp:TextBox ID="txtLateEndDate" runat="server" CssClass="bgType2" Width="30" MaxLength="3"></asp:TextBox>
            </td>
            <td align="center" class="P0">
                <asp:TextBox ID="txtLateFeeRatio" runat="server" CssClass="bgType2" Width="50" MaxLength="5"></asp:TextBox>
                <asp:TextBox ID="txtHfLateFeeSeq" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td align="center" class="P0">
                <asp:TextBox ID="txtInsDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="90"></asp:TextBox>
                <asp:Literal ID="ltInsDt" runat="server" Visible="false"></asp:Literal>
                <asp:HiddenField ID="hfInsDt" runat="server"/>
                <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td align="center" class="P0">
                <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table class="TypeA">
            <tbody>
            <tr>
                <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
            </tr>
            </tbody>
        </table>
    </EmptyDataTemplate>
</asp:ListView>
<table class="TbCel-Type4-A-1">
    <col width="30%"/>
	<col width="30%"/>
	<col width="30%"/>
	<col width="10%"/>
    <tbody>
        <tr>
            <td><asp:TextBox ID="txtLateStartDate" runat="server" CssClass="bgType2" Width="30" MaxLength="3"></asp:TextBox>
            ~ <asp:TextBox ID="txtLateEndDate" runat="server" CssClass="bgType2" Width="30" MaxLength="3"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtLateFeeRatio" runat="server" CssClass="bgType2" Width="50" MaxLength="5"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtInsDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                <img align="absmiddle" alt="Calendar" onclick="ContCalendar(this, '<%=txtInsDt.ClientID%>', '<%=hfInsDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value="" />
                <asp:HiddenField ID="hfInsDt" runat="server"/>
                <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
        </tr>
    </tbody>
</table>
<div class="Btn-Type2-wp FloatR">
	<div class="Btn-Tp2-L">
		<div class="Btn-Tp2-R">
			<div class="Btn-Tp2-M">
				<span><asp:LinkButton ID="lnkbtnEntireReset" runat="server" OnClick="lnkbtnEntireReset_Click"></asp:LinkButton></span>
			</div>
		</div>
	</div>
</div>
<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>