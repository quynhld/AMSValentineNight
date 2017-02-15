<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="RequestMonthList.aspx.cs" Inherits="KN.Web.Management.Manage.RequestMonthList"  ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
<!--//
    function fnCheckRentCd(strTxt) 
    {
        var strRentCd = document.getElementById("<%=ddlSearchRentCd.ClientID%>");

        if (strRentCd.value == "") 
        {
            strRentCd.focus();
            alert(strTxt);
            return false;
        }
    }
//-->
</script>
<asp:UpdatePanel ID="upSearchList" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlSearchRentCd" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <fieldset class="sh-field2">
	        <legend>검색</legend>
	        <ul class="sf2-ag MrgL10 ">
	            <li><asp:DropDownList ID="ddlSearchRentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchRentCd_SelectedIndexChanged"></asp:DropDownList></li>
	            <li><asp:DropDownList ID="ddlSearchYear" runat="server"></asp:DropDownList></li>
	            <li><asp:DropDownList ID="ddlSearchMonth" runat="server"></asp:DropDownList></li>
	            <li><asp:CheckBox ID="chkDirectlyInput" runat="server" TextAlign="Left" AutoPostBack="true" OnCheckedChanged="chkDirectlyInput_CheckedChanged"/></li>
	            <li>
			        <div class="Btn-Type4-wp">
				        <div class="Btn-Tp4-L">
					        <div class="Btn-Tp4-R">
						        <div class="Btn-Tp4-M">
							        <span><asp:LinkButton ID="lnkbtnMakeList" runat="server" OnClick="lnkbtnMakeList_Click"></asp:LinkButton></span>
						        </div>
					        </div>
				        </div>
			        </div>
	            </li>
            </ul>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="upMakeList" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlSearchRentCd" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="chkDirectlyInput" EventName="CheckedChanged"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnMakeList" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnRegist" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvRequestMonthList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemCreated="lvRequestMonthList_ItemCreated"
            OnLayoutCreated="lvRequestMonthList_LayoutCreated" OnItemDataBound="lvRequestMonthList_ItemDataBound">
            <LayoutTemplate>
                <table class="TbCel-Type5-C" cellpadding="0">
                    <col width="10%"/>
                    <col width="30%"/>
                    <col width="30%"/>
                    <col width="30%"/>
                    <thead>
	                    <tr>
		                    <th class="Fr-line"><span><asp:Literal ID="ltTopMonth" runat="server"></asp:Literal></span></th>
		                    <th><span><asp:Literal ID="ltTopMonthCnt" runat="server"></asp:Literal></span></th>
		                    <th><span><asp:Literal ID="ltTopStartDt" runat="server"></asp:Literal></span></th>
		                    <th class="Ls-line"><span><asp:Literal ID="ltTopStartMonth" runat="server"></asp:Literal></span></th>
	                    </tr>
                    </thead>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center"><asp:Literal ID="ltMonth" runat="server"></asp:Literal></td>
                    <td align="center">
                        <asp:DropDownList ID="ddlMonthCnt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthCnt_OnSelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox ID="txtHfOrgMonthCnt" runat="server" Visible="false" Width="30"></asp:TextBox>
                        <asp:TextBox ID="txtHfChgMonthCnt" runat="server" Visible="false" Width="30"></asp:TextBox>
                        <asp:TextBox ID="txtHfMonthCnt" runat="server" Visible="false" Width="30"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txtStartDt" runat="server" CssClass="bgType2" ReadOnly="true" Width="70"></asp:TextBox>
                        <asp:Literal ID="ltCalendarImg" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hfStartDt" runat="server"/>
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="ddlStartMonth" runat="server"></asp:DropDownList>&nbsp;/&nbsp;<asp:DropDownList ID="ddlRealStartMonth" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TbCel-Type4-A">
                    <col width="10%"/>
                    <col width="30%"/>
                    <col width="30%"/>
                    <col width="30%"/>
                    <thead>
	                    <tr>
		                    <th class="Fr-line"><span><asp:Literal ID="ltTopMonth" runat="server"></asp:Literal></span></th>
		                    <th><span><asp:Literal ID="ltTopMonthCnt" runat="server"></asp:Literal></span></th>
		                    <th><span><asp:Literal ID="ltTopStartDt" runat="server"></asp:Literal></span></th>
		                    <th class="Ls-line"><span><asp:Literal ID="ltTopStartMonth" runat="server"></asp:Literal></span></th>
	                    </tr>
                    </thead>
                    <tbody>
                        <td colspan="4" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tbody>                
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
	    <div class="Btwps FloatR">
	        <div class="Btn-Type3-wp ">
		        <div class="Btn-Tp3-L">
			        <div class="Btn-Tp3-R">
				        <div class="Btn-Tp3-M">
					        <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>