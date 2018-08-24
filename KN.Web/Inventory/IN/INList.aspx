<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="INList.aspx.cs" Inherits="KN.Web.Inventory.INList" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <fieldset class="sh-field2 MrgB10">
                <ul class="sf2-ag MrgL10">
                    <li>
                        <asp:Literal ID="ltSearchName" runat="server"></asp:Literal></li>
                    <li>
                        <asp:TextBox ID="txtSearchNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input"></asp:TextBox></li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" Text="Search"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upINList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="3%" />
                    <col width="15%" />
                    <col width="15%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="10%" />
                    <col width="15%" />
                    <col width="5%" />
                    <col width="6%" />
                    <col width="6%" />
                    <col width="5%" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltIvnInId" runat="server" Text="No"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltCreateDate" runat="server" Text="Create Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltModifyDate" runat="server" Text="Modify Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltCreateUser" runat="server" Text="Create User"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltModifyUser" runat="server" Text="Modify User"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltUsedFor" runat="server" Text="User For"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltNote" runat="server" Text="Note"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltStatus" runat="server" Text="Status"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltDetails" runat="server" Text="Details"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="LtEdit" runat="server" Text="Edit"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="LtRemove" runat="server" Text="Remove"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="3%" />
                        <col width="15%" />
                        <col width="15%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="10%" />
                        <col width="15%" />
                        <col width="5%" />
                        <col width="6%" />
                        <col width="6%" />
                        <col width="5%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsIvnInId" runat="server" Text='<%# Eval("IVN_IN_ID") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsCreateDate" runat="server" Text='<%# Eval("CreateDate") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsModifyDate" runat="server" Text='<%# Eval("ModifyDate") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsCreateUser" runat="server" Text='<%# Eval("CreateUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsModifyUser" runat="server" Text='<%# Eval("ModifyUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsUsedFor" runat="server" Text='<%# Eval("UsedFor") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsNote" runat="server" Text='<%# Eval("Note") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="lnkDetails" PostBackUrl='<%# "~/Inventory/IN/InDetails.aspx?ID="+Eval("IVN_IN_ID").ToString()%>'> Details </asp:LinkButton></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="LnkEdit" PostBackUrl='<%# "~/Inventory/IN/InModified.aspx?editIdIn="+Eval("IVN_IN_ID").ToString()+"&status=0"%>'> Edit </asp:LinkButton></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="LnkRemove" PostBackUrl='<%# "~/Inventory/IN/InModified.aspx?editIdIn="+Eval("IVN_IN_ID").ToString()+"&status=1"%>'> Remove </asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsIvnInId" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsCreateDate" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsModifyDate" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsCreateUser" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsModifyUser" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsUsedFor" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsNote" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltInsStatus" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="ltDetails" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="LtEdit" runat="server"></asp:Literal></td>
                    <td class="TbTxtCenter">
                        <asp:Literal ID="LtRemove" runat="server"></asp:Literal></td>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:LinkButton ID="lnkAddNewRow" runat="server"></asp:LinkButton></td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>

            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width: 100%"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="TypeA">
        <col width="3%" />
        <col width="15%" />
        <col width="15%" />
        <col width="10%" />
        <col width="10%" />
        <col width="10%" />
        <col width="15%" />
        <col width="5%" />
        <col width="6%" />
        <col width="6%" />
        <col width="5%" />
        <tbody>
            <tr>
                <td></td>
                <td>
                    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" /></td>
                <td></td>
                <td>

                    <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" />

                </td>
                <td></td>
                <td>
                    <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
                                <div class="Btn-Tp3-M">
                                    <span>
                                        <asp:LinkButton ID="lnkAddNew" runat="server" OnClick="lnkAddNew_Click" Text="Add New" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
