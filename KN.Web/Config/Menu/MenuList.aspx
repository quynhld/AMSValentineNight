<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="KN.Web.Config.Menu.MenuList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnChangePopup(strText1, strData)
    {
        window.open("<%=Master.PAGE_POPUP1%>?" + strText1 + "=" + strData, 'SearchTitle', 'status=no, resizable=no, width=570, height=280, left=200,top=200, scrollbars=no, menubar=no, toolbar=no, location=no');
        
        return false;
    }
    
    function fnSubmitCheck(strInsText, strInsTitle, strInsLink)
    {
        var strDepth1 = document.getElementById("<%=txtDepth1.ClientID%>");
        var strDepth2 = document.getElementById("<%=txtDepth2.ClientID%>");
        var strDepth3 = document.getElementById("<%=txtDepth3.ClientID%>");
        var strDepth4 = document.getElementById("<%=txtDepth4.ClientID%>");
        var strTitle = document.getElementById("<%=txtTitle.ClientID%>");
        var strLink = document.getElementById("<%=ddlHiddenYn.ClientID%>");
        var strURL = document.getElementById("<%=txtUrl.ClientID%>");

        if (trim(strDepth1.value) == "")
        {
            strDepth1.focus();
            alert(strInsText);
            return false;
        }

        if (trim(strDepth2.value) == "")
        {
            strDepth2.focus();
            alert(strInsText);
            return false;
        }

        if (trim(strDepth3.value) == "")
        {
            strDepth3.focus();
            alert(strInsText);
            return false;
        }

        if (trim(strDepth4.value) == "")
        {
            strDepth4.focus();
            alert(strInsText);
            return false;
        }

        if (trim(strTitle.value) == "")
        {
            strTitle.focus();
            alert(strInsTitle);
            return false;
        }

        if (strLink.value == "Y")
        {
            if (trim(strURL.value) == "")
            {
                strURL.focus();
                alert(strInsLink);
                return false;
            }
        }

        return true;
    }

    function fnExgistLine()
    {
        var strErrorLine = document.getElementById("<%=hfExgistLine.ClientID%>");
        var strErrorText = document.getElementById("<%=hfExgistMsg.ClientID%>");

        if (strErrorLine != null && strErrorText != null)
        {
            if (trim(strErrorLine.value) != "" && trim(strErrorText.value) != "")
            {
                alert(strErrorText.value + "\n" + ": " + strErrorLine.value);
                document.getElementById("<%=hfExgistLine.ClientID%>").value = "";
                document.getElementById("<%=hfExgistMsg.ClientID%>").value = "";
            }
            else if (trim(strErrorLine.value) == "" && trim(strErrorText.value) != "")
            {
                alert(strErrorText.value);
                document.getElementById("<%=hfExgistMsg.ClientID%>").value = "";
            }
        }
    }
//-->
</script>
<table cellspacing="0" class="TbCel-Type4-A">
    <col width="70px"/>
    <col width="55px"/>
    <col width="55px"/>
    <col width="55px"/>
    <col width="55px"/>
    <col width="190px"/>
    <col width="160px"/>
    <col width="50px"/>
    <col width="50px"/>
    <col width="80px"/>
    <tr>
        <th style="text-align:center" rowspan="2"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
        <th class="Bd-Lt" colspan="4"><asp:Literal ID="ltDepth" runat="server"></asp:Literal></th>                    
        <th class="Bd-Lt" rowspan="2"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></th>
        <th class="Bd-Lt" rowspan="2"><asp:Literal ID="ltUrl" runat="server"></asp:Literal></th>
        <th class="Bd-Lt" rowspan="2"><asp:Literal ID="ltHiddenYn" runat="server"></asp:Literal></th>
        <th class="Bd-Lt" rowspan="2"><asp:Literal ID="ltBoardYn" runat="server"></asp:Literal></th>
        <th class="Bd-Lt" rowspan="2"></th>
    </tr>
    <tr>
        <th class="Bd-Lt"><asp:Literal ID="ltDepth1" runat="server" Text="1"></asp:Literal></th>
        <th class="Bd-Lt"><asp:Literal ID="ltDepth2" runat="server" Text="2"></asp:Literal></th>
        <th class="Bd-Lt"><asp:Literal ID="ltDepth3" runat="server" Text="3"></asp:Literal></th>
        <th class="Bd-Lt"><asp:Literal ID="ltDepth4" runat="server" Text="4"></asp:Literal></th>
    </tr>
