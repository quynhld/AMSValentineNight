<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MenuAddonList.aspx.cs" Inherits="KN.Web.Config.Menu.MenuAddonList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

    <script language="javascript" type="text/javascript">
<!--//
function fnDetailView(intMenuSeq)
{
    document.getElementById("<%=hfMenuSeq.ClientID%>").value = intMenuSeq;
    <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;
}

function fnUpdateCheckValidate(strControlID, strText1, strText2, strText3)
{
    var strMenuSeq = document.getElementById("<%=hfMenuSeq.ClientID%>");
    var strControl = document.getElementById(strControlID);
    
    if (trim(strMenuSeq.value) == "")
    {
        alert(strText1);
        return false;
    }
    else if (trim(strControl.value) == "")
    {
        alert(strText2);
        strControl.focus();
        return false;
    }
    else
    {
        alert(strText3);
        return true;
    }
}

function fnCheckValidate(strControlID, strText1, strText2)
{
    var strMenuSeq = document.getElementById("<%=hfMenuSeq.ClientID%>");
    var strControl = document.getElementById(strControlID);
    
    if (trim(strMenuSeq.value) == "")
    {
        alert(strText1);
        return false;
    }
    else if (trim(strControl.value) == "")
    {
        alert(strText2);
        strControl.focus();
        return false;
    }
    else
    {
        return true;
    }
}

function fnCheckSelect(strText)
{
    var strMenuSeq = document.getElementById("<%=hfMenuSeq.ClientID%>");
    
    if (trim(strMenuSeq.value) == "")
    {
        alert(strText);
        return false;
    }
    else
    {
        return true;
    }
}
//-->
</script>
<table cellspacing="0" class="TbCel-Type1">
    <col width="260px"/>
    <col width="390px"/>
    <tr>
        <th style="text-align:center"><asp:Literal ID="ltMenuTitle" runat="server"></asp:Literal></th>
        <th style="text-align:center"><asp:Literal ID="ltMenuUrl" runat="server"></asp:Literal></th>
    </tr>
</table>
<div style="overflow-y:scroll;height:150px;width:820px;">
    <asp:ListView ID="lvMenulist" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvMenulist_ItemCreated" OnItemDataBound="lvMenulist_ItemDataBound">
        <LayoutTemplate>
            <table cellspacing="0" class="TbCel-Type1 iw800">
                <col width="260px"/>
                <col width="370px"/>
                <tr id="iphItemPlaceHolderID" runat="server"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr onclick='javascript:fnDetailView("<%#Eval("MenuSeq")%>");' style="cursor:pointer;">
                <td style="text-align:left" class="P0"><asp:Literal ID="ltlMenuTitle" runat="server"></asp:Literal></td>
                <td style="text-align:left" class="P0"><asp:Literal ID="ltIMenuUrl" runat="server"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table cellspacing="0" class="TbCel-Type1">
                <col width="260px"/>
                <col width="370px"/>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
