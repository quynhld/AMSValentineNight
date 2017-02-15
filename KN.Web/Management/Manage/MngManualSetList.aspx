<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngManualSetList.aspx.cs" Inherits="KN.Web.Management.Manage.MngManualSetList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
    <script language="javascript" type="text/javascript">
    <!--//
        function fnMakingLine(strTxt)
        {
            var strItems = document.getElementById("<%=ddlItems.ClientID%>");
            var strYear = document.getElementById("<%=ddlYear.ClientID%>");
            var strMonth = document.getElementById("<%=ddlMonth.ClientID%>");

            if (trim(strItems.value) != "")
            {

                if (trim(strYear.value) != "")
                {
                    if (trim(strMonth.value) != "")
                    {
                        return true;
                    }
                    else
                    {
                        alert(strTxt);
                        strMonth.focus();

                        return false;
                    }
                }
                else
                {
                    alert(strTxt);
                    strYear.focus();

                    return false;
                }
            }
            else
            {
                alert(strTxt);
                strItems.focus();
                
                return false;
            }            
        }
    //-->
    </script>
    <fieldset class="sh-field2">
        <legend>검색</legend>
        <ul class="sf2-ag MrgL10 ">
            <li><asp:Literal ID="ltItem" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlItems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged"></asp:DropDownList></li>
            <li><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList></li>
            <li><asp:Literal ID="ltYear" runat="server"></asp:Literal></li>
            <li><asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></li>
            <li><asp:Literal ID="ltMonth" runat="server"></asp:Literal></li><li>
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
            <li>
                <div class="Btn-Type4-wp ">
                    <div class="Btn-Tp-L">
                        <div class="Btn-Tp-R">
                            <div class="Btn-Tp-M">
                                <span><asp:LinkButton ID="lnkbtnMakeLine" runat="server" OnClick="lnkbtnMakeLine_Click"></asp:LinkButton></span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </fieldset>
    <table cellspacing="0" class="TbCel-Type4-A">
        <col width="200px" />
        <col width="184px" />
        <col width="184px" />
        <col width="138px" />
        <col width="138px" />
        <tr>
            <th align="center"><asp:Literal ID="ltTopPayment" runat="server"></asp:Literal></th>
            <th class="Bd-Lt" align="center"><asp:Literal ID="ltTopMngYYYYMM" runat="server"></asp:Literal></th>
            <th class="Bd-Lt" align="center"><asp:Literal ID="ltTopRoomNo" runat="server"></asp:Literal></th>
            <th class="Bd-Lt" align="center"><asp:Literal ID="ltTopMovieInDt" runat="server"></asp:Literal></th>
            <th class="Bd-Lt" align="center"><asp:Literal ID="ltTopAmt" runat="server"></asp:Literal></th>
        </tr>
    </table>
    <div style="overflow-y: scroll; height: 150px; width: 840px;">
        <asp:ListView ID="lvMngManualList" runat="server" ItemPlaceholderID="iphd" OnItemCreated="lvMngManualList_ItemCreated" OnItemDataBound="lvMngManualList_ItemDataBound" OnLayoutCreated="lvMngManualList_LayoutCreated">
            <LayoutTemplate>
                <table cellspacing="0" width="818px">
                    <col width="200px" />
                    <col width="184px" />
                    <col width="184px" />
                    <col width="138px" />
                    <col width="112px" />
                    <tr id="iphd" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center"><asp:DropDownList ID="ddlItem" runat="server" Enabled="false"></asp:DropDownList></td>
                    <td class="Bd-Lt" align="center"><asp:Literal ID="ltMngYYYYMM" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt" align="center"><asp:Literal ID="ltRoomNo" runat="server" ></asp:Literal></td>
                    <td class="Bd-Lt" align="center"><asp:Literal ID="ltMoveInDt" runat="server"></asp:Literal></td>
                    <td class="Bd-Lt" align="center"><asp:Literal ID="ltAmt" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table cellspacing="0" width="818px">
                    <col width="200px" />
                    <col width="184px" />
                    <col width="184px" />
                    <col width="138px" />
                    <col width="112px" />
                    <tr>
                        <td colspan="5" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>