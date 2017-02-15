<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngGraphYear.aspx.cs" Inherits="KN.Web.Management.Remote.MngGraphYear" ValidateRequest="false"%>
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
	    <li><asp:LinkButton ID="lnkbtnDay" runat="server" onclick="lnkbtnDay_Click"></asp:LinkButton></li>
	    <li><asp:LinkButton ID="lnkbtnMonth" runat="server" onclick="lnkbtnMonth_Click"></asp:LinkButton></li>
	    <li class="Over CursorNon"><asp:Literal ID="ltYear" runat="server"></asp:Literal></li>
    </ul>
</div>
<fieldset class="sh-field4 ">
    <legend>검색</legend>
    <ul class="sf4-ag MrgL140">
	    <li><asp:TextBox ID="txtSearchFloor" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	    <li><asp:Literal ID="ltFloor" runat="server"></asp:Literal></li>
	    <li><asp:TextBox ID="txtSearchRoom" runat="server" Width="60px" MaxLength="8" CssClass="sh-input"></asp:TextBox></li>
	    <li><asp:Literal ID="ltRoom" runat="server"></asp:Literal></li>
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
    </ul>
</fieldset>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRecompile" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <div class="MrgT10"><cc1:ZedGraphWeb ID="zgwGraph" runat="server" Width="840" Height="220"></cc1:ZedGraphWeb></div>
        <ul class="Cal-Tinfo MrgB10">
	        <li class="Ct1"><asp:Literal ID="ltAmountUsed" runat="server"></asp:Literal><span></span></li>
	        <li class="Ct2"><asp:Literal ID="ltCharge" runat="server"></asp:Literal><span></span></li>
        </ul>
        <asp:ListView ID="lvMngMonthReadView" runat="server" GroupPlaceholderID="groupPlaceHolderID" GroupItemCount="5" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvMngMonthReadView_ItemDataBound">
            <LayoutTemplate>
                <table class="Cal-yr MrgT10">             
                    <tr id="groupPlaceHolderID" runat="server"></tr>                             
                </table>
            </LayoutTemplate>        
            <GroupTemplate>
                <tr>
                    <td id="iphItemPlaceHolderID" runat="server"></td>
                </tr>
            </GroupTemplate>        
            <ItemTemplate>
                <td>
                    <table>
                        <tr>
                            <th>
                                <asp:Literal ID="ltYearVal" runat="server"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <dl class="Cov">
					                <dt>&nbsp;</dt>
					                <dd class="GraIcB"><asp:Literal ID="ltTotalUse" runat="server"></asp:Literal>(㎥)</dd>
					                <dd class="GraIcR"><asp:Literal ID="ltTotalCharge" runat="server"></asp:Literal>(VND)</dd>
				                </dl>
                            </td>
                        </tr>			        
                    </table>
                </td>
            </ItemTemplate>
            <EmptyItemTemplate>
                <td>
                    <table>
                        <tr>
                            <th></th>
                        </tr>
                        <tr>
                            <td>
                                <dl class="Cov">
					                <dt>&nbsp;</dt>
					                <dd class="GraIcB"></dd>
					                <dd class="GraIcR"></dd>
				                </dl>
                            </td>
                        </tr>			        
                    </table>
                </td>
            </EmptyItemTemplate>           
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:ImageButton ID="imgbtnRecompile" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnRecompile_Click"/>
<asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
</asp:Content>