<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="ChargeSettingListForTower.aspx.cs" Inherits="KN.Web.Management.Remote.ChargeSettingListForTower" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">
    <!--//
        function fnFirstCheckValidate(strText)
        {
            var strGenCharge = document.getElementById("<%=txtFirstGenCharge.ClientID%>");
            var strPeakCharge = document.getElementById("<%=txtFirstPeakCharge.ClientID%>");
            var strNightCharge = document.getElementById("<%=txtFirstNightCharge.ClientID%>");
            var strStartDt = document.getElementById("<%=txtFirstStartDt.ClientID%>");

            if (trim(strGenCharge.value) == "")
            {
                strGenCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strPeakCharge.value) == "")
            {
                strPeakCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strNightCharge.value) == "")
            {
                strNightCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strStartDt.value) == "")
            {
                alert(strText);
                return false;
            }

            return true;
        }

        function fnSecondCheckValidate(strText)
        {
            var strDdlRoomNo = document.getElementById("<%=ddlRoomNo.ClientID%>");
            var strGenCharge = document.getElementById("<%=txtSecondGenCharge.ClientID%>");
            var strPeakCharge = document.getElementById("<%=txtSecondPeakCharge.ClientID%>");
            var strNightCharge = document.getElementById("<%=txtSecondNightCharge.ClientID%>");
            var strStartDt = document.getElementById("<%=txtSecondStartDt.ClientID%>");

            if (trim(strDdlRoomNo.value) == "")
            {
                strDdlRoomNo.focus();
                alert(strText);
                return false;
            }

            if (trim(strGenCharge.value) == "")
            {
                strGenCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strPeakCharge.value) == "")
            {
                strPeakCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strNightCharge.value) == "")
            {
                strNightCharge.focus();
                alert(strText);
                return false;
            }

            if (trim(strStartDt.value) == "")
            {
                alert(strText);
                return false;
            }

            return true;
        }
    //-->
    </script>
    <div class="FloatR2">
        (<asp:Literal ID="ltFirstBaseRate" runat="server"></asp:Literal>&nbsp;:&nbsp;<asp:Literal ID="ltRealBaseRate" runat="server"></asp:Literal><asp:HiddenField ID="hfRealBaseRate" runat="server" />)
    </div>
    <asp:Literal ID="ltCommon" runat="server"></asp:Literal>
    <table class="TbCel-Type6-A">
        <col width="16%" />
        <col width="19%" />
        <col width="19%" />
        <col width="19%" />
        <col width="19%" />
        <col width="8%" />
        <tr>
            <th><asp:Literal ID="ltFirstSeq" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltFirstGenCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltFirstPeakCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltFirstNightCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltFirstStartDt" runat="server"></asp:Literal></th>
            <th>&nbsp;</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upRemoteForTower" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgbtnRegist" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvChargeInfoList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvChargeInfoList_ItemDataBound" OnItemCreated="lvChargeInfoList_ItemCreated" OnItemDeleting="lvChargeInfoList_ItemDeleting" OnItemUpdating="lvChargeInfoList_ItemUpdating">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="16%" />
                        <col width="19%" />
                        <col width="19%" />
                        <col width="19%" />
                        <col width="19%" />
                        <col width="8%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtGenCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtPeakCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtNightCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="90"></asp:TextBox>
                            <asp:Literal ID="ltStartDt" runat="server" Visible="false"></asp:Literal>
                            <asp:HiddenField ID="hfStartDt" runat="server" />
                            <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtHfChargeSeq" runat="server" Visible="false"></asp:TextBox>
                            <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                            <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="6" style="text-align: center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <table class="TbCel-Type6-A">
                <colgroup>
                    <col width="16%" />
                    <col width="19%" />
                    <col width="19%" />
                    <col width="19%" />
                    <col width="19%" />
                    <col width="8%" />
                    <tbody>
                        <tr>
                            <td align="center" class="P0">&nbsp;</td>
                            <td align="center" class="P0"><asp:TextBox ID="txtFirstGenCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td align="center" class="P0"><asp:TextBox ID="txtFirstPeakCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td align="center" class="P0"><asp:TextBox ID="txtFirstNightCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtFirstStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                                <img alt="Calendar" onclick="Calendar(this, '<%=txtFirstStartDt.ClientID%>', '<%=hfFirstStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfFirstStartDt" runat="server" />
                                <asp:TextBox ID="txtHfFirstOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <span><asp:ImageButton ID="imgbtnRegist" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnRegist_Click" /></span>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="hfAlertText" runat="server" />
    <br />
    <asp:Literal ID="ltIndividual" runat="server"></asp:Literal>
    <table class="TbCel-Type6-A">
        <col width="16%" />
        <col width="16%" />
        <col width="15%" />
        <col width="15%" />
        <col width="15%" />
        <col width="15%" />
        <col width="8%" />
        <tr>
            <th><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSecondSeq" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSecondGenCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSecondPeakCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSecondNightCharge" runat="server"></asp:Literal></th>
            <th><asp:Literal ID="ltSecondStartDt" runat="server"></asp:Literal></th>
            <th>&nbsp;</th>
        </tr>
    </table>
    <asp:UpdatePanel ID="upRoomList" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvChargelistForRoom" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvChargelistForRoom_ItemDataBound" OnItemCreated="lvChargelistForRoom_ItemCreated" OnItemDeleting="lvChargelistForRoom_ItemDeleting" OnItemUpdating="lvChargelistForRoom_ItemUpdating">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="16%" />
                        <col width="16%" />
                        <col width="15%" />
                        <col width="15%" />
                        <col width="15%" />
                        <col width="15%" />
                        <col width="8%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0"><asp:Literal ID="ltRoomNo" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:Literal ID="ltSeq" runat="server"></asp:Literal></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtGenCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtPeakCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0"><asp:TextBox ID="txtNightCharge" runat="server" CssClass="bgType2" Width="50" MaxLength="18"></asp:TextBox></td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="90"></asp:TextBox>
                            <asp:Literal ID="ltStartDt" runat="server" Visible="false"></asp:Literal>
                            <asp:HiddenField ID="hfStartDt" runat="server" />
                            <asp:TextBox ID="txtHfOriginDt" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtHfChargeSeq" runat="server" Visible="false"></asp:TextBox>
                            <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif" /></span>
                            <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif" /></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                            <tr>
                                <td colspan="6" style="text-align: center">
                                    <asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <table class="TbCel-Type6-A">
                <colgroup>
                    <col width="16%" />
                    <col width="16%" />
                    <col width="15%" />
                    <col width="15%" />
                    <col width="15%" />
                    <col width="15%" />
                    <col width="8%" />
                    <tbody>
                        <tr>
                            <td align="center" class="P0"><asp:DropDownList ID="ddlRoomNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoomNo_SelectedIndexChanged"></asp:DropDownList></td>
                            <td align="center" class="P0">&nbsp;</td>
                            <td align="center" class="P0"><asp:TextBox ID="txtSecondGenCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td align="center" class="P0"><asp:TextBox ID="txtSecondPeakCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td align="center" class="P0"><asp:TextBox ID="txtSecondNightCharge" runat="server" CssClass="bgType2" MaxLength="18" Width="50"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtSecondStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                                <img alt="Calendar" onclick="Calendar(this, '<%=txtSecondStartDt.ClientID%>', '<%=hfSecondStartDt.ClientID%>', true)" src="/Common/Images/Common/calendar.gif" style="cursor: pointer;" value="" />
                                <asp:HiddenField ID="hfSecondStartDt" runat="server" />
                                <asp:TextBox ID="txtHfSecondOriginDt" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <span><asp:ImageButton ID="imgbtnInput" runat="server" ImageUrl="~/Common/Images/Icon/plus.gif" OnClick="imgbtnInput_Click" /></span>
                            </td>
                        </tr>
                    </tbody>
                </colgroup>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
    <!--//
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    //-->
    </script>
</asp:Content>