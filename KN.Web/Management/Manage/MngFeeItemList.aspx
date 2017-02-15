<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngFeeItemList.aspx.cs" Inherits="KN.Web.Management.Manage.MngFeeItemList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnCheckValidate(strText)
    {
        var strMngFeeNmEn = document.getElementById("<%=txtMngFeeNmEn.ClientID%>");
        var strMngFeeNmVi = document.getElementById("<%=txtMngFeeNmVi.ClientID%>");
        var strMngFeeNmKr = document.getElementById("<%=txtMngFeeNmKr.ClientID%>");

        if (trim(strMngFeeNmEn.value) == "")
        {
            strMngFeeNmEn.focus();
            alert(strText);
            return false;
        }

        if (trim(strMngFeeNmVi.value) == "")
        {
            strMngFeeNmVi.focus();
            alert(strText);
            return false;
        }

        if (trim(strMngFeeNmKr.value) == "")
        {
            strMngFeeNmKr.focus();
            alert(strText);
            return false;
        }

        return true;
    }
</script>
<table class="TbCel-Type3">
    <col width="150"/>
    <col width="120"/>
    <col width="150"/>
    <col width="150"/>
    <col width="150"/>
    <col width="120"/>
	<tr>			
		<th><asp:Literal ID="ltMngRentCd" runat="server"></asp:Literal></th>
		<th><asp:Literal ID="ltItemTy" runat="server"></asp:Literal></th>
		<th><asp:Literal ID="ltMngFeeNmEn" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMngFeeNmVi" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMngFeeNmKr" runat="server"></asp:Literal></th>
        <th>&nbsp;</th>
	</tr>
</table>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
    </Triggers>
    <ContentTemplate>
        <div style="overflow-y:scroll;height:350px;width:840px;">
        <asp:ListView ID="lvRentItemList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvRentItemList_ItemDataBound" OnItemCreated="lvRentItemList_ItemCreated" 
            OnItemDeleting="lvRentItemList_ItemDeleting" OnItemUpdating="lvRentItemList_ItemUpdating">
            <LayoutTemplate>
                <table class="TbCel-Type4-A iw820 bdFFF">
                    <col width="150"/>
                    <col width="120"/>
                    <col width="150"/>
                    <col width="150"/>
                    <col width="150"/>
                    <col width="120"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" class="P0">
                        <asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtRentCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td align="center" class="P0">
                        <asp:Literal ID="ltMngItemTy" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtMngItemTy" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td align="center" class="P0"><asp:TextBox ID="txtMngFeeNmEn" runat="server" CssClass="bgType2" Width="140" MaxLength="100" ></asp:TextBox></td>
                    <td align="center" class="P0"><asp:TextBox ID="txtMngFeeNmVi" runat="server" CssClass="bgType2" Width="140" MaxLength="100"></asp:TextBox></td>
                    <td align="center" class="P0"><asp:TextBox ID="txtMngFeeNmKr" runat="server" CssClass="bgType2" Width="140" MaxLength="100"></asp:TextBox></td>
                    <td align="center" class="P0">
                        <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                        <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type3 iw820 bdFFF">
                    <tbody><tr><td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr></tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        </div>
        <table class="TbCel-Type4-A-1">
            <col width="150"/>
            <col width="120"/>
            <col width="150"/>
            <col width="150"/>
            <col width="150"/>
            <col width="120"/>
            <tbody>
                <tr>
                    <td align="center"><asp:DropDownList ID="ddlRentCd" runat="server"></asp:DropDownList></td>
                    <td align="center"><asp:TextBox ID="txtMngFeeCd" runat="server" Width="100" MaxLength="4"></asp:TextBox></td>
                    <td align="center"><asp:TextBox ID="txtMngFeeNmEn" runat="server" CssClass="bgType2" Width="140" MaxLength="100"></asp:TextBox></td>
                    <td align="center"><asp:TextBox ID="txtMngFeeNmVi" runat="server" CssClass="bgType2" Width="140" MaxLength="100"></asp:TextBox></td>
                    <td align="center"><asp:TextBox ID="txtMngFeeNmKr" runat="server" CssClass="bgType2" Width="140" MaxLength="100"></asp:TextBox></td>
                    <td align="center"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
                </tr>
            </tbody>
        </table>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfFeeTy" runat="server" Visible="false"></asp:TextBox>
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