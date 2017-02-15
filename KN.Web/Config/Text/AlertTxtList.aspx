<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AlertTxtList.aspx.cs" Inherits="KN.Web.Config.Text.AlertTxtList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckData(strText, strConfText)
    {
        var strExpressCd = document.getElementById("<%=txtExpressCd.ClientID%>");
        var strEnglish = document.getElementById("<%=txtAlertEn.ClientID%>");
        var strViet = document.getElementById("<%=txtAlertVi.ClientID%>");
        var strKorean = document.getElementById("<%=txtAlertKr.ClientID%>");

        if (trim(strExpressCd.value) == "")
        {
            strExpressCd.focus();
            alert(strText);
            return false;
        }

        if (trim(strEnglish.value) == "")
        {
            strEnglish.focus();
            alert(strText);
            return false;
        }

        if (trim(strViet.value) == "")
        {
            strViet.focus();
            alert(strText);
            return false;
        }
        if (trim(strKorean.value) == "")
        {
            strKorean.focus();
            alert(strText);
            return false;
        }

        if (fnConfirm(strConfText))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    function fnExgistLine()
    {
        var strErrorText = document.getElementById("<%=hfExgistMsg.ClientID%>");

        if (strErrorText != null)
        {
            if (trim(strErrorText.value) != "")
            {
                alert(strErrorText.value);
                document.getElementById("<%=hfExgistMsg.ClientID%>").value = "";
            }
        }
    }
//-->
</script>
<fieldset class="sh-field1 MrgB10">
    <legend><asp:Literal ID="ltSearch" runat="server"></asp:Literal></legend>
    <ul class="sf1-ag MrgL180">
		<li><asp:DropDownList ID="ddlTopAlertTy" runat="server"></asp:DropDownList></li>
        <li><asp:DropDownList ID="ddlSearch" runat="server"></asp:DropDownList></li>
        <li><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></li>
        <li>
            <div class="Btn-Type4-wp">
                <div class="Btn-Tp4-L">
                    <div class="Btn-Tp4-R">
                        <div class="Btn-Tp4-M">
                            <span> <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>
</fieldset>
<table cellspacing="0" class="TbCel-Type4-A">
	<colgroup>
		<col width="80px"/>
		<col width="170px"/>
		<col width="130px"/>
		<col width="130px"/>
		<col width="130px"/>
		<col width="120px"/>
		<col width="80px"/>
	</colgroup>
    <tr>
        <th ><asp:Literal ID="ltAlertTy" runat="server"></asp:Literal></th>                    
        <th ><asp:Literal ID="ltExpressCd" runat="server"></asp:Literal></th>
        <th ><asp:Literal ID="ltAlertEn" runat="server"></asp:Literal></th>
        <th ><asp:Literal ID="ltAlertVi" runat="server"></asp:Literal></th>
		<th ><asp:Literal ID="ltAlertKr" runat="server"></asp:Literal></th>
		<th ><asp:Literal ID="ltUseYn" runat="server"></asp:Literal></th>
		<th ></th>
    </tr>
</table>
<div style="overflow-y:scroll;height:370px;width:840px;">
<asp:ListView ID="lvAlertList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvAlertList_ItemCreated" 
     OnItemDataBound="lvAlertList_ItemDataBound" OnItemDeleting="lvAlertList_ItemDeleting" OnItemUpdating="lvAlertList_ItemUpdating">
    <LayoutTemplate>
        <table cellspacing="0" class="TbCel-Type4-A iw820">
			<colgroup>
				<col width="80px"/>
				<col width="170px"/>
				<col width="130px"/>
				<col width="130px"/>
				<col width="130px"/>
				<col width="100px"/>
				<col width="80px"/>
			</colgroup>
            <tr id="iphItemPlaceHolderID" runat="server"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td class="TbTxtCenter">
				<asp:DropDownList ID="ddlAlertTy" runat="server" Enabled="false"></asp:DropDownList>
				<asp:TextBox ID="txtHfAlertSeq" runat="server" MaxLength="8" Visible="false"></asp:TextBox>
			</td>
            <td class="TbTxtCenter"><asp:TextBox ID="txtExpressCd" runat="server" MaxLength="200" ReadOnly="true" class="iw100"></asp:TextBox></td>
            <td class="TbTxtCenter"><asp:TextBox ID="txtAlertEn" runat="server" MaxLength="200" class="iw100"></asp:TextBox></td>
			<td class="TbTxtCenter"><asp:TextBox ID="txtAlertVi" runat="server" MaxLength="200" class="iw100"></asp:TextBox></td>
			<td class="TbTxtCenter"><asp:TextBox ID="txtAlertKr" runat="server" MaxLength="200" class="iw100"></asp:TextBox></td>
            <td class="TbTxtCenter">
                <asp:DropDownList ID="ddlUseYn" runat="server">
                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td  class="TbTxtCenter">
                <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table cellspacing="0" class="TypeA">
            <col width="110px"/>
            <col width="180px"/>
            <col width="180px"/>
            <col width="50px"/>
            <col width="130px"/>
            <tr><td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td></tr>
        </table>
    </EmptyDataTemplate>
</asp:ListView>
</div>
<table cellspacing="0" class="TbCel-Type4-A-1">
	<colgroup>
		<col width="80px"/>
		<col width="170px"/>
		<col width="130px"/>
		<col width="130px"/>
		<col width="130px"/>
		<col width="120px"/>
		<col width="100px"/>
	</colgroup>
    <tr>
        <td  class="TbTxtCenter"><asp:DropDownList ID="ddlAlertTy" runat="server"></asp:DropDownList></td>
        <td  class="TbTxtCenter"><asp:TextBox ID="txtExpressCd" runat="server" MaxLength="200"  class="iw100"></asp:TextBox></td>
        <td  class="TbTxtCenter"><asp:TextBox ID="txtAlertEn" runat="server" MaxLength="200"  class="iw100"></asp:TextBox></td>
		<td  class="TbTxtCenter"><asp:TextBox ID="txtAlertVi" runat="server" MaxLength="200"  class="iw100"></asp:TextBox></td>
		<td  class="TbTxtCenter"><asp:TextBox ID="txtAlertKr" runat="server" MaxLength="200"  class="iw100"></asp:TextBox></td>
        <td  class="TbTxtCenter">
            <asp:DropDownList ID="ddlUseYn" runat="server">
                <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                <asp:ListItem Text="N" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="TbTxtCenter"><span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click"/></span></td>
    </tr>
</table>
<asp:HiddenField ID="hfExgistMsg" runat="server"/>
<script language="javascript" type="text/javascript">
<!--//
    fnExgistLine();
//-->
</script>
</asp:Content>