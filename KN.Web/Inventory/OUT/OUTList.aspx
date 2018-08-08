<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="OUTList.aspx.cs" Inherits="KN.Web.Inventory.OUTList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="upPaymentList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="20"/>
                    <col width="70"/>
                    <col width="110"/>
                    <col width="210"/>
                    <col width="130"/>
                    <col width="50"/>
                    <col width="50"/>
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltCreator" Text="Người tạo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltCreateDate" Text="Ngày tạo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltUserFor" Text ="Nội dung" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltNote" Text="Ghi chú" runat="server"></asp:Literal>
                            </th>
                            <th>
                            </th>
                            <th >
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lvLstOUT" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" InsertItemPosition="None">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="20"/>
                        <col width="70"/>
                        <col width="110"/>
                        <col width="210"/>
                        <col width="130"/>
                        <col width="50"/>
                        <col width="50"/>
                        <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"  Text='<%#Eval("INV_OUT_ID") %>'></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsCreator" runat="server" Text='<%#Eval("CreateUser") %>'></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsCreateDate" runat="server"  Text='<%#Eval("CreateDate") %>'></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsUseFor" runat="server"  Text='<%#Eval("UsedFor") %>'></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsNote" runat="server"  Text='<%#Eval("Note") %>'></asp:Literal></td>
			            <td class="TbTxtCenter"><a href ='../OUT/OUTNEW.aspx?outId=<%#Eval("INV_OUT_ID") %>'>Edit</a></td>
			            <td class="TbTxtCenter"><asp:LinkButton ID="lnkViewDetails"  runat="server" Text="Details"></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <EditItemTemplate></EditItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                        <tr>
                            <td colspan="5" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" Text="Không có dữ liệu" runat="server"></asp:Literal></td>
                        </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width:100%"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>