</table>
<asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnInsert" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnTitle" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
    <div style="overflow-y:scroll;height:430px;width:840px;">
    <asp:ListView ID="lvMnglist" runat="server" OnItemDataBound="lvMnglist_ItemDataBound" ItemPlaceholderID="iphItemPlaceHolderID"
         OnItemCreated="lvMnglist_ItemCreated" OnItemDeleting="lvMnglist_ItemDeleting" OnItemUpdating="lvMnglist_ItemUpdating">
        <LayoutTemplate>
            <table cellspacing="0" class="TbCel-Type4-A iw820 bdFFF">
                <col width="70px"/>
                <col width="55px"/>
                <col width="55px"/>
                <col width="55px"/>
                <col width="55px"/>
                <col width="190px"/>
                <col width="160px"/>
                <col width="50px"/>
                <col width="50px"/>
                <col width="60px"/>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="TbTxtCenter P0"><asp:TextBox ID="txtSeq" runat="server" MaxLength="2" Width="25px" ReadOnly="true"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0"><asp:TextBox ID="txtDepth1" runat="server" MaxLength="2" Width="25px"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0"><asp:TextBox ID="txtDepth2" runat="server" MaxLength="2" Width="25px"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0"><asp:TextBox ID="txtDepth3" runat="server" MaxLength="2" Width="25px"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0"><asp:TextBox ID="txtDepth4" runat="server" MaxLength="2" Width="25px"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0">
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="1000" Width="140px"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/>
                </td>
                <td class="Bd-Lt TbTxtCenter P0"><asp:TextBox ID="txtUrl" runat="server" MaxLength="1000" Width="130px"></asp:TextBox></td>
                <td class="Bd-Lt TbTxtCenter P0">
                    <asp:DropDownList ID="ddlHiddenYn" runat="server">
                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="Bd-Lt TbTxtCenter P0">
                    <asp:DropDownList ID="ddlBoardYn" runat="server">
                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="Bd-Lt TbTxtCenter P0">
			        <span>
			            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Common/Images/Icon/edit.gif" ImageAlign="AbsMiddle"/>
			        </span>
			        <span>
			            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Common/Images/Icon/Trash.gif" ImageAlign="AbsMiddle"/>
			        </span>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TbCel-Type4-A">
		        <col width="70px"/>
		        <col width="55px"/>
		        <col width="55px"/>
		        <col width="55px"/>
		        <col width="55px"/>
		        <col width="190px"/>
		        <col width="170px"/>
		        <col width="60px"/>
		        <col width="60px"/>
		        <col width="50px"/>
                <tr>
                    <td colspan="10" class="P0" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="upClick" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlHiddenYn" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnTitle" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="TbCel-Type4-A-1">
		    <col width="70px"/>
		    <col width="55px"/>
		    <col width="55px"/>
		    <col width="55px"/>
		    <col width="55px"/>
		    <col width="190px"/>
		    <col width="160px"/>
		    <col width="50px"/>
		    <col width="50px"/>
		    <col width="80px"/>
            <tr>
                <td style="text-align:center"> </td>
                <td style="text-align:center"><asp:TextBox ID="txtDepth1" runat="server" MaxLength="2" Width="25"></asp:TextBox></td>
                <td style="text-align:center"><asp:TextBox ID="txtDepth2" runat="server" MaxLength="2" Width="25"></asp:TextBox></td>
                <td style="text-align:center"><asp:TextBox ID="txtDepth3" runat="server" MaxLength="2" Width="25"></asp:TextBox></td>
                <td style="text-align:center"><asp:TextBox ID="txtDepth4" runat="server" MaxLength="2" Width="25"></asp:TextBox></td>
                <td style="text-align:center">
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="1000" Width="105"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" ImageAlign="AbsMiddle" Width="17px" Height="15px"/>
                </td>
                <td style="text-align:center"><asp:TextBox ID="txtUrl" runat="server" MaxLength="1000" Width="105"></asp:TextBox></td>
                <td style="text-align:center">
                    <asp:DropDownList ID="ddlHiddenYn" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHiddenYn_SelectedIndexChanged">
                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:center">
                    <asp:DropDownList ID="ddlBoardYn" runat="server">
                        <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:center">
		            <span><asp:ImageButton ID="imgbtnInsert" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" ImageAlign="AbsMiddle" OnClick="imgbtnInsert_Click"/></span>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfErrData" runat="server"/>
        <asp:HiddenField ID="hfErrText" runat="server"/>
        <asp:HiddenField ID="hfExgistLine" runat="server"/>
        <asp:HiddenField ID="hfExgistMsg" runat="server"/>
        <asp:ImageButton ID="imgbtnTitle" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnTitle_Click"/>    
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">
<!--//
    fnExgistLine();
//-->
</script>
</asp:Content>