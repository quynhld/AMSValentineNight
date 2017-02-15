<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngGraphDay.aspx.cs" Inherits="KN.Web.Management.Remote.MngGraphDay" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<%@ Register TagPrefix="cc1" Namespace="ZedGraph.Web" Assembly="ZedGraph.Web" %>
<asp:Content ID="ctCont" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    function fnRecompile()
    {
        document.getElementById("<%=imgbtnRecompile.ClientID%>").click();
    }
</script>
<%--<ul>
    <li>
        <asp:Literal ID="ltEnergyWarning" runat="server"></asp:Literal>&nbsp;<asp:ImageButton ID="imgbtnCheckData" runat="server" BorderWidth="0" ImageUrl="~/Common/Images/Btn/Recomplie.png" ImageAlign="AbsMiddle" OnClientClick="javascript:return fnRecompile();"/>
    </li>
</ul>--%>
<div class="Tab-wp MrgB10">
    <ul class="TabM">
	    <li class="Over CursorNon"><asp:Literal ID="ltTabDay" runat="server"></asp:Literal></li>
	    <li><asp:LinkButton ID="lnkbtnMonth" runat="server" onclick="lnkbtnMonth_Click"></asp:LinkButton></li>
	    <li><asp:LinkButton ID="lnkbtnYear" runat="server" onclick="lnkbtnYear_Click"></asp:LinkButton></li>
    </ul>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlSearchFloor" EventName="SelectedIndexChanged"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRecompile" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <fieldset class="sh-field2 MrgB10">
        <legend>검색</legend>
            <ul class="sf2-ag MrgL10">
                <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
                <li><asp:DropDownList ID="ddlSearchFloor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchFloor_SelectedIndexChanged"></asp:DropDownList></li>
                <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
                <li><asp:DropDownList ID="ddlSearchRoom" runat="server"></asp:DropDownList></li>
                <li>
                    <div class="C235-st FloatL">
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </li>
                <li>
                    <div class="Btn-Type4-wp">
                        <div class="Btn-Tp4-L">
	                        <div class="Btn-Tp4-R">
		                        <div class="Btn-Tp4-M">
			                        <span><asp:LinkButton ID="lnkbtnSearch" runat="server" onclick="lnkbtnSearch_Click"></asp:LinkButton></span>
		                        </div>
	                        </div>
                        </div>
                    </div>		        
                </li>
		        <li>
			        <div class="Btn-Type1-wp Mrg0">
				        <div class="Btn-Tp-L">
					        <div class="Btn-Tp-R">
						        <div class="Btn-Tp-M">
							        <span><asp:LinkButton ID="lnkbtnTable" runat="server" onclick="lnkbtnTable_Click"></asp:LinkButton></span>
						        </div>
					        </div>
				        </div>
			        </div>
		        </li>
            </ul>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="upGraph" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRecompile" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <div><cc1:ZedGraphWeb ID="zgwGraph" runat="server" Width="840" Height="450"></cc1:ZedGraphWeb></div>
        <div class="Btwps FloatR2">
	        <div class="Btn-Type2-wp FloatL">
		        <div class="Btn-Tp2-L">
			        <div class="Btn-Tp2-R">
				        <div class="Btn-Tp2-M">
					        <span><asp:LinkButton ID="lnkbtnModify" runat="server" onclick="lnkbtnModify_Click"></asp:LinkButton></span>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <asp:ImageButton ID="imgbtnRecompile" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnRecompile_Click"/>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfEnergyYear" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfEnergyMonth" runat="server" Visible="false"></asp:TextBox>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>