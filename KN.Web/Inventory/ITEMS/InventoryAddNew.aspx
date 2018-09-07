<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InventoryAddNew.aspx.cs" Inherits="KN.Web.Inventory.InventoryAddNew" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="upAddItem" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkBtnImportExcel" />
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
               
                <table class="Type-viewB-PN iw840">
                    <colgroup>
                        <col  width="15%" />
                        <col width="70%" />
                        <col width="15%" />
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltAddonFile" runat="server" Text="Select Excel File"></asp:Literal>
                            </th>
                            <td><span class="Ls-line">
                                <asp:FileUpload ID="fuExcelUpload" runat="server" Width="300px" />
                                <asp:Literal ID="ltSampleFile" runat="server" Visible="false"></asp:Literal>
                                </span></td>
                            <td>
                                                   <div class="Btn-Type3-wp ">
                        <div class="Btn-Tp3-L">
                            <div class="Btn-Tp3-R">
                                <div class="Btn-Tp3-M">
                                    <span>
                                        <asp:LinkButton ID="lnkBtnImportExcel" runat="server" Text="Import" OnClick="lnkBtnImportExcel_Click"></asp:LinkButton></span>
                                </div>
                            </div>
                        </div>
                    </div>
                            </td>
                        </tr>
                    </colgroup>
                </table>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
