<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngReadDayForTower.aspx.cs" Inherits="KN.Web.Management.Remote.MngReadDayForTower" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
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
        <col width="12%" />
        <col width="12%" />
        <col width="12%" />
        <col width="12%" />
        <col width="12%" />
        <col width="14%" />
        <col width="12%" />
        <col width="14%" />
        <tr>
            <th><asp:Literal ID="ltTopFloorRoom" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopDay" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopGenUsed" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopPeakUsed" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopNightUsed" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopMngFeeNET" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopMngFeeVAT" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltTopCharge" runat="server"></asp:Literal></th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvDayChargeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID"
                OnItemDataBound="lvDayChargeList_ItemDataBound" OnItemCreated="lvDayChargeList_ItemCreated">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="12%" />
                        <col width="12%" />
                        <col width="12%" />
                        <col width="12%" />
                        <col width="12%" />
                        <col width="14%" />
                        <col width="12%" />
                        <col width="14%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0"><asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltDayList" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltGenUsed" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltPeakUsed" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltNightUsed" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltInsMngFeeNET" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltInsMngFeeVAT" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltCharge" runat="server"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="8" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div>
                <span id="spanPageNavi" runat="server" style="width: 100%"></span>
            </div>
            <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click" />
            <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click" />
            <asp:HiddenField ID="hfCurrentPage" runat="server" />
            <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="hfVatRation" runat="server" />
            <asp:TextBox ID="txtHfYYYYMM" runat="server" Visible="false"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>