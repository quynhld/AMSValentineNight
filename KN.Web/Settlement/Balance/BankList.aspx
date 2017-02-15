<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="BankList.aspx.cs" Inherits="KN.Web.Settlement.Balance.BankList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnOpenFindBank()
    {
        var strParams1 = document.getElementById("<%=hfParamData.ClientID%>").value;
        var strParams2 = document.getElementById("<%=ddlCompNm.ClientID%>").value;

        window.open("/Common/Popup/PopupBankList.aspx?Params1=" + strParams1 + "&Params2=" + strParams2, "BankList", "status=no, resizable=no, width=600, height=350, left=100, top=100, scrollbars=no, menubar=no, toolbar=no, location=no");

        return false;
    }
    
    function fnHoldItem()
    {    
    }
</script>
    <table cellspacing="0" class="TbCel-Type4-A">
	    <colgroup>
            <col width="160px"/>
            <col width="100px"/>
            <col width="160px"/>
            <col width="220px"/>
            <col width="100px"/>
            <col width="100px"/>
	    </colgroup>
        <tr>
            <th><asp:Literal ID="ltTopCompNm" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltTopAccountCd" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltTopBankNm" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltTopAccountNo" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltTopAccCd" runat="server"></asp:Literal></th>
            <th class="Bd-Lt">&nbsp;</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnInsert" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlCompNm" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div style="overflow-y:scroll;height:370px;width:840px;">
                <asp:ListView ID="lvBankList" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnitemCreated="lvBankList_ItemCreated"
                    OnItemUpdating="lvBankList_ItemUpdating" OnItemDeleting="lvBankList_ItemDeleting" OnItemDataBound="lvBankList_ItemDataBound" OnItemCommand="lvBankList_ItemCommand">
                    <LayoutTemplate>
                        <table class="TypeA iw820">
                            <colgroup>
                                <col width="160px"/>
                                <col width="100px"/>
                                <col width="160px"/>
                                <col width="220px"/>
                                <col width="100px"/>
                                <col width="80px"/>
                            </colgroup>
                            <tr id="iphItemlPlaceholderId" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>            
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltCompNm" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfCompCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:Literal ID="ltAccountCd" runat="server"></asp:Literal>
                                <asp:TextBox ID="txtHfAccountCd" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtBankNm" runat="server" Width="140px" MaxLength="50"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAccountNo" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAccCd" runat="server" Width="60px" MaxLength="8"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <span>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Common/Images/Icon/edit.gif"/>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Common/Images/Icon/Trash.gif"/>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="TypeA iw820">
                            <colgroup>
                                <col width="160px"/>
                                <col width="100px"/>
                                <col width="160px"/>
                                <col width="220px"/>
                                <col width="100px"/>
                                <col width="80px"/>
                            </colgroup>
                            <tr>
                                <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>        
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <table cellspacing="0" class="TbCel-Type4-A">
	            <colgroup>
                    <col width="160px"/>
                    <col width="100px"/>
                    <col width="160px"/>
                    <col width="220px"/>
                    <col width="100px"/>
                    <col width="80px"/>
	            </colgroup>
                <tr id="trInsertAuthGrp" runat="server" style="background-color:#E5E5FE">
                    <td class="Bd-Lt TbTxtCenter" colspan="2">
                        <asp:DropDownList ID="ddlCompNm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompNm_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="Bd-Lt TbTxtCenter">
                        <asp:TextBox ID="txtBankNm" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Common/Images/Icon/icn_tabIcn.gif" OnClientClick="javascript:return fnOpenFindBank();" />
                    </td>
                    <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAccountNo" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAccCd" runat="server" Width="80px" MaxLength="8"></asp:TextBox></td>
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton ID="imgbtnInsert" runat="server" OnClick="imgbtnInsert_Click" ImageUrl="~/Common/Images/Icon/plus.gif"/>
                        </span>
                    </td>    
                </tr>
            </table>
            <asp:HiddenField ID="hfParamData" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>