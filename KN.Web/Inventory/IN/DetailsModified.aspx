<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="DetailsModified.aspx.cs" Inherits="KN.Web.Inventory.IN.DetailsModified" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1B iw840">
                <tr>
                    <th colspan="6">
                        <asp:Literal runat="server" ID="LbDetail" Text="Item Detail"></asp:Literal></th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="lbInvInId" Text="INV IN ID" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlInvInId" AutoPostBack="True" CssClass="bgType2" runat="server"></asp:DropDownList>
                    </td>
                    <th align="center">
                        <asp:Literal ID="lbIvnId" Text="TVN ID" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlIvnId" AutoPostBack="True" CssClass="bgType2" runat="server"></asp:DropDownList>
                    </td>
                    <th align="center">
                        <asp:Literal ID="lbAmoubt" Text="Amount" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtAmount" CssClass="bgType2" Width="150" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="lbNoteDetails" Text="Note" runat="server"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtNoteDetails" TextMode="multiline" Columns="25" Rows="3" runat="server"></asp:TextBox>
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
