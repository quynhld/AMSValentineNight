﻿<%@ Page Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="GoodsOrderList.aspx.cs" Inherits="KN.Web.Stock.Order.GoodsOrderList" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" runat="server">
<script language="javascript" type="text/javascript">
    <!--//
        function fnMovePage(intPageNo) 
        {
            if (intPageNo == null) 
            {
                intPageNo = 1;
            }
            
            document.getElementById("<%=hfCurrentPage.ClientID%>").value = intPageNo;
            <%=Page.GetPostBackEventReference(imgbtnPageMove)%>;
        }
        
        function fnDetailView(strGoodsOrderSeq)
        {
            document.getElementById("<%=hfGoodsOrderSeq.ClientID%>").value = strGoodsOrderSeq;
            
            <%=Page.GetPostBackEventReference(imgbtnDetailView)%>;
            return false;
        }
    //-->
    </script>
	<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="ddlRentCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="ddlGrpCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="ddlMainCd" EventName="SelectedIndexChanged"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
            <fieldset class="sh-field5 MrgB10">
	            <legend>검색</legend>
	            <ul class="sf5-ag MrgL30 ">
	                <li><asp:Literal ID="ltSearch" runat="server"></asp:Literal></li>
	                <li><asp:DropDownList ID="ddlRentCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRentCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlSvcZoneCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSvcZoneCd_SelectedIndexChanged"></asp:DropDownList></li>
				</ul>
	            <ul class="sf5-ag MrgL30 ">
					<li><asp:DropDownList ID="ddlGrpCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrpCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlMainCd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCd_SelectedIndexChanged"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlSubCd" runat="server"></asp:DropDownList></li>
				</ul>
				 <ul class="sf5-ag MrgL30 bgimgN">
	                <li><asp:DropDownList ID="ddlProcessCd" runat="server"></asp:DropDownList></li>
	                <li><asp:DropDownList ID="ddlStatusCd" runat="server"></asp:DropDownList></li>
		            <li><asp:Literal ID="ltCdNm" runat="server"></asp:Literal></li>
		            <li><asp:TextBox ID="txtCdNm" runat="server" Width="60px" MaxLength="255" CssClass="sh-input"></asp:TextBox></li>
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
			            <div class="Btn-Type4-wp">
				            <div class="Btn-Tp4-L">
					            <div class="Btn-Tp4-R">
						            <div class="Btn-Tp4-M">
							            <span><asp:LinkButton ID="lnkbtnReset" runat="server" OnClick="lnkbtnReset_Click"></asp:LinkButton></span>
						            </div>
					            </div>
				            </div>
			            </div>
		            </li>
	            </ul>
            </fieldset>
	    </ContentTemplate>
	</asp:UpdatePanel>
	<asp:UpdatePanel ID="upReqList" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        </Triggers>
        <ContentTemplate>
            <asp:ListView ID="lvGoodsOrderList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" 
                OnLayoutCreated="lvGoodsOrderList_LayoutCreated" OnItemDataBound="lvGoodsOrderList_ItemDataBound" OnItemCreated="lvGoodsOrderList_ItemCreated">
                <LayoutTemplate>
                    <table class="TbCel-Type6-C">
	                    <tr>
		                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltDpt" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltUnitSellingPrice" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltProcessNm" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltStateNm" runat="server"></asp:Literal></th>
	                    </tr>
	                    <tr runat="server" id="iphItemplaceHolderId"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
	                <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick='javascript:fnDetailView("<%#Eval("OrderSeq")%>");'>
			            <td><asp:Literal ID="ltInsSeq" runat="server"></asp:Literal></td>
			            <td class="Bd-Lt"><asp:Literal ID="ltInsDpt" runat="server"></asp:Literal></td>
			            <td class="Bd-Lt"><asp:Literal ID="ltInsComp" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsItemNm" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsUnitSellingPrice" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsAmount" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsProcessNm" runat="server"></asp:Literal></td>
		                <td class="Bd-Lt"><asp:Literal ID="ltInsStateNm" runat="server"></asp:Literal></td>
	                </tr>        
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="TbCel-Type6-C">
	                    <tr>
		                    <th><asp:Literal ID="ltSeq" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltDpt" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltComp" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltItemNm" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltUnitSellingPrice" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltAmount" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltProcessNm" runat="server"></asp:Literal></th>
		                    <th class="Bd-Lt"><asp:Literal ID="ltStateNm" runat="server"></asp:Literal> </th>
	                    </tr>
	                    <tr>
	                        <td colspan="9" align="center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
	                    </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <div class="Clear">
                    <span id="spanPageNavi" runat="server" style="width:100%;display:block;" class="Clear"></span>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="Btn-Type2-wp FloatR">
        <div class="Btn-Tp2-L">
            <div class="Btn-Tp2-R">
                <div class="Btn-Tp2-M">
                    <span><asp:LinkButton ID="lnkbtnRegist" runat="server" OnClick="lnkbtnRegist_Click"></asp:LinkButton></span>
                </div>
            </div>
        </div>
    </div>	  
    <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnPageMove_Click"/>
    <asp:ImageButton ID="imgbtnDetailView" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnDetailView_Click"/>
	<asp:UpdatePanel ID="upHfPanel" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="lnkbtnReset" EventName="Click"/>
	        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
	    </Triggers>
	    <ContentTemplate>
            <asp:HiddenField ID="hfCurrentPage" runat="server"/>
            <asp:HiddenField ID="hfGoodsOrderSeq" runat="server"/>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>