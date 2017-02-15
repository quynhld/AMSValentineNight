<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ChargeSettingList.aspx.cs" Inherits="KN.Web.Management.Remote.ChargeSettingList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined) {
            callCalendar();
        }
    }       
    function fnCheckValidate(strText)
    {
        var intAmountStart = document.getElementById("<%=txtAmountStart.ClientID%>");
        var intAmountEnd = document.getElementById("<%=txtAmountEnd.ClientID%>");
        var strCharge = document.getElementById("<%=txtCharge.ClientID%>");
        var strStartDt = document.getElementById("<%=txtStartDt.ClientID%>");

        if (trim(intAmountStart.value) == "")
        {
            intAmountStart.focus();
            alert(strText);
            return false;
        }

        if (trim(intAmountEnd.value) == "")
        {
            intAmountEnd.focus();
            alert(strText);
            return false;
        }

        if (trim(strCharge.value) == "")
        {
            strCharge.focus();
            alert(strText);
            return false;
        }

        if (trim(strStartDt.value) == "")
        {
            alert(strText);
            return false;
        }

        document.getElementById("<%=hfStartDt.ClientID%>").value = strStartDt.value;
        return true;
    }

    function callCalendar() {
        $("#<%=txtStartDt.ClientID %>").datepicker();     
    }

    $(document).ready(function () {
        callCalendar();
    });

</script>														
<div class="FloatR2">(<asp:Literal ID="ltTopBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal><asp:HiddenField ID="hfRealBaseRate" runat="server"/> )</div>
<asp:UpdatePanel ID="upSettlement" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnEntireReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click" />  
            <asp:AsyncPostBackTrigger ControlID="txtStartDt" EventName="TextChanged" />
        </Triggers>
      <ContentTemplate>
<table class="TbCel-Type6-A">
	<colgroup>
        <col width="30%"/>
        <col width="30%"/>
        <col width="30%"/>
        <col width="10%"/>
        <tr>
            <th>
                <asp:Literal ID="ltAmountUsed" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltCharge" runat="server"></asp:Literal>
            </th>
            <th>
                <asp:Literal ID="ltStartDt" runat="server"></asp:Literal>
            </th>
            <th>
                &nbsp;</th>
        </tr>
    </colgroup>
</table>
<asp:ListView ID="lvChargeInfoList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvChargeInfoList_ItemDataBound" OnItemCreated="lvChargeInfoList_ItemCreated" 
    OnItemDeleting="lvChargeInfoList_ItemDeleting" OnItemUpdating="lvChargeInfoList_ItemUpdating">        
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
            <td align="center" class="P0"><asp:TextBox ID="txtAmountStart"  runat="server" CssClass="bgType2" Width="40" MaxLength="4"></asp:TextBox> 
            ~ <asp:TextBox ID="txtAmountEnd" runat="server" CssClass="bgType2" Width="40" MaxLength="4"></asp:TextBox>
            </td>
            <td align="center" class="P0">
                <asp:TextBox ID="txtCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox>
                <asp:TextBox ID="txtHfChargeSeq" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td align="center" class="P0">
                <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" Width="90" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                <asp:Literal ID="ltStartDt" runat="server" Visible="false"></asp:Literal>
                <asp:HiddenField ID="hfStartDt" runat="server"/>
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
<table class="TbCel-Type6-A">
    <colgroup>
        <col width="30%"/>
        <col width="30%"/>
        <col width="30%"/>
        <col width="10%"/>
        <tbody>
            <tr>
                <td>
                    <asp:TextBox ID="txtAmountStart" runat="server" CssClass="bgType2" 
                        MaxLength="4" Width="40"></asp:TextBox>
                    ~
                    <asp:TextBox ID="txtAmountEnd" runat="server" CssClass="bgType2" MaxLength="4" 
                        Width="40"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtCharge" runat="server" CssClass="bgType2" MaxLength="18" 
                        Width="50"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDt" runat="server"  
                        CssClass="bgType2" ontextchanged="txtStartDt_TextChanged" ReadOnly="true" 
                        Width="70" ></asp:TextBox>
                    <img alt="Calendar" onclick="CallCalendar('#<%=txtStartDt.ClientID%>')" src="/Common/Images/Common/calendar.gif"
                                    style="cursor: pointer;" value="" />
                    <asp:HiddenField ID="hfStartDt" runat="server" />
                    <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td>
                    <span>
                    <asp:ImageButton ID="imgbtnRegist" runat="server" 
                        ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click" />
                    </span>
                </td>
            </tr>
        </tbody>
    </colgroup>
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
<asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    </ContentTemplate>
</asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>							
</asp:Content>