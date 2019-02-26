<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="InventoryList.aspx.cs" Inherits="KN.Web.Inventory.InventoryList" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">

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
                <legend>검색</legend>
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
                        <asp:TextBox ID="txtStartDt" runat="server" Width="70px"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtStartDt.ClientID%>', '<%=hfStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                        <asp:HiddenField ID="hfStartDt" runat="server" />
                    </li>
                    <li><span>~</span></li>
                    <li>
                        <asp:TextBox ID="txtEndDt" runat="server" Width="70px"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" onclick="Calendar(this, '<%=txtEndDt.ClientID%>', '<%=hfEndDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                        <asp:HiddenField ID="hfEndDt" runat="server" />
                    </li>
                    <li>
                        <div class="Btn-Type4-wp">
                            <div class="Btn-Tp4-L">
                                <div class="Btn-Tp4-R">
                                    <div class="Btn-Tp4-M">
                                        <span>
                                            <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                                    </div>
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
                    <col width="60" />
                    <col width="150" />
                    <col width="70" />
                    <col width="210" />
                    <col width="110" />
                    <col width="110" />
                    <col width="130" />
                    <thead>
                        <tr>
                            <th class="Fr-line">
                                <asp:Literal ID="ltSeq" runat="server" Text="No"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltText" runat="server" Text="Item Name"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltLocation" runat="server" Text="Location"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltType" runat="server" Text="Image"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltSize" runat="server" Text="Size"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="ltRemainAmout" runat="server" Text="Amount"></asp:Literal>
                            </th>
                            <th class="Ls-line">
                                <asp:Literal ID="ltUtilFee" runat="server" Text="Details"></asp:Literal>
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
                        <col width="60" />
                        <col width="150" />
                        <col width="70" />
                        <col width="210" />
                        <col width="110" />
                        <col width="110" />
                        <col width="130" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltInsSeq" runat="server" Text='<% #Eval("IVN_ID")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltIvtNm" runat="server" Text='<% #Eval("Item_Name")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltIvtLocation" runat="server" Text='<% #Eval("Item_LC_Area")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <div class="zoom" style='width=60px;height=60px; background-image:url(<%# Eval("Item_Photo") %>)'></div>
                            <asp:Image ID="imgIvtImage" runat="server"  style="width:60px;height:60px;" ImageUrl='<%# Eval("Item_Photo").ToString() %>'></asp:Image>
                        </td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltIvtSize" runat="server" Text='<% #Eval("Item_Size_W")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:Literal ID="ltIvtAmout" runat="server" Text='<% #Eval("Item_Amout")%>'></asp:Literal></td>
                        <td class="TbTxtCenter">
                            <asp:LinkButton runat="server" ID="lnkDetails" PostBackUrl='<%# "~/Inventory/ITEMS/InventoryDetails.aspx?ID="+Eval("IVN_ID").ToString()%>'> Details </asp:LinkButton></td>
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
            <asp:HiddenField ID="hfCurrentPage" runat="server" />
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="TypeA">
        <colgroup>
            <col width="60" />
            <col width="150" />
            <col width="70" />
            <col width="210" />
            <col width="110" />
            <col width="110" />
            <col width="130" />
        </colgroup>
        <tr>
            <td colspan="5"></td>

            <td>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkAddCategory" runat="server" Text="Add New Category" OnClick="lnkAddCategory_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="Btn-Type3-wp ">
                    <div class="Btn-Tp3-L">
                        <div class="Btn-Tp3-R">
                            <div class="Btn-Tp3-M">
                                <span>
                                    <asp:LinkButton ID="lnkBtnAdd" runat="server" Text="Add New" OnClick="lnkBtnAdd_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>

            </td>
        </tr>
    </table>

</asp:Content>
