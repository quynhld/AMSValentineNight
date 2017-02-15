<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngModifyDayForTower.aspx.cs" Inherits="KN.Web.Management.Remote.MngModifyDayForTower" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
        function fnMovePage(intPageNo)
        {
            if (intPageNo == null)
            {
                intPageNo = 1;
            }

            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }

        function fnCheckValidate(strAlert)
        {
            var strSearchFloor = document.getElementById("<%=txtSearchFloor.ClientID%>");
            var strSearchRoom = document.getElementById("<%=txtSearchRoom.ClientID%>");
            var strYear = document.getElementById("<%=ddlYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");

            if (trim(strSearchFloor.value) == "" && trim(strSearchRoom.value) == "" && trim(strYear.value) == "" && trim(strMonth.value) == "")
            {
                alert(strAlert);
                return false;
            }

            return true;
        }
    </script>
    <div class="Tab-wp MrgB10">
        <ul class="TabM">
            <li class="Over CursorNon"><asp:Literal ID="ltTabDay" runat="server"></asp:Literal></li>
            <li><asp:LinkButton ID="lnkbtnMonth" runat="server" OnClick="lnkbtnMonth_Click"></asp:LinkButton></li>
        </ul>
    </div>
    <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10">
            <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
            <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
            <li>
                <div class="C235-st FloatL">
                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
                </div>
            </li>
            <li>
                <div class="Btn-Type4-wp">
                    <div class="Btn-Tp4-L">
                        <div class="Btn-Tp4-R">
                            <div class="Btn-Tp4-M">
                                <span><asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <table class="TbCel-Type6-A MrgT10 ">
        <col width="120px" />
        <col width="120px" />
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        <col width="100px" />
        <tr>
            <th rowspan="2"><asp:Literal ID="ltTopFloorRoom" runat="server"></asp:Literal></th>
            <th rowspan="2"><asp:Literal ID="ltTopYearMonth" runat="server"></asp:Literal></th>
            <th colspan="3"><asp:Literal ID="ltTopAccmulated" runat="server"></asp:Literal></th>
            <th colspan="3"><asp:Literal ID="ltTopAmounted" runat="server"></asp:Literal></th>
        </tr>
        <tr>
            <th><asp:Literal ID="ltTopGenVal" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopPeakVal" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopNightVal" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopGenUse" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopPeakUse" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopNightUse" runat="server"></asp:Literal></th>
        </tr>
    </table>
    <asp:ListView ID="lvMonthChargeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
        OnItemDataBound="lvMonthChargeList_ItemDataBound" OnItemCreated="lvMonthChargeList_ItemCreated">
        <LayoutTemplate>
            <table class="TbCel-Type4-A">
                <col width="120px" />
                <col width="120px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <tbody>
                    <tr id="iphItemPlaceHolderID" runat="server">
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td align="center" class="P0">
                    <asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfFloorNo" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtHfRoomNo" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td align="center" class="P0">
                    <asp:Literal ID="ltYearMonthList" runat="server"></asp:Literal>
                    <asp:TextBox ID="txtHfYearMonth" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td align="center" class="P0"><asp:TextBox ID="txtGenVal" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
                <td align="center" class="P0"><asp:TextBox ID="txtPeakVal" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
                <td align="center" class="P0"><asp:TextBox ID="txtNightVal" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
                <td align="center" class="P0"><asp:TextBox ID="txtGenUse" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
                <td align="center" class="P0"><asp:TextBox ID="txtPeakUse" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
                <td align="center" class="P0"><asp:TextBox ID="txtNightUse" runat="server" Width="90px" MaxLength="10"></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="TypeA">
                <tr>
                    <td colspan="8" style="text-align: center">
                        <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <div>
        <span id="spanPageNavi" runat="server" style="width: 100%"></span>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span><asp:LinkButton ID="lnkbtnModify" runat="server" OnClick="lnkbtnModify_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="Btwps FloatR">
        <div class="Btn-Type2-wp FloatL">
            <div class="Btn-Tp2-L">
                <div class="Btn-Tp2-R">
                    <div class="Btn-Tp2-M">
                        <span><asp:LinkButton ID="lnkbtnList" runat="server" OnClick="lnkbtnList_Click"></asp:LinkButton></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif"
        OnClick="imgbtnPageMove_Click" />
    <asp:HiddenField ID="hfCurrentPage" runat="server" />
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfVatRation" runat="server" />
    <asp:TextBox ID="txtHfYear" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfMonth" runat="server" Visible="false"></asp:TextBox>
</asp:Content>