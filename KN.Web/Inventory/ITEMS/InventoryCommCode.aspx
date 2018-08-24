<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InventoryCommCode.aspx.cs" Inherits="KN.Web.Inventory.InventoryCommCode" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    function fnMovePage(intPageNo) {
        if (intPageNo == null) {
            intPageNo = 1;
        }

        document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
        <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
    }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <fieldset class="sh-field2 MrgB10">
                
                <ul class="sf2-ag MrgL10">
                    <li>
                        <asp:Literal ID="ltSearchName" Text="Item Name" runat="server"></asp:Literal></li>
                    <li>
                        <asp:TextBox ID="txtSearchNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>
                    <li>
                        <div class="C235-st FloatL">
                            <asp:DropDownList ID="ddlItemType" runat="server"></asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M"><span>
                                        <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span></div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPaymentList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="120" />
                    <col width="120" />
                    <col width="120" />
                    <col width="130" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltSeq" runat="server" Text="Group Name"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltText" runat="server" Text="Group Type Name"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltLocation" runat="server" Text="Group SubType Name"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltType" runat="server" Text="Image"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvItemList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID">
                <LayoutTemplate>
                    <table class="TypeA">
                    <col width="120" />
                    <col width="120" />
                    <col width="120" />
                    <col width="130" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltGroupName" runat="server" Text='<% #Eval("grName")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltTypeName" runat="server" Text='<% #Eval("grTypeName")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltSubTypeName" runat="server" Text='<% #Eval("grSubTypeName")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="Edit" CommandName="edit" Text="Edit"></asp:LinkButton>
                            </td>
                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <td class="TbTxtCenter">
                            <asp:DropDownList runat="server" ID="ddlEditGroup"></asp:DropDownList>
                        <td class="TbTxtCenter">
                            <asp:DropDownList runat="server" ID="ddlEditType"></asp:DropDownList>
                        <td class="TbTxtCenter">
                            <asp:TextBox runat="server" ID="txtEditSubType"></asp:TextBox>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete"></asp:LinkButton>
                        </td>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <td class="TbTxtCenter">
                            <asp:DropDownList runat="server" ID="ddlGroup"></asp:DropDownList>
                        <td class="TbTxtCenter">
                            <asp:DropDownList runat="server" ID="ddlType"></asp:DropDownList>
                        <td class="TbTxtCenter">
                            <asp:TextBox runat="server" ID="txtSubType"></asp:TextBox>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="lnkCreate" Text="Create"></asp:LinkButton>
                        </td>
                </InsertItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width: 100%"></span>
            </div>
            <asp:HiddenField ID="hfCurrentPage" runat="server"/>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="TypeA">
        <colgroup>
            <col width="60" />
            <col width="150" />
            <col width="70" />
            <col width="210" />
        </colgroup>
        <tr>
            <td>
               <%-- <asp:DropDownList runat="server" ID="ddlInsertGr">
                    <asp:ListItem Text="IT Department" Value="1"></asp:ListItem>
                    <asp:ListItem Text="IT Department" Value="1"></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:TextBox runat="server" ID="txtGroupName"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTypeName" />
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSubTypeName" />
            </td>
            <td>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkBtnAdd" runat="server" Text="Add New" OnClick="lnkBtnAdd_Click1"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>

            </td>
        </tr>
    </table>
</asp:Content>
