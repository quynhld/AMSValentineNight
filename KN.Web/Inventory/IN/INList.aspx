<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="INList.aspx.cs" Inherits="KN.Web.Inventory.INList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            
        </Triggers>
        <ContentTemplate>
            <fieldset class="sh-field2 MrgB10">
                <ul class="sf2-ag MrgL10">
                    <li><asp:Literal ID="ltSearchName" runat="server"></asp:Literal></li>
                    <li><asp:TextBox ID="txtSearchNm" runat="server" Width="60px" MaxLength="20" CssClass="sh-input" ></asp:TextBox></li>
	                <li><asp:Literal ID="ltSearchRoom" runat="server"></asp:Literal></li>
	                <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input" ></asp:TextBox></li>
	                <li>
	                    <div class="C235-st FloatL">
	                        <asp:DropDownList ID="ddlRentNm" runat="server" ></asp:DropDownList>
                        </div>
	                </li>
	                <li>
		            <div class="Btn-Type4-wp">
                        <div class="Btn-Tp4-L">
                            <div class="Btn-Tp4-R">
                                <div class="Btn-Tp4-M"><span><asp:LinkButton ID="lnkbtnSearch" runat="server" ></asp:LinkButton></span></div>
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
                    <col width="60"/>
                    <col width="70"/>
                    <col width="110"/>
                    <col width="210"/>
                    <col width="130"/>
                    <col width="130"/>
                    <col width="130"/>
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
            <asp:ListView ID="lvPaymentList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" >
                <LayoutTemplate>
                    <table class="TypeA">
                        <col width="60"/>
                        <col width="70"/>
                        <col width="110"/>
                        <col width="210"/>
                        <col width="130"/>
                        <col width="130"/>
                        <col width="130"/>
                        <tbody><tr id="iphItemPlaceHolderID" runat="server"></tr></tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <td class="TbTxtCenter"><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentNm" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRoomNo" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsName" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsMngFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsRentalFee" runat="server"></asp:Literal></td>
			            <td class="TbTxtCenter"><asp:Literal ID="ltInsUtilFee" runat="server"></asp:Literal></td>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                        <tr>
                            <td colspan="5" style="text-align:center"><asp:LinkButton ID="lnkAddNewRow" runat="server"></asp:LinkButton></td>
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
                        <table class="TypeA">
                        <col width="60"/>
                        <col width="70"/>
                        <col width="110"/>
                        <col width="210"/>
                        <col width="130"/>
                        <col width="130"/>
                        <col width="130"/>
                        <tbody>
                            <tr>
                                <td></td>
                                <td><asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" /></td>
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
                                           <asp:LinkButton ID="lnkAddNew" runat="server" onClick="lnkAddNew_Click" Text="Add New"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
</asp:Content>