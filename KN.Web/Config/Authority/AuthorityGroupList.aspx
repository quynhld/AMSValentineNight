<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="AuthorityGroupList.aspx.cs" Inherits="KN.Web.Config.Authority.AuthorityGroupList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnCheckValidate(strText)
        {
            var strInsAuthNm = document.getElementById("<%=txtInsAuthNm.ClientID%>");
            var strInsAuthENm = document.getElementById("<%=txtInsAuthENm.ClientID%>");
            var strInsAuthKNm = document.getElementById("<%=txtInsAuthKNm.ClientID%>");

            if (trim(strInsAuthNm.value) == "")
            {
                alert(strText);
                strInsAuthNm.focus();

                return false;
            }

            if (trim(strInsAuthENm.value) == "")
            {
                alert(strText);
                strInsAuthENm.focus();

                return false;
            }

            if (trim(strInsAuthKNm.value) == "")
            {
                alert(strText);
                strInsAuthKNm.focus();

                return false;
            }

            return true;
        }
    //-->
    </script>
    <table cellspacing="0" class="TbCel-Type4-A">
	    <colgroup>
            <col width="50px"/>
            <col width="170px"/>
            <col width="170px"/>
            <col width="170px"/>
            <col width="170px"/>
            <col width="110px"/>
	    </colgroup>
        <tr>
            <th ><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltAuthCd" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltAuthNm" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltAuthENm" runat="server"></asp:Literal></th>
            <th class="Bd-Lt"><asp:Literal ID="ltAuthKNm" runat="server"></asp:Literal></th>
            <th class="Bd-Lt">&nbsp;</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upList" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnInsert" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div style="overflow-y:scroll;height:370px;width:840px;">
                <asp:ListView ID="lvAuthGrp" runat="server" ItemPlaceholderID="iphItemlPlaceholderId" OnitemCreated="lvAuthGrp_ItemCreated"
                    OnItemUpdating="lvAuthGrp_ItemUpdating" OnItemDeleting="lvAuthGrp_ItemDeleting" OnItemDataBound="lvAuthGrp_ItemDataBound" OnItemCommand="lvAuthGrp_ItemCommand">
                    <LayoutTemplate>
                        <table class="TypeA iw820">
                            <col width="50px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="90px"/>
                            <tr id="iphItemlPlaceholderId" runat="server"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>            
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtSeq" runat="server" MaxLength="5" Width="25px" ReadOnly="true"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter">
                                <asp:TextBox ID="txtAuthCd" runat="server" Width="125px" MaxLength="8" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtHfAuthLvl" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAuthNm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAuthENm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
                            <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtAuthKNm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
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
                            <col width="50px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="170px"/>
                            <col width="90px"/>
                            <tr>
                                <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </table>        
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <table cellspacing="0" class="TbCel-Type4-A">
	            <colgroup>
                    <col width="50px"/>
                    <col width="170px"/>
                    <col width="170px"/>
                    <col width="170px"/>
                    <col width="170px"/>
                    <col width="90px"/>
	            </colgroup>
                <tr id="trInsertAuthGrp" runat="server" style="background-color:#E5E5FE">
                    <td colspan="2" class="Bd-Lt TbTxtCenter">
                        <asp:TextBox ID="txtInsAuthCd" runat="server" Width="125px" MaxLength="8" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtHfAuthCd" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAuthNm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAuthENm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
                    <td class="Bd-Lt TbTxtCenter"><asp:TextBox ID="txtInsAuthKNm" runat="server" Width="125px" MaxLength="50"></asp:TextBox></td>
                    <td class="Bd-Lt TbTxtCenter">
                        <span>
                            <asp:ImageButton ID="imgbtnInsert" runat="server" OnClick="imgbtnInsert_Click" ImageUrl="~/Common/Images/Icon/plus.gif"/>
                        </span>
                    </td>    
                </tr>
            </table>
            <asp:TextBox ID="txtMaxAuthCd" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtHfAuthLvl" runat="server" Visible="false"></asp:TextBox>
</asp:Content>