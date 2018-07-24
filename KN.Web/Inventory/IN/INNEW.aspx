<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="INNEW.aspx.cs" Inherits="KN.Web.Inventory.INNEW" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">

    <asp:UpdatePanel ID="upBasicInfo" runat="server" UpdateMode="Conditional">
        <Triggers>
            <%--            <asp:PostBackTrigger ControlID="lnkbtnWrite" />--%>
        </Triggers>
        <ContentTemplate>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltBasicInfo" runat="server"></asp:Literal>
                <asp:Literal ID="ltIncharge" runat="server"></asp:Literal>
                <asp:TextBox ID="txtInchage" runat="server" MaxLength="20" Width="100px" CssClass="bgType2"></asp:TextBox>)
            </div>
            <table cellspacing="0" class="TbCel-Type2-A">
                <colgroup>
                    <col width="147px" />
                    <col width="178px" />
                    <col width="147px" />
                    <col width="178px" />
                    <tbody>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="Location"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlLocation" runat="server" Width="600">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="Literal3" runat="server" Text="Use for"></asp:Literal>
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txtLessorRoomNo" runat="server" Width="600"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
            <div id="lineRow" style="display: none">
            </div>
            <div class="Tb-Tp-tit">
                <asp:Literal ID="ltMngFee" runat="server" Text="List Items"></asp:Literal>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            <asp:ListView ID="lsvAddIn" runat="server" DataKeyNames="IVN_ID" DataSourceID="SqlDataSource1" EnableModelValidation="True" InsertItemPosition="LastItem" OnItemCreated="lsvAddIn_ItemCreated">
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server"  class="TbCel-Type2-A" >
                                    <tr >
                                        <th runat="server"></th>
                                        <th runat="server"></th>
                                        <th runat="server">Item_Name</th>
                                        <th runat="server">Item_EName</th>
                                        <th runat="server">Item_Amout/Item Remain</th>
                                        <th runat="server">Item_Type</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="Link" NextPageImageUrl="~/Common/Images/Btn/next.gif" PreviousPageImageUrl="~/Common/Images/Btn/prev.gif"  />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <InsertItemTemplate>
                    <tr style="">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="Item_NameTextBox" runat="server"  DataTextField="Item_Name" DataValueField="IVN_ID" />
                        </td>
                        <td>
                            <asp:DropDownList ID="Item_ENameTextBox" runat="server"  DataTextField="Item_Name" DataValueField="IVN_ID"  />
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemAmount" runat="server"/>
                            <asp:TextBox ID="TxtItem_AmountRemain" runat="server" Text='<%# Bind("Item_Amout") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Item_TypeTextBox" runat="server" Text='<%# Bind("Item_Type") %>' />
                        </td>
                                                <td>
                            <asp:LinkButton ID="InsertButton" runat="server" CommandName="Add" Text="Insert" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF;color: #333333;">

                        <td>
                            <asp:Label ID="IVN_IDLabel" runat="server" Text='<%# Eval("IVN_ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_NameLabel" runat="server" Text='<%# Eval("Item_Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_ENameLabel" runat="server" Text='<%# Eval("Item_EName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_AmoutLabel" runat="server" Text='<%# Eval("Item_Amout") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_TypeLabel" runat="server" Text='<%# Eval("Item_Type") %>' />
                        </td>
                                                <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF;color: #284775;">

                        <td>
                            <asp:Label ID="IVN_IDLabel" runat="server" Text='<%# Eval("IVN_ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_NameLabel" runat="server" Text='<%# Eval("Item_Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_ENameLabel" runat="server" Text='<%# Eval("Item_EName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_AmoutLabel" runat="server" Text='<%# Eval("Item_Amout") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_TypeLabel" runat="server" Text='<%# Eval("Item_Type") %>' />
                        </td>
                                                <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #999999;">

                        <td>
                            <asp:Label ID="IVN_IDLabel1" runat="server" Text='<%# Eval("IVN_ID") %>' />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="Item_NameTextBox" Text='<%# Bind("Item_Name") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="Item_ENameTextBox" Text='<%# Bind("Item_EName") %>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="Item_AmoutTextBox" runat="server" Text='<%# Bind("Item_Amout") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Item_TypeTextBox" runat="server" Text='<%# Bind("Item_Type") %>' />
                        </td>
                                                <td>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                    </tr>
                </EditItemTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">

                        <td>
                            <asp:Label ID="IVN_IDLabel" runat="server" Text='<%# Eval("IVN_ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_NameLabel" runat="server" Text='<%# Eval("Item_Name") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_ENameLabel" runat="server" Text='<%# Eval("Item_EName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_AmoutLabel" runat="server" Text='<%# Eval("Item_Amout") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Item_TypeLabel" runat="server" Text='<%# Eval("Item_Type") %>' />
                        </td>
                                                <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <div class="Btwps FloatR2">
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnWrite" runat="server"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkbtnCancel" runat="server"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
