<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OUTNEW.aspx.cs" Inherits="KN.Web.Inventory.OUTNEW" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <fieldset class="sh-field5 MrgB10">
        <ul class="sf5-ag MrgL30">

            <li>
                <asp:Literal ID="Literal1" Text="Title" runat="server"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtUsedFor" Text="Title" runat="server" Width="270px"></asp:TextBox>
            </li>
            <li>
                <asp:Literal ID="ltCreateBy" runat="server" Text="Create By"></asp:Literal>
            </li>
            <li>
                <b>
                    <asp:Literal ID="ltBindCreateBy" runat="server"></asp:Literal></b>
            </li>
            <li>
                <asp:Literal ID="ModBy" runat="server" Text="Modify By"></asp:Literal>
            </li>
            <li>
                <b>
                    <asp:Literal ID="ltBindModBy" runat="server"></asp:Literal></b>
            </li>
        </ul>
        <ul class="sf5-ag MrgL30">
            <li>
                <asp:Literal ID="Literal8" Text="Note" runat="server"></asp:Literal>
            </li>
            <li>
                <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" Width="271px"></asp:TextBox>
            </li>
            <li>
                <asp:Literal ID="Literal2" runat="server" Text="Create Date"></asp:Literal>
            </li>
            <li>
                <asp:Literal ID="ltCreateDate" runat="server"></asp:Literal>
            </li>
            <li>
                <asp:Literal ID="Literal4" runat="server" Text="Modify Date"></asp:Literal>
            </li>
            <li>
                <asp:Literal ID="ltModDate" runat="server"></asp:Literal>
            </li>
        </ul>
    </fieldset>
    <asp:UpdatePanel ID="upPaymentList" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvOutDetails" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" InsertItemPosition="LastItem" OnItemCreated="lvOutDetails_ItemCreated" OnItemCommand="lvOutDetails_ItemCommand">
                <LayoutTemplate>
                    <table class="TypeA">
                        <colgroup>
                            <col width="30" />
                            <col width="240" />
                            <col width="70" />
                            <col width="110" />
                            <col width="130" />
                            <col width="130" />
                            <col width="130" />
                            <thead>
                                <tr>
                                    <th>
                                        <asp:Literal ID="ltSeq" Text="STT" runat="server"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="ltItemName" Text="Item Name" runat="server"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="ltAmount" Text="Amount" runat="server"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="ltUnit" Text="Unit" runat="server"></asp:Literal>
                                    </th>
                                    <th>
                                        <asp:Literal ID="ltNote" Text="Note" runat="server"></asp:Literal>
                                    </th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                        </colgroup>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr  style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltOutDetailID" runat="server" Text='<%#Eval("ID") %>'></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltItemName" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Literal></td>
                            <asp:Literal ID="ltItemID" runat="server" Text='<%#Eval("INV_ID") %>' Visible="false"></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltItemAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltUnit" runat="server" Text='<%#Eval("ItemUnit") %>'></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:Literal ID="ltNote" runat="server" Text='<%#Eval("Note") %>'></asp:Literal></td>
                        <td class="Bd-Lt TbTxtCenter">
                            <asp:LinkButton ID="ltRemove" runat="server" CommandName="Remove" Text="Remove" OnClick="ltRemove_Click" ></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <InsertItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutDetailID" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:DropDownList runat="server" ID="ddrSelectItems" OnSelectedIndexChanged="ddrSelectItems_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td class="TbTxtCenter">
                            <asp:TextBox ID="txtItemAmount" runat="server"></asp:TextBox></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="txtUnit" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:TextBox ID="txtInsertNote" runat="server"></asp:TextBox></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton ID="ltAdd" runat="server" Text="Add" OnClick="ltAdd_Click"></asp:LinkButton></td>
                    </tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
