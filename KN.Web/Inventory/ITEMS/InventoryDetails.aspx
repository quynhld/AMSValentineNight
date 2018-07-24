<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InventoryDetails.aspx.cs" Inherits="KN.Web.Inventory.InventoryDetails" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="upAddItem" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID ="lnkbtnSave" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" class="TbCel-Type1B iw840">
                <tr>
                    <th colspan="6">
                        <asp:Literal runat="server" ID="ltee" Text="Item Info"></asp:Literal></th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltIVTEName" Text="Name" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIVTEngName" runat="server" CssClass="bgType2" Width="100"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltIVTViName" Text="Vietnamese Name" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIVTViName" runat="server" CssClass="bgType2" Width="150"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltIVTUnit" runat="server" Text="Unit" /></th>
                    <td>
                        <asp:TextBox ID="txtIVTUnit" runat="server" CssClass="bgType2" Width="100" />
                    </td>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltIVTCategory" Text="Category" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIVTCategory" runat="server" CssClass="bgType2" Width="100"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltQuantity" Text="Quantity" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtQuantity" runat="server" CssClass="bgType2" Width="100"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltIVModel" Text="Model" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIVTModel" runat="server" CssClass="bgType2" Width="100"></asp:TextBox></td>
                </tr>
                <tr>
                    <th colspan="6"></th>
                </tr>
                <tr>
                    <th colspan="6">
                        <asp:Literal runat="server" ID="ltLocation" Text="Location"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltIVTLocation" runat="server" Text="Area"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtArea" runat="server" CssClass="bgType2" Width="100"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltIvtZone" runat="server" Text="Zone"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtZone" CssClass="grBg bgType2" Width="100px" runat="server" /></td>
                    <th align="center">
                        <asp:Literal ID="ltIvtNo" runat="server" Text="No"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtNo" CssClass="grBg bgType2" Width="100px" runat="server" /></td>
                </tr>
                <tr>
                    <th colspan="6"></th>
                </tr>
                <tr>
                    <th colspan="6">
                        <asp:Literal runat="server" ID="ltSize" Text="Size"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltIvtHeight" Text="Height" runat="server"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtHeight" runat="server" Width="100px"></asp:TextBox></td>
                    <th align="center">
                        <asp:Literal ID="ltIVTWidth" runat="server" Text="Width"></asp:Literal></th>
                    <td>
                        <asp:TextBox ID="txtIvtWidth" runat="server" Width="100px" />

                    </td>
                    <th align="center">
                        <asp:Literal ID="Literal8" runat="server" Text="Wide/"></asp:Literal>
                        <asp:Literal ID="Literal9" runat="server" Text="Radius"></asp:Literal>
                    </th>
                    <td>
                        <asp:TextBox ID="txtIvtWide" runat="server" Width="100px" />/
                 <asp:TextBox ID="txtIvtRadius" runat="server" Width="100px" /></td>
                </tr>
                <tr>
                    <th colspan="6"></th>
                </tr>
                <tr>
                    <th align="center">
                        <asp:Literal ID="ltIvtImage" runat="server" Text="Image Path"></asp:Literal></th>
                    <td colspan="4">
                        <asp:Image runat="server" ID="IvtImage" Width="150px" Height="75px" />
                        <asp:FileUpload runat="server" ID="fuIvtImage" />

                    </td>
                    <td colspan="2">
                        <div class="Btn-Type3-wp ">
                            <div class="Btn-Tp3-L">
                                <div class="Btn-Tp3-R">
                                    <div class="Btn-Tp3-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnSave" Text="Save" runat="server" OnClick="lnkbtnSave_Click"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPaymentList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="60" />
                    <col width="70" />
                    <col width="110" />
                    <col width="210" />
                    <col width="130" />
                    <col width="130" />
                    <col width="130" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltSeq" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRentNm" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRoomNo" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltName" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltMngFee" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRentalFee" runat="server"></asp:Literal>
                            </th>
                            <th class="Ls-line">
                                <asp:Literal ID="ltUtilFee" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lsvIN" runat="server" ItemPlaceholderID="iphItemPlaceHolderID">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="60" />
                        <col width="70" />
                        <col width="110" />
                        <col width="210" />
                        <col width="130" />
                        <col width="130" />
                        <col width="130" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
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
            <table class="TypeA">
                <colgroup>
                    <col width="60" />
                    <col width="70" />
                    <col width="110" />
                    <col width="210" />
                    <col width="130" />
                    <col width="130" />
                    <col width="130" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                            </th>
                            <th class="Ls-line">
                                <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lsvOut" runat="server" ItemPlaceholderID="iphItemPlaceHolderID">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="60" />
                        <col width="70" />
                        <col width="110" />
                        <col width="210" />
                        <col width="130" />
                        <col width="130" />
                        <col width="130" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
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