<asp:UpdatePanel ID="upPanel" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnParamInsert" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnLinkInsert" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="chkReadAuthList" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="chkWriteAuthList" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="chkModAuthList" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnEntireRead" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnEntireWrite" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnEntireMod" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnAuthSave" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <table cellpadding="0" class="TbCel-Type1">
            <colgroup>
                <col width="130px"/>
                <col width="520px"/>
                <tr>
                    <th><asp:Literal ID="ltItemTitle" runat="server"></asp:Literal></th>
                    <td><asp:Literal ID="ltDataTitle" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltItemReadAuth" runat="server"></asp:Literal>
                        <br />
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span><asp:LinkButton ID="lnkbtnEntireRead" runat="server" OnClick="lnkbtnEntireRead_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </th>
                    <td>
                        <asp:CheckBoxList ID="chkReadAuthList" runat="server" AutoPostBack="true" CellPadding="0" CellSpacing="0" 
                            OnSelectedIndexChanged="chkReadAuthList_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltItemWriteAuth" runat="server"></asp:Literal>
                        <br />
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span><asp:LinkButton ID="lnkbtnEntireWrite" runat="server" OnClick="lnkbtnEntireWrite_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </th>
                    <td>
                        <asp:CheckBoxList ID="chkWriteAuthList" runat="server" AutoPostBack="true" CellPadding="0" CellSpacing="0" 
                            OnSelectedIndexChanged="chkWriteAuthList_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltItemModAuth" runat="server"></asp:Literal>
                        <br />
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span><asp:LinkButton ID="lnkbtnEntireMod" runat="server" OnClick="lnkbtnEntireMod_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </th>
                    <td>
                        <asp:CheckBoxList ID="chkModAuthList" runat="server" AutoPostBack="true" CellPadding="0" CellSpacing="0" 
                            OnSelectedIndexChanged="chkModAuthList_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Btn-Type2-wp FloatR2">
	                        <div class="Btn-Tp2-L">
		                        <div class="Btn-Tp2-R">
			                        <div class="Btn-Tp2-M">
				                        <span><asp:LinkButton ID="lnkbtnAuthSave" runat="server" onclick="lnkbtnAuthSave_Click"></asp:LinkButton></span>
			                        </div>
		                        </div>
	                        </div>
                        </div>
                    </td>
                </tr>
            </colgroup>
        </table>
        <asp:HiddenField ID="hfMenuSeq" runat="server"/>
        <asp:TextBox ID="txtHfReadAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfWriteAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfModDelAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfTotalAuthGrpTy" runat="server" Visible="false"></asp:TextBox>
        <asp:ListView ID="lvLinkList" runat="server" ItemPlaceholderID="iphItemPlaceHoderID" OnItemDataBound="lvLinkList_ItemDataBound"
            OnLayoutCreated="lvLinkList_LayoutCreated" OnItemCreated="lvLinkList_ItemCreated" OnItemCommand="lvLinkList_ItemCommand"
            OnItemUpdating="lvLinkList_ItemUpdating" OnItemDeleting="lvLinkList_ItemDeleting">
            <LayoutTemplate>
                <table cellpadding="0" class="TbCel-Type1">
                    <col width="160px"/>
                    <col width="340px"/>
                    <col width="150px"/>
                    <tr>
                        <th style="text-align:center"><asp:Literal ID="ltLinkTitle" runat="server"></asp:Literal></th>
                        <th style="text-align:center"><asp:Literal ID="ltLinkContent" runat="server"></asp:Literal></th>
                        <th></th>
                    </tr>
                    <tr id="iphItemPlaceHoderID" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlLink" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtHfLink" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td><asp:TextBox ID="txtLink" runat="server" Width="330" MaxLength="255"></asp:TextBox></td>
                    <td style="text-align:center">
                        <span><asp:ImageButton ID="imgbtnUpdate" CommandName="UPDATE" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                        <span><asp:ImageButton ID="imgbtnDelete" CommandName="DELETE" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellpadding="0" class="TbCel-Type1">
                    <col width="160px"/>
                    <col width="340px"/>
                    <col width="150px"/>
                    <tr>
                        <th style="text-align:center"><asp:Literal ID="ltLinkTitle" runat="server"></asp:Literal></th>
                        <th style="text-align:center"><asp:Literal ID="ltLinkContent" runat="server"></asp:Literal></th>
                        <th></th>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <table cellpadding="0" class="TbCel-Type1">
            <colgroup>
                <col width="160px"/>
                <col width="340px"/>
                <col width="150px"/>
                <tr>
                    <td><asp:DropDownList ID="ddlLink" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtLink" runat="server" MaxLength="255" Width="330"></asp:TextBox></td>
                    <td style="text-align:center">
                        <span><asp:ImageButton ID="imgbtnLinkInsert" runat="server" CommandName="LINKINSERT" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnLinkInsert_Click"/></span>
                    </td>
                </tr>
            </colgroup>
        </table>
        <asp:ListView ID="lvParamList" runat="server" ItemPlaceholderID="iphItemPlaceHoderID" OnItemDataBound="lvParamList_ItemDataBound" 
            OnLayoutCreated="lvParamList_LayoutCreated" OnItemCreated="lvParamList_ItemCreated" OnItemCommand="lvParamList_ItemCommand"
            OnItemUpdating="lvParamList_ItemUpdating" OnItemDeleting="lvParamList_ItemDeleting">
            <LayoutTemplate>
                <table cellpadding="0" class="TbCel-Type1">
                    <col width="160px"/>
                    <col width="340px"/>
                    <col width="150px"/>
                    <tr>
                        <th style="text-align:center"><asp:Literal ID="ltParamsTitle" runat="server"></asp:Literal></th>
                        <th style="text-align:center"><asp:Literal ID="ltParamsContent" runat="server"></asp:Literal></th>
                        <th></th>
                    </tr>
                    <tr id="iphItemPlaceHoderID" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlParams" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtHfParams" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td><asp:TextBox ID="txtParams" runat="server" Width="330" MaxLength="255"></asp:TextBox></td>
                    <td style="text-align:center">
                        <span><asp:ImageButton ID="imgbtnUpdate" CommandName="UPDATE" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                        <span><asp:ImageButton ID="imgbtnDelete" CommandName="DELETE" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellpadding="0" class="TbCel-Type1">
                    <col width="160px"/>
                    <col width="340px"/>
                    <col width="150px"/>
                    <tr>
                        <th style="text-align:center"><asp:Literal ID="ltParamsTitle" runat="server"></asp:Literal></th>
                        <th style="text-align:center"><asp:Literal ID="ltParamsContent" runat="server"></asp:Literal></th>
                        <th></th>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </EmptyDataTemplate>
         </asp:ListView>
        <table cellpadding="0" class="TbCel-Type1">
            <colgroup>
                <col width="160px"/>
                <col width="340px"/>
                <col width="150px"/>
                <tr>
                    <td><asp:DropDownList ID="ddlParams" runat="server"></asp:DropDownList></td>
                    <td><asp:TextBox ID="txtParams" runat="server" MaxLength="255" Width="330"></asp:TextBox></td>
                    <td style="text-align:center">
                        <span><asp:ImageButton ID="imgbtnParamInsert" runat="server" CommandName="PARAMINSERT" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnParamInsert_Click"/></span>
                    </td>
                </tr>
            </colgroup>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click"/>
</asp:Content>