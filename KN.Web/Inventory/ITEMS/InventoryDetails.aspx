<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InventoryDetails.aspx.cs" Inherits="KN.Web.Inventory.InventoryDetails" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
    function fnMovePageIn(intInPageNo) {
        if (intInPageNo == null) {
            intInPageNo = 1;
        }

        document.getElementById("<%=hfInCurrentPage.ClientID%>").value = intInPageNo;
        <%=Page.GetPostBackEventReference(imgbtnInPageMove)%>;
    }

        function fnMovePageOut(intOutPageNo) {
            if (intOutPageNo == null) {
                intOutPageNo = 1;
            }

            document.getElementById("<%=hfOutCurrentPage.ClientID%>").value = intOutPageNo;
                <%=Page.GetPostBackEventReference(imgbtnOutPageMove)%>;
            }
    </script>
    <asp:UpdatePanel ID="upAddItem" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnSave" />
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
    <asp:UpdatePanel ID="upInList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="20" />
                    <col width="260" />
                    <col width="110" />
                    <col width="110" />
                    <col width="110" />
                    <col width="110" />
                    <col width="120" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltInSeq" runat="server" Text="Seq"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInTitle" runat="server" Text="Title"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInCreateBy" runat="server" Text="Create By"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInCreateDate" runat="server" Text="Create Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInModBy" runat="server" Text="Mod By"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInModDate" runat="server" Text="Mode Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltInDetails" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lsvIN" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" InsertItemPosition="None">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="20" />
                        <col width="260" />
                        <col width="110" />
                        <col width="110" />
                        <col width="110" />
                        <col width="110" />
                        <col width="120" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemSeq" runat="server" Text='<%#Eval("RealSeq") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemTitle" runat="server" Text='<%#Eval("UsedFor") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemCreator" runat="server" Text='<%#Eval("CreateUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemCreateDate" runat="server" Text='<%#Eval("CreateDate","{0:d}") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemModBy" runat="server" Text='<%#Eval("ModifyUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemModDate" runat="server" Text='<%#Eval("ModifyDate","{0:d}") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInItemStt" runat="server" Text='<%#Eval("Status") %>'></asp:Literal></td>
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
                <span id="spanInPageNavi" runat="server" style="width: 100%"></span>
            </div>
            <asp:HiddenField ID="hfInCurrentPage" runat="server" />
            <asp:ImageButton ID="imgbtnInPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnInPageMove_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upOutList" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <table class="TypeA">
                <colgroup>
                    <col width="20" />
                    <col width="260" />
                    <col width="110" />
                    <col width="110" />
                    <col width="110" />
                    <col width="110" />
                    <col width="120" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltOutSeq" runat="server" Text="Seq"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutTitle" runat="server" Text="Title"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutCreateBy" runat="server" Text="Create By"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutCreateDate" runat="server" Text="Create Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutModBy" runat="server" Text="Mod By"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutModDate" runat="server" Text="Mode Date"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltOutDetails" runat="server"></asp:Literal>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </colgroup>
            </table>
            <asp:ListView ID="lsvOut" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" InsertItemPosition="None">
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="20" />
                        <col width="260" />
                        <col width="110" />
                        <col width="110" />
                        <col width="110" />
                        <col width="110" />
                        <col width="120" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsSeq" runat="server" Text='<%#Eval("RealSeq") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemTitle" runat="server" Text='<%#Eval("UsedFor") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemCreator" runat="server" Text='<%#Eval("CreateUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemCreateDate" runat="server" Text='<%#Eval("CreateDate","{0:d}") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemModBy" runat="server" Text='<%#Eval("ModifyUser") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemModeDate" runat="server" Text='<%#Eval("ModifyDate","{0:d}") %>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltOutItemStt" runat="server" Text='<%#Eval("Status") %>'></asp:Literal></td>
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
                <span id="spanOutPageNavi" runat="server" style="width: 100%"></span>
            </div>
            <asp:HiddenField ID="hfOutCurrentPage" runat="server" />
            <asp:ImageButton ID="imgbtnOutPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnOutPageMove_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
