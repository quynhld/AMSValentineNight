<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InModified.aspx.cs" Inherits="KN.Web.Inventory.IN.InModified" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1B iw840">
                <tr>
                    <th colspan="6">
                        <asp:Literal runat="server" ID="lbInfo" Text="Item Info"></asp:Literal></th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="lbCreateUser" Text="Create User" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtCreateUser" runat="server" CssClass="bgType2" Width="150"></asp:TextBox>
                    </td>
                    <th align="center">
                        <asp:Literal ID="lbModifyUser" Text="Modify User" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtModifyUser" runat="server" CssClass="bgType2" Width="150"></asp:TextBox>
                    </td>

                    <th align="center">
                        <asp:Literal ID="lbUsedFor" Text="Used For" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtUsedFor" runat="server" CssClass="bgType2" Width="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="lbNote" Text="Note" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtNote" TextMode="multiline" Columns="25" Rows="3" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="4"></td>
                    <td colspan="2">
                        <div class="Btn-Type3-wp ">
                            <div class="Btn-Tp3-L">
                                <div class="Btn-Tp3-R">
                                    <div class="Btn-Tp3-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnSave" Text="Save" runat="server" OnClick="btnSave"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
