<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Template/MainFrame.Master" AutoEventWireup="true" CodeBehind="MngReadMonth.aspx.cs" Inherits="KN.Web.Management.Remote.MngReadMonth" ValidateRequest="false"%>
<%@ MasterType VirtualPath="~/Common/Template/MainFrame.Master"%>
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

    function fnDetailView(strData2, strData3, strData4)
    {
        // Datum1 : 입주자번호
        // Datum2 : 섹션코드
        // Datum3 : 방번호
        // Datum4 : 거주년월(YYYYMM)
        <%=Page.GetPostBackEventReference(imgbtnDetailview)%>;

        window.open("/Common/RDPopup/RDPopupEnergyDetail.aspx?Datum1=&Datum2=" + strData2 + "&Datum3=" + strData3 + "&Datum4=" + strData4, "EnergyDetail", "status=no, resizable=no, width=730, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no");
        
        return false;
    }
    
    function fnRecompile()
    {
        document.getElementById("<%=imgbtnRecompile.ClientID%>").click();
    }
    
    function fnMakeExcelFile()
    {
        document.getElementById("<%=imgbtnMakeExcel.ClientID%>").click();
    }
</script>
<%--<ul>
    <li>
        <asp:Literal ID="ltEnergyWarning" runat="server"></asp:Literal>&nbsp;<asp:ImageButton ID="imgbtnCheckData" runat="server" BorderWidth="0" ImageUrl="~/Common/Images/Btn/Recomplie.png" ImageAlign="AbsMiddle" OnClientClick="javascript:return fnRecompile();"/>
    </li>
</ul>--%>
<div class="Tab-wp MrgB10">
    <ul class="TabM">
        <li ><asp:LinkButton ID="lnkbtnDay" runat="server" onclick="lnkbtnDay_Click"></asp:LinkButton></li>
        <li class="Over CursorNon"><asp:Literal ID="ltMonth" runat="server"></asp:Literal></li>
        <li><asp:LinkButton ID="lnkbtnYear" runat="server" onclick="lnkbtnYear_Click"></asp:LinkButton></li>
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
	    <li>
		    <div class="Btn-Type1-wp Mrg0">
			    <div class="Btn-Tp-L">
				    <div class="Btn-Tp-R">
					    <div class="Btn-Tp-M">
						    <span><asp:LinkButton ID="lnkbtnGraph" runat="server" OnClick="lnkbtnGraph_Click"></asp:LinkButton></span>
					    </div>
				    </div>
			    </div>
		    </div>
	    </li>
    </ul>
</fieldset>
<table class="TbCel-Type6-A MrgT10 ">
    <col width="20%"/>
    <col width="20%"/>
    <col width="20%"/>
    <col width="10%"/>
    <col width="10%"/>
    <col width="20%"/>
    <tr>
        <th><asp:Literal ID="ltFloorRoom" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltYearMonth" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltAmountUsed" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMngFeeNET" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltMngFeeVAT" runat="server"></asp:Literal></th>
        <th><asp:Literal ID="ltCharge" runat="server"></asp:Literal></th>        
    </tr>
</table>
<asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnSearch" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="lnkbtnGraph" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnPageMove" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnDetailview" EventName="Click"/>
        <asp:AsyncPostBackTrigger ControlID="imgbtnRecompile" EventName="Click"/>
    </Triggers>
    <ContentTemplate>
        <asp:ListView ID="lvDayChargeList" runat="server" ItemPlaceholderID="iphItemPlaceHolderID" OnItemDataBound="lvDayChargeList_ItemDataBound" OnItemCreated="lvDayChargeList_ItemCreated">
            <LayoutTemplate>
                <table class="TbCel-Type4-A">
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="20%"/>
                    <col width="10%"/>
                    <col width="10%"/>
                    <col width="20%"/>
                    <tbody>
                        <tr id="iphItemPlaceHolderID" runat="server"></tr>
                    </tbody>                
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="background-color:#FFFFFF;cursor:pointer;" onmouseover="this.style.backgroundColor='#E4EEF5'" onmouseout="this.style.backgroundColor='#FFFFFF'" onclick="javascript:return fnDetailView('<%#Eval("RentCd")%>','<%#Eval("RoomNo")%>','<%#Eval("EnergyMonth")%>');">
                    <td align="center" class="P0"><asp:Literal ID="ltFloorRoomList" runat="server"></asp:Literal></td>
                    <td align="center" class="P0"><asp:Literal ID="ltYearMonthList" runat="server"></asp:Literal></td>
                    <td align="center" class="P0"><asp:Literal ID="ltAmountUsedList" runat="server"></asp:Literal></td>
                    <td align="center" class="P0"><asp:Literal ID="ltInsMngFeeNET" runat="server"></asp:Literal></td>
                    <td align="center" class="P0"><asp:Literal ID="ltInsMngFeeVAT" runat="server"></asp:Literal></td>
                    <td align="center" class="P0"><asp:Literal ID="ltChargeList" runat="server"></asp:Literal></td>            
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="TypeA">
                    <tbody>
                    <tr>
                        <td colspan="6" style="text-align:center"><asp:Literal ID="ltINFO_HAS_NO_DATA" runat="server"></asp:Literal></td>
                    </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div>
            <span id="spanPageNavi" runat="server" style="width:100%"></span>
        </div>
        <asp:ImageButton ID="imgbtnPageMove" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnPageMove_Click"/>
        <asp:ImageButton ID="imgbtnDetailview" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnDetailview_Click"/>
        <asp:ImageButton ID="imgbtnRecompile" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" OnClick="imgbtnRecompile_Click"/>
        <asp:HiddenField ID="hfCurrentPage" runat="server"/>
        <asp:TextBox ID="txtHfRentCd" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtHfChargeTy" runat="server" Visible="false"></asp:TextBox>
        <asp:HiddenField ID="hfVatRation" runat="server"/>
        <asp:TextBox ID="txtHfYYYYMM" runat="server" Visible="false"></asp:TextBox>
        <div class="Btwps FloatR">
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
        <div class="Btwps FloatR">
            <div class="Btn-Type2-wp FloatL">
                <div class="Btn-Tp2-L">
                    <div class="Btn-Tp2-R">
                        <div class="Btn-Tp2-M">
	                        <span><asp:LinkButton ID="lnkbtnExcelReport" runat="server" OnClientClick="javascript:return fnMakeExcelFile();"></asp:LinkButton></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:ImageButton ID="imgbtnMakeExcel" runat="server" ImageUrl="~/Common/Images/Common/blank.gif" onClick="imgbtnMakeExcel_Click"/>
</asp:Content>