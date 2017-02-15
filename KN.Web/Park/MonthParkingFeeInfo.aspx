<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MonthParkingFeeInfo.aspx.cs" Inherits="KN.Web.Park.MonthParkingFeeInfo"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>		
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">
    </script>
    <asp:UpdatePanel ID="upListPanel" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRentNm" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkSamePrev" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="C235-st FloatL">
                <asp:DropDownList ID="ddlRentNm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectChanged"></asp:DropDownList>
                <asp:CheckBox ID="chkSamePrev" runat="server" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="chkSamePrev_CheckedChanged"/>
            </div>
	        <table class="TbCel-Type3-A">
                <col width="25%"/>
                <col width="25%"/>
                <col width="25%"/>
                <col width="25%"/>
		        <tr>
			        <th><asp:Literal ID="ltApplyDt" runat="server"></asp:Literal></th>
			        <th class="Bd-Lt"><asp:Literal ID="ltKind" runat="server"></asp:Literal></th>
			        <th class="Bd-Lt"><asp:Literal ID="ltFee" runat="server"></asp:Literal></th>
			        <th class="Bd-Lt">&nbsp;</th>		
		        </tr>
	        </table>
	        <asp:ListView ID="lvMonthParkingFeeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnLayoutCreated="lvMonthParkingFeeList_LayoutCreated"
	        OnItemDataBound="lvMonthParkingFeeList_ItemDataBound" OnItemCreated="lvMonthParkingFeeList_ItemCreated" OnItemDeleting="lvMonthParkingFeeList_ItemDeleting"
	        OnItemUpdating="lvMonthParkingFeeList_ItemUpdating">
                <LayoutTemplate>
                    <table class="TbCel-Type4-A">
                        <col width="25%" />
                        <col width="25%" />
		                <col width="25%" />
		                <col width="25%" />
                        <tbody>
                            <tr id="iphItemPlaceHolderID" runat="server"></tr>
                        </tbody>                
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="P0">
                            <asp:TextBox ID="txtApplyDt" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
	                        <asp:Literal ID="ltCalendar" runat="server"></asp:Literal>
	                        <asp:HiddenField ID="hfApplyDt" runat="server"/>
                        </td>
                        <td align="center" class="P0 Bd-Lt">
                            <asp:DropDownList ID="ddlCarTy" runat="server" Enabled="false"></asp:DropDownList>
                            <asp:TextBox ID="txtHfCarTy" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" class="P0 Bd-Lt">
                            <asp:TextBox ID="txtFee" runat="server" CssClass="bgType2" MaxLength="13"></asp:TextBox>
                            <asp:Literal ID="ltDong" runat="server" ></asp:Literal>
                        </td>
                        <td align="center" class="P0 Bd-Lt">
                            <span><asp:ImageButton ID="imgbtnModify" CommandName="Update" runat="server" ImageUrl="~/Common/Images/Icon/edit.gif"/></span>
                            <span><asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Common/Images/Icon/Trash.gif"/></span>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TypeA">
                        <tbody>
                        <tr>
                            <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                        </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>        
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upButtonPanel" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRentNm" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkSamePrev" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="Btwps FloatR">
                <div class="Btn-Type2-wp FloatL">
                    <div class="Btn-Tp2-L">
	                    <div class="Btn-Tp2-R">
		                    <div class="Btn-Tp2-M">
			                    <span><asp:LinkButton ID="lnkbtnRegist" runat="server" onclick="lnkbtnRegist_Click"></asp:LinkButton></span>
		                    </div>
	                    </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfAlertText" runat="server"/>
    <script language="javascript" type="text/javascript">
        var strAlertText = document.getElementById("<%=hfAlertText.ClientID%>");

        if (trim(strAlertText.value) != "")
        {
            alert(strAlertText.value);
            document.getElementById("<%=hfAlertText.ClientID%>").value = "";
        }
    </script>
</asp:Content>