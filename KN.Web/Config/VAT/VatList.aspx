<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="VatList.aspx.cs" Inherits="KN.Web.Config.VAT.VatList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnMovePage(intPageNo) 
    {
        if (intPageNo == null) 
        {
            intPageNo = 1;
        }
        
        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
        <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
    }

    function fnCheckData(strAlertTxt, strDateTxt)
    {
        var strItems = document.getElementById("<%=ddlManageFee.ClientID%>");
        var strAppliedDt = document.getElementById("<%=hfAppliedDt.ClientID%>");
        var strStartDt = document.getElementById("<%=hfStartDt.ClientID%>");
        var strVatRatio = document.getElementById("<%=txtVatRatio.ClientID%>");

        if (trim(strItems.value) == "0000")
        {
            alert(strAlertTxt);
            strItems.focus();
            return false;
        }

        if (trim(strAppliedDt.value) == "")
        {
            alert(strAlertTxt);
            return false;
        }
        
        if (trim(strStartDt.value) != "")
        {
            if (Number(strStartDt.value) > Number(strAppliedDt.value.replace(/\-/gi, "")))
            {
                alert(strDateTxt);
                return false;
            }
        }

        if (trim(strVatRatio.value) == "")
        {
            alert(strAlertTxt);
            strVatRatio.focus();
            return false;
        }

        return true;
    }
</script>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlManageFee" EventName="SelectedIndexChanged"/>
    </Triggers>
    <ContentTemplate>
	    <table class="TbCel-Type2-C">
		    <tr>
			    <th><asp:Literal ID="ltTopItems" runat="server"></asp:Literal></th>
			    <td>
			        <asp:DropDownList ID="ddlManageFee" runat="server" OnSelectedIndexChanged="ddlManageFee_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
			        <asp:HiddenField ID="hfStartDt" runat="server" />
			    </td>
			    <th class="Bd-Lt"><asp:Literal ID="ltTopStartDt" runat="server"></asp:Literal></th>
			    <td>
			        <asp:TextBox ID="txtAppliedDt" runat="server" TextMode="SingleLine" ReadOnly="true" Width="80"></asp:TextBox>&nbsp;
			        <a href="#"><img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtAppliedDt.ClientID%>', '<%=hfAppliedDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor:pointer;" value=""/></a>
			        <asp:HiddenField ID="hfAppliedDt" runat="server" />
			    </td>
			    <th class="Bd-Lt"><asp:Literal ID="ltTopRatio" runat="server"></asp:Literal></th>
			    <td><asp:TextBox ID="txtVatRatio" runat="server" MaxLength="5" Width="50"></asp:TextBox>&nbsp;%</td>
			    <td><asp:ImageButton ID="imgbtnAdd" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnAdd_Click"/></td>
		    </tr>
	    </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="upList" runat="server">
    <Triggers>
    </Triggers>
    <ContentTemplate>
	<asp:ListView ID="lvVatList" runat="server" ItemPlaceholderID="iphID"
	    OnItemDataBound="lvVatList_ItemDataBound" OnLayoutCreated="lvVatList_LayoutCreated" OnItemCreated="lvVatList_ItemCreated">
	    <LayoutTemplate>
            <table class="TbCel-Type2-C">
                <tr>
                    <th><asp:Literal ID="ltItems" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltEndDt" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRatio" runat="server"></asp:Literal></th>
                </tr>
                <tr id="iphID" runat="server"></tr>
            </table>
	    </LayoutTemplate>
	    <ItemTemplate>
	        <tr>
	            <td><asp:Literal ID="ltManageFee" runat="server"></asp:Literal></td>
	            <td align="center"><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></td>
	            <td align="center"><asp:Literal ID="ltEndDt" runat="server"></asp:Literal></td>
	            <td align="center"><asp:Literal ID="ltVatRatio" runat="server"></asp:Literal>&nbsp;%</td>
	        </tr>
	    </ItemTemplate>
	    <EmptyDataTemplate>
            <table class="TbCel-Type5-B">
                <tr>
                    <th><asp:Literal ID="ltItems" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltStartDt" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltEndDt" runat="server"></asp:Literal></th>
                    <th><asp:Literal ID="ltRatio" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <td colspan="4"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
	    </EmptyDataTemplate>
	</asp:ListView>
    <div class="Clear">
        <span id="spanPageNavi" runat="server" style="width:100%"></span>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:HiddenField ID="hfCurrentPage" runat="server"/>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